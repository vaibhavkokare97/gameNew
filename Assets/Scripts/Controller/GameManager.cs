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

        [SerializeField] private ScoreManager _scoreManager;
        public ScoreManager ScoreManager => _scoreManager;

        [SerializeField] private SaveManager _saveManager;
        public SaveManager SaveManager => _saveManager;

        [SerializeField] private AudioManager _audioManager;
        public AudioManager AudioManager => _audioManager;

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
    }
}
