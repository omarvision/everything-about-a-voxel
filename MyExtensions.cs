
using UnityEngine;

public static class MyExtensions 
{
    public static void ExportToDesktop(this Mesh m, string name)
    {
        //Purpose: export all the Unity Mesh object data to file

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.AppendFormat("vertexBufferCount = {0}\n", m.vertexBufferCount);
        sb.AppendFormat("vertexCount = {0}\n", m.vertexCount);
        sb.AppendFormat("bounds = {0}\n", m.bounds);
        sb.AppendFormat("hideFlags = {0}\n", m.hideFlags);
        sb.AppendFormat("indexFormat = {0}\n", m.indexFormat);
        sb.AppendFormat("isReadable = {0}\n", m.isReadable);
        sb.AppendLine();

        for (int i = 0; i < m.vertices.Length; i++)
        {
            sb.AppendFormat("vertices[{0}] = {1}\n", i, m.vertices[i]);
        }
        sb.AppendLine();

        for (int i = 0; i < m.uv.Length; i++)
        {
            sb.AppendFormat("uv[{0}] ={1}\n", i, m.uv[i]);
        }
        sb.AppendLine();

        for (int i = 0; i < m.triangles.Length; i++)
        {
            sb.AppendFormat("triangles[{0}] = {1}\n", i, m.triangles[i]);
        }
        sb.AppendLine();

        // ----------------------------------------------------------------------------------------

        for (int i = 0; i < m.normals.Length; i++)
        {
            sb.AppendFormat("normals[{0}] = {1}\n", i, m.normals[i]);
        }
        sb.AppendLine();

        for (int i = 0; i < m.tangents.Length; i++)
        {
            sb.AppendFormat("tangents[{0}] = {1}\n", i, m.tangents[i]);
        }
        sb.AppendLine();

        for (int i = 0; i < m.colors.Length; i++)
        {
            sb.AppendLine(string.Format("colors[{0}] = {1}", i, m.colors[i]));
        }
        sb.AppendLine();

        for (int i = 0; i < m.colors32.Length; i++)
        {
            sb.AppendLine(string.Format("colors32[{0}] = {1}", i, m.colors32[i]));
        }
        sb.AppendLine();

        // ----------------------------------------------------------------------------------------

        for (int i = 0; i < m.uv2.Length; i++)
        {
            sb.AppendFormat("uv2[{0}] ={1}\n", i, m.uv2[i]);
        }
        for (int i = 0; i < m.uv3.Length; i++)
        {
            sb.AppendFormat("uv3[{0}] ={1}\n", i, m.uv3[i]);
        }
        for (int i = 0; i < m.uv4.Length; i++)
        {
            sb.AppendFormat("uv4[{0}] ={1}\n", i, m.uv4[i]);
        }
        for (int i = 0; i < m.uv5.Length; i++)
        {
            sb.AppendFormat("uv5[{0}] ={1}\n", i, m.uv5[i]);
        }
        for (int i = 0; i < m.uv6.Length; i++)
        {
            sb.AppendFormat("uv6[{0}] ={1}\n", i, m.uv6[i]);
        }
        for (int i = 0; i < m.uv7.Length; i++)
        {
            sb.AppendFormat("uv7[{0}] ={1}\n", i, m.uv7[i]);
        }
        for (int i = 0; i < m.uv8.Length; i++)
        {
            sb.AppendFormat("uv8[{0}] ={1}\n", i, m.uv8[i]);
        }              

        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
        System.IO.File.WriteAllText(desktopPath + "\\" + name, sb.ToString());
    }
}
