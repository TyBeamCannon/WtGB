using UnityEngine;

public class PlayerMiningInteration : MonoBehaviour
{
    private MiningNode currentNode;

    //Reference to the fatigue manager
    [SerializeField] private FatigueManager fatigueManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentNode != null)
        {
            if (fatigueManager.UseStamina(3))
            {
                currentNode.Mine();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out MiningNode node))
        {
            currentNode = node;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out MiningNode node) && node == currentNode)
        {
            currentNode = null;
        }

    }
}
