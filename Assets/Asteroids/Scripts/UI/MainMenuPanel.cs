using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class MainMenuPanel : Panel
    {
        [SerializeField]
        private StorePanel _storePanelPrefab;

        private Button _playBtn;
        private Button _settingsBtn;
        private Button _storeBtn;
        private UIHelper _ui;

        protected override void Awake()
        {
            base.Awake();

            _ui = FindObjectOfType<UIHelper>();

            _playBtn = root.Q<Button>("play");
            _settingsBtn = root.Q<Button>("settings");
            _storeBtn = root.Q<Button>("store");

            _playBtn.RegisterCallback<ClickEvent>(OnPlay);
            _settingsBtn.RegisterCallback<ClickEvent>(OnSettings);
            _storeBtn.RegisterCallback<ClickEvent>(OnStore);
        }

        private void OnStore(ClickEvent evt)
        {
            _ui.OpenPanel(_storePanelPrefab);
        }

        private void OnSettings(ClickEvent evt)
        {
        }

        private void OnPlay(ClickEvent evt)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
