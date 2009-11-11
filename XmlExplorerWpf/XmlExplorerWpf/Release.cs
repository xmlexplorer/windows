using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using WpfControls;

namespace XmlExplorer
{
    public class Release
    {
        public string Url { get; set; }
        public Version Version { get; set; }
        public DevelopmentStatus DevelopmentStatus { get; set; }

        public static void BeginCheckForNewerRelease(EventHandler<CheckForNewerReleaseCompletedEventArgs> onFinished)
        {
            if (onFinished == null)
                throw new ArgumentNullException("onFinished");

            WebClient client = new WebClient();

            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(OnDownloadStringCompleted);

            string address = Properties.Settings.Default.ReleasesUrl;

            client.DownloadStringAsync(new Uri(address), onFinished);
        }

        static void OnDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            EventHandler<CheckForNewerReleaseCompletedEventArgs> onFinished = e.UserState as EventHandler<CheckForNewerReleaseCompletedEventArgs>;
            if (onFinished == null)
                return;

            Exception error = null;
            bool cancelled = e.Cancelled;
            Release result = null;

            if (!cancelled)
            {
                error = e.Error;

                if (error == null)
                {
                    string rss = e.Result;

                    XDocument document = XDocument.Parse(rss);

                    var releases = ParseReleases(document);

                    Version currentVersion = AssemblyInfo.Default.Version;

                    DevelopmentStatus desiredDevelopmentStatus = Properties.Settings.Default.DevelopmentStatus;

                    Release latestRelease = null;

                    foreach(var release in releases)
                    {
                        if (release.DevelopmentStatus >= desiredDevelopmentStatus)
                        {
                            if (latestRelease == null || release.Version > latestRelease.Version)
                            {
                                latestRelease = release;
                            }
                        }
                    }
                    
                    if(latestRelease.Version > currentVersion)
                        result = latestRelease;
                }
            }

            CheckForNewerReleaseCompletedEventArgs args = new CheckForNewerReleaseCompletedEventArgs(error, cancelled, result);

            onFinished(null, args);
        }

        static List<Release> ParseReleases(XDocument document)
        {
            List<Release> releases = new List<Release>();

            // regex to match a valid release version
            Regex regex = new Regex(@"\d+.\d+.\d+");

            foreach (var item in document.Element("rss").Element("channel").Descendants("item"))
            {
                string title = item.Element("title").Value;
                Match match = regex.Match(title);
                if (!match.Success)
                    continue;

                DevelopmentStatus developmentStatus = XmlExplorer.DevelopmentStatus.Stable;

                string titleLower = title.ToLower();
                if (titleLower.Contains("alpha"))
                    developmentStatus = XmlExplorer.DevelopmentStatus.Alpha;
                else if (titleLower.Contains("beta"))
                    developmentStatus = XmlExplorer.DevelopmentStatus.Beta;

                Version version = new Version(match.Groups[0].Value);

                string link = item.Element("link").Value;

                Release release = new Release()
                {
                    DevelopmentStatus = developmentStatus,
                    Url = link,
                    Version = version,
                };

                releases.Add(release);
            }

            return releases;
        }
    }

    public enum DevelopmentStatus
    {
        Alpha = 0,
        Beta = 1,
        Stable = 2,
    }

    public class CheckForNewerReleaseCompletedEventArgs : AsyncCompletedEventArgs
    {
        public CheckForNewerReleaseCompletedEventArgs(Exception error, bool cancelled, Release result)
            : base(error, cancelled, null)
        {
            this.Result = result;
        }

        public Release Result { get; private set; }
    }
}
