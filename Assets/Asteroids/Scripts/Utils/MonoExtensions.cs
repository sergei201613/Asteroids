using System.Collections;
using UnityEngine;
using System;

namespace TeaGames.MonoBehaviourExtensions
{
    static class MonoExtensions
    {
        /// <param name="delay">Delay in seconds.</param>
        public static Action Delay(this MonoBehaviour mono, float delay, Action action)
        {
            IEnumerator DelayCoroutine()
            {
                yield return new WaitForSeconds(delay);
                action?.Invoke();
            }

            mono.StartCoroutine(DelayCoroutine());
            return action;
        }

        public static T RequireComponent<T>(this MonoBehaviour mono) where T : Component
        {
            if (mono.TryGetComponent<T>(out var component))
                return component;
            else
                throw new MissingComponentException($"Missing {typeof(T).FullName} component of {mono.gameObject.name} game object!");
        }
    }
}