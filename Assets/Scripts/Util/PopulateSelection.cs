using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateSelection : MonoBehaviour
{
	public GameObject buttonPrefab;

	private GameObject[] btns;

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
	}

    public void Depopulate()
    {
        foreach(GameObject btn in btns)
        {
			Destroy(btn.gameObject);
		}
    }
}
