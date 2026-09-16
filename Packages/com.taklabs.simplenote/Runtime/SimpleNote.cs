using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

#if VRC_SDK_VRCSDK3
using VRC.SDKBase;
#endif

namespace TakLabs.SimpleNote
{
    [AddComponentMenu("Miscellaneous/Simple Note")]
    public class SimpleNote : MonoBehaviour
#if VRC_SDK_VRCSDK3
        , IEditorOnly
#endif
    {
        [TextArea(3, 20)]
        public string note;

        [HideInInspector]
        public float noteHeight = 60f;

        [HideInInspector]
        public TextAsset license;

#if UNITY_EDITOR
        private void Reset()
        {
            if (license != null) return;

            MonoScript script = MonoScript.FromMonoBehaviour(this);
            string sourcesDir = Path.GetDirectoryName(AssetDatabase.GetAssetPath(script));
            string rootDir = Path.GetDirectoryName(sourcesDir);
            string licensePath = Path.Combine(rootDir, "license.md").Replace("\\", "/");

            license = AssetDatabase.LoadAssetAtPath<TextAsset>(licensePath);
        }
#endif
    }
}