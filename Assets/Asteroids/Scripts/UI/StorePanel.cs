using UnityEngine;
using UnityEngine.UIElements;
using TeaGames.UIFramework;

namespace TeaGames.Asteroids.UI
{
    public class StorePanel : Panel
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private StoreData _storeData;
        [SerializeField] private VisualTreeAsset _storeItemVta;
        [SerializeField] private VisualTreeAsset _storeItemSoonVta;
        [SerializeField] private PurchaseConfirmPopup _purchaseConfirmPopupPrefab;
        [SerializeField] private InfoPopup _infoPopupPrefab;
        [SerializeField] private string _itemPurchasedText;
        [SerializeField] private string _itemSelectedText;
        [SerializeField] private string _notEnoughCoinsText;
        private PurchaseConfirmPopup _confirmPurchasePopup;

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);
            CreateProductItems(root);
        }

        private void CreateProductItems(VisualElement root)
        {
            var itemsParent = root.Q<VisualElement>("items-parent");
            for (int i = 0; i < _storeData.Products.Count; i++)
            {
                var item = _storeItemVta.Instantiate();
                SetupItemView(i, item);

                var button = item.Q<Button>("button");
                int idx = i;
                button.RegisterCallback<ClickEvent>(evt => OnItemClicked(idx));

                itemsParent.Add(item);
            }

            var itemSoon = _storeItemSoonVta.Instantiate();
            itemSoon.SetEnabled(false);
            itemsParent.Add(itemSoon);
        }

        private void RefreshProductItems()
        {
            var itemsParent = root.Q<VisualElement>("items-parent");
            var items = itemsParent.Query<VisualElement>("button");

            for (int i = 0; i < _storeData.Products.Count; i++)
            {
                SetupItemView(i, items.AtIndex(i));
            }
        }

        private void SetupItemView(int i, VisualElement item)
        {
            var name = item.Q<Label>("label");
            var price = item.Q<Label>("price");
            var icon = item.Q<VisualElement>("icon");
            var priceIcon = item.Q<VisualElement>("price-icon");
            var button = item.Q<Button>("button");

            name.text = _storeData.Products[i].Name;
            price.text = _storeData.Products[i].Price.ToString("N0");
            var iconSprite = _storeData.Products[i].Icon;
            icon.style.backgroundImage = new StyleBackground(iconSprite);

            var product = _storeData.Products[i];
            bool hasProduct = _playerData.HasProduct(product);
            bool productSelected = _playerData.IsProductSelected(product);

            priceIcon.style.display = hasProduct ? 
                DisplayStyle.None : DisplayStyle.Flex;

            price.text = hasProduct ? _itemPurchasedText : price.text;
            price.text = productSelected ? _itemSelectedText : price.text;

            if (productSelected)
                button.AddToClassList("item-selected");
            else
                button.RemoveFromClassList("item-selected");
        }

        private void OnItemClicked(int idx)
        {
            var product = _storeData.Products[idx];

            if (!_playerData.HasProduct(product))
            {
                TryBuyProduct(product);
            }
            else
            {
                // TODO: Not all product can be selected
                _playerData.SelectProduct(product);
                RefreshProductItems();
            }

        }

        private void TryBuyProduct(Product product)
        {
            string name = product.Name;
            string price = product.Price.ToString("N0");

            if (_playerData.CanBuy(product))
            {
                _confirmPurchasePopup = uiManager.OpenPopup(_purchaseConfirmPopupPrefab);

                _confirmPurchasePopup.Init(uiManager, name, price);
                _confirmPurchasePopup.Confirmed += () =>
                {
                    _playerData.AddProduct(product);
                    RefreshProductItems();
                };
            }
            else
            {
                var popup = uiManager.OpenPopup(_infoPopupPrefab);
                popup.Text = _notEnoughCoinsText;
            }
        }
    }
}
