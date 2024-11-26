using UnityEngine;
using UnityEngine.UI;

public class VoxelChunk : MonoBehaviour
{
	VoxelCell[] cells;

	VoxelMesh voxelMesh;

	void Awake()
	{
		voxelMesh = GetComponentInChildren<VoxelMesh>();

		cells = new VoxelCell[VoxelMetrics.chunkSizeX * VoxelMetrics.chunkSizeZ * VoxelMetrics.chunkSizeY];
	}

	public void AddCell(int index, VoxelCell cell)
	{
		cells[index] = cell;
		cell.chunk = this;
		cell.transform.SetParent(transform, false);
	}

	public void Refresh()
    {
		enabled = true;
    }

	void LateUpdate()
    {
		voxelMesh.Triangulate(cells);
		enabled = false;
    }
}