using System;
using System.IO;
using UnityEngine;

namespace Lab5Games.VersionBuilder
{
    [System.Serializable]
    public class AppVersion : ScriptableObject
    {
        [System.Serializable]
        public class VersionFormat
        {
            public int major;
            public int minor;
            public int patch;

            public string status;
            public string date;

            public string GetVersionCode()
            {
                return $"{major}.{minor}.{patch}";
            }

            public string GetVersionCodeFull()
            {
                return $"{major}.{minor}.{patch} [{status}]";
            }
        }

        public VersionFormat Windows;
        public VersionFormat iOS;
        public VersionFormat Android;
        public VersionFormat WebGL;
    }
}
