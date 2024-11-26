using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VoxelMesh : MonoBehaviour
{
    Mesh voxelMesh;
	MeshCollider meshCollider;

    List<Vector3> vertices;
    List<int> triangles;
	List<Color> colors;

	void Awake()
	{
		GetComponent<MeshFilter>().mesh = voxelMesh = new Mesh();
		meshCollider = gameObject.AddComponent<MeshCollider>();
		voxelMesh.name = "Voxel Mesh";
		vertices = new List<Vector3>();
		triangles = new List<int>();
		colors = new List<Color>();
	}

	public void Triangulate(VoxelCell[] cells)
	{
		voxelMesh.Clear();
		vertices.Clear();
		triangles.Clear();
		colors.Clear();
		for (int i = 0; i < cells.Length; i++)
		{
			if (cells[i].solid)
            {
				Triangulate(cells[i]);
            }
		}
		voxelMesh.vertices = vertices.ToArray();
		voxelMesh.triangles = triangles.ToArray();
		voxelMesh.colors = colors.ToArray();
		voxelMesh.RecalculateNormals();

		meshCollider.sharedMesh = voxelMesh;
	}

	void Triangulate(VoxelCell cell)
	{
		Vector3 center = cell.transform.localPosition;

		if (!cell.GetNeighbor(VoxelDirections.pZ))
        {
			AddVerticies(
				center + VoxelMetrics.voxelFront[0],
				center + VoxelMetrics.voxelFront[1],
				center + VoxelMetrics.voxelFront[2],
				center + VoxelMetrics.voxelFront[3],
				cell.color
				);
        }
		else
        {
			if  (!cell.GetNeighbor(VoxelDirections.pZ).solid)
            {
				AddVerticies(
				center + VoxelMetrics.voxelFront[0],
				center + VoxelMetrics.voxelFront[1],
				center + VoxelMetrics.voxelFront[2],
				center + VoxelMetrics.voxelFront[3],
				cell.color
				);
			}
        }

		if (!cell.GetNeighbor(VoxelDirections.nZ))
		{
			AddVerticies(
				center + VoxelMetrics.voxelBack[0],
				center + VoxelMetrics.voxelBack[1],
				center + VoxelMetrics.voxelBack[2],
				center + VoxelMetrics.voxelBack[3],
				cell.color
				);
		}
		else
        {
			if (!cell.GetNeighbor(VoxelDirections.nZ).solid)
			{
				AddVerticies(
				center + VoxelMetrics.voxelBack[0],
				center + VoxelMetrics.voxelBack[1],
				center + VoxelMetrics.voxelBack[2],
				center + VoxelMetrics.voxelBack[3],
				cell.color
				);
			}
		}


        if (!cell.GetNeighbor(VoxelDirections.pX))
        {
            AddVerticies(
                center + VoxelMetrics.voxelRight[0],
                center + VoxelMetrics.voxelRight[1],
                center + VoxelMetrics.voxelRight[2],
                center + VoxelMetrics.voxelRight[3],
				cell.color
				);
        }
        else
        {
            if (!cell.GetNeighbor(VoxelDirections.pX).solid)
            {
                AddVerticies(
                center + VoxelMetrics.voxelRight[0],
                center + VoxelMetrics.voxelRight[1],
                center + VoxelMetrics.voxelRight[2],
                center + VoxelMetrics.voxelRight[3],
				cell.color
				);
            }
        }

        if (!cell.GetNeighbor(VoxelDirections.nX))
        {
            AddVerticies(
                center + VoxelMetrics.voxelLeft[0],
                center + VoxelMetrics.voxelLeft[1],
                center + VoxelMetrics.voxelLeft[2],
                center + VoxelMetrics.voxelLeft[3],
				cell.color
				);
        }
        else
        {
            if (!cell.GetNeighbor(VoxelDirections.nX).solid)
            {
                AddVerticies(
                center + VoxelMetrics.voxelLeft[0],
                center + VoxelMetrics.voxelLeft[1],
                center + VoxelMetrics.voxelLeft[2],
                center + VoxelMetrics.voxelLeft[3],
				cell.color
				);
            }
        }

        if (!cell.GetNeighbor(VoxelDirections.nY))
		{
			AddVerticies(
				center + VoxelMetrics.voxelTop[0],
				center + VoxelMetrics.voxelTop[1],
				center + VoxelMetrics.voxelTop[2],
				center + VoxelMetrics.voxelTop[3],
				cell.color
				);
		}
		else
		{
			if (!cell.GetNeighbor(VoxelDirections.nY).solid)
			{
				AddVerticies(
				center + VoxelMetrics.voxelTop[0],
				center + VoxelMetrics.voxelTop[1],
				center + VoxelMetrics.voxelTop[2],
				center + VoxelMetrics.voxelTop[3],
				cell.color
				);
			}
		}

		if (!cell.GetNeighbor(VoxelDirections.pY))
		{
			AddVerticies(
				center + VoxelMetrics.voxelBottom[0],
				center + VoxelMetrics.voxelBottom[1],
				center + VoxelMetrics.voxelBottom[2],
				center + VoxelMetrics.voxelBottom[3],
				cell.color
				);
		}
		else
		{
			if (!cell.GetNeighbor(VoxelDirections.pY).solid)
			{
				AddVerticies(
				center + VoxelMetrics.voxelBottom[0],
				center + VoxelMetrics.voxelBottom[1],
				center + VoxelMetrics.voxelBottom[2],
				center + VoxelMetrics.voxelBottom[3],
				cell.color
				);
			}
		}
	}

	void AddVerticies(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Color color)
	{
		int vertexIndex = vertices.Count;

		vertices.Add(v1);
		vertices.Add(v2);
		vertices.Add(v3);
		vertices.Add(v4);
		colors.Add(color);
		colors.Add(color);
		colors.Add(color);
		colors.Add(color);

		AddTriangles(vertexIndex);
	}

	void AddTriangles(int t)
    {
		triangles.Add(t + 0);
		triangles.Add(t + 2);
		triangles.Add(t + 1);
		triangles.Add(t + 1);
		triangles.Add(t + 2);
		triangles.Add(t + 3);
	}
}
