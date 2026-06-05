using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Controller;

namespace Assets.Scripts.Card
{
    [RequireComponent(typeof(Button))]
    public class CardView : MonoBehaviour
    {
        public int cardId;
        private Sprite _cardSprite;

        public Sprite cardSprite
        {
            get => _cardSprite;
            set
            {
                _cardSprite = value;
                _spriteImage.sprite = value;
            }
        }


        [Header("Visuals")]
        [SerializeField] private Image _spriteImage;


        [Header("Animation")]
        [SerializeField] private float flipDuration = 0.25f;
        [SerializeField]
        private AnimationCurve flipCurve =
            AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        private bool isFaceUp;
        private bool isFlipping;

        public bool IsFaceUp => isFaceUp;
        public bool IsFlipping => isFlipping;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => Flip());
        }

        private void Start()
        {
            Flip(); // start face down
        }

        public void Flip()
        {
            if (isFlipping)
                return;

            Debug.Log("clicked card with id: " + cardId);
            StartCoroutine(FlipAnimation());
        }

        private IEnumerator FlipAnimation()
        {
            isFlipping = true;

            float startY = isFaceUp ? 180f : 0f;
            float targetY = isFaceUp ? 0f : 180f;

            float elapsed = 0f;
            bool visualSwapped = false;

            while (elapsed < flipDuration)
            {
                elapsed += Time.deltaTime;

                float normalizedTime = Mathf.Clamp01(elapsed / flipDuration);

                float curvedTime = flipCurve.Evaluate(normalizedTime);

                float currentY = Mathf.Lerp(startY, targetY, curvedTime);

                _spriteImage.transform.localRotation = Quaternion.Euler(0f, currentY, 0f);

                bool shouldSwapVisualAfter = isFaceUp ? currentY <= 90f : currentY >= 90f;

                if (!visualSwapped && shouldSwapVisualAfter)
                {
                    visualSwapped = true;

                    _spriteImage.sprite = isFaceUp
                        ? cardSprite
                        : GameManager.Instance.CardController.defaultSprite;
                }

                yield return null;
            }

            _spriteImage.transform.localRotation =
                Quaternion.Euler(0f, targetY, 0f);

            isFaceUp = !isFaceUp;

            isFlipping = false;

            if (!isFaceUp)
            {
                Flip(); // to hide
            }
        }
    }
}
