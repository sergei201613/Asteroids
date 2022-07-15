using UnityEngine;

namespace TeaGames.Asteroids
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField]
        public int Coins { get; private set; }

        public void AddCoins(int value)
        {
            if (value <= 0)
                return;

            Coins += value;
        }
    }
}
