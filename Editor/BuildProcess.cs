using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System;
using System.IO;

namespace Lab5Games.VersionBuilder.Editor
{
    public class BuildProcess : IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }

        
        public void OnPreprocessBuild(BuildReport report)
        {
            var asset = AssetDatabase.LoadAssetAtPath<AppVersion>("Assets/Resources/AppVersion.asset");

            if (asset == null)
                throw new NullReferenceException("AppVersion asset is null");



            switch(EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    ++asset.Windows.patch;
                    asset.Windows.date = DateTime.Now.ToString("yyyy-mm-dd");

                    PlayerSettings.bundleVersion = asset.Windows.GetVersionCode();

                    Debug.LogWarning($"Build new version: {asset.Windows.GetVersionCodeFull()}");
                    break;

                case BuildTarget.iOS:
                    ++asset.iOS.patch;
                    asset.iOS.date = DateTime.Now.ToString("yyyy-mm-dd");

                    PlayerSettings.bundleVersion = asset.iOS.GetVersionCode();
                    PlayerSettings.iOS.buildNumber = asset.iOS.patch.ToString();

                    Debug.LogWarning($"Build new version: {asset.iOS.GetVersionCodeFull()}");
                    break;

                case BuildTarget.Android:
                    ++asset.Android.patch;
                    asset.Android.date = DateTime.Now.ToString("yyyy-mm-dd");

                    PlayerSettings.bundleVersion = asset.Android.GetVersionCode();
                    PlayerSettings.Android.bundleVersionCode = asset.iOS.patch;

                    Debug.LogWarning($"Build new version: {asset.Android.GetVersionCodeFull()}");
                    break;

                case BuildTarget.WebGL:
                    ++asset.WebGL.patch;
                    asset.WebGL.date = DateTime.Now.ToString("yyyy-mm-dd");

                    PlayerSettings.bundleVersion = asset.WebGL.GetVersionCode();

                    Debug.LogWarning($"Build new version: {asset.WebGL.GetVersionCodeFull()}");
                    break;
            }


            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
        }


        [InitializeOnLoadMethod]
        private static void CreateDefaultAsset()
        {
            if (AssetDatabase.LoadAssetAtPath<AppVersion>("Assets/Resources/AppVersion.asset") == null)
            {
                var asset = ScriptableObject.CreateInstance<AppVersion>();

                if(!AssetDatabase.IsValidFolder("Assets/Resources"))
                {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                }

                AssetDatabase.CreateAsset(asset, "Assets/Resources/AppVersion.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}
