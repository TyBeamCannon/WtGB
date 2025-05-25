using UnityEngine;
using System.Collections;

namespace CardStats
{
    public class CardExecutor : MonoBehaviour
    {
        public Card card;

        public void execute()
        {
            switch (card.cardType)
            {
                case Card.CardType.Attack:
                    ExecuteAttack();
                    break;
                case Card.CardType.Defense:
                    ExecuteDefense();
                    break;
                case Card.CardType.Counter:
                    ExecuteCounter();
                    break;
                case Card.CardType.Silly:
                    ExecuteSilly();
                    break;
            }
        }

        void ExecuteAttack()
        {
            int damage = Random.Range(card.damageMin, card.damageMax + 1);
        }

        void ExecuteDefense()
        {
            int block = Random.Range(card.defenseMin, card.defenseMax + 1);
        }

        IEnumerator ExecuteCounter()
        {
            yield return new WaitForSeconds(card.counterTime);

            if(Random.value < card.counterChance)
            {
            }
            else
            {
            }
        }

        IEnumerator ExecuteSilly()
        {
            yield return new WaitForSeconds(card.castTime);

            yield return new WaitForSeconds(card.cooldown);

        }
    }

}