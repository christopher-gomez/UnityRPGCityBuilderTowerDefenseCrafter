using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : GBuilding
{
	void Awake()
	{
		this.type = ResourceType.Food;
		this.ObjectName = "Building";
		this.buildingName = "Farm";
	}
	// Start is called before the first frame update
	public override void Start()
    {
		base.Start();
	}

    // Update is called once per frame
    public override void Update()
    {
		base.Update();
	}

}
