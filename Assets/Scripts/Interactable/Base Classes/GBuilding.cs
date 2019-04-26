using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GBuilding : Building
{
	public float resourceMultiplier;

	public ResourceType type;
	public float rate = 10f; // Rate at which building generates resources. DEFAULT: 10 seconds
	[HideInInspector]
	public float generatedResource;
	[HideInInspector]
	public float totalGeneratedResources;
	public int level = 1;


	public override void Start()
	{
		base.Start();
		SetResourceMultiplier();
	}

	public void BeginProduction()
	{
		InvokeRepeating("GenerateResource", 1, rate);
	}
	public void GenerateResource()
	{
		generatedResource = CalculateResource();
		totalGeneratedResources += generatedResource;
		//Debug.Log("Generated " + generatedResource + " " + resource.ToString());
		//Debug.Log("Total " + resource.ToString() + " generated: " + totalGeneratedResources);
		gameManager.IncrementResource(type, generatedResource);
	}

	private float CalculateResource()
	{
		return 1 * resourceMultiplier;
	}

	public void SetResourceMultiplier()
	{
		switch (level)
		{
			case 1:
				resourceMultiplier = 1f;
				break;
			case 2:
				resourceMultiplier = 1.5f;
				break;
			case 3:
				resourceMultiplier = 2.0f;
				break;
		}
	}

}