using UnityEngine;
using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class StoreController : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            _closeButton = root.Q<Button>("close-button");
            _closeButton.RegisterCallback<ClickEvent>(e => OnCloseButtonClicked());
        }

        private void OnCloseButtonClicked()
        {
            Destroy(gameObject);
        }
    }
}
