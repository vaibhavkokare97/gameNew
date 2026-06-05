using UnityEngine;

namespace Assets.Scripts.Card
{
    [CreateAssetMenu(fileName = "AllCardsGameData", menuName = "Scriptable Objects/AllCardsGameData")]
    public class AllCardsGameData : ScriptableObject
    {
        public CardDataScriptable[] AllCards;
    }
}
