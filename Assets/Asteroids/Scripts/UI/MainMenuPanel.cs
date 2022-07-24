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
        private SettingsPanel _settingsPanelPrefab;
        [SerializeField]
        private GameSelectionPanel _gameSelectionPanelPrefab;
        [SerializeField]
        private PlayerData _playerData;
        [SerializeField]
        private string _recordTextFormat;
        [SerializeField]
        private bool _isIngame;

        private GameMode _gm;

        protected override void Awake()
        {
            base.Awake();

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
            var settingsBtn = root.Q<Button>("settings");
            var mainMenuBtn = root.Q<Button>("main-menu");

            continueBtn.RegisterCallback<ClickEvent>(ContinueGame);
            newGameBtn.RegisterCallback<ClickEvent>(Play);
            settingsBtn.RegisterCallback<ClickEvent>(OpenSettings);
            mainMenuBtn.RegisterCallback<ClickEvent>(OpenMainMenu);
        }

        private void InitDefault()
        {
            var playBtn = root.Q<Button>("play");
            var settingsBtn = root.Q<Button>("settings");
            var storeBtn = root.Q<Button>("store");

            playBtn.RegisterCallback<ClickEvent>(SelectGame);
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
            UIHelper.Instance.OpenPanel(_storePanelPrefab);
        }

        private void OpenSettings(ClickEvent evt)
        {
            UIHelper.Instance.OpenPanel(_settingsPanelPrefab);
        }

        private void Play(ClickEvent evt)
        {
            SceneManager.LoadScene("Game");
        }

        private void SelectGame(ClickEvent evt)
        {
            UIHelper.Instance.OpenPanel(_gameSelectionPanelPrefab);
        }
    }
}
