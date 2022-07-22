using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class MainMenuPanel : Panel
    {
        [SerializeField]
        private StorePanel _storePanelPrefab;
        [SerializeField]
        private PlayerData _playerData;
        [SerializeField]
        private string _recordTextFormat;
        [SerializeField]
        private bool _isIngame;

        private UIHelper _ui;
        private GameMode _gm;

        protected override void Awake()
        {
            base.Awake();

            _ui = FindObjectOfType<UIHelper>();
            _gm = FindObjectOfType<GameMode>();

            var _recordLbl = root.Q<Label>("record");
            var _coinsLbl = root.Q<Label>("coins");

            string record = _playerData.Record.ToString();
            _recordLbl.text = string.Format(_recordTextFormat, record);
            _coinsLbl.text = _playerData.Coins.ToString();

            if (_isIngame)
                InitIngame();
            else
                InitDefault();
        }

        private void InitIngame()
        {
            var continueBtn = root.Q<Button>("continue");
            var newGameBtn = root.Q<Button>("new-game");
            var mainMenuBtn = root.Q<Button>("main-menu");

            continueBtn.RegisterCallback<ClickEvent>(ContinueGame);
            newGameBtn.RegisterCallback<ClickEvent>(Play);
            mainMenuBtn.RegisterCallback<ClickEvent>(OpenMainMenu);
        }

        private void InitDefault()
        {
            var playBtn = root.Q<Button>("play");
            var settingsBtn = root.Q<Button>("settings");
            var storeBtn = root.Q<Button>("store");

            playBtn.RegisterCallback<ClickEvent>(Play);
            settingsBtn.RegisterCallback<ClickEvent>(OpenSettings);
            storeBtn.RegisterCallback<ClickEvent>(OpenStore);
        }

        private void OpenMainMenu(ClickEvent evt)
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void ContinueGame(ClickEvent evt)
        {
            _gm.TogglePause();
        }

        private void OpenStore(ClickEvent evt)
        {
            _ui.OpenPanel(_storePanelPrefab);
        }

        private void OpenSettings(ClickEvent evt)
        {
        }

        private void Play(ClickEvent evt)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
