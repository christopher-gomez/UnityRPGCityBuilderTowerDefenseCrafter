using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Pickupable
{
	void PickUp();
	int GetStacks();
	void SetStacks(int stacks);
	void IncrementStacks(int stacks);
	GameObject GetIconPrefab();
	GameObject GetIcon();
	Item.PickupType GetPickupType();
}
