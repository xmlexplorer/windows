using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfControls
{
    /// <summary>
    /// Interaction logic for FontChooserLite.xaml
    /// </summary>
    public partial class FontChooserLite : System.Windows.Window
    {
        #region Private fields and types

        private ICollection<FontFamily> _familyCollection;          // see FamilyCollection property

        private bool _updatePending;                                // indicates a call to OnUpdate is scheduled
        private bool _familyListValid;                              // indicates the list of font families is valid
        private bool _typefaceListValid;                            // indicates the list of typefaces is valid
        private bool _typefaceListSelectionValid;                   // indicates the current selection in the typeface list is valid

        private static readonly double[] CommonlyUsedFontSizes = new double[]
        {
            3.0,    4.0,   5.0,   6.0,   6.5,
            7.0,    7.5,   8.0,   8.5,   9.0,
            9.5,   10.0,  10.5,  11.0,  11.5,
            12.0,  12.5,  13.0,  13.5,  14.0,
            15.0,  16.0,  17.0,  18.0,  19.0,
            20.0,  22.0,  24.0,  26.0,  28.0,  30.0,  32.0,  34.0,  36.0,  38.0,
            40.0,  44.0,  48.0,  52.0,  56.0,  60.0,  64.0,  68.0,  72.0,  76.0,
            80.0,  88.0,  96.0, 104.0, 112.0, 120.0, 128.0, 136.0, 144.0
        };

        // Specialized metadata object for font chooser dependency properties
        private class FontPropertyMetadata : FrameworkPropertyMetadata
        {
            public readonly DependencyProperty TargetProperty;

            public FontPropertyMetadata(
                object defaultValue,
                PropertyChangedCallback changeCallback,
                DependencyProperty targetProperty
                )
                : base(defaultValue, changeCallback)
            {
                TargetProperty = targetProperty;
            }
        }

        private delegate void UpdateCallback();

        #endregion

        #region Construction and initialization

        public FontChooserLite()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            // Hook up events for the font family list and associated text box.
            fontFamilyTextBox.SelectionChanged += new RoutedEventHandler(fontFamilyTextBox_SelectionChanged);
            fontFamilyTextBox.TextChanged += new TextChangedEventHandler(fontFamilyTextBox_TextChanged);
            fontFamilyTextBox.PreviewKeyDown += new KeyEventHandler(fontFamilyTextBox_PreviewKeyDown);
            fontFamilyList.SelectionChanged += new SelectionChangedEventHandler(fontFamilyList_SelectionChanged);

            // Hook up events for the typeface list.
            typefaceList.SelectionChanged += new SelectionChangedEventHandler(typefaceList_SelectionChanged);

            // Hook up events for the font size list and associated text box.
            sizeTextBox.TextChanged += new TextChangedEventHandler(sizeTextBox_TextChanged);
            sizeTextBox.PreviewKeyDown += new KeyEventHandler(sizeTextBox_PreviewKeyDown);
            sizeList.SelectionChanged += new SelectionChangedEventHandler(sizeList_SelectionChanged);

            // Hook up events for text decoration check boxes.
            RoutedEventHandler textDecorationEventHandler = new RoutedEventHandler(textDecorationCheckStateChanged);
            underlineCheckBox.Checked += textDecorationEventHandler;
            underlineCheckBox.Unchecked += textDecorationEventHandler;
            baselineCheckBox.Checked += textDecorationEventHandler;
            baselineCheckBox.Unchecked += textDecorationEventHandler;
            strikethroughCheckBox.Checked += textDecorationEventHandler;
            strikethroughCheckBox.Unchecked += textDecorationEventHandler;
            overlineCheckBox.Checked += textDecorationEventHandler;
            overlineCheckBox.Unchecked += textDecorationEventHandler;

            // Initialize the list of font sizes and select the nearest size.
            foreach (double value in CommonlyUsedFontSizes)
            {
                sizeList.Items.Add(new FontSizeListItem(value));
            }
            OnSelectedFontSizeChanged(SelectedFontSize);

            // Initialize the font family list and the current family.
            if (!_familyListValid)
            {
                InitializeFontFamilyList();
                _familyListValid = true;
                OnSelectedFontFamilyChanged(SelectedFontFamily);
            }

            // Schedule background updates.
            ScheduleUpdate();
        }

        #endregion

        #region Event handlers

        private void OnOKButtonClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private int _fontFamilyTextBoxSelectionStart;

        private void fontFamilyTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            _fontFamilyTextBoxSelectionStart = fontFamilyTextBox.SelectionStart;
        }

        private void fontFamilyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = fontFamilyTextBox.Text;

            // Update the current list item.
            if (SelectFontFamilyListItem(text) == null)
            {
                // The text does not exactly match a family name so consider applying auto-complete behavior.
                // However, only do so if the following conditions are met:
                //   (1)  The user is typing more text rather than deleting (i.e., the new text length is
                //        greater than the most recent selection start index), and
                //   (2)  The caret is at the end of the text box.
                if (text.Length > _fontFamilyTextBoxSelectionStart 
                    && fontFamilyTextBox.SelectionStart == text.Length)
                {
                    // Get the current list item, which should be the nearest match for the text.
                    FontFamilyListItem item = fontFamilyList.Items.CurrentItem as FontFamilyListItem;
                    if (item != null)
                    {
                        // Does the text box text match the beginning of the family name?
                        string familyDisplayName = item.ToString();
                        if (string.Compare(text, 0, familyDisplayName, 0, text.Length, true, CultureInfo.CurrentCulture) == 0)
                        {
                            // Set the text box text to the complete family name and select the part not typed in.
                            fontFamilyTextBox.Text = familyDisplayName;
                            fontFamilyTextBox.SelectionStart = text.Length;
                            fontFamilyTextBox.SelectionLength = familyDisplayName.Length - text.Length;
                        }
                    }
                }
            }
        }

        private void sizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            double sizeInPoints;
            if (double.TryParse(sizeTextBox.Text, out sizeInPoints))
            {
                double sizeInPixels = FontSizeListItem.PointsToPixels(sizeInPoints);
                if (!FontSizeListItem.FuzzyEqual(sizeInPixels, SelectedFontSize))
                {
                    SelectedFontSize = sizeInPixels;
                }
            }
        }

        private void fontFamilyTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            OnComboBoxPreviewKeyDown(fontFamilyTextBox, fontFamilyList, e);
        }

        private void sizeTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            OnComboBoxPreviewKeyDown(sizeTextBox, sizeList, e);
        }

        private void fontFamilyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FontFamilyListItem item = fontFamilyList.SelectedItem as FontFamilyListItem;
            if (item != null)
            {
                SelectedFontFamily = item.FontFamily;
            }
        }

        private void sizeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FontSizeListItem item = sizeList.SelectedItem as FontSizeListItem;
            if (item != null)
            {
                SelectedFontSize = item.SizeInPixels;
            }
        }

        private void typefaceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypefaceListItem item = typefaceList.SelectedItem as TypefaceListItem;
            if (item != null)
            {
                SelectedFontWeight = item.FontWeight;
                SelectedFontStyle = item.FontStyle;
                SelectedFontStretch = item.FontStretch;
            }
        }

        private void textDecorationCheckStateChanged(object sender, RoutedEventArgs e)
        {
            TextDecorationCollection textDecorations = new TextDecorationCollection();

            if (underlineCheckBox.IsChecked.Value)
            {
                textDecorations.Add(TextDecorations.Underline[0]);
            }
            if (baselineCheckBox.IsChecked.Value)
            {
                textDecorations.Add(TextDecorations.Baseline[0]);
            }
            if (strikethroughCheckBox.IsChecked.Value)
            {
                textDecorations.Add(TextDecorations.Strikethrough[0]);
            }
            if (overlineCheckBox.IsChecked.Value)
            {
                textDecorations.Add(TextDecorations.OverLine[0]);
            }

            textDecorations.Freeze();
            SelectedTextDecorations = textDecorations;
        }

        #endregion

        #region Public properties and methods

        /// <summary>
        /// Collection of font families to display in the font family list. By default this is Fonts.SystemFontFamilies,
        /// but a client could set this to another collection returned by Fonts.GetFontFamilies, e.g., a collection of
        /// application-defined fonts.
        /// </summary>
        public ICollection<FontFamily> FontFamilyCollection
        {
            get
            {
                return (_familyCollection == null) ? Fonts.SystemFontFamilies : _familyCollection;
            }

            set
            {
                if (value != _familyCollection)
                {
                    _familyCollection = value;
                    InvalidateFontFamilyList();
                }
            }
        }

        /// <summary>
        /// Sets the font chooser selection properties to match the properites of the specified object.
        /// </summary>
        public void SetPropertiesFromObject(DependencyObject obj)
        {
            foreach (DependencyProperty property in _chooserProperties)
            {
                FontPropertyMetadata metadata = property.GetMetadata(typeof(FontChooserLite)) as FontPropertyMetadata;
                if (metadata != null)
                {
                    this.SetValue(property, obj.GetValue(metadata.TargetProperty));
                }
            }
        }

        /// <summary>
        /// Sets the properites of the specified object to match the font chooser selection properties.
        /// </summary>
        public void ApplyPropertiesToObject(DependencyObject obj)
        {
            foreach (DependencyProperty property in _chooserProperties)
            {
                FontPropertyMetadata metadata = property.GetMetadata(typeof(FontChooserLite)) as FontPropertyMetadata;
                if (metadata != null)
                {
                    obj.SetValue(metadata.TargetProperty, this.GetValue(property));
                }
            }
        }

        #endregion

        #region Other dependency properties

        public static readonly DependencyProperty SelectedFontFamilyProperty = RegisterFontProperty(
            "SelectedFontFamily",
            TextBlock.FontFamilyProperty, 
            new PropertyChangedCallback(SelectedFontFamilyChangedCallback)
            );
        public FontFamily SelectedFontFamily
        {
            get { return GetValue(SelectedFontFamilyProperty) as FontFamily; }
            set { SetValue(SelectedFontFamilyProperty, value); }
        }
        static void SelectedFontFamilyChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((FontChooserLite)obj).OnSelectedFontFamilyChanged(e.NewValue as FontFamily);
        }

        public static readonly DependencyProperty SelectedFontWeightProperty = RegisterFontProperty(
            "SelectedFontWeight",
            TextBlock.FontWeightProperty,
            new PropertyChangedCallback(SelectedTypefaceChangedCallback)
            );
        public FontWeight SelectedFontWeight
        {
            get { return (FontWeight)GetValue(SelectedFontWeightProperty); }
            set { SetValue(SelectedFontWeightProperty, value); }
        }

        public static readonly DependencyProperty SelectedFontStyleProperty = RegisterFontProperty(
            "SelectedFontStyle",
            TextBlock.FontStyleProperty,
            new PropertyChangedCallback(SelectedTypefaceChangedCallback)
            );
        public FontStyle SelectedFontStyle
        {
            get { return (FontStyle)GetValue(SelectedFontStyleProperty); }
            set { SetValue(SelectedFontStyleProperty, value); }
        }

        public static readonly DependencyProperty SelectedFontStretchProperty = RegisterFontProperty(
           "SelectedFontStretch",
           TextBlock.FontStretchProperty,
           new PropertyChangedCallback(SelectedTypefaceChangedCallback)
           );
        public FontStretch SelectedFontStretch
        {
            get { return (FontStretch)GetValue(SelectedFontStretchProperty); }
            set { SetValue(SelectedFontStretchProperty, value); }
        }

        static void SelectedTypefaceChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((FontChooserLite)obj).InvalidateTypefaceListSelection();
        }

        public static readonly DependencyProperty SelectedFontSizeProperty = RegisterFontProperty(
           "SelectedFontSize",
           TextBlock.FontSizeProperty,
           new PropertyChangedCallback(SelectedFontSizeChangedCallback)
           );
        public double SelectedFontSize
        {
            get { return (double)GetValue(SelectedFontSizeProperty); }
            set { SetValue(SelectedFontSizeProperty, value); }
        }
        private static void SelectedFontSizeChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((FontChooserLite)obj).OnSelectedFontSizeChanged((double)(e.NewValue));
        }

        public static readonly DependencyProperty SelectedTextDecorationsProperty = RegisterFontProperty(
           "SelectedTextDecorations",
           TextBlock.TextDecorationsProperty,
           new PropertyChangedCallback(SelectedTextDecorationsChangedCallback)
           );
        public TextDecorationCollection SelectedTextDecorations
        {
            get { return GetValue(SelectedTextDecorationsProperty) as TextDecorationCollection; }
            set { SetValue(SelectedTextDecorationsProperty, value); }
        }
        private static void SelectedTextDecorationsChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            FontChooserLite chooser = (FontChooserLite)obj;
            chooser.OnTextDecorationsChanged();
        }

        #endregion

        #region Dependency property helper functions

        // Helper function for registering font chooser dependency properties other than typographic properties.
        private static DependencyProperty RegisterFontProperty(
            string propertyName,
            DependencyProperty targetProperty,
            PropertyChangedCallback changeCallback
            )
        {
            return DependencyProperty.Register(
                propertyName,
                targetProperty.PropertyType,
                typeof(FontChooserLite),
                new FontPropertyMetadata(
                    targetProperty.DefaultMetadata.DefaultValue,
                    changeCallback,
                    targetProperty
                    )
                );
        }

        #endregion

        #region Dependency property tables

        // Array of all font chooser dependency properties
        private static readonly DependencyProperty[] _chooserProperties = new DependencyProperty[]
        {
            // other properties
            SelectedFontFamilyProperty,
            SelectedFontWeightProperty,
            SelectedFontStyleProperty,
            SelectedFontStretchProperty,
            SelectedFontSizeProperty,
            SelectedTextDecorationsProperty
        };

        #endregion

        #region Property change handlers

        // Handle changes to the SelectedFontFamily property
        private void OnSelectedFontFamilyChanged(FontFamily family)
        {
            // If the family list is not valid do nothing for now. 
            // We'll be called again after the list is initialized.
            if (_familyListValid)
            {
                // Select the family in the list; this will return null if the family is not in the list.
                FontFamilyListItem item = SelectFontFamilyListItem(family);

                // Set the text box to the family name, if it isn't already.
                string displayName = (item != null) ? item.ToString() : FontFamilyListItem.GetDisplayName(family);
                if (string.Compare(fontFamilyTextBox.Text, displayName, true, CultureInfo.CurrentCulture) != 0)
                {
                    fontFamilyTextBox.Text = displayName;
                }

                // The typeface list is no longer valid; update it in the background to improve responsiveness.
                InvalidateTypefaceList();
            }
        }

        // Handle changes to the SelectedFontSize property
        private void OnSelectedFontSizeChanged(double sizeInPixels)
        {
            // Select the list item, if the size is in the list.
            double sizeInPoints = FontSizeListItem.PixelsToPoints(sizeInPixels);
            if (!SelectListItem(sizeList, sizeInPoints))
            {
                sizeList.SelectedIndex = -1;
            }

            // Set the text box contents if it doesn't already match the current size.
            double textBoxValue;
            if (!double.TryParse(sizeTextBox.Text, out textBoxValue) || !FontSizeListItem.FuzzyEqual(textBoxValue, sizeInPoints))
            {
                sizeTextBox.Text = sizeInPoints.ToString();
            }
        }

        // Handle changes to any of the text decoration properties.
        private void OnTextDecorationsChanged()
        {
            bool underline = false;
            bool baseline = false;
            bool strikethrough = false;
            bool overline = false;

            TextDecorationCollection textDecorations = SelectedTextDecorations;
            if (textDecorations != null)
            {
                foreach (TextDecoration td in textDecorations)
                {
                    switch (td.Location)
                    {
                        case TextDecorationLocation.Underline:
                            underline = true;
                            break;
                        case TextDecorationLocation.Baseline:
                            baseline = true;
                            break;
                        case TextDecorationLocation.Strikethrough:
                            strikethrough = true;
                            break;
                        case TextDecorationLocation.OverLine:
                            overline = true;
                            break;
                    }
                }
            }

            underlineCheckBox.IsChecked = underline;
            baselineCheckBox.IsChecked = baseline;
            strikethroughCheckBox.IsChecked = strikethrough;
            overlineCheckBox.IsChecked = overline;
        }

        #endregion

        #region Background update logic

        // Schedule background initialization of the font famiy list.
        private void InvalidateFontFamilyList()
        {
            if (_familyListValid)
            {
                InvalidateTypefaceList();

                fontFamilyList.Items.Clear();
                fontFamilyTextBox.Clear();
                _familyListValid = false;

                ScheduleUpdate();
            }
        }

        // Schedule background initialization of the typeface list.
        private void InvalidateTypefaceList()
        {
            if (_typefaceListValid)
            {
                typefaceList.Items.Clear();
                _typefaceListValid = false;

                ScheduleUpdate();
            }
        }

        // Schedule background selection of the current typeface list item.
        private void InvalidateTypefaceListSelection()
        {
            if (_typefaceListSelectionValid)
            {
                _typefaceListSelectionValid = false;
                ScheduleUpdate();
            }
        }

        // Schedule background initialization.
        private void ScheduleUpdate()
        {
            if (!_updatePending)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new UpdateCallback(OnUpdate));
                _updatePending = true;
            }
        }

        // Dispatcher callback that performs background initialization.
        private void OnUpdate()
        {
            _updatePending = false;

            if (!_familyListValid)
            {
                // Initialize the font family list.
                InitializeFontFamilyList();
                _familyListValid = true;
                OnSelectedFontFamilyChanged(SelectedFontFamily);

                // Defer any other initialization until later.
                ScheduleUpdate();
            }
            else if (!_typefaceListValid)
            {
                // Initialize the typeface list.
                InitializeTypefaceList();
                _typefaceListValid = true;

                // Select the current typeface in the list.
                InitializeTypefaceListSelection();
                _typefaceListSelectionValid = true;

                // Defer any other initialization until later.
                ScheduleUpdate();
            }
            else if (!_typefaceListSelectionValid)
            {
                // Select the current typeface in the list.
                InitializeTypefaceListSelection();
                _typefaceListSelectionValid = true;

                // Defer any other initialization until later.
                ScheduleUpdate();
            }
        }

        #endregion

        #region Content initialization

        private void InitializeFontFamilyList()
        {
            ICollection<FontFamily> familyCollection = FontFamilyCollection;
            if (familyCollection != null)
            {
                FontFamilyListItem[] items = new FontFamilyListItem[familyCollection.Count];

                int i = 0;

                foreach (FontFamily family in familyCollection)
                {
                    items[i++] = new FontFamilyListItem(family);
                }

                Array.Sort<FontFamilyListItem>(items);

                foreach (FontFamilyListItem item in items)
                {
                    fontFamilyList.Items.Add(item);
                }
            }
        }

        private void InitializeTypefaceList()
        {
            FontFamily family = SelectedFontFamily;
            if (family != null)
            {
                ICollection<Typeface> faceCollection = family.GetTypefaces();

                TypefaceListItem[] items = new TypefaceListItem[faceCollection.Count];

                int i = 0;

                foreach (Typeface face in faceCollection)
                {
                    items[i++] = new TypefaceListItem(face);
                }

                Array.Sort<TypefaceListItem>(items);

                foreach (TypefaceListItem item in items)
                {
                    typefaceList.Items.Add(item);
                }
            }
        }

        private void InitializeTypefaceListSelection()
        {
            // If the typeface list is not valid, do nothing for now.
            // We'll be called again after the list is initialized.
            if (_typefaceListValid)
            {
                Typeface typeface = new Typeface(SelectedFontFamily, SelectedFontStyle, SelectedFontWeight, SelectedFontStretch);

                // Select the typeface in the list.
                SelectTypefaceListItem(typeface);
            }
        }

        #endregion

        #region List box helpers

        // Update font family list based on selection.
        // Return list item if there's an exact match, or null if not.
        private FontFamilyListItem SelectFontFamilyListItem(string displayName)
        {
            FontFamilyListItem listItem = fontFamilyList.SelectedItem as FontFamilyListItem;
            if (listItem != null && string.Compare(listItem.ToString(), displayName, true, CultureInfo.CurrentCulture) == 0)
            {
                // Already selected
                return listItem;
            }
            else if (SelectListItem(fontFamilyList, displayName))
            {
                // Exact match found
                return fontFamilyList.SelectedItem as FontFamilyListItem;
            }
            else
            {
                // Not in the list
                return null;
            }
        }

        // Update font family list based on selection.
        // Return list item if there's an exact match, or null if not.
        private FontFamilyListItem SelectFontFamilyListItem(FontFamily family)
        {
            FontFamilyListItem listItem = fontFamilyList.SelectedItem as FontFamilyListItem;
            if (listItem != null && listItem.FontFamily.Equals(family))
            {
                // Already selected
                return listItem;
            }
            else if (SelectListItem(fontFamilyList, FontFamilyListItem.GetDisplayName(family)))
            {
                // Exact match found
                return fontFamilyList.SelectedItem as FontFamilyListItem;
            }
            else
            {
                // Not in the list
                return null;
            }
        }

        // Update typeface list based on selection.
        // Return list item if there's an exact match, or null if not.
        private TypefaceListItem SelectTypefaceListItem(Typeface typeface)
        {
            TypefaceListItem listItem = typefaceList.SelectedItem as TypefaceListItem;
            if (listItem != null && listItem.Typeface.Equals(typeface))
            {
                // Already selected
                return listItem;
            }
            else if (SelectListItem(typefaceList, new TypefaceListItem(typeface)))
            {
                // Exact match found
                return typefaceList.SelectedItem as TypefaceListItem;
            }
            else
            {
                // Not in list
                return null;
            }
        }

        // Update list based on selection.
        // Return true if there's an exact match, or false if not.
        private bool SelectListItem(ListBox list, object value)
        {
            ItemCollection itemList = list.Items;

            // Perform a binary search for the item.
            int first = 0;
            int limit = itemList.Count;

            while (first < limit)
            {
                int i = first + (limit - first) / 2;
                IComparable item = (IComparable)(itemList[i]);
                int comparison = item.CompareTo(value);
                if (comparison < 0)
                {
                    // Value must be after i
                    first = i + 1;
                }
                else if (comparison > 0)
                {
                    // Value must be before i
                    limit = i;
                }
                else
                {
                    // Exact match; select the item.
                    list.SelectedIndex = i;
                    itemList.MoveCurrentToPosition(i);
                    list.ScrollIntoView(itemList[i]);
                    return true;
                }
            }

            // Not an exact match; move current position to the nearest item but don't select it.
            if (itemList.Count > 0)
            {
                int i = Math.Min(first, itemList.Count - 1);
                itemList.MoveCurrentToPosition(i);
                list.ScrollIntoView(itemList[i]);
            }

            return false;
        }

        // Logic to handle UP and DOWN arrow keys in the text box associated with a list.
        // Behavior is similar to a Win32 combo box.
        private void OnComboBoxPreviewKeyDown(TextBox textBox, ListBox listBox, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    // Move up from the current position.
                    MoveListPosition(listBox, -1);
                    e.Handled = true;
                    break;

                case Key.Down:
                    // Move down from the current position, unless the item at the current position is
                    // not already selected in which case select it.
                    if (listBox.Items.CurrentPosition == listBox.SelectedIndex)
                    {
                        MoveListPosition(listBox, +1);
                    }
                    else
                    {
                        MoveListPosition(listBox, 0);
                    }
                    e.Handled = true;
                    break;
            }
        }

        private void MoveListPosition(ListBox listBox, int distance)
        {
            int i = listBox.Items.CurrentPosition + distance;
            if (i >= 0 && i < listBox.Items.Count)
            {
                listBox.Items.MoveCurrentToPosition(i);
                listBox.SelectedIndex = i;
                listBox.ScrollIntoView(listBox.Items[i]);
            }
        }

        #endregion
    }
}
