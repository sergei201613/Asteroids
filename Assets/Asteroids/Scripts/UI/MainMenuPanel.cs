using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TeaGames.UIFramework;
using TeaGames.ServiceLocator;
using static UnityEngine.SceneManagement.SceneManager;
using TeaGames.Utils;
using System;

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

        private const string FreeFlightScene = "Game";
        private const string MainMenuScene = "MainMenu";

        private FreeFlightGameMode _gm;
        private YandexGamesInteraction _yandex;

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            if (_isIngame)
            {
                // TODO: main menu shouldn't know about FreeFlightGameMode
                _gm = SceneServices.Get<FreeFlightGameMode>();
            }
            else
            {
                _yandex = GlobalServices.Get<YandexGamesInteraction>();
            }

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
            var rewardBtn = root.Q<Button>("reward-button");

            playBtn.RegisterCallback<ClickEvent>(SelectGame);
            settingsBtn.RegisterCallback<ClickEvent>(OpenSettings);
            storeBtn.RegisterCallback<ClickEvent>(OpenStore);
            rewardBtn.RegisterCallback<ClickEvent>(OpenRewardVideo);
        }

        private void OpenRewardVideo(ClickEvent evt)
        {
            _yandex.ShowRewarded();
        }

        private void OpenMainMenu(ClickEvent evt)
        {
            SceneHelper.ChangeSceneAsync(MainMenuScene);
        }

        private void ContinueGame(ClickEvent evt)
        {
            _gm.TogglePause();
        }

        private void OpenStore(ClickEvent evt)
        {
            uiManager.OpenPanel(_storePanelPrefab);
        }

        private void OpenSettings(ClickEvent evt)
        {
            uiManager.OpenPanel(_settingsPanelPrefab);
        }

        private void Play(ClickEvent evt)
        {
            SceneHelper.ChangeSceneAsync(FreeFlightScene);
        }

        private void SelectGame(ClickEvent evt)
        {
            uiManager.OpenPanel(_gameSelectionPanelPrefab);
        }
    }
}
