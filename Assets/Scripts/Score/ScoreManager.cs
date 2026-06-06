using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Score
{
    public class ScoreManager
    {
        public ScoreManager()
        {
            Reset();
        }

        public int MatchCount { get; private set; }
        public int TurnCount { get; private set; }

        public static Action<int, int> OnScoreUpdated;

        public void IncrementMatches()
        {
            MatchCount++;
            OnScoreUpdated?.Invoke(MatchCount, TurnCount);
        }

        public void IncrementTurns()
        {
            TurnCount++;
            OnScoreUpdated?.Invoke(MatchCount, TurnCount);
        }

        public void Reset()
        {
            MatchCount = 0;
            TurnCount = 0;
            OnScoreUpdated?.Invoke(MatchCount, TurnCount);
        }
    }
}