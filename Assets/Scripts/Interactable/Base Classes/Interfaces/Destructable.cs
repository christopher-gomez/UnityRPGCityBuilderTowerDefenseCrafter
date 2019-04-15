﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Destructable
{
	int GetHealth();
	void TakeDamage(int dam);
	void OnRightClick();
}
