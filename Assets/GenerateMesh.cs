using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class GenerateMesh : MonoBehaviour {

    public Vector3[] newVertices;
    public Vector2[] newUV;
    public int[] newTriangles;

    internal void GenerateMeshNow()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;
    }
}

#if UNITY_EDITOR
public class GenerateMeshEditor : Editor
{
    GenerateMesh _target;

    private void OnEnable()
    {
        _target = (GenerateMesh)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Test Mesh Generation"))
        {
            _target.GenerateMeshNow();
        }
    }
}
#endif
