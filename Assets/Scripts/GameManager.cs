using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private HUDManager hudManager;

	[SerializeField]
	private GameObject land;
	public GameObject playerCharacter;

	private Player player;
	private GridV4 grid;
	[SerializeField]
	public GameObject[] buildings;
	
	private int population;
	private float numFood;
	private float numStone;
	private float numWood;
	private float numGold;
	
	void Awake()
	{
		grid = land.GetComponent<GridV4>();
		player = playerCharacter.GetComponentInChildren<Player>();
	}

	// Update is called once per frame
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			if (PlacingObject())
			{
				CancelBuild();
				return;
			}
			hudManager.ToggleBuildMode();
		}
	}

	public void Spawn()
	{
	}

	public bool CheckLocationEmpty(Vector3 pt)
	{
		return grid.CheckCellEmpty(pt);
	}

	public Vector3 MousePosition()
	{
		return grid.GetMouseTileCoordinate();
	}

	public Vector3 PlayerPosition()
	{
		return player.gameObject.transform.position;
	}

	public bool IsPlayerNextToTile(Vector3 pos)
	{
		return player.NextToTile(pos);
	}

	public void IncrementGeneratedResource(Building.ResourceType type, float amt)
	{
		switch (type)
		{
			case Building.ResourceType.Food:
				numFood += amt;
				hudManager.UpdateFood(numFood.ToString());
				break;
			case Building.ResourceType.Wood:
				numWood += amt;
				break;
			case Building.ResourceType.Stone:
				numStone += amt;
				break;
			case Building.ResourceType.Gold:
				numGold += amt;
				break;
		}
	}

	public void IncrementResource(Item.PickupType type, float amt)
	{
		switch(type)
		{
			case Item.PickupType.Wood:
				numWood += amt;
				break;
			case Item.PickupType.Gold:
				numGold += amt;
				break;
		}
	}

	public float GetWood()
	{
		return this.numWood;
	}

	public float GetFood()
	{
		return this.numFood;
	}

	public float GetGold()
	{
		return this.numGold;
	}

	public float GetStone()
	{
		return this.numStone;
	}
	public void InitBuild(GameObject obj)
	{
		Camera.main.fieldOfView = 25f;
		grid.InitCells(obj);
	}

	public void CancelBuild()
	{
		Camera.main.fieldOfView = 10f;
		grid.DestroyCells();
	}

	public void DeregisterGridObject(GameObject obj)
	{
		grid.RemoveObjectFromLocations(obj.transform.position, obj);
	}

	public bool PlacingObject()
	{
		return grid.IsPlacingBuilding();
	}

}
