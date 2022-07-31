using UnityEngine;
using UnityEngine.Serialization;

namespace TeaGames.UIFramework
{
    public class PurchaseConfirmPopup : ConfirmPopup
    {
        [FormerlySerializedAs("text")]
        [SerializeField] private string _textFormat;

        public void Init(UIManager uiManager, string name, string price)
        {
            base.Init(uiManager);
            label.text = string.Format(_textFormat, name, price);
        }
    }
}
