using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralWorld : MonoBehaviour
{

	public int gridSize;
	public float radius = 1;

	private Mesh mesh;
	private Vector3[] vertices;
	private Vector3[] normals;
	private Color32[] cubeUV;

	private void Awake()
	{
		Generate();
	}

	private void Generate()
	{
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Sphere";
		//WaitForSeconds wait = new WaitForSeconds(0.05f);

		UpdateMesh(0);
		//CreateVertices();
		//CreateTriangles();

		return;
	}

	public void UpdateMesh(int activeFace)
	{
		mesh.Clear();
		//vertices.Clear();
		//normals.Clear();
		//triangles.Clear();
		CreateFaces(activeFace);
	}

	public void CreateFaces(int activeFace)
    {
		vertices = new Vector3[(((gridSize + 1) * (gridSize + 1)) + (gridSize * gridSize + gridSize + gridSize))];
		normals = new Vector3[vertices.Length];
		cubeUV = new Color32[vertices.Length];

		int v = 0;
		int t = 0;

		int[] trianglesX = new int[(gridSize * gridSize) * 6];
		int[] trianglesY = new int[(gridSize * gridSize) * 6];
		int[] trianglesZ = new int[(gridSize * gridSize) * 6];

		if (activeFace == 0)
		{
			v = CreateXVertices(0, v);
			v = CreateXRingVertices(0, gridSize / 2, v);

			mesh.vertices = vertices;
			mesh.normals = normals;
			mesh.colors32 = cubeUV;

			t = CreateXTriangles(trianglesX, gridSize, t);
			CreateXRingTriangles(trianglesY, trianglesZ, gridSize, 0);

			mesh.subMeshCount = 3;
			mesh.SetTriangles(trianglesX, 0);
			mesh.SetTriangles(trianglesY, 1);
			mesh.SetTriangles(trianglesZ, 2);
		}
		else if (activeFace == 4)
		{
			v = CreateXVertices(gridSize, v);
			v = CreateXRingVertices(gridSize, gridSize / 2, v);

			mesh.vertices = vertices;
			mesh.normals = normals;
			mesh.colors32 = cubeUV;

			t = CreateXTriangles(trianglesX, gridSize, t);
			CreateXRingTriangles(trianglesY, trianglesZ, gridSize, gridSize);

			mesh.subMeshCount = 3;
			mesh.SetTriangles(trianglesX, 0);
			mesh.SetTriangles(trianglesY, 1);
			mesh.SetTriangles(trianglesZ, 2);
		}
		else if (activeFace == 1)
		{
			v = CreateYVertices(gridSize, v);
			v = CreateYRingVertices(gridSize, gridSize / 2, v);

			mesh.vertices = vertices;
			mesh.normals = normals;
			mesh.colors32 = cubeUV;

			t = CreateYTriangles(trianglesY, gridSize, t);
			CreateYRingTriangles(trianglesX, trianglesZ, gridSize, gridSize);

			mesh.subMeshCount = 3;
			mesh.SetTriangles(trianglesX, 0);
			mesh.SetTriangles(trianglesY, 1);
			mesh.SetTriangles(trianglesZ, 2);
		}
		else if (activeFace == 5)
        {
			v = CreateYVertices(0, v);
			v = CreateYRingVertices(0, gridSize / 2, v);

			mesh.vertices = vertices;
			mesh.normals = normals;
			mesh.colors32 = cubeUV;

			t = CreateYTriangles(trianglesY, gridSize, t);
			CreateYRingTriangles(trianglesY, trianglesZ, gridSize, 0);

			mesh.subMeshCount = 3;
			mesh.SetTriangles(trianglesX, 0);
			mesh.SetTriangles(trianglesY, 1);
			mesh.SetTriangles(trianglesZ, 2);
		}
		else if (activeFace == 2)
        {
			v = CreateZVertices(0, v);
			v = CreateZRingVertices(0, gridSize / 2, v);

			mesh.vertices = vertices;
			mesh.normals = normals;
			mesh.colors32 = cubeUV;

			t = CreateZTriangles(trianglesZ, gridSize, t);
			CreateZRingTriangles(trianglesY, trianglesZ, gridSize, 0);

			mesh.subMeshCount = 3;
			mesh.SetTriangles(trianglesX, 0);
			mesh.SetTriangles(trianglesY, 1);
			mesh.SetTriangles(trianglesZ, 2);
		}
		else
        {
			v = CreateZVertices(gridSize, v);
			v = CreateZRingVertices(gridSize, gridSize / 2, v);

			mesh.vertices = vertices;
			mesh.normals = normals;
			mesh.colors32 = cubeUV;

			t = CreateZTriangles(trianglesZ, gridSize, t);
			CreateZRingTriangles(trianglesY, trianglesZ, gridSize, gridSize);

			mesh.subMeshCount = 3;
			mesh.SetTriangles(trianglesX, 0);
			mesh.SetTriangles(trianglesY, 1);
			mesh.SetTriangles(trianglesZ, 2);
		}
		//CreateFaceVertices(activeFace, 2);
		//CreateTriangles();
	}

	private int CreateXVertices(int offset, int v)
	{
		for (int y = 0; y <= gridSize; y++)
		{
			for (int x = 0; x <= gridSize; x++)
			{
				if (offset == 0)
                {
					SetVertex(v++, x, y, offset);
                }
				else
                {
					SetVertex(v++, x, gridSize - y, offset);
				}
			}
		}
		return v;
	}

	private int CreateYVertices(int offset, int v)
	{
		for (int z = 0; z <= gridSize; z++)
		{
			for (int y = 0; y <= gridSize; y++)
			{
				if (offset == 0)
				{
					SetVertex(v++, offset, y, z);
				}
				else
				{
					SetVertex(v++, offset, y, gridSize - z);
				}
			}
		}
		return v;
	}

	private int CreateZVertices(int offset, int v)
	{
		for (int x = 0; x <= gridSize; x++)
		{
			for (int z = 0; z <= gridSize; z++)
			{
				if (offset == 0)
				{
					SetVertex(v++, x, offset, z);
				}
				else
				{
					SetVertex(v++, gridSize - x, offset, z);
				}
			}
		}
		return v;
	}

	private int CreateXRingVertices(int offset, int gridSize, int v)
	{
		for (int z = 1; z <= gridSize; z++)
        {
            for (int x = 0; x <= gridSize; x++)
            {
                SetVertex(v++, x * 2, 0, z * 2);
            }
            for (int y = 1; y <= gridSize; y++)
            {
                SetVertex(v++, gridSize * 2, y * 2, z * 2);
            }
            for (int x = gridSize -1; x >= 0; x--)
            {
                SetVertex(v++, x * 2, gridSize * 2, z * 2);
            }
            for (int y = gridSize - 1; y > 0; y--)
			{
				SetVertex(v++, 0, y * 2, z * 2);
			}
		}
		return v;
	}

	private int CreateYRingVertices(int offset, int gridSize, int v)
	{
		return v;
	}

	private int CreateZRingVertices(int offset, int gridSize, int v)
	{
		return v;
	}



	private int CreateXTriangles(int[] triangles, int gridSize, int t)
	{
		for (int y = 0; y < gridSize; y++)
		{
			for (int x = 0; x < gridSize; x++)
			{
				triangles[t] = x + (y * (gridSize + 1));
				triangles[t + 1] = triangles[t + 4] = x + ((y + 1) * (gridSize + 1));
				triangles[t + 2] = triangles[t + 3] = x + (y * (gridSize + 1)) + 1;
				triangles[t + 5] = x + ((y + 1) * (gridSize + 1)) + 1;
				t = t + 6;
			}
		}
		return t;
	}

	private int CreateYTriangles(int[] triangles, int gridSize, int t)
	{
		for (int z = 0; z < gridSize; z++)
		{
			for (int y = 0; y < gridSize; y++)
			{
				triangles[t] = y + (z * (gridSize + 1));
				triangles[t + 1] = triangles[t + 4] = y + ((z + 1) * (gridSize + 1));
				triangles[t + 2] = triangles[t + 3] = y + (z * (gridSize + 1)) + 1;
				triangles[t + 5] = y + ((z + 1) * (gridSize + 1)) + 1;
				t = t + 6;
			}
		}
		return t;
	}

	private int CreateZTriangles(int[] triangles, int gridSize, int t)
	{
		for (int x = 0; x < gridSize; x++)
		{
			for (int z = 0; z < gridSize; z++)
			{
				triangles[t] = z + (x * (gridSize + 1));
				triangles[t + 1] = triangles[t + 4] = z + ((x + 1) * (gridSize + 1));
				triangles[t + 2] = triangles[t + 3] = z + (x * (gridSize + 1)) + 1;
				triangles[t + 5] = z + ((x + 1) * (gridSize + 1)) + 1;
				t = t + 6;
			}
		}
		return t;
	}

	private void CreateXRingTriangles(int[] yTriangles, int[] zTriangles, int gridSize, int offset)
	{
		int yT = 0, zT = 0;

		int baseIndex = gridSize * gridSize + gridSize + gridSize + 1;
		int ringIndex = gridSize * 2;

		if (offset == 0)
		{
			for (int n = 0; n < gridSize; n++, n++)
			{
				zTriangles[zT] = n;
				zTriangles[zT + 1] = zTriangles[zT + 4] = zTriangles[zT + 7] = n + 1;
				zTriangles[zT + 2] = zTriangles[zT + 3] = baseIndex + (n / 2);
				zTriangles[zT + 5] = zTriangles[zT + 6] = baseIndex + (n / 2) + 1;
				zTriangles[zT + 8] = n + 2;
				zT = zT + 9;
			}
			for (int n = 0; n < gridSize; n++, n++)
			{
				yTriangles[yT] = gridSize + (n * (gridSize + 1));
				yTriangles[yT + 1] = yTriangles[yT + 4] = yTriangles[yT + 7] = gridSize + ((n + 1) * (gridSize + 1));
				yTriangles[yT + 2] = yTriangles[yT + 3] = baseIndex + (gridSize / 2) + (n / 2);
				yTriangles[yT + 5] = yTriangles[yT + 6] = baseIndex + (gridSize / 2) + (n / 2) + 1;
				yTriangles[yT + 8] = gridSize + ((n + 2) * (gridSize + 1));
				yT = yT + 9;
			}
			for (int n = 0; n < gridSize; n++, n++)
			{
				zTriangles[zT] = (gridSize * gridSize) + gridSize + n;//
				zTriangles[zT + 1] = zTriangles[zT + 4] = baseIndex + gridSize + (gridSize / 2) - (n / 2);
				zTriangles[zT + 2] = zTriangles[zT + 3] = zTriangles[zT + 8] = (gridSize * gridSize) + gridSize + (n + 1);//
				zTriangles[zT + 5] = zTriangles[zT + 6] = baseIndex + gridSize + (gridSize / 2) - (n / 2) - 1;
				zTriangles[zT + 7] = (gridSize * gridSize) + gridSize + (n + 2);//
				zT = zT + 9;
			}
			for (int n = 2; n < gridSize; n++, n++)
			{
				yTriangles[yT] = (n * (gridSize + 1));
				yTriangles[yT + 1] = yTriangles[yT + 4] = yTriangles[yT + 7] = baseIndex + gridSize + gridSize - (n / 2);
				yTriangles[yT + 2] = yTriangles[yT + 3] = ((n + 1) * (gridSize + 1));
				yTriangles[yT + 5] = yTriangles[yT + 6] = ((n + 2) * (gridSize + 1));
				yTriangles[yT + 8] = baseIndex + gridSize + gridSize - (n / 2) - 1;
				yT = yT + 9;
			}

			//yTriangles[yT] = gridSize + 1;
			yTriangles[yT + 1] = yTriangles[yT + 4] = yTriangles[yT + 7] = baseIndex;
			yTriangles[yT + 2] = yTriangles[yT + 3] = (1 * (gridSize + 1));
			yTriangles[yT + 5] = yTriangles[yT + 6] = (2 * (gridSize + 1));
			yTriangles[yT + 8] = baseIndex + gridSize + gridSize - 1;
			yT = yT + 9;
		}

		for (int x = 0; x < (gridSize / 2) - 1; x++)
        {
			for (int z = 0; z < gridSize / 2; z++)
            {
				zTriangles[zT] = baseIndex + (ringIndex * (x + 1)) + z;
				zTriangles[zT + 1] = zTriangles[zT + 4] = baseIndex + (ringIndex * x) + z;
				zTriangles[zT + 2] = zTriangles[zT + 3] = baseIndex + (ringIndex * (x + 1)) + 1 + z;
				zTriangles[zT + 5] = baseIndex + (ringIndex * x) + 1 + z;
				zT = zT + 6;
			}

			for (int y = 0; y < gridSize / 2; y++)
			{
				yTriangles[yT] = baseIndex + (ringIndex * (x + 1)) + (gridSize / 2) + y;
				yTriangles[yT + 1] = yTriangles[yT + 4] = baseIndex + (ringIndex * x) + (gridSize / 2) + y;
				yTriangles[yT + 2] = yTriangles[yT + 3] = baseIndex + (ringIndex * (x + 1)) + (gridSize / 2) + 1 + y;
				yTriangles[yT + 5] = baseIndex + (ringIndex * x) + (gridSize / 2) + 1 + y;
				yT = yT + 6;
			}

			for (int z = 0; z < gridSize / 2; z++)
			{
				zTriangles[zT] = baseIndex + (ringIndex * (x + 1)) + gridSize + z;
				zTriangles[zT + 1] = zTriangles[zT + 4] = baseIndex + (ringIndex * x) + gridSize + z;
				zTriangles[zT + 2] = zTriangles[zT + 3] = baseIndex + (ringIndex * (x + 1)) + gridSize + 1 + z;
				zTriangles[zT + 5] = baseIndex + (ringIndex * x) + gridSize + 1 + z;
				zT = zT + 6;
			}

			for (int y = 0; y < (gridSize / 2) - 1; y++)
			{
                yTriangles[yT] = baseIndex + (ringIndex * (x + 1)) + gridSize + (gridSize / 2) + y;
                yTriangles[yT + 1] = yTriangles[yT + 4] = baseIndex + (ringIndex * x) + gridSize + (gridSize / 2) + y;
                yTriangles[yT + 2] = yTriangles[yT + 3] = baseIndex + (ringIndex * (x + 1)) + gridSize + (gridSize / 2) + 1 + y;
                yTriangles[yT + 5] = baseIndex + (ringIndex * x) + gridSize + (gridSize / 2) + 1 + y;
                yT = yT + 6;
            }

			yTriangles[yT] = baseIndex + (ringIndex * (x + 1)) + gridSize + gridSize - 1;
			yTriangles[yT + 1] = yTriangles[yT + 4] = baseIndex + (ringIndex * x) + gridSize + gridSize - 1;
			yTriangles[yT + 2] = yTriangles[yT + 3] = baseIndex + (ringIndex * (x + 1));
			yTriangles[yT + 5] = baseIndex + (ringIndex * x);
			yT = yT + 6;
		}
	}

	private void CreateYRingTriangles(int[] yTriangles, int[] zTriangles, int gridSize, int offset)
	{
	}

	private void CreateZRingTriangles(int[] yTriangles, int[] zTriangles, int gridSize, int offset)
	{
	}

	private void SetVertex(int i, int x, int y, int z)
	{
		Vector3 vec = new Vector3(x, y, z) * 2f / gridSize - Vector3.one;
		float x2 = vec.x * vec.x;
		float y2 = vec.y * vec.y;
		float z2 = vec.z * vec.z;
		Vector3 s;
		s.x = vec.x * Mathf.Sqrt(1f - y2 / 2f - z2 / 2f + y2 * z2 / 3f);
		s.y = vec.y * Mathf.Sqrt(1f - x2 / 2f - z2 / 2f + x2 * z2 / 3f);
		s.z = vec.z * Mathf.Sqrt(1f - x2 / 2f - y2 / 2f + x2 * y2 / 3f);
		normals[i] = s;
		vertices[i] = normals[i] * radius;
		cubeUV[i] = new Color32((byte)x, (byte)y, (byte)z, 0);
	}

	private void OnDrawGizmos()
	{
		if (vertices == null)
		{
			return;
		}
		Gizmos.color = Color.black;
		for (int i = 0; i < vertices.Length; i++)
		{
			Gizmos.color = Color.black;
			Gizmos.DrawSphere(vertices[i], 1.1f);
			//Gizmos.color = Color.yellow;
			//Gizmos.DrawRay(vertices[i], normals[i]);
		}
	}

}