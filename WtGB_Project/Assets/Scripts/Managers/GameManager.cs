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

    public bool inMainMenu;

    [Header("Data Persistance")]
    public GameManagerData data;

    [Header("Script Managers")]
    [SerializeField] PlayerCharacter playerScript;
    [SerializeField] TimeManager timeManager;

    [Header("Player")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject inventory;
    bool invOpen = false;

    [Header("Dialogue")]
    [SerializeField] GameObject dialogueBox;
    bool dialogueActive;

    [Header("Time")]
    [SerializeField] GameObject clockUI;

    [Header("Level")]
    [SerializeField] GameObject[] sceneSpawnPoints;

    public bool WindowedMode { get { return data.WindowedMode; } set {  data.WindowedMode = value; } }
    public int ResIndex { get { return data.ResIndex; } set { data.ResIndex = value; } }
    public float MasterValue { get { return data.MasterValue; } set { data.MasterValue = value; } }
    public float SFXValue { get { return data.SFXValue; } set { data.SFXValue = value; } }
    public float MusicValue { get { return data.MusicValue;  } set { data.MusicValue = value; } }
    public int GoldCount { get { return data.GoldCount; } set { data.GoldCount = value; } }
    public List<Item> InventoryItems { get { return data.PlayerItems; } set { data.PlayerItems = value; } }
    public List<int> InventoryItemSlots { get { return data.InventoryItemSlots; } set { data.InventoryItemSlots = value; } }
    public int Hour { get { return data.Hour; } set { data.Hour = value; } }
    public int Minute { get { return data.Minute; } set { data.Minute = value; } }

    private void Awake()
    {
#if UNITY_EDITOR
        InventoryItems.Clear();
        InventoryItemSlots.Clear();
#endif
        if (instance == null)
            instance = this;

        player = GameObject.FindWithTag("Player");
        if (player != null)
            playerScript = player.GetComponent<PlayerCharacter>();
    }

    private void Update()
    {
        if (inMainMenu == false && Input.GetKeyDown(KeyCode.Escape))
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

    public void ResetInventory(List<InventoryItem> items)
    {
        InventoryItemSlots.Clear();
        InventoryItems.Clear();

        for (int i = 0; i < InventoryItems.Count; i++)
        {
            InventoryItemSlots.Add(i);
        }
    }

    void OpenInventory()
    {
        invOpen = true;

        Instantiate(inventory, this.transform);

        foreach (InventorySlot slot in InventoryManager.instance.inventorySlots)
        {
            if (slot.myItem !=  null)
            {
                Destroy(slot.myItem.gameObject);
                slot.myItem = null;
            }
        }
        for (int i = 0; i < InventoryItems.Count; i++)
        {
            InventoryManager.instance.LoadIntoInventory(InventoryItems[i], InventoryItemSlots[i]);
        }
    }

    void CloseInventory()
    {
        invOpen = false;
        InventoryManager.instance.settingsScreen.SetActive(false);
        InventoryItemSlots.Clear();
        InventoryItemSlots = InventoryManager.instance.SaveItemSlotFromInventory();
        InventoryItems = InventoryManager.instance.SaveItemFromInventory();
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
