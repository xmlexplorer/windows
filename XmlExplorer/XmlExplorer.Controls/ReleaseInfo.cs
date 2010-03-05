using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XmlExplorer.Controls
{
    public class ReleaseInfo
    {
        public Version Version { get; set; }
        public string Url { get; set; }
        public ReleaseStatus Status { get; set; }
    }

    public class ReleaseInfoCollection : List<ReleaseInfo>, IComparer<ReleaseInfo>
    {
        public ReleaseInfoCollection()
            : base()
        {
        }

        public ReleaseInfoCollection(IEnumerable<ReleaseInfo> collection)
            : base(collection)
        {
        }

        public static ReleaseInfoCollection FromRss(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return FromRss(stream);
            }
        }

        public static ReleaseInfoCollection FromRss(MemoryStream stream)
        {
            ReleaseInfoCollection releases = new ReleaseInfoCollection();

            // regex to match a valid release version
            Regex regex = new Regex(@"\d+.\d+.\d+");

            XDocument document = XDocument.Load(stream);

            foreach (var item in document.Element("rss").Element("channel").Descendants("item"))
            {
                string title = item.Element("title").Value;
                Match match = regex.Match(title);
                if (!match.Success)
                    continue;

                string titleLower = title.ToLower();

                if (titleLower.Contains("deleted") || titleLower.Contains("removed"))
                    continue;

                ReleaseStatus status = ReleaseStatus.Stable;

                if (titleLower.Contains("alpha"))
                    status = ReleaseStatus.Alpha;
                else if (titleLower.Contains("beta"))
                    status = ReleaseStatus.Beta;

                Version version = new Version(match.Groups[0].Value);

                if (releases.Exists(r => r.Version == version))
                    continue;

                string link = item.Element("link").Value;

                ReleaseInfo release = new ReleaseInfo()
                {
                    Status = status,
                    Url = link,
                    Version = version,
                };

                releases.Add(release);
            }

            return releases;
        }

        #region Replaced XPathNavigator code with LINQ to XML code :)

        //private static ReleaseInfoCollection FromRss(MemoryStream stream)
        //{
        //    ReleaseInfoCollection releases = new ReleaseInfoCollection();

        //    XPathDocument document = new XPathDocument(stream);

        //    XPathNavigator navigator = document.CreateNavigator();
        //    foreach (XPathNavigator itemNavigator in navigator.Select("/rss/channel/item"))
        //    {
        //        try
        //        {
        //            string title = itemNavigator.Evaluate("string(title)") as string;
        //            if (title == null)
        //                continue;

        //            Regex regex = new Regex(@"\d+.\d+.\d+");
        //            Match match = regex.Match(title);
        //            if (!match.Success)
        //                continue;

        //            ReleaseInfo release = new ReleaseInfo();

        //            // update found, get release link
        //            release.Url = itemNavigator.Evaluate("string(link)") as string;

        //            if (string.IsNullOrEmpty(release.Url))
        //                continue;

        //            release.Version = new Version(match.Groups[0].Value);

        //            if (release.Version == null)
        //                continue;

        //            releases.Add(release);
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine(ex);
        //        }
        //    }

        //    return releases;
        //}

        #endregion

        public ReleaseInfo GetLatest(ReleaseStatus minimumStatus)
        {
            if (this.Count < 1)
                return null;

            var filteredReleases = this.Where(r => ((int)r.Status) >= ((int)minimumStatus)).ToList();

            if (filteredReleases.Count < 1)
                return null;

            filteredReleases.Sort(this);

            return filteredReleases[filteredReleases.Count - 1];
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