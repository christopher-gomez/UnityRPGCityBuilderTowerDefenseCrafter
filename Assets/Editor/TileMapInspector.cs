using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(GridV4))]
public class TileMapInspector : Editor
{
    public override void OnInspectorGUI()
    {
			DrawDefaultInspector();
			if(GUILayout.Button("Regenerate"))
      {
				GridV4 map = target as GridV4;
				map.Generate();
			}
		}
}
