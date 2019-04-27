using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateSelection : MonoBehaviour
{
	public GameObject buttonPrefab;
	private GameObject[] btns;
	[SerializeField]
	private Text totalWood;
	[SerializeField]
	private Text totalStone;
	[SerializeField]
	private Text totalGold;
	public GameManager gm;

	public void Populate(GameObject[] buildings)
	{
		btns = new GameObject[buildings.Length];
		int i = 0;
		foreach(GameObject building in buildings)
		{
			btns[i] = Instantiate(buttonPrefab, transform);
			btns[i].GetComponent<BuildingSelection>().Init(building);
			i++;
		}
		totalWood.text = gm.GetWood().ToString();
		totalGold.text = gm.GetGold().ToString();
		totalStone.text = gm.GetStone().ToString();
	}

    public void Depopulate()
    {
        foreach(GameObject btn in btns)
        {
			Destroy(btn.gameObject);
		}
    }
}
