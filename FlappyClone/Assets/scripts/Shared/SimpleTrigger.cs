using UnityEngine;
using UnityEngine.Events;

namespace FlappyClone.Shared
{
    public class SimpleTrigger : MonoBehaviour
    {
        public UnityEvent Triggered = new UnityEvent();
        public void Trigger() => Triggered.Invoke();
    }
}
