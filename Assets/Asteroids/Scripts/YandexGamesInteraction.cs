using System.Runtime.InteropServices;
using UnityEngine;

namespace TeaGames.Asteroids
{
    public class YandexGamesInteraction : MonoBehaviour
    {
        public static YandexGamesInteraction Instance { get; private set; }

        public bool IsWebGL => Application.platform == RuntimePlatform.WebGLPlayer;

        private void Awake()
        {
            Instance = this;

            Loaded();
            SetRecord(123);
        }

        public void Loaded()
        {
            if (IsWebGL)
            {
                loaded();
            }
        }

        public void SetRecord(int value)
        {
            if (IsWebGL)
            {
                setRecord(value);
            }
        }

        [DllImport("__Internal")]
        private static extern void loaded();

        [DllImport("__Internal")]
        private static extern void setRecord(int value);
    }
}
