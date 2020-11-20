using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttachSemiCircleMesh : MonoBehaviour {

    [Range(3, 100)]
    public int numberOfSegments;

    public float radius;

    public Material material;

    // Use this for initialization
    void Start()
    {
        if (GetComponent<MeshRenderer>() == null)
        {
            gameObject.AddComponent<MeshRenderer>();
        }

        if (GetComponent<MeshFilter>() == null)
        {
            gameObject.AddComponent<MeshFilter>();
        }

        GetComponent<MeshRenderer>().material = material;

        FormGeometry();
    }

    void FormGeometry()
    {
        List<Vector3> verts = new List<Vector3>(numberOfSegments + 1 + 2);
        List<int> tris = new List<int>(3 * numberOfSegments + 2);

        verts.Add(Vector3.zero);

        float angleStep = Mathf.PI / numberOfSegments;
        float degRad90 = Mathf.PI * 0.5f;
        for (int i = 0; i < numberOfSegments+1; i++)
        {
            Vector3 normPos = new Vector3(Mathf.Cos(i * angleStep), Mathf.Sin(i * angleStep), 0f);
            verts.Add(normPos * radius);
        }

        for (int i = 0; i < numberOfSegments; i++)
        {
            tris.Add(0);
            tris.Add(i + 2);
            tris.Add(i + 1);
        }

        Mesh mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();

        mesh.Optimize();
        mesh.RecalculateBounds();

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
