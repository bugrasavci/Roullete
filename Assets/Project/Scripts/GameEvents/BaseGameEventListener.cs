using UnityEngine;
using UnityEngine.Events;

namespace Assets.Project.Scripts.GameEvents
{
    public class BaseGameEventListener<T, TGameEvent> : MonoBehaviour, IGameEventListener<T>
        where TGameEvent : BaseGameEvent<T>
    {
        public TGameEvent[] events;
        public UnityEvent<T> response;

        private void OnEnable()
        {
            foreach (TGameEvent e in events) e.RegisterListener(this);
        }

        private void OnDisable()
        {
            foreach (TGameEvent e in events) e.UnregisterListener(this);
        }

        public void Invoke(T t)
        {
            response.Invoke(t);
        }

        public string AssetLocation =>
            $"{gameObject.scene.name}/" +
            $"{gameObject.transform.root.name}/" +
            $"{gameObject.name}";
    }

    public interface IGameEventListener<T>
    {
        void Invoke(T t);
        string AssetLocation { get; }
    }
}