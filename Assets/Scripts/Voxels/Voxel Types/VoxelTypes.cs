using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VoxelTypeId
{
    AIR,
    GRASS,
    DIRT,
    STONE,
    SAND
}

public struct VoxelType
{
    public VoxelTypeId ID;
    public bool Solid;
    public Color VoxelColor;
}