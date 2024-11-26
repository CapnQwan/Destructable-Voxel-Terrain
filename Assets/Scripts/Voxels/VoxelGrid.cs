using UnityEngine;

public class VoxelGrid : MonoBehaviour
{
	public int chunkCountX = 20, chunkCountZ = 20;

	int cellCountX, cellCountY, cellCountZ;

	public VoxelChunk chunkPrefab;

	public VoxelCell cellPrefab;

	VoxelCell[] cells;
	VoxelChunk[] chunks;
	VoxelType[] VoxelTypes;

	void Awake()
	{
		cellCountX = chunkCountX * VoxelMetrics.chunkSizeX;
		cellCountY = VoxelMetrics.chunkSizeY;
		cellCountZ = chunkCountZ * VoxelMetrics.chunkSizeZ;

		CreatTypes();
		CreateChunks();
		CreateCells();
	}

	void CreateChunks()
	{
		chunks = new VoxelChunk[chunkCountX * chunkCountZ];

		for (int z = 0, i = 0; z < chunkCountZ; z++)
		{
			for (int x = 0; x < chunkCountX; x++)
			{
				VoxelChunk chunk = chunks[i++] = Instantiate(chunkPrefab);
				chunk.transform.SetParent(transform);
			}
		}
	}

	void CreateCells() 
	{
		cells = new VoxelCell[cellCountX * cellCountY * cellCountZ];
		for (int y = 0, i = 0; y < cellCountY; y++)
		{
			for (int z = 0; z < cellCountZ; z++)
			{
				for (int x = 0; x < cellCountX; x++)
				{
					CreateCell(x, y, z, i++);
				}
			}
		}
	}

	void CreateCell(int x, int y, int z, int i)
	{
		Vector3 position;
		position.x = x * 2f;
		position.y = y * 2f * -1f;
		position.z = z * 2f;

		VoxelTypeId type;

		VoxelCell cell = cells[i] = Instantiate<VoxelCell>(cellPrefab);

		if(y < 4)
        {
			type = VoxelTypeId.AIR;
		}
		else if (y < 8)
        {
			type = VoxelTypeId.GRASS;
		}
		else
        {
			type = VoxelTypeId.STONE;
		}

		cell.SetType(VoxelTypes[(int)type]);

		cell.transform.localPosition = position;
		cell.coordinates = VoxelCoordinates.FromOffsetCoordinates(x, y, z);

		if (x > 0)
		{
			cell.SetNeighbor(VoxelDirections.nX, cells[i - 1]);
		}

		if (y > 0)
        {
			cell.SetNeighbor(VoxelDirections.nY, cells[i - (cellCountX * cellCountZ)]);
		}

		if (z > 0)
		{
			cell.SetNeighbor(VoxelDirections.nZ, cells[i - cellCountX]);
		}

		AddCellToChunk(x, y, z, cell);
	}

	void AddCellToChunk(int x, int y, int z, VoxelCell cell)
    {
		int chunkX = x / VoxelMetrics.chunkSizeX;
		int chunkZ = z / VoxelMetrics.chunkSizeZ;
		VoxelChunk chunk = chunks[chunkX + chunkZ * chunkCountX];

		int localX = x - chunkX * VoxelMetrics.chunkSizeX;
		int localZ = z - chunkZ * VoxelMetrics.chunkSizeZ;
		chunk.AddCell(localX + (localZ * VoxelMetrics.chunkSizeX) + (y * VoxelMetrics.chunkSizeX * VoxelMetrics.chunkSizeZ), cell);
	}

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			HandleInput();
		}
	}

	void HandleInput()
	{
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(inputRay, out hit))
		{
			VoxelCell cell = GetCell(hit.point + (inputRay.direction / 100));
			cell.EditCell();
		}
	}

	public VoxelCell GetCell(Vector3 position)
	{
		position = transform.InverseTransformPoint(position);
		VoxelCoordinates coordinates = VoxelCoordinates.FromPosition(position);
		int index = coordinates.X + (coordinates.Z * cellCountX) + (coordinates.Y * cellCountX * cellCountZ);
		return cells[index];
	}

	void CreatTypes()
    {
		VoxelTypes = new VoxelType[System.Enum.GetValues(typeof(VoxelTypeId)).Length];


		VoxelTypes[(int)VoxelTypeId.AIR].ID = VoxelTypeId.AIR;
		VoxelTypes[(int)VoxelTypeId.AIR].Solid = false;
		VoxelTypes[(int)VoxelTypeId.AIR].VoxelColor = Color.white;

		VoxelTypes[(int)VoxelTypeId.GRASS].ID = VoxelTypeId.GRASS;
		VoxelTypes[(int)VoxelTypeId.GRASS].Solid = true;
		VoxelTypes[(int)VoxelTypeId.GRASS].VoxelColor = Color.green;

		VoxelTypes[(int)VoxelTypeId.STONE].ID = VoxelTypeId.STONE;
		VoxelTypes[(int)VoxelTypeId.STONE].Solid = true;
		VoxelTypes[(int)VoxelTypeId.STONE].VoxelColor = Color.grey;

		Debug.Log("Types 0 = " + VoxelTypes[0].ID);
		Debug.Log("Types 1 = " + VoxelTypes[1].ID);
	}
}
