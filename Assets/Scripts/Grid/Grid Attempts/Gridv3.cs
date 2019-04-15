using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Gridv3 : MonoBehaviour
{
	public int GridSize;

	void Awake()
	{
		MeshFilter filter = gameObject.GetComponent<MeshFilter>();
		var mesh = new Mesh();
		var verticies = new List<Vector3>();

		var indicies = new List<int>();
		for (int i = 0; i < GridSize; i++)
		{
			verticies.Add(new Vector3(i, 1.5f, 0));
			verticies.Add(new Vector3(i, 1.5f, GridSize));

			indicies.Add(4 * i + 0);
			indicies.Add(4 * i + 1);

			verticies.Add(new Vector3(0, 1.5f, i));
			verticies.Add(new Vector3(GridSize, 1.5f, i));

			indicies.Add(4 * i + 2);
			indicies.Add(4 * i + 3);
		}

		mesh.vertices = verticies.ToArray();
		mesh.SetIndices(indicies.ToArray(), MeshTopology.Lines, 0);
		filter.mesh = mesh;

		MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
		meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
		meshRenderer.material.color = Color.white;
	}
}
