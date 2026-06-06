using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Score
{
    public class ScoreUI : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI matchText;
        [SerializeField] private TextMeshProUGUI turnText;

        private void OnEnable()
        {
            ScoreManager.OnScoreUpdated += UpdateScore;
        }

        public void UpdateScore(int matchCount, int turnCount)
        {
            matchText.text = $"Matches: {matchCount}";
            turnText.text = $"Turns: {turnCount}";
        }

        private void OnDisable()
        {
            ScoreManager.OnScoreUpdated -= UpdateScore;
        }
    }
}