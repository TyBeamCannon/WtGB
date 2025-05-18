using CardStats;
using DPUtils.Systems.DateTime;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Script Managers")]
    [SerializeField] PlayerCharacter playerScript;


    [Header("Player")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject inventory;
    bool invOpen = false;
    [SerializeField] List<InventoryItem> invItems;
    [SerializeField] List<int> invItemSlot; 
    [SerializeField] int goldCount;

    [Header("Dialogue")]
    [SerializeField] GameObject dialogueBox;
    bool dialogueActive;

    [Header("Time")]
    [SerializeField] GameObject clockUI;

    [Header("Level")]
    [SerializeField] GameObject[] sceneSpawnPoints;


    public int GoldCount { get { return goldCount; } set { goldCount = value; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        player = GameObject.FindWithTag("Player");
        if (player != null)
            playerScript = player.GetComponent<PlayerCharacter>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!invOpen && !dialogueActive)
            {
                OpenInventory();
            }
            else if (invOpen && !dialogueActive)
            {
                CloseInventory();
            }
        }
        
    }

    void ResetInventory(List<InventoryItem> items)
    {
        invItems.Clear();
        invItemSlot.Clear();

        invItems = items;
        for (int i = 0; i < invItems.Count; i++)
        {
            invItemSlot.Add(i);
        }
    }

    void OpenInventory()
    {
        invOpen = true;
        Instantiate(inventory, this.transform);
        for (int i = 0; i < invItems.Count; i++)
        {
            InventoryManager.instance.LoadIntoInventory(invItems[i], invItemSlot[i]);
        }
    }

    void CloseInventory()
    {
        invOpen = false;
        invItems = InventoryManager.instance.SaveItemFromInventory();
        invItemSlot = InventoryManager.instance.SaveItemSlotFromInventory();
        Destroy(transform.GetChild(transform.childCount - 1).gameObject);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueActive = true;
        Instantiate(dialogueBox, this.transform);
        DialogueManager.instance.StartDialogue(dialogue);
    }
    
    public void EndDialogue()
    {
        dialogueActive = false;
        DialogueManager.instance.EndDialogue();
        Destroy(transform.GetChild(transform.childCount - 1).gameObject);
    }
}
