using System.Collections.Generic;
using UnityEngine;

namespace TeaGames.Asteroids
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField]
        public int Coins { get; private set; }

        public IEnumerator<Product> Products => _products.GetEnumerator();

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
    }
}
