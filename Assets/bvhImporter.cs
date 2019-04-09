using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Winterdust;
using UnityEditor;

public class bvhImporter : MonoBehaviour {
    public string assetLocation;
    public BVH myBvh;
    public GameObject skeletonGO;
    public GameObject modelGO;
    public AnimationClip clip;

    // Use this for initialization
    internal void ImportBVH()
    {
        myBvh = new BVH(Application.dataPath + "//" + assetLocation);
        skeletonGO = myBvh.makeSkeleton();
        clip = myBvh.makeAnimationClip();
    }

    internal void ApplyMesh()
    {
        MeshSkinner ms = new MeshSkinner(modelGO, skeletonGO);
        ms.work();
        ms.finish();
    }
}

[CustomEditor(typeof(bvhImporter))]
public class bvhImporterEditor : Editor
{
    bvhImporter _target;

    private void OnEnable()
    {
        _target = (bvhImporter)target;    
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Import BVH file"))
        {
            _target.ImportBVH();
            _target.ApplyMesh();
        }
        if (GUILayout.Button("Apply skinned mesh"))
        {
            _target.ApplyMesh();
        }
    }
}