using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshDebugExtension
{
    public static IEnumerable<string> EachVerticiesInfo(this Mesh mesh)
    {
        var triindecies = mesh.GetTriangles(0);
        int numTriangle = triindecies.Length / 3;

        var vertices = new List<Vector3>();
        mesh.GetVertices(vertices);
        var normals = new List<Vector3>();
        mesh.GetNormals(normals);
        var uvs = new List<Vector2>();
        mesh.GetUVs(0, uvs);

        for (int tri = 0; tri < numTriangle; tri++) {
            var sb = new StringBuilder();
            sb.AppendFormat("Tri[{0}]\n", tri);
            for (int i = 0; i < 3; i++) {
                var vi = triindecies[tri * 3 + i];
                sb.AppendFormat("  V={0} ({1}), N={2}, Uv={3}\n",
                    vi,
                    vertices[vi].ToString(),
                    normals[vi].ToString(),
                    (uvs.Count > vi) ? uvs[vi].ToString() : "");
            }
            yield return sb.ToString();
        }
        // foreach (var vi in triangles)
        // {
        //     yield return string.Format("index={0} ({1}), normal={2}",
        //         vi,
        //         mesh.vertices[vi].ToString(),
        //         mesh.normals[vi].ToString());
        // }
    }
}

public class MeshLog : MonoBehaviour
{
    void PrintInfo(Mesh mesh)
    {
        var sb = new StringBuilder();
        foreach (var (s, index) in mesh.EachVerticiesInfo().Select((s, i) => (s, i)))
        {
            sb.AppendFormat("[{0}] {1}\n", index, s);
        }
        Debug.Log($"<{gameObject.name}> Vertecies={mesh.vertexCount} \n{sb.ToString()}");
    }

    void Start()
    {
        var mf = GetComponent<MeshFilter>();
        PrintInfo(mf.mesh);
    }
}
