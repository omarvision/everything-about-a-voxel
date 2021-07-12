using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class VoxelFlat : MonoBehaviour
{
    public Vector3[] vertices = new Vector3[24];
    public Vector2[] uv = new Vector2[24];
    public int[] triangles = new int[36];
    private Mesh mesh = null;

    private void Start()
    {
        getMeshObject();
        initVoxelData();
        setMeshData();
    }
    private void Update()
    {
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

        // EFAB back
        E = 0;
        F = 1;
        A = 2;
        B = 3;
        vertices[E] = new Vector3(LO, HI, LO);
        vertices[F] = new Vector3(HI, HI, LO);
        vertices[A] = new Vector3(LO, LO, LO);
        vertices[B] = new Vector3(HI, LO, LO);
        uv[E] = new Vector2(0, 1);
        uv[F] = new Vector2(1, 1);
        uv[A] = new Vector2(0, 0);
        uv[B] = new Vector2(1, 0);
        triangles[0] = A;
        triangles[1] = E;
        triangles[2] = F;
        triangles[3] = B;
        triangles[4] = A;
        triangles[5] = F;

        // FHBD right
        F = 4;
        H = 5;
        B = 6;
        D = 7;
        vertices[F] = new Vector3(HI, HI, LO);
        vertices[H] = new Vector3(HI, HI, HI);
        vertices[B] = new Vector3(HI, LO, LO);
        vertices[D] = new Vector3(HI, LO, HI);
        uv[F] = new Vector2(0, 1);
        uv[H] = new Vector2(1, 1);
        uv[B] = new Vector2(0, 0);
        uv[D] = new Vector2(1, 0);
        triangles[6] = B;
        triangles[7] = F;
        triangles[8] = H;
        triangles[9] = D;
        triangles[10] = B;
        triangles[11] = H;

        // HGDC forward
        H = 8;
        G = 9;
        D = 10;
        C = 11;
        vertices[H] = new Vector3(HI, HI, HI);
        vertices[G] = new Vector3(LO, HI, HI);
        vertices[D] = new Vector3(HI, LO, HI);
        vertices[C] = new Vector3(LO, LO, HI);
        uv[H] = new Vector2(0, 1);
        uv[G] = new Vector2(1, 1);
        uv[D] = new Vector2(0, 0);
        uv[C] = new Vector2(1, 0);
        triangles[12] = D;
        triangles[13] = H;
        triangles[14] = G;
        triangles[15] = C;
        triangles[16] = D;
        triangles[17] = G;

        // GECA left
        G = 12;
        E = 13;
        C = 14;
        A = 15;
        vertices[G] = new Vector3(LO, HI, HI);
        vertices[E] = new Vector3(LO, HI, LO);
        vertices[C] = new Vector3(LO, LO, HI);
        vertices[A] = new Vector3(LO, LO, LO);
        uv[G] = new Vector2(0, 1);
        uv[E] = new Vector2(1, 1);
        uv[C] = new Vector2(0, 0);
        uv[A] = new Vector2(1, 0);
        triangles[18] = C;
        triangles[19] = G;
        triangles[20] = E;
        triangles[21] = A;
        triangles[22] = C;
        triangles[23] = E;

        // EFGH up
        E = 16;
        F = 17;
        G = 18;
        H = 19;
        vertices[E] = new Vector3(LO, HI, LO);
        vertices[F] = new Vector3(HI, HI, LO);
        vertices[G] = new Vector3(LO, HI, HI);
        vertices[H] = new Vector3(HI, HI, HI);
        uv[E] = new Vector2(0, 0);
        uv[F] = new Vector2(1, 0);
        uv[G] = new Vector2(0, 1);
        uv[H] = new Vector2(1, 1);
        triangles[24] = E;
        triangles[25] = G;
        triangles[26] = H;
        triangles[27] = F;
        triangles[28] = E;
        triangles[29] = H;

        // ABCD down
        A = 20;
        B = 21;
        C = 22;
        D = 23;
        vertices[A] = new Vector3(LO, LO, LO);
        vertices[B] = new Vector3(HI, LO, LO);
        vertices[C] = new Vector3(LO, LO, HI);
        vertices[D] = new Vector3(HI, LO, HI);
        uv[A] = new Vector2(0, 1);
        uv[B] = new Vector2(1, 1);
        uv[C] = new Vector2(0, 0);
        uv[D] = new Vector2(1, 0);
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

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }
}
