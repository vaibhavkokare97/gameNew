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

        private CardController _cardController;

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
            _cardController = FindAnyObjectByType<CardController>();
            GetComponent<Button>().onClick.AddListener(delegate
            {
                StartCoroutine(FlipAndMatch());
            }
            );
        }

        private void Start()
        {
            Flip(); // start face down
        }

        private void Flip()
        {
            if (isFlipping)
                return;

            StartCoroutine(FlipAnimation());
        }

        private IEnumerator FlipAndMatch()
        {
            if (isFlipping)
                yield break;
            yield return FlipAnimation();
            yield return CheckForCardMatch();
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

                    _spriteImage.sprite = isFaceUp ? cardSprite : _cardController.defaultSprite;
                }

                yield return null;
            }

            _spriteImage.transform.localRotation =
                Quaternion.Euler(0f, targetY, 0f);

            isFaceUp = !isFaceUp;

            isFlipping = false;
        }

        IEnumerator CheckForCardMatch()
        {
            if (_cardController.lastDrawnCard != null)
            {
                if ( MatchFound(_cardController.lastDrawnCard))
                {
                    // remove animation
                    Debug.Log("Match found for card id: " + cardId);
                    Destroy(_cardController.lastDrawnCard._spriteImage);
                    Destroy(_spriteImage);
                    _cardController.lastDrawnCard = null;

                }
                else
                {
                    if (!IsFaceUp) Flip();
                    if (!_cardController.lastDrawnCard.IsFaceUp) _cardController.lastDrawnCard.Flip();
                }

                _cardController.lastDrawnCard = null;
                yield break;
            }

            if (_cardController.lastDrawnCard == null) _cardController.lastDrawnCard = this;
            yield return null;
        }



        public bool MatchFound(CardView otherCard)
        {
            if (otherCard == null) return false;

            if (otherCard.cardId == cardId && otherCard != this)
            {
                return true;
            }
            return false;
        }
    }
}
