using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FlappyClone.Singletons
{
    [DisallowMultipleComponent]
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; private set; }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Awake()
        {
            Instance = Instance == null
                ? GetComponent<T>()
                : throw new Exception($"Singleton type '{typeof(T).Name}' already has an instance at '${string.Join(".", Instance.transform.parent.name, Instance.transform.name)}'");

            OnAwake();
        }

        protected virtual void OnAwake() { }

    }
}
