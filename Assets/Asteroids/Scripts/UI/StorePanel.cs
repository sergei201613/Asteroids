using UnityEngine;
using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class StorePanel : Panel
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private StoreData _storeData;
        [SerializeField] private VisualTreeAsset _storeItemVta;
        [SerializeField] private Popup _confirmPopupPrefab;

        private Button _closeButton;

        private void Awake()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            _closeButton = root.Q<Button>("close-button");
            _closeButton.RegisterCallback<ClickEvent>(e => OnCloseButtonClicked());

            CreateProductItems(root);
        }

        private void CreateProductItems(VisualElement root)
        {
            var itemsParent = root.Q<VisualElement>("items-parent");
            for (int i = 0; i < _storeData.Products.Count; i++)
            {
                var item = _storeItemVta.Instantiate();

                var button = item.Q<Button>("button");
                var name = item.Q<Label>("label");
                var price = item.Q<Label>("price");
                var icon = item.Q<VisualElement>("icon");

                name.text = _storeData.Products[i].Name;
                price.text = _storeData.Products[i].Price.ToString("N0");
                var iconSprite = _storeData.Products[i].Icon;
                icon.style.backgroundImage = new StyleBackground(iconSprite);

                int idx = i;
                button.RegisterCallback<ClickEvent>(evt => OnItemClicked(idx));
                itemsParent.Add(item);
            }
        }

        private void OnItemClicked(int idx)
        {
            print("Click " + idx);

            UIHelper.OpenPopup(_confirmPopupPrefab);
        }

        private void OnCloseButtonClicked()
        {
            Destroy(gameObject);
        }
    }
}
