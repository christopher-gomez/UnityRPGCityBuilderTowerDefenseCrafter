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
	[SerializeField]
	private GameObject[] trees;
	[SerializeField]
	private int population;
	[SerializeField]
	private float numFood;
	[SerializeField]
	private float numStone;
	[SerializeField]
	private float numWood;
	[SerializeField]
	private float numGold;



	void Awake()
	{
		grid = land.GetComponent<GridV4>();
		player = playerCharacter.GetComponentInChildren<Player>();
	}

	// Update is called once per frame
	void Update()
	{

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

	public void IncrementResource(Building.ResourceType type, float amt)
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

	public void MovePlayer(Vector3 pos)
	{
		player.Move(pos);
	}
	public void InitBuild(GameObject obj)
	{
		grid.InitCells(obj);
	}

	public void CancelBuild()
	{
		grid.DestroyCells();
	}

	public void DeregisterObject(GameObject obj)
	{
		grid.RemoveObjectFromLocations(obj.transform.position);
		grid.RemoveObjectFromScene(obj);
	}

}
