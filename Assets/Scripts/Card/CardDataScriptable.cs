using UnityEngine;

namespace Assets.Scripts.Card
{
    [CreateAssetMenu(fileName = "CardDataScriptable", menuName = "Scriptable Objects/CardDataScriptable")]
    public class CardDataScriptable : ScriptableObject
    {
        public int ID;
        public Sprite CardSprite;
    }
}
