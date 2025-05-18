using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]

public class GameManagerData : ScriptableObject
{
	[SerializeField] bool settingsInitailized;

	[SerializeField] bool windowedMode;
	[SerializeField] int resIndex;
	[SerializeField] float masterValue;
	[SerializeField] float sfxValue;
	[SerializeField] float musicValue;


	[SerializeField] int goldCount;
	[SerializeField] List<Item> playerItems;
	[SerializeField] List<int> inventoryItemSlots;
	[SerializeField] int currentStamina;
	[SerializeField] int maxStamina;
	[SerializeField] int hour;
	[SerializeField] int minute;

    void Start()
    {
		settingsInitailized = false;
    }

    public bool SettingsInitialized
	{
		get { return settingsInitailized; }
		set { settingsInitailized = value; }
	}

	public bool WindowedMode
	{
		get { return windowedMode; }
		set { windowedMode = value; }
	}

	public int ResIndex
	{
		get { return resIndex; }
		set { resIndex = value; }
	}

	public float MasterValue
	{
		get { return masterValue; }
		set {  masterValue = value; }
	}

	public float SFXValue
	{
		get { return sfxValue; }
		set { sfxValue = value; }
	}

	public float MusicValue
	{
		get { return musicValue; }
		set { musicValue = value; }
	}

	public int GoldCount
	{
		get { return goldCount; }
		set { goldCount = value; }
	}

	public List<Item> PlayerItems
	{
		get { return playerItems; }
		set { playerItems = value; }
	}

	public List<int> InventoryItemSlots
	{
		get { return inventoryItemSlots; }
		set { inventoryItemSlots = value; }
	}

	public int Hour
	{
		get { return hour; }
		set { hour = value; }
	}

	public int Minute
	{
		get { return minute; }
		set { minute = value; }
	}

	public int CurrentStamina
	{
		get { return currentStamina; }
		set { currentStamina = value; }
	}

	public int MaxStamina
	{
		get { return maxStamina; }
		set { maxStamina = value; }
	}
}
