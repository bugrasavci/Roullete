using System;
using System.IO;
using UnityEngine;

namespace Assets.Project.Scripts.SaveSystem
{
    [Serializable]
    public class SavedObject<T> where T : class
    {
        public T data;

        private string _filePath;

        private string _jsonString;

        public bool IsLoaded => !string.IsNullOrEmpty(_jsonString);

        public SavedObject(string fileName)
        {

            _filePath = Path.Combine(Application.persistentDataPath, fileName + ".game");
            if (File.Exists(_filePath)) LoadJson();
        }

        public string GetShortName(string fileName)
        {
            return fileName.Replace("ObjectSO", "").ToLower();
        }

        private void LoadJson()
        {
            _jsonString = File.ReadAllText(_filePath);
        }

        public void SaveJson()
        {
            _jsonString = JsonUtility.ToJson(data);
            File.WriteAllText(_filePath, _jsonString);
        }

        public void LoadData()
        {
            data = JsonUtility.FromJson<T>(_jsonString);
        }

        public void Delete()
        {
            if (File.Exists(_filePath)) File.Delete(_filePath);
        }

    }
}