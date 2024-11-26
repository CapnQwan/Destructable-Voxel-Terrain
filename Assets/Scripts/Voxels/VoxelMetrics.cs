using UnityEngine;

public static class VoxelMetrics
{
	public const float faces = 6;

    public const float gridSize = 2f;
	public const float posHalfSize = gridSize / 2;
	public const float negHalfSize = (gridSize / 2) * -1;

	public const int chunkSizeX = 8, chunkSizeY = 20, chunkSizeZ = 8;

	public static Vector3[] voxelBack = {
		new Vector3(negHalfSize, negHalfSize, negHalfSize),
		new Vector3(posHalfSize, negHalfSize, negHalfSize),
		new Vector3(negHalfSize, posHalfSize, negHalfSize),
		new Vector3(posHalfSize, posHalfSize, negHalfSize)
	};

	public static Vector3[] voxelFront = {
		new Vector3(negHalfSize, posHalfSize, posHalfSize),
		new Vector3(posHalfSize, posHalfSize, posHalfSize),
		new Vector3(negHalfSize, negHalfSize, posHalfSize),
		new Vector3(posHalfSize, negHalfSize, posHalfSize)
	};

	public static Vector3[] voxelLeft = {
		new Vector3(negHalfSize, negHalfSize, negHalfSize),
		new Vector3(negHalfSize, posHalfSize, negHalfSize),
		new Vector3(negHalfSize, negHalfSize, posHalfSize),
		new Vector3(negHalfSize, posHalfSize, posHalfSize)
	};

	public static Vector3[] voxelRight = {
		new Vector3(posHalfSize, posHalfSize, posHalfSize),
		new Vector3(posHalfSize, posHalfSize, negHalfSize),
		new Vector3(posHalfSize, negHalfSize, posHalfSize),
		new Vector3(posHalfSize, negHalfSize, negHalfSize)
	};

	public static Vector3[] voxelTop = {
		new Vector3(negHalfSize, posHalfSize, negHalfSize),
		new Vector3(posHalfSize, posHalfSize, negHalfSize),
		new Vector3(negHalfSize, posHalfSize, posHalfSize),
		new Vector3(posHalfSize, posHalfSize, posHalfSize)
	};

	public static Vector3[] voxelBottom = {
		new Vector3(posHalfSize, negHalfSize, posHalfSize),
		new Vector3(posHalfSize, negHalfSize, negHalfSize),
		new Vector3(negHalfSize, negHalfSize, posHalfSize),
		new Vector3(negHalfSize, negHalfSize, negHalfSize)
	};
}
