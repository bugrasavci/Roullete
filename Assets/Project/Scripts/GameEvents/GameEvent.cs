using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Project.Scripts.GameEvents
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/Game Event", order = 0)]
    public class GameEvent : ScriptableObject
    {
        private List<UnityEvent> _preEventListeners = new List<UnityEvent>();
        private List<UnityEvent> _listeners = new List<UnityEvent>();
        private List<UnityEvent> _postEventListeners = new List<UnityEvent>();

        [SerializeField] private bool logsEnabled = true;

        [ContextMenu("Invoke Game Event")]
        public void Invoke()
        {
            if (logsEnabled)
                Debug.Log($"[GameEvent] {name} Called");
            InvokeListeners();
        }

        private void InvokeListeners()
        {
            InvokePreEventListeners();
            InvokeEventListeners();
            InvokePostEventListeners();
        }

        private void InvokePreEventListeners()
        {
            for (var i = _preEventListeners.Count - 1; i >= 0; i--)
            {
                _preEventListeners[i]?.Invoke();
            }
        }

        private void InvokeEventListeners()
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i]?.Invoke();
            }
        }

        private void InvokePostEventListeners()
        {
            for (var i = _postEventListeners.Count - 1; i >= 0; i--)
            {
                _postEventListeners[i]?.Invoke();
            }
        }

        public void RegisterPreEventListener(UnityEvent listener)
        {
            _preEventListeners.Add(listener);
        }

        public void RegisterListener(UnityEvent listener)
        {
            _listeners.Add(listener);
        }

        public void RegisterPostEventListener(UnityEvent listener)
        {
            _postEventListeners.Add(listener);
        }

        public void UnregisterPreEventListener(UnityEvent listener)
        {
            _preEventListeners.Remove(listener);
        }

        public void UnregisterListener(UnityEvent listener)
        {
            _listeners.Remove(listener);
        }

        public void UnregisterPostEventListener(UnityEvent listener)
        {
            _postEventListeners.Remove(listener);
        }
    }
}