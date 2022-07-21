using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class ConfirmPopup : Popup
    {
        private System.Action _onConfirm;
        private System.Action _onCancle;
        private UIHelper _ui;
        protected Button cancleButton;
        protected Button confirmButton;

        protected override void Awake()
        {
            base.Awake();

            _ui = FindObjectOfType<UIHelper>();

            cancleButton = root.Q<Button>("cancle");
            confirmButton = root.Q<Button>("confirm");

            cancleButton.RegisterCallback<ClickEvent>(e => OnCancle());
            confirmButton.RegisterCallback<ClickEvent>(e => OnConfirm());
        }

        public ConfirmPopup OnConfirm(System.Action action)
        {
            _onConfirm = action;
            return this;
        }

        public ConfirmPopup OnCancle(System.Action action)
        {
            _onCancle = action;
            return this;
        }

        private void OnCancle()
        {
            _ui.ClosePopup(this);
            _onCancle?.Invoke();
        }

        private void OnConfirm()
        {
            _ui.ClosePopup(this);
            _onConfirm?.Invoke();
        }
    }
}
