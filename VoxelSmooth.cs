using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class VoxelSmooth : MonoBehaviour
{
    public Vector3[] vertices = new Vector3[8];
    public Vector2[] uv = new Vector2[8];
    public int[] triangles = new int[36];
    private Mesh mesh = null;

    private void Start()
    {
        getMeshObject();
        initVoxelData();
        setMeshData();
    }
    private void getMeshObject()
    {
        MeshFilter mf = this.GetComponent<MeshFilter>();
        MeshCollider mc = this.GetComponent<MeshCollider>();

        if (Application.isEditor == true)
        {
            mf.sharedMesh = new Mesh();
            mesh = mf.sharedMesh;
        }
        else
        {
            mf.mesh = new Mesh();
            mesh = mf.mesh;
        }

        mc.sharedMesh = mesh;
    }
    private void initVoxelData()
    {
        int A, B, C, D, E, F, G, H;
        float LO = -0.5f;
        float HI = 0.5f;

        /*        
                   G           H            A   -  -  - 
                                            B   +  -  -
               E           F                C   -  -  +
                                            D   +  -  +
                                            E   -  +  -
                   C           D            F   +  +  -
                                            G   -  +  +
               A           B                H   +  +  + 

       */

        // the corners
        A = 0;
        B = 1;
        C = 2;
        D = 3;
        E = 4;
        F = 5;
        G = 6;
        H = 7;
        vertices[A] = new Vector3(LO, LO, LO);
        vertices[B] = new Vector3(HI, LO, LO);        
        vertices[C] = new Vector3(LO, LO, HI);
        vertices[D] = new Vector3(HI, LO, HI);
        vertices[E] = new Vector3(LO, HI, LO);
        vertices[F] = new Vector3(HI, HI, LO);        
        vertices[G] = new Vector3(LO, HI, HI);
        vertices[H] = new Vector3(HI, HI, HI);
        uv[A] = Vector2.zero;       //the uv count has to match the vertex count
        uv[B] = Vector2.zero;
        uv[C] = Vector2.zero;
        uv[D] = Vector2.zero;
        uv[E] = Vector2.zero;
        uv[F] = Vector2.zero;
        uv[G] = Vector2.zero;
        uv[H] = Vector2.zero;

        // triangles
        triangles[0] = A;  //connect the points of triangle in clockwise
        triangles[1] = E;
        triangles[2] = F;
        triangles[3] = B;
        triangles[4] = A;
        triangles[5] = F;

        triangles[6] = B;
        triangles[7] = F;
        triangles[8] = H;
        triangles[9] = D;
        triangles[10] = B;
        triangles[11] = H;

        triangles[12] = D;
        triangles[13] = H;
        triangles[14] = G;
        triangles[15] = C;
        triangles[16] = D;
        triangles[17] = G;

        triangles[18] = C;
        triangles[19] = G;
        triangles[20] = E;
        triangles[21] = A;
        triangles[22] = C;
        triangles[23] = E;

        triangles[24] = E;
        triangles[25] = G;
        triangles[26] = H;
        triangles[27] = F;
        triangles[28] = E;
        triangles[29] = H;

        triangles[30] = C;
        triangles[31] = A;
        triangles[32] = B;
        triangles[33] = D;
        triangles[34] = C;
        triangles[35] = B;
    }
    private void setMeshData()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();   //size of mesh
        mesh.RecalculateNormals();  //for lighting
        mesh.RecalculateTangents(); //shaders
    }
}
