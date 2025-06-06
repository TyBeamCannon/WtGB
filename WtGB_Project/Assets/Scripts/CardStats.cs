using UnityEngine;
using UnityEngine.UI;

namespace CardStats
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Card")]

    public class Card : ScriptableObject
    {

        public string cardName;

        public Sprite card;

        public CardType cardType;        

        [Header("Attack Stats")]
        public int damageMin;

        public int damageMax;

        [Header("Defense Stats")]
        public int defenseMin;

        public int defenseMax;

        [Header("Counter Stats")]
        public float counterTime;
        public float counterChance; // is the % to trigger a counter.
        public int counterDamage;   
        public float counterRange;

        [Header("Silly Stats")]
        public float castTime;
        public float cooldown;
        public string sideEffect;
        internal object cardImage;

        public enum CardType
        {
            Attack,
            Defense,
            Counter,
            Silly

        }
    }
}
