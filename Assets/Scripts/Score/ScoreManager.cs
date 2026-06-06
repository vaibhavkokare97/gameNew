using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Score
{
    public class ScoreManager : MonoBehaviour
    {
        public int MatchCount { get; private set; }
        public int TurnCount { get; private set; }

        public void IncrementMatches() => MatchCount++;

        public void IncrementTurns() => TurnCount++;

        public void Reset()
        {
            MatchCount = 0;
            TurnCount = 0;
        }
    }
}