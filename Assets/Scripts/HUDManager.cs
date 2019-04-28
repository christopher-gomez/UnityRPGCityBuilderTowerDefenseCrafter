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

	public SimpleHealthBar healthBar;

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

	public void UpdateStamina(float current, float max)
	{
		//totalStamina.text = amt;
		healthBar.UpdateBar(current, max);
		if(current <= max / 2)
		{
			if(current <= max /4)
			{
				healthBar.UpdateColor(Color.red);
			}
			else
			{
				healthBar.UpdateColor(Color.yellow);
			}
		}
	}

}
