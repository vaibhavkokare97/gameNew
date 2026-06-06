using Assets.Scripts.SaveLoad;
using Assets.Scripts.Audio;
using Assets.Scripts.Score;
using Assets.Scripts.State;
using System;
using UnityEngine;
using Assets.Scripts.UI;
using Assets.Scripts.Card;

namespace Assets.Scripts.Controller
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private UIManager _UIManager;
        public UIManager UIManager => _UIManager;

        public ScoreManager scoreManager;
        public SaveManager saveManager;
        public AudioManager audioManager;

        [SerializeField] private CardController _cardController;
        public CardController CardController => _cardController;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance.gameObject);
            }

            Instance = this;
        }

        private void OnEnable()
        {
            scoreManager = new ScoreManager();
            saveManager = new SaveManager();
            audioManager = new AudioManager();

            CardController.OnMatchComplete += HandleCardMatched;
            CardController.OnTurnComplete += HandleTurnComplete;    
            CardController.OnFlipInitiated += FlipSound;
        }

        private void HandleCardMatched(int cardId)
        {
            scoreManager.IncrementMatches();
            audioManager.PlayOneShot(0); // 0 represents the match sound
        }

        private void HandleTurnComplete(int cardId)
        {
            scoreManager.IncrementTurns();
            audioManager.PlayOneShot(1); // 1 represents the turn sound
        }

        private void FlipSound()
        {
            audioManager.PlayOneShot(2); // 2 represents the flip sound
        }

        private void OnDisable()
        {

            CardController.OnMatchComplete -= HandleCardMatched;
            CardController.OnTurnComplete -= HandleTurnComplete;

            scoreManager = null;
            saveManager = null;
            audioManager = null;


        }
    }
}
