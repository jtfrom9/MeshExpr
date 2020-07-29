using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeshGen : MonoBehaviour
{
    void Awake()
    {
        var mesh = new Mesh();

        var vertices = new List<Vector3> {
            //　反時計回り (裏側)
            // new Vector3 (1, 0, 0),
            // new Vector3 (0, 1, 0),
            // new Vector3 (-1, 0, 0),

            // 時計回り (表)
            new Vector3 (-1, 0, 0), // 0
            new Vector3 (0, 1, 0),  // 1
            new Vector3 (1, 0, 0),  // 2
        
            // new Vector3 (-1, 0, 0), // 0
            // new Vector3 (1, 0, 0), // 2

            new Vector3 (0, -1, 0.5f) // 3
        };
        mesh.SetVertices(vertices);

        var triangles = new List<int> { 0, 1, 2, 0, 2, 3 };
        // var triangles = new List<int> { 0, 1, 2, 3, 4, 5 };
        mesh.SetTriangles(triangles, 0);

        // 法線計算。これでライトが考慮される（色がつく. material次第)
        mesh.RecalculateNormals();

        foreach(var (s,index) in mesh.EachVerticiesInfo().Select((s,i)=>(s,i))) {
            Debug.Log($"[{index}] {s}");
        }

        // Debug.Log($"verticies={string.Join(",", mesh.vertices)}");
        // Debug.Log($"normals={string.Join(",", mesh.normals)}");
        // Debug.Log($"triangles={string.Join(",",mesh.triangles)}");

        var meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }
}
