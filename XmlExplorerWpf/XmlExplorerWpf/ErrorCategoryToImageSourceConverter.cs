using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Xml.Schema;
using System.Windows.Media;

namespace XmlExplorer
{
    public class ErrorCategoryToImageSourceConverter : IValueConverter
    {
        private static ImageSourceConverter _imageSourceConverter;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is XmlSeverityType))
                return value;

            if (_imageSourceConverter == null)
                _imageSourceConverter = new ImageSourceConverter();

            XmlSeverityType severity = (XmlSeverityType)value;
            switch (severity)
            {
                case XmlSeverityType.Error:
                    return _imageSourceConverter.ConvertFromString(@"pack://application:,,/Resources/Error16.png");

                case XmlSeverityType.Warning:
                    return _imageSourceConverter.ConvertFromString(@"pack://application:,,/Resources/Warning16.png");
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
