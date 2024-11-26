using UnityEngine;

[System.Serializable]
public struct VoxelCoordinates
{
	[SerializeField]
	private int x, y, z;

	public int X
	{
		get
		{
			return x;
		}
	}

	public int Z
	{
		get
		{
			return z;
		}
	}

	public int Y
	{
		get
		{
			return y;
		}
	}

	public VoxelCoordinates(int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public static VoxelCoordinates FromPosition(Vector3 position)
	{
		float x = (position.x + 1) / 2;
		float y = ((position.y * -1) + 1) / 2;
		float z = (position.z + 1) / 2;

		int iX = Mathf.FloorToInt(x);
		int iY = Mathf.FloorToInt(y);
		int iZ = Mathf.FloorToInt(z);

		return new VoxelCoordinates(iX, iY, iZ);
	}

	public static VoxelCoordinates FromOffsetCoordinates(int x, int y, int z)
	{
		return new VoxelCoordinates(x, y, z);
	}

	public override string ToString()
	{
		return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
	}

	public string ToStringOnSeparateLines()
	{
		return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
	}
}
