using UnityEngine;

namespace CardStats
{
    public class BattleStarter : MonoBehaviour
    {
        public CardBattleUIManager battleUIManager;
        public GameObject battleUIRoot;

        bool battleStarted = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (battleUIRoot != null)
            {
                battleUIRoot.SetActive(false);
                battleUIManager = battleUIRoot.GetComponent<CardBattleUIManager>();
            }
               
        }

        // Update is called once per frame
        void OnTriggerEnter(Collider other)
        {
            if (battleStarted) return;

            if (other.CompareTag("Player"))
            {
                TriggerBattle();
                battleStarted = true;
            }
        }

        public void TriggerBattle()
        {
            if (battleUIRoot != null)
                battleUIRoot.SetActive(true);

          
            if (battleUIManager != null)
                battleUIManager.StartBattle();

            Debug.Log("Battle triggered by collision");
        }
    }
}
