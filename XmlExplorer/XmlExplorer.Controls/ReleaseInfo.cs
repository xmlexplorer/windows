using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using System.IO;

namespace XmlExplorer.Controls
{
    public class ReleaseInfo
    {
        public Version Version { get; set; }
        public string Url { get; set; }
    }

    public class ReleaseInfoCollection : List<ReleaseInfo>, IComparer<ReleaseInfo>
    {
        public static ReleaseInfoCollection FromRss(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return FromRss(stream);
            }
        }

        private static ReleaseInfoCollection FromRss(MemoryStream stream)
        {
            ReleaseInfoCollection releases = new ReleaseInfoCollection();

            XPathDocument document = new XPathDocument(stream);

            XPathNavigator navigator = document.CreateNavigator();
            foreach (XPathNavigator itemNavigator in navigator.Select("/rss/channel/item"))
            {
                try
                {
                    string title = itemNavigator.Evaluate("string(title)") as string;
                    if (title == null)
                        continue;

                    Regex regex = new Regex(@"\d+.\d+.\d+");
                    Match match = regex.Match(title);
                    if (!match.Success)
                        continue;

                    ReleaseInfo release = new ReleaseInfo();

                    // update found, get release link
                    release.Url = itemNavigator.Evaluate("string(link)") as string;

                    if (string.IsNullOrEmpty(release.Url))
                        continue;

                    release.Version = new Version(match.Groups[0].Value);

                    if (release.Version == null)
                        continue;

                    releases.Add(release);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }

            return releases;
        }

        public ReleaseInfo Latest
        {
            get
            {
                if (this.Count < 1)
                    return null;

                this.Sort();

                return this[this.Count - 1];
            }
        }

        public new void Sort()
        {
            base.Sort(this);
        }

        #region IComparer<ReleaseInfo> Members

        public int Compare(ReleaseInfo x, ReleaseInfo y)
        {
            if (x == null && y == null)
                return 0;

            if (x == null)
                return -1;

            if (y == null)
                return 1;

            if (x.Version == null && y.Version == null)
                return 0;

            if (x.Version == null)
                return -1;

            if (y.Version == null)
                return 1;

            return x.Version.CompareTo(y.Version);
        }

        #endregion
    }
}
