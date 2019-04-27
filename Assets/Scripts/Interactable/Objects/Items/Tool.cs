using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : WorldObject
{
    public enum ToolType {
        Axe, Hoe, Gun
    }

	public ToolType toolType;

	public int damage;

	public override void Start()
    {
		base.Start();
	}

    public override void Update()
    {
		base.Update();
	}
}
