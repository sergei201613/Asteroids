using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class ConfirmPopup : Popup
    {
        private System.Action _onConfirm;
        private System.Action _onCancle;
        protected Button cancleButton;
        protected Button confirmButton;

        protected override void Awake()
        {
            base.Awake();

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
            UIHelper.Instance.ClosePopup(this);
            _onCancle?.Invoke();
        }

        private void OnConfirm()
        {
            UIHelper.Instance.ClosePopup(this);
            _onConfirm?.Invoke();
        }
    }
}
