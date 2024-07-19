using UnityEngine;

//Please connect this file to DynamicMesh object at the scene

public class DynamicMesh : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;
    Color[] colors;
    float[] stats;

    void Start()
    {
        //I know it would be better to recalculate triangles,
        //So triangles would face upwards, not downwards
        //But for me it was easier to rotate the objecs
        //Rather than rewrite whole trangle generation method from scratch
        transform.rotation = Quaternion.Euler(-90, 0, -180);
        GenerateMesh();
    }

    private void Update()
    {
        // This is not the optimal way to update mesh
        // I would like to create a Unity Event at Stats.cs
        // Then connect this method call to it
        // And Invoke the method at Stats::TryDecreaseStat and Stats::TryIncreaseStat

        //I would do that, but as I understood for assignment, we cannot modify other files
        UpdateMesh();
    }

    #region GenerateNewMesh
    private void GenerateMesh()
    {
        mesh = new Mesh();
        vertices = new Vector3[7];
        triangles = new int[(vertices.Length - 1) * 3];
        uvs = new Vector2[vertices.Length];
        SetupStats();

        GetComponent<MeshFilter>().mesh = mesh;
        SetupVertices();
        SetupTriangles();
        SetupUV();
        SetupColors();

        RegenerateMesh();
    }

    private void SetupVertices()
    {
        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertices.Length - 1; i++)
        {
            float radius = stats[i];
            //Thanks god I had to remember vector rotation for physics class.
            //According to Trigonometry circle
            //Cos(angleInRads) * radius = Vector (X, 0) 
            //Sin(angleInRads) * radius = Vector (0, Y)
            //I used this data for getting X and Y coordinates of hexagon vectices
            float angle = Mathf.Deg2Rad * (60 * i + 30);
            vertices[i + 1] = new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0);
        }

        mesh.vertices = vertices;
    }
    
    private void SetupTriangles()
    {
        for (int i = 0; i < vertices.Length - 1; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i == 5 ? 1 : i + 2;
        }
        mesh.triangles = triangles;
    }

    private void SetupUV()
    {
        uvs = new Vector2[] { 
            new Vector2(0.5f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
        };
        mesh.uv = uvs;
    }

    private void SetupColors() {
        colors = new Color[vertices.Length];

        colors[0] = new Color(1, 1, 1, 1);
        Color green = new Color(0, 1, 0, 1);
        Color red = new Color(1, 0, 0, 1);

        for (int i = 0; i < colors.Length - 1; i++) {
            float tempColorIntencity = stats[i];
            tempColorIntencity *= 16;
            tempColorIntencity -= 4;
            tempColorIntencity /= 12;

            colors[i + 1] = Color.Lerp(red, green, tempColorIntencity);
        }

        mesh.colors = colors;
    }

    private void RegenerateMesh()
    {
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
    #endregion

    #region UpdateMesh
    void UpdateMesh()
    {
        SetupStats();
        GenerateMesh();
    }

    //I really appreciate variables Stats::statsValueNormalized
    //But I would like to get full control over the data
    private void SetupStats()
    {
        stats = new float[vertices.Length];
        float[] tempStats = Stats.statsValue;

        for (int i = 0; i < tempStats.Length; i++)
        {
            stats[i] = (tempStats[i]) / 16;
        }
    }
    #endregion
}
