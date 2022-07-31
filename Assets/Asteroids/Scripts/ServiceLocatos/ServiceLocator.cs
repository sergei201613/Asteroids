using UnityEngine;
using TeaGames.MonoBehaviourExtensions;

namespace TeaGames.ServiceLocator
{
    [DefaultExecutionOrder(-100)]
    public abstract class ServiceLocator<T> : MonoBehaviour where T : ServiceLocator<T>
    {
        public static T Instance { get; private set; }

        public static TS Get<TS>() where TS : Component
        {
            return Instance.RequireComponent<TS>();
        }

        protected virtual void Awake()
        {
            var instances = FindObjectsOfType<T>();

            if (instances.Length > 1)
                throw new System.Exception($"More than one instance of {typeof(T).FullName}!");

            Instance = instances[0];
        }
    }
}
