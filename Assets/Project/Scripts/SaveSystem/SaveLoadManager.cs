using System;
using System.Collections.Generic;
using System.Linq;


#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Assets.Project.Scripts.SaveSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        [SerializeField] private List<SavedObjectBase> savedObjects;

        private void Awake()
        {
            InitAll();
            OnInitAllActionOver();
        }

        protected virtual void OnInitAllActionOver()
        {
            Debug.Log($"<color=blue>[SaveLoadManager]</color> On Init All Action Over");
        }

        [ContextMenu("Init All")]
        public void InitAll()
        {
            Debug.Log($"<color=green>[SaveLoadManager]</color> Start Init All");
            foreach (var x in savedObjects)
            {
                x.Init();
                Debug.Log($"<color=cyan>[SaveLoadManager]</color> Init {x.name}");
            }
        }

        [ContextMenu("Save All")]
        public void SaveAll()
        {
            savedObjects.ForEach(x => x.SaveManual());
        }

        [ContextMenu("Reset All")]
        public void ResetAll()
        {
            savedObjects.ForEach(x => x.Reset());
        }

        public void AddSavedObject(SavedObjectBase sob)
        {
            savedObjects.Add(sob);
        }

        public List<SavedObjectBase> GetSavedObjects()
        {
            return savedObjects;
        }

#if UNITY_EDITOR
        public bool IsContains(string s)
        {
            return savedObjects.Any(savedObject => savedObject.name.Equals(s));
        }

        [ContextMenu("Clear Duplicates")]
        public void ClearDuplicates()
        {
            var dic = new Dictionary<Type, List<string>>();
            for (int i = savedObjects.Count - 1; i >= 0; i--)
            {
                var obj = savedObjects[i];
                var objType = obj.GetType();
                var objName = obj.name;
                Debug.LogError($"<color=yellow>{objType}</color>{objName}");
                if (dic.TryGetValue(objType, out var nameList))
                {
                    if (nameList.Contains(objName))
                    {
                        Debug.LogError($"<color=red>{objType}</color>{objName} is already exist");
                        savedObjects.RemoveAt(i);
                        continue;
                    }

                    Debug.LogError($"<color=green>{objType}</color>{objName} is new");
                    nameList.Add(objName);
                }
                else
                {
                    Debug.LogError($"<color=cyan>{objType}</color>type of {objName} is new");
                    dic.Add(objType, new List<string> { objName });
                }
            }

            EditorUtility.SetDirty(this);
        }

        [ContextMenu("Clear Missing Objects")]
        public void ClearMissingObjects()
        {
            for (int i = savedObjects.Count - 1; i >= 0; i--)
            {
                var obj = savedObjects[i];
                if (obj == null)
                    savedObjects.RemoveAt(i);
            }
            EditorUtility.SetDirty(this);
        }
#endif
    }
}