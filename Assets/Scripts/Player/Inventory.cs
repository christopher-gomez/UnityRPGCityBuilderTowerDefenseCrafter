using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	//WorldObject[] slots;

	List<WorldObject> slots;
	GameObject inventoryObj;

	[SerializeField]
	private int maxSlots = 10;

	//float totalWeight = 0.0f;
	//int gold = 0;

	private GameManager gm;

	public void Awake()
	{
		slots = new List<WorldObject>();
		slots.Capacity = maxSlots;
		inventoryObj = new GameObject();
		inventoryObj.name = "Inventory";
		inventoryObj.transform.parent = transform;
	}

	public void Start()
	{
		gm = FindObjectOfType<GameManager>();
	}

	public void AddItem(Collider col)
	{
		WorldObject worldObj = col.gameObject.GetComponent<WorldObject>();

		Pickupable item = worldObj.GetComponent<Pickupable>();

		if(item.GetPickupType() == Item.PickupType.Health)
		{
			// Increment player health
			return;
		}

		if (slots.Count == 0)
		{
			slots.Add(worldObj);
			worldObj.transform.parent.transform.parent = inventoryObj.transform;
			worldObj.transform.parent.gameObject.SetActive(false);
			gm.IncrementResource(item.GetPickupType(), item.GetStacks());
		}
		else if (slots.Count == maxSlots)
		{
			Debug.Log("Your inventory is full. Drop something to open a slot.");
			return;
		}
		else
		{
			bool inInv = false;
			for (int i = 0; i < slots.Count; i++)
			{
				if (slots[i].ObjectName == worldObj.ObjectName)
				{
					inInv = true;
					slots[i].GetComponent<Pickupable>().IncrementStacks(item.GetStacks());
					gm.IncrementResource(item.GetPickupType(), item.GetStacks());
					Destroy(col.gameObject.transform.parent.gameObject);
					break;
				}
			}
			if (inInv == false)
			{
				slots.Add(worldObj);
				gm.IncrementResource(item.GetPickupType(), item.GetStacks());
				worldObj.transform.parent.transform.parent = inventoryObj.transform;
				worldObj.transform.parent.gameObject.SetActive(false);
			}
		}
	}

	public void DropItem(WorldObject obj)
	{
		/*foreach (WorldObject i in slots)
		{
			if (i.ObjectName == obj.ObjectName)
			{
				var itemScript = i.GetComponent<Pickupable>();
				var icon = itemScript.GetIcon();
				var trans = player.transform;
				var pos = trans.position;

				// i.transform.parent = null;
				i.transform.position = pos + trans.TransformDirection(new Vector3(0, 0.5f, 0.5f));

				i.gameObject.SetActive(true);

				//totalWeight -= itemScript.weight;
				if (icon)
				{
					Destroy(icon);
				}
				i.gameObject.SetActive(true);
				i = null;
			}
		}*/
	}

	//function MoveItem(from : int, to : int)
	//{
	/*if (slots[from])
	{
		if (slots[to])
		{
			var switchedSlot : GameObject = slots[to];
			slots[to] = slots[from];
			slots[from] = switchedSlot;
		}
		else
		{
			slots[to] = slots[from];
			slots[from] = null;
		}
	}
	else
	{
		Debug.LogWarning("Attempting to move a missing item; there is no item at slot: " + from);

	}*/
	//}
}
