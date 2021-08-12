using System.Collections;
using UnityEngine;

namespace MonoBehaviourExtensions
{
    static class TimeUtils
    {
        public static System.Action Delay(this MonoBehaviour mono, float delayInSeconds, System.Action action)
        {
            IEnumerator Del()
            {
                yield return new WaitForSeconds(delayInSeconds);
                action?.Invoke();
            }

            mono.StartCoroutine(Del());
            return action;
        }
    }
}