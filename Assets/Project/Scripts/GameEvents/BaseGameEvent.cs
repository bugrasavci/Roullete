using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.GameEvents
{
    public class BaseGameEvent<T> : ScriptableObject
    {
        private List<IGameEventListener<T>> _listeners = new List<IGameEventListener<T>>();
        private List<string> _assetLocations = new List<string>();

        [SerializeField] private bool logsEnabled = true;

        public void Invoke(T t)
        {
            if (logsEnabled)
                Debug.Log($"[GameEvent] {name} Called with: {t}");
            for (int i = _listeners.Count - 1; i >= 0; i--) _listeners[i].Invoke(t);
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            _listeners.Add(listener);
            _assetLocations.Add(listener.AssetLocation);
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            _listeners.Remove(listener);
            _assetLocations.Remove(listener.AssetLocation);
        }
    }
}