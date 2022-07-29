using TeaGames.MonoBehaviourExtensions;
using UnityEngine;

namespace TeaGames.ServiceLocator
{
    [DefaultExecutionOrder(-100)]
    public abstract class ServiceLocator<T> : MonoBehaviour where T : ServiceLocator<T>
    {
        public static T Instance { get; private set; }

        public bool IsInitialized { get; private set; } = true;

        public event System.Action Initialized;

        public static TS GetService<TS>() where TS : Component
        {
            return Instance.RequireComponent<TS>();
        }

        protected virtual void Awake()
        {
            var instances = FindObjectsOfType<T>();

            if (instances.Length > 1)
                throw new System.Exception($"More than one instance of {typeof(T).FullName}!");

            Instance = instances[0];

            if (IsInitialized)
                Initialized?.Invoke();
        }
    }
}
