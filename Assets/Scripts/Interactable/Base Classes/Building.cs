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

	[HideInInspector]
	public bool isValidPosition;

	[SerializeField]
	private Material validPosition;
	[SerializeField]
	private Material invalidPosition;
	[SerializeField]
	public GameObject selectionArea;

	public override void Start()
	{
		base.Start();
		isValidPosition = true;
	}

	public override void OnLeftClick()
	{
		base.OnLeftClick();
	}

	public override void OnRightClick()
	{
		base.OnRightClick();
	}

	public void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.name == "World")
		{
			return;
		}
		Player player = col.GetComponent<Player>();
		if(player != null)
		{
			isValidPosition = false;
			SelectionAreaColor(isValidPosition);
			return;
		}
		WorldObject obj = col.transform.parent.GetComponentInChildren<WorldObject>();
		if (obj == null)
		{
			return;
		}
		if (obj.ObjectName.ToString() != "Grass" && obj.ObjectName.ToString() != "Flower")
		{
			// Debug.Log("Collision with " + obj.ObjectName);
			isValidPosition = false;
			SelectionAreaColor(isValidPosition);
		}
	}

	public void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == "World")
		{
			return;
		}
		Player player = col.GetComponent<Player>();
		if (player != null)
		{
			isValidPosition = true;
			SelectionAreaColor(isValidPosition);
			return;
		}
		WorldObject obj = col.gameObject.transform.parent.GetComponentInChildren<WorldObject>();
		if (obj == null)
		{
			return;
		}
		if (obj.ObjectName.ToString() != "Grass" && obj.ObjectName.ToString() != "Flower")
		{
			// Debug.Log("Collision with " + obj.ObjectName);
			isValidPosition = true;
			SelectionAreaColor(isValidPosition);
		}
	}

	private void SelectionAreaColor(bool isValid)
	{
		if(isValid)
		{
			foreach (Transform t in selectionArea.transform)
			{
				t.GetComponentInChildren<MeshRenderer>().material = validPosition;
			}
		}
		else
		{
			foreach (Transform t in selectionArea.transform)
			{
				t.GetComponentInChildren<MeshRenderer>().material = invalidPosition;
			}
		}
	}
}
