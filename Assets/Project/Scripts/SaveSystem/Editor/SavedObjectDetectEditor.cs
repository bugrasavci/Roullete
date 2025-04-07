using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Project.Scripts.SaveSystem.Editor
{
    [CustomEditor(typeof(SaveLoadManager))]
    public class SavedObjectDetectEditor : UnityEditor.Editor
    {
        private GUIStyle _styleCorrectText;
        private GUIStyle _styleWrongText;

        private SavedObjectBase[] _savedObjects;

        private void OnEnable()
        {
            _styleCorrectText = new GUIStyle
            {
                normal =
                {
                    textColor = Color.green
                }
            };

            _styleWrongText = new GUIStyle
            {
                normal =
                {
                    textColor = Color.red
                }
            };
        }

        public override void OnInspectorGUI()
        {
            var slm = (SaveLoadManager)target;

            if (GUILayout.Button("Detect Saved Objects"))
            {
                _savedObjects = GetAllInstances<SavedObjectBase>();
            }

            if (GUILayout.Button("Add Missing Saved Objects"))
            {
                foreach (SavedObjectBase savedObject in _savedObjects)
                {
                    if (!slm.IsContains(savedObject.name)) slm.AddSavedObject(savedObject);
                }
            }

            if (GUILayout.Button("Reset Saved Objects"))
            {
                foreach (SavedObjectBase savedObject in slm.GetSavedObjects().Where(savedObject => savedObject != null))
                {
                    savedObject.Reset();
                }
            }

            if (_savedObjects != null)
            {
                GUILayout.Label($"Detected asset count: {_savedObjects.Length}");

                foreach (SavedObjectBase savedObject in _savedObjects)
                {
                    GUILayout.Label(
                        savedObject.name,
                        slm.IsContains(savedObject.name) ? _styleCorrectText : _styleWrongText
                    );
                }
            }

            DrawDefaultInspector();
        }

        private static T[] GetAllInstances<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            var a = new T[guids.Length];

            for (var i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;
        }
    }
}