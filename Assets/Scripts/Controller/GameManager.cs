using Assets.Scripts.Audio;
using Assets.Scripts.Card;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.Score;
using Assets.Scripts.State;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controller
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public UIManager uiManager { get; private set; }
        public ScoreManager scoreManager { get; private set; }
        public SaveManager saveManager { get; private set; }
        public AudioManager audioManager { get; private set; }

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
            uiManager = new UIManager();

            CardController.OnMatchComplete += HandleCardMatched;
            CardController.OnTurnComplete += HandleTurnComplete;
            CardController.OnFlipInitiated += FlipSound;

            UIManager.OnGameStartUI += HandleGameStartUI;
            UIManager.OnGameStopUI += HandleGameStopUI;
        }

        private void HandleGameStartUI((int, int) levelDifficulty, int seed)
        {
            _cardController.Initiate(levelDifficulty, seed);
            AppState.currentState = AppState.State.InGame;
        }

        private void HandleGameStopUI()
        {
            AppState.currentState = AppState.State.MainMenu;
            scoreManager.Reset();
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
            CardController.OnFlipInitiated -= FlipSound;

            UIManager.OnGameStartUI -= HandleGameStartUI;
            UIManager.OnGameStopUI -= HandleGameStopUI;

            scoreManager = null;
            saveManager = null;
            audioManager = null;
            uiManager = null;

        }
    }
}
