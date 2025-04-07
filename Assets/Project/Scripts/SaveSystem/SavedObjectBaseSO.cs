using UnityEngine;

namespace Assets.Project.Scripts.SaveSystem
{
    public abstract class SavedObjectBase : ScriptableObject
    {
        public abstract void Init();
        public abstract void SaveManual();
        public abstract void Reset();
    }

    public abstract class SavedObjectBaseSO<T> : SavedObjectBase where T : class
    {
        [SerializeField] private SavedObject<T> defaultValues;

        private SavedObject<T> _savedObject;

        protected T DefaultData => defaultValues.data;

        protected T SavedData => _savedObject.data;

        public override void Init()
        {
            _savedObject = new SavedObject<T>(name);

            if (!_savedObject.IsLoaded)
            {
                _savedObject.data = defaultValues.data.DeepCopy();
                _savedObject.SaveJson();
            }
            _savedObject.LoadData();
        }

        public override void SaveManual()
        {
            _savedObject.SaveJson();
        }

        public override void Reset()
        {
            _savedObject?.Delete();
            _savedObject = null;
        }
    }
}