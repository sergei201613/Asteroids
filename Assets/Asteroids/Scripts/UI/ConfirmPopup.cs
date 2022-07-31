using UnityEngine.UIElements;

namespace TeaGames.UIFramework
{
    public class ConfirmPopup : Popup
    {
        public event System.Action Confirmed;
        public event System.Action Cancled;

        protected Button cancleButton;
        protected Button confirmButton;

        private const string ConfirmButtonQuery = "confirm";
        private const string CancleButtonQuery = "cancle";

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            confirmButton = root.Q<Button>(ConfirmButtonQuery);
            cancleButton = root.Q<Button>(CancleButtonQuery);

            confirmButton.RegisterCallback<ClickEvent>(e => OnConfirmed());
            cancleButton.RegisterCallback<ClickEvent>(e => OnCancled());
        }

        private void OnConfirmed()
        {
            Confirmed?.Invoke();
            uiManager.ClosePopup(this);
        }

        private void OnCancled()
        {
            Cancled?.Invoke();
            uiManager.ClosePopup(this);
        }
    }
}
