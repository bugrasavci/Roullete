using UnityEngine;
using UnityEngine.Events;

namespace Assets.Project.Scripts.GameEvents
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent[] Events;
        public UnityEvent PreEventResponse;
        public UnityEvent Response;
        public UnityEvent PostEventResponse;

        private void OnEnable()
        {
            foreach (var e in Events)
                if (PreEventResponse.GetPersistentEventCount() > 0)
                    e.RegisterPreEventListener(PreEventResponse);
            foreach (var e in Events)
                if (Response.GetPersistentEventCount() > 0)
                    e.RegisterListener(Response);
            foreach (var e in Events)
                if (PostEventResponse.GetPersistentEventCount() > 0)
                    e.RegisterPostEventListener(PostEventResponse);
        }

        private void OnDisable()
        {
            foreach (var e in Events)
                if (PreEventResponse.GetPersistentEventCount() > 0)
                    e.UnregisterPreEventListener(PreEventResponse);
            foreach (var e in Events)
                if (Response.GetPersistentEventCount() > 0)
                    e.UnregisterListener(Response);
            foreach (var e in Events)
                if (PostEventResponse.GetPersistentEventCount() > 0)
                    e.UnregisterPostEventListener(PostEventResponse);
        }
    }
}