using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System;
using System.IO;

namespace Lab5Games.VersionBuilder.Editor
{
    public class BuildVersionProcess : IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }

        public static string GetFileLocation()
        {
            return Path.Combine(Application.streamingAssetsPath, "appVersion.txt");
        }

        public static bool CheckFileExists()
        {
            return File.Exists(GetFileLocation());
        }

        [MenuItem("Lab5Games/Create Version File")]
        private static void CreateVersionFile()
        {

            string url = GetFileLocation();

            if (File.Exists(url))
            {
                Debug.LogWarning($"VersionBuilder: Version file is existed at {url}");
                return;
            }

            AppVersion appVersion = new AppVersion(0, 0, 0);

            appVersion.date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string json = EditorJsonUtility.ToJson(appVersion);

            string dir = Path.GetDirectoryName(url);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(url, json);

            PlayerSettings.bundleVersion = appVersion.GetVersionCode();

            AssetDatabase.Refresh();

        }

        public void OnPreprocessBuild(BuildReport report)
        {
            if (EditorUtility.DisplayDialog("Version Builder", "Do you want use Version Builder ?", "Yes", "No"))
            {
                if (!CheckFileExists())
                {
                    throw new Exception("BuildVersionProcess: Missing version file, create one from Unity top bar 'Lab5Games/Create Version File'");
                }

                string json = File.ReadAllText(GetFileLocation());

                AppVersion appVersion = new AppVersion(0, 0, 0, "ERROR");
                EditorJsonUtility.FromJsonOverwrite(json, appVersion);

                // increased build number
                ++appVersion.build;
                appVersion.date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                SetupPlayerSettings(appVersion);

                // override version file
                File.WriteAllText(GetFileLocation(), EditorJsonUtility.ToJson(appVersion));

                AssetDatabase.Refresh();
            }
        }

        private static void SetupPlayerSettings(AppVersion version)
        {
            Debug.Log($"BuildVersionProcess: Target version {version.GetVersionCodeFull()}");

            PlayerSettings.bundleVersion = version.GetVersionCode();

            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.iOS:
                    PlayerSettings.iOS.buildNumber = version.build.ToString();
                    break;

                case BuildTarget.Android:
                    PlayerSettings.Android.bundleVersionCode = version.build;
                    break;
            }
        }
    }
}
