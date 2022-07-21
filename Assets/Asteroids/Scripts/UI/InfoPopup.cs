using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class InfoPopup : Popup
    {
        private Button _okButton;
        private UIHelper _ui;

        protected override void Awake()
        {
            base.Awake();

            _ui = FindObjectOfType<UIHelper>();

            _okButton = root.Q<Button>("ok");
            _okButton.RegisterCallback<ClickEvent>(e => OnOk());
        }

        private void OnOk()
        {
            _ui.ClosePopup(this);
        }
    }
}
