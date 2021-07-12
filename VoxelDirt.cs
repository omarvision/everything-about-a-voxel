using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class VoxelDirt : MonoBehaviour
{
    #region --- helper ---
    private enum enumCorner
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
    }
    private enum enumTexture
    {
        grass,
        dirt,
        grassdirt,
    }
    private enum enumSide
    {
        EFAB_back,
        FHBD_right,
        HGDC_forward,
        GECA_left,
        EFGH_up,
        ABCD_down,
    }
    #endregion

    public Vector3[] vertices = new Vector3[24];
    public Vector2[] uv = new Vector2[24];
    public int[] triangles = new int[36];
    public Vector2 TexturesInDirection = new Vector2(3f, 1f);
    private Mesh mesh = null;
    private int A, B, C, D, E, F, G, H;
    private float LO = -0.5f;   //the minus value   -
    private float HI = 0.5f;    //the plus value    +

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
        setVerticesSide(enumSide.EFAB_back);
        setUVSide(enumSide.EFAB_back, enumTexture.grassdirt);
        setTriangleSide(enumSide.EFAB_back);

        setVerticesSide(enumSide.FHBD_right);
        setUVSide(enumSide.FHBD_right, enumTexture.grassdirt);
        setTriangleSide(enumSide.FHBD_right);

        setVerticesSide(enumSide.HGDC_forward);
        setUVSide(enumSide.HGDC_forward, enumTexture.grassdirt);
        setTriangleSide(enumSide.HGDC_forward);

        setVerticesSide(enumSide.GECA_left);
        setUVSide(enumSide.GECA_left, enumTexture.grassdirt);
        setTriangleSide(enumSide.GECA_left);

        setVerticesSide(enumSide.EFGH_up);
        setUVSide(enumSide.EFGH_up, enumTexture.grass);
        setTriangleSide(enumSide.EFGH_up);

        setVerticesSide(enumSide.ABCD_down);
        setUVSide(enumSide.ABCD_down, enumTexture.dirt);
        setTriangleSide(enumSide.ABCD_down);
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

        mesh.ExportToDesktop(this.gameObject.name + ".txt");
    }
    private Vector3 Corner(enumCorner code)
    {
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

        switch (code)
        {
            case enumCorner.A:
                return new Vector3(LO, LO, LO);     //  A   -  -  -
            case enumCorner.B:
                return new Vector3(HI, LO, LO);     //  B   +  -  -
            case enumCorner.C:
                return new Vector3(LO, LO, HI);     //  C   -  -  +
            case enumCorner.D:
                return new Vector3(HI, LO, HI);     //  D   +  -  +
            case enumCorner.E:
                return new Vector3(LO, HI, LO);     //  E   -  +  -
            case enumCorner.F:
                return new Vector3(HI, HI, LO);     //  F   +  +  -
            case enumCorner.G:
                return new Vector3(LO, HI, HI);     //  G   -  +  +
            case enumCorner.H:
                return new Vector3(HI, HI, HI);     //  H   +  +  +
            default:
                Debug.Log("Corner: Error");
                return Vector3.zero;
        }
    }
    private void setVerticesSide(enumSide side)
    {
        int i = (int)side * 4;

        switch (side)
        {
            case enumSide.EFAB_back:
                vertices[i++] = Corner(enumCorner.E);
                vertices[i++] = Corner(enumCorner.F);
                vertices[i++] = Corner(enumCorner.A);
                vertices[i++] = Corner(enumCorner.B);
                break;
            case enumSide.FHBD_right:
                vertices[i++] = Corner(enumCorner.F);
                vertices[i++] = Corner(enumCorner.H);
                vertices[i++] = Corner(enumCorner.B);
                vertices[i++] = Corner(enumCorner.D);
                break;
            case enumSide.HGDC_forward:
                vertices[i++] = Corner(enumCorner.H);
                vertices[i++] = Corner(enumCorner.G);
                vertices[i++] = Corner(enumCorner.D);
                vertices[i++] = Corner(enumCorner.C);
                break;
            case enumSide.GECA_left:
                vertices[i++] = Corner(enumCorner.G);
                vertices[i++] = Corner(enumCorner.E);
                vertices[i++] = Corner(enumCorner.C);
                vertices[i++] = Corner(enumCorner.A);
                break;
            case enumSide.EFGH_up:
                vertices[i++] = Corner(enumCorner.E);
                vertices[i++] = Corner(enumCorner.F);
                vertices[i++] = Corner(enumCorner.G);
                vertices[i++] = Corner(enumCorner.H);
                break;
            case enumSide.ABCD_down:
                vertices[i++] = Corner(enumCorner.A);
                vertices[i++] = Corner(enumCorner.B);
                vertices[i++] = Corner(enumCorner.C);
                vertices[i++] = Corner(enumCorner.D);
                break;
            default:
                Debug.Log("setVerticesSide: Error unhandled side " + side.ToString());
                break;
        }
    }
    private void setUVSide(enumSide side, enumTexture tex)
    {
        float xpercent = 1f / TexturesInDirection.x;
        float ypercent = 1f / TexturesInDirection.y;
        int p0 = (int)tex;
        int p1 = (int)tex + 1;

        Vector2 LO = new Vector2(xpercent * p0, ypercent * p0);
        Vector2 HI = new Vector2(xpercent * p1, ypercent * p1);

        int i = (int)side * 4;

        switch (side)
        {
            case enumSide.EFAB_back:
            case enumSide.FHBD_right:
            case enumSide.HGDC_forward:
            case enumSide.GECA_left:
                uv[i++] = new Vector2(LO.x, HI.y);  //  -   +
                uv[i++] = new Vector2(HI.x, HI.y);  //  +   +
                uv[i++] = new Vector2(LO.x, LO.y);  //  -   -
                uv[i++] = new Vector2(HI.x, LO.y);  //  +   -
                break;
            case enumSide.EFGH_up:
                uv[i++] = new Vector2(LO.x, LO.y);  //  -   -
                uv[i++] = new Vector2(HI.x, LO.y);  //  +   -
                uv[i++] = new Vector2(LO.x, HI.y);  //  -   +
                uv[i++] = new Vector2(HI.x, HI.y);  //  +   +
                break;
            case enumSide.ABCD_down:
                uv[i++] = new Vector2(LO.x, HI.y);  //  -   +
                uv[i++] = new Vector2(HI.x, HI.y);  //  +   +
                uv[i++] = new Vector2(LO.x, LO.y);  //  -   -
                uv[i++] = new Vector2(HI.x, LO.y);  //  +   -
                break;
            default:
                Debug.Log("setUVSide: Error");
                break;
        }
    }
    private void setTriangleSide(enumSide side)
    {
        int i = (int)side * 6;

        switch (side)
        {
            case enumSide.EFAB_back:
                E = 0;
                F = 1;
                A = 2;
                B = 3;
                triangles[i++] = A;
                triangles[i++] = E;
                triangles[i++] = F;
                triangles[i++] = B;
                triangles[i++] = A;
                triangles[i++] = F;
                break;
            case enumSide.FHBD_right:
                F = 4;
                H = 5;
                B = 6;
                D = 7;
                triangles[i++] = B;
                triangles[i++] = F;
                triangles[i++] = H;
                triangles[i++] = D;
                triangles[i++] = B;
                triangles[i++] = H;
                break;
            case enumSide.HGDC_forward:
                H = 8;
                G = 9;
                D = 10;
                C = 11;
                triangles[i++] = D;
                triangles[i++] = H;
                triangles[i++] = G;
                triangles[i++] = C;
                triangles[i++] = D;
                triangles[i++] = G;
                break;
            case enumSide.GECA_left:
                G = 12;
                E = 13;
                C = 14;
                A = 15;
                triangles[i++] = C;
                triangles[i++] = G;
                triangles[i++] = E;
                triangles[i++] = A;
                triangles[i++] = C;
                triangles[i++] = E;
                break;
            case enumSide.EFGH_up:
                E = 16;
                F = 17;
                G = 18;
                H = 19;
                triangles[i++] = E;
                triangles[i++] = G;
                triangles[i++] = H;
                triangles[i++] = F;
                triangles[i++] = E;
                triangles[i++] = H;
                break;
            case enumSide.ABCD_down:
                A = 20;
                B = 21;
                C = 22;
                D = 23;
                triangles[i++] = C;
                triangles[i++] = A;
                triangles[i++] = B;
                triangles[i++] = D;
                triangles[i++] = C;
                triangles[i++] = B;
                break;
            default:
                Debug.Log("setTrianglesSide: Error");
                break;
        }
    }
}
