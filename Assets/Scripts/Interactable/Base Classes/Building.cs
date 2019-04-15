using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : WorldObject
{
	public enum ResourceType
	{
		Food, Stone, Wood, Gold, Water
	}
	public string buildingName;

	public override void OnLeftClick()
	{
		base.OnLeftClick();
	}

	public override void OnRightClick()
	{
		base.OnRightClick();
	}
}
