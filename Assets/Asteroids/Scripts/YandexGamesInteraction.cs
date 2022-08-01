using System.Runtime.InteropServices;
using UnityEngine;

namespace TeaGames.Asteroids
{
    public class YandexGamesInteraction : MonoBehaviour
    {
        public static YandexGamesInteraction Instance { get; private set; }

        public bool IsWebGL => Application.platform == RuntimePlatform.WebGLPlayer;

        [SerializeField] private PlayerData _playerData;

        private void Awake()
        {
            Instance = this;

            Loaded();
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

        public void ShowInterstitial()
        {
            if (IsWebGL)
            {
                showInterstitial();
            }
        }

        public void ShowRewarded()
        {
            if (IsWebGL)
            {
                showRewarded();
            }
        }

        public void OnRewarded()
        {
            _playerData.AddCoins(4000);
        }

        [DllImport("__Internal")]
        private static extern void loaded();

        [DllImport("__Internal")]
        private static extern void setRecord(int value);

        [DllImport("__Internal")]
        private static extern void showInterstitial();

        [DllImport("__Internal")]
        private static extern void showRewarded();
    }
}
