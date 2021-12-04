using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System;
using System.IO;

namespace Lab5Games.Editor
{
    public class BuildProcess : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            string strVersion = PlayerSettings.bundleVersion;

            AppVersion appVersion = new AppVersion(strVersion);

            if (appVersion.status == "ERROR")
                appVersion = new AppVersion(0, 0, 0, "DEBUG");

            ++appVersion.build;
            PlayerSettings.bundleVersion = appVersion.VersionCodeFull;

            switch(EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.iOS:
                    PlayerSettings.iOS.buildNumber = appVersion.build.ToString();
                    break;

                case BuildTarget.Android:
                    PlayerSettings.Android.bundleVersionCode = appVersion.build;
                    break;
            }
        }
    }
}
