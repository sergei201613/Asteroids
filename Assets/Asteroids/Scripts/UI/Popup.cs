using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace TeaGames.UIFramework
{
    public class Popup : Widget
    {
        public string Text
        {
            get => label.text;
            set => label.text = value;
        }

        protected VisualElement popup;
        protected Label label;

        private const string HideStyle = "popup-hide";
        private const string ShowStyle = "popup-show";
        private const string PopupQuery = "popup";
        private const string TextQuery = "text";

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            popup = root.Q(PopupQuery);
            label = root.Q<Label>(TextQuery);
        }

        public void Show(System.Action onComplete = null)
        {
            popup.RegisterCallback<TransitionEndEvent>(e => onComplete?.Invoke());
            StartCoroutine(ShowCoroutine());
        }

        public void Hide(System.Action onComplete = null)
        {
            popup.RegisterCallback<TransitionEndEvent>(e => onComplete?.Invoke());
            popup.RemoveFromClassList(ShowStyle);
            popup.AddToClassList(HideStyle);
        }

        private IEnumerator ShowCoroutine()
        {
            popup.AddToClassList(HideStyle);

            yield return new WaitForEndOfFrame();

            popup.RemoveFromClassList(HideStyle);
            popup.AddToClassList(ShowStyle);
        }
    }
}
