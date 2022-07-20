using UnityEngine;
using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class StorePanel : Panel
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private StoreData _storeData;
        [SerializeField] private VisualTreeAsset _storeItemVta;
        [SerializeField] private PurchaseConfirmPopup _purchaseConfirmPopupPrefab;
        [SerializeField] private InfoPopup _infoPopupPrefab;
        [SerializeField] private string _itemPurchasedText;
        [SerializeField] private string _notEnoughCoinsText;

        private Button _closeButton;
        private PurchaseConfirmPopup _confirmPurchasePopup;

        protected override void Awake()
        {
            base.Awake();

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
                ConfigureItem(i, item);
                itemsParent.Add(item);
            }
        }

        private void ConfigureItem(int i, TemplateContainer item)
        {
            var button = item.Q<Button>("button");
            var name = item.Q<Label>("label");
            var price = item.Q<Label>("price");
            var icon = item.Q<VisualElement>("icon");
            var priceIcon = item.Q<VisualElement>("price-icon");

            name.text = _storeData.Products[i].Name;
            price.text = _storeData.Products[i].Price.ToString("N0");
            var iconSprite = _storeData.Products[i].Icon;
            icon.style.backgroundImage = new StyleBackground(iconSprite);

            var product = _storeData.Products[i];
            bool hasProduct = _playerData.HasProduct(product);

            priceIcon.style.display = hasProduct ? 
                DisplayStyle.None : DisplayStyle.Flex;

            price.text = hasProduct ? _itemPurchasedText : price.text;

            int idx = i;
            button.RegisterCallback<ClickEvent>(evt => OnItemClicked(idx));
        }

        private void OnItemClicked(int idx)
        {
            var product = _storeData.Products[idx];
            string name = product.Name;
            string price = product.Price.ToString("N0");

            if (_playerData.CanBuy(product))
            {
                _confirmPurchasePopup = UIHelper.OpenPopup(_purchaseConfirmPopupPrefab);
                _confirmPurchasePopup.Init(name, price).OnConfirm(() =>
                {
                    _playerData.AddProduct(product);
                });
            }
            else
            {
                UIHelper.OpenPopup(_infoPopupPrefab)
                    .SetText(_notEnoughCoinsText);
            }
        }

        private void OnCloseButtonClicked()
        {
            UIHelper.ClosePanel(this);
        }
    }
}
