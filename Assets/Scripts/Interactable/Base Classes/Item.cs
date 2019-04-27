using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : WorldObject, Pickupable
{
	public int stacks;
	private GameObject icon;
	[SerializeField]
	private GameObject iconPrefab;

	public enum PickupType {
		Wood, Gold, Health
	}

	public PickupType pickupType;
	public override void Start()
  {
		base.Start();
	}

  // Update is called once per frame
  public override void Update()
  {
		base.Update();
	}

	public PickupType GetPickupType()
	{
		return this.pickupType;
	}

	public GameObject GetIcon()
	{
		return icon;
	}

	public GameObject GetIconPrefab()
	{
		return iconPrefab;
	}

	public int GetStacks()
	{
		return stacks;
	}

	public void SetStacks(int stacks)
	{
		this.stacks = stacks;
	}

	public void IncrementStacks(int stacks)
	{
		this.stacks += stacks;
	}

	public void PickUp()
	{
		Destroy(gameObject.transform.parent.gameObject);
	}
}
