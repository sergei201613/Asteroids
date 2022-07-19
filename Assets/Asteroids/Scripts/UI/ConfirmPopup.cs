using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class ConfirmPopup : Popup
    {
        private Button _cancleButton;
        private Button _confirmButton;

        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            _cancleButton = root.Q<Button>("cancle");
            _confirmButton = root.Q<Button>("confirm");

            _cancleButton.RegisterCallback<ClickEvent>(e => OnCancle());
            _confirmButton.RegisterCallback<ClickEvent>(e => OnConfirm());
        }

        private void OnCancle()
        {
            UIHelper.ClosePopup(this);
        }

        private void OnConfirm()
        {
            print("TODO: Confirm");
            OnCancle();
        }
    }
}
