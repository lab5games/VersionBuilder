using System;
using System.IO;
using UnityEngine;

namespace Lab5Games.VersionBuilder
{
    [System.Serializable]
    public class AppVersion : IEquatable<AppVersion>, IComparable<AppVersion>
    {
        public int major;
        public int minor;
        public int build;

        public string status;
        
        public string date;

        public static string GetFileLocation()
        {
            return "file://" + Path.Combine(Application.streamingAssetsPath, "appVersion.txt");
        }

        public AppVersion(int major, int minor, int build)
        {
            this.major = major;
            this.minor = minor;
            this.build = build;
        }

        public AppVersion(int major, int minor, int build, string status) : this(major, minor, build)
        {
            this.status = status;
        }

        public string GetVersionCode()
        {
            return $"{major}.{minor}.{build}";
        }

        public string GetVersionCodeFull()
        {
            if (string.IsNullOrEmpty(status))
                return GetVersionCode();
            else
                return $"{major}.{minor}.{build}-{status}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return Equals(obj as AppVersion);
        }

        public bool Equals(AppVersion other)
        {
            if (other == null)
                return false;

            return this.major == other.major &&
                this.minor == other.minor &&
                this.build == other.build;
        }

        public override int GetHashCode()
        {
            return major ^ minor ^ build;
        }

        public int CompareTo(AppVersion other)
        {
            if (other == null)
                throw new ArgumentNullException();

            if (this.major != other.major)
                return this.major.CompareTo(other.major);

            if (this.minor != other.minor)
                return this.minor.CompareTo(other.minor);

            return this.build.CompareTo(other.build);
        }
    }
}
