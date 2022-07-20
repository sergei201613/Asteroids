using UnityEngine;

namespace TeaGames.Asteroids.UI
{
    public class PurchaseConfirmPopup : ConfirmPopup
    {
        [SerializeField]
        private string text;

        public ConfirmPopup Init(string name, string price)
        {
            label.text = string.Format(text, name, price);
            return this;
        }
    }
}
