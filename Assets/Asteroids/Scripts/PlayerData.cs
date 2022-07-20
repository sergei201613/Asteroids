using System.Collections.Generic;
using UnityEngine;

namespace TeaGames.Asteroids
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField]
        public int Coins { get; private set; }

        public IReadOnlyList<Product> Products => _products.AsReadOnly();

        [SerializeField]
        private List<Product> _products = new();

        public void AddCoins(int value)
        {
            if (value <= 0)
                return;

            Coins += value;
        }

        public bool HasProduct(Product product)
        {
            return _products.Contains(product);
        }

        public void AddProduct(Product product)
        {
            if (product == null)
                throw new System.ArgumentNullException();

            if (HasProduct(product))
                return;

            _products.Add(product);

            Coins -= product.Price;
        }

        public bool CanBuy(Product product)
        {
            if (product == null)
                return false;

            return Coins >= product.Price;
        }
    }
}
