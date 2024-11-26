using UnityEngine;

public class VoxelCell : MonoBehaviour
{
    public VoxelCoordinates coordinates;

    public Color color;

    public VoxelTypeId VoxelType = VoxelTypeId.AIR;

    public bool solid;

    public VoxelChunk chunk;

    [SerializeField]
    VoxelCell[] neighbors;

    public VoxelCell GetNeighbor(VoxelDirections direction)
    {
        return neighbors[(int)direction];
    }

    public void SetNeighbor(VoxelDirections direction, VoxelCell cell)
    {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }

    public void Refresh()
    {
        chunk.Refresh();
    }

    public void EditCell()
    {
        solid = false;
        chunk.Refresh();
        for (int i = 0; i < neighbors.Length; i++)
        {
            VoxelCell neighbor = neighbors[i];
            if (neighbor != null && neighbor.chunk != chunk)
            {
                neighbor.chunk.Refresh();
            }
        }
    }

    public void SetType(VoxelType type)
    {
        VoxelType = type.ID;
        solid = type.Solid;
        color = type.VoxelColor;
    }
}
