using Assets.Scripts.Audio;
using Assets.Scripts.Card;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.Score;
using Assets.Scripts.State;
using Assets.Scripts.UI;
using System;
using System.Collections;
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
            audioManager = new AudioManager();
            uiManager = new UIManager();

            CardController.OnMatchComplete += HandleCardMatched;
            CardController.OnTurnComplete += HandleTurnComplete;
            CardController.OnFlipInitiated += FlipSound;

            UIManager.OnGameStartUI += HandleGameStartUI;
            UIManager.OnGameStopUI += HandleGameStopUI;
            UIManager.OnLoadState += (solvedIDs, matchCount, turnCount) =>
            {
                scoreManager.SolvedIDs = new HashSet<int>(solvedIDs);
                scoreManager.SetScore(matchCount, turnCount);
            };
            ScoreManager.OnGameWon += HandleGameWon;

            CardController.OnBoardGenerateCallback += (levelDimensions, levelDifficulty, seed) =>
            {
                scoreManager.Reset();
                scoreManager.Seed = seed;
                scoreManager.DifficultyLevel = levelDifficulty;
                scoreManager.LevelDimension = levelDimensions;
            };
        }

        private void HandleGameWon()
        {
            SaveManager.DeleteSave();
            StartCoroutine(DelayedGameStopUI());
        }

        private IEnumerator DelayedGameStopUI()
        {
            yield return new WaitForSeconds(1f); // Adjust the delay as needed
            UIManager.OnGameStopUI?.Invoke(true);
        }

        private void HandleGameStartUI((int, int) levelDimensions, int levelDifficulty, int seed)
        {
            _cardController.Initiate(levelDimensions, levelDifficulty, seed);
            AppState.currentState = AppState.State.InGame;
        }

        private void HandleGameStopUI(bool? gameWon)
        {
            AppState.currentState = AppState.State.MainMenu;
            if (gameWon == false)
            {
                SaveManager.Save(new SaveData(scoreManager.DifficultyLevel, scoreManager.Seed, 
                    new List<int>(scoreManager.SolvedIDs).ToArray(), 
                    scoreManager.MatchCount, scoreManager.TurnCount));
            }
            scoreManager.Reset();
        }

        private void HandleCardMatched(int cardId)
        {
            scoreManager.IncrementMatches();
            scoreManager.SolvedIDs.Add(cardId);
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

        public bool IsCardSolved(int cardId)
        {
            if (cardId == -1)
                return false;
            return scoreManager.SolvedIDs.Contains(cardId);
        }

        private void OnDisable()
        {

            CardController.OnMatchComplete -= HandleCardMatched;
            CardController.OnTurnComplete -= HandleTurnComplete;
            CardController.OnFlipInitiated -= FlipSound;

            UIManager.OnGameStartUI -= HandleGameStartUI;
            UIManager.OnGameStopUI -= HandleGameStopUI;
            UIManager.OnLoadState -= (solvedIDs, matchCount, turnCount) =>
            {
                scoreManager.SolvedIDs = new HashSet<int>(solvedIDs);
                scoreManager.SetScore(matchCount, turnCount);
            };
            ScoreManager.OnGameWon -= HandleGameWon;

            CardController.OnBoardGenerateCallback -= (levelDimensions, levelDifficulty, seed) =>
            {
                scoreManager.Reset();
                scoreManager.Seed = seed;
                scoreManager.DifficultyLevel = levelDifficulty;
                scoreManager.LevelDimension = levelDimensions;
            };

            scoreManager = null;
            audioManager = null;
            uiManager = null;

        }
    }
}
