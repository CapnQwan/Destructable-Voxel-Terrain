public enum VoxelDirections
{
	pX, pY, pZ, nX, nY, nZ
}

public static class VoxelDirectionExtensions
{

	public static VoxelDirections Opposite(this VoxelDirections direction)
	{
		return (int)direction < 3 ? (direction + 3) : (direction - 3);
	}
}