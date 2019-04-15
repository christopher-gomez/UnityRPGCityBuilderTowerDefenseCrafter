using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	//WorldObject[] slots;

	List<WorldObject> slots;

	//float totalWeight = 0.0f;
	//int gold = 0;

	private Player player;

	public void Start()
	{
		player = GetComponent<Player>();
		slots = new List<WorldObject>();
	}

	public void AddItem(WorldObject obj)
	{
		WorldObject worldObj = obj.GetComponent<WorldObject>();

		Pickupable item = worldObj.GetComponent<Pickupable>();

		for (int i = 0; i + 1 <= slots.Count; i++)
		{

			if (slots[i].ObjectName == obj.ObjectName)
			{

				slots[i].GetComponent<Pickupable>().IncrementStacks(item.GetStacks());
				Destroy(obj);
				break;
			}
			if (slots[i] == null)
			{
				slots[i] = obj;
				//totalWeight += item.weight;
				obj.transform.parent = transform;
				obj.gameObject.SetActive(false);

				//var abilityArray = new Array(player.GetComponent(AbilityCaster).abilities);
				//abilityArray.Add(item.ability);
				//player.GetComponent(AbilityCaster).abilities = abilityArray;
				break;
			}
			else if (i + 1 >= slots.Count)
			{
				Debug.Log("Your inventory is full. Drop something to open a slot.");
				break;
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
