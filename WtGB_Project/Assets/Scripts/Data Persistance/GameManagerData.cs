using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class GameManagerData : ScriptableObject
{
	[SerializeField] int goldCount;
	[SerializeField] List<Item> playerItems;
	[SerializeField] List<int> inventoryItemSlots;
	[SerializeField] int currentStamina;
	[SerializeField] int maxStamina;
	[SerializeField] int hour;
	[SerializeField] int minute;

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
