using UnityEngine;
using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class StoreController : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private StoreData _storeData;
        [SerializeField] private VisualTreeAsset _storeItemVta;

        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            _closeButton = root.Q<Button>("close-button");
            _closeButton.RegisterCallback<ClickEvent>(e => OnCloseButtonClicked());

            var itemsParent = root.Q<VisualElement>("items-parent");
            for (int i = 0; i < _storeData.Products.Count; i++)
            {
                var item = _storeItemVta.Instantiate();
                var button = item.Q<Button>("button");

                int idx = i;
                button.RegisterCallback<ClickEvent>(evt => OnItemClicked(idx));
                itemsParent.Add(item);
            }
        }

        private void OnItemClicked(int idx)
        {
            print("Click " + idx);
        }

        private void OnCloseButtonClicked()
        {
            Destroy(gameObject);
        }
    }
}
