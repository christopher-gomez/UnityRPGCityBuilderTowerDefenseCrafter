using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldObject : MonoBehaviour, Interactable
{
	public enum ObjectType
	{
		NaturalResource, GeneratedResource, Consumable, Weapon, Armor, Building, Animal, Enemy, NPC
	}
	public string ObjectName;
	protected GameManager gameManager;

	[SerializeField]
	protected ObjectType objType;

	public ObjectType GetObjectType()
	{
		return objType;
	}

	public string description;

	protected Collider col;
	// Start is called before the first frame update

	public void OnBecameVisible()
	{
		gameObject.SetActive(true);
	}

	public void OnBecameInvisible()
	{
		gameObject.SetActive(false);
	}
	public virtual void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
		col = GetComponentInChildren<Collider>();
	}

	public virtual void OnLeftClick()
	{
		Debug.Log("Left click on: " + ObjectName);
	}

	public virtual void OnRightClick()
	{
		Debug.Log("Right click on " + ObjectName);
	}

	public bool NoOverlap()
	{
		Collider[] hits = Physics.OverlapBox(transform.position, new Vector3(1, 0, 1));
		if (hits.Length > 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void CheckClick()
	{

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;

			// Cursor on the map
			if (col.Raycast(ray, out hitInfo, Mathf.Infinity))
			{
				OnLeftClick();
			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;

			// Cursor on the map
			if (col.Raycast(ray, out hitInfo, Mathf.Infinity))
			{
				OnRightClick();
			}
		}
	}

	// Update is called once per frame
	public virtual void Update()
	{
		CheckClick();
	}
}
