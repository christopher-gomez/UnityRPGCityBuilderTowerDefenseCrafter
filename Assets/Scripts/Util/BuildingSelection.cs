using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingSelection : MonoBehaviour
{
	private GameObject building;
	private GameManager gm;
	private HUDManager hm;
	void Start()
	{
		gm = FindObjectOfType<GameManager>();
		hm = FindObjectOfType<HUDManager>();
	}
	public void Init(GameObject building)
	{
		this.building = building;
		GetComponentInChildren<Text>().text = building.GetComponentInChildren<Building>().buildingName;
	}

	public void select()
	{
		gm.InitBuild(building);
		hm.CloseSelectionMenu();
	}
}
