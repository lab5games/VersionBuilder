using UnityEngine;

namespace Lab5Games
{
    public class AppVersion 
    {
        public int major;
        public int minor;
        public int build;
        public string status;

        public string VersionCodeFull => $"{major}.{minor}.{build} - {status}";

        public AppVersion(int major, int minor, int patch, string status)
        {
            this.major = major;
            this.minor = minor;
            this.build = patch;
            this.status = status;
        }

        public AppVersion(string strVersion)
        {
            string[] splitArray = strVersion.Split('.','-');

            if (splitArray.Length != 4)
            {
                this.major = 0;
                this.minor = 0;
                this.build = 0;
                this.status = "ERROR";
            }
            else
            {
                this.major = int.Parse(splitArray[0].Trim());
                this.minor = int.Parse(splitArray[1].Trim());
                this.build = int.Parse(splitArray[2].Trim());
                this.status = splitArray[3].Trim();
            }
        }
    }
}
