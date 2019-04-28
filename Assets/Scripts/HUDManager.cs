using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
	public GameManager gameManager;
	[SerializeField]
	private Text totalFood, totalStamina;
	[SerializeField]
	private GameObject selectionMenu;
	private PopulateSelection selectionContent;

	void Awake()
	{
		selectionContent = selectionMenu.GetComponentInChildren<PopulateSelection>();
	}

	public void ToggleBuildMode()
	{
		selectionMenu.gameObject.SetActive(!selectionMenu.activeSelf);
		if(selectionMenu.gameObject.activeSelf)
		{
			selectionContent.Populate(gameManager.buildings);
		}
		else
		{
			selectionContent.Depopulate();
		}
	}

	public void CloseSelectionMenu()
	{
		selectionMenu.SetActive(false);
		selectionContent.Depopulate();
	}

	public void UpdateFood(string amt)
	{
		totalFood.text = amt;
	}

	public void UpdateStamina(string amt)
	{
		totalStamina.text = amt;
	}

}
