using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SyncPos : MonoBehaviour {

    public Transform objectToSync;

    public void Update()
    {
        SyncPositions(transform, objectToSync);
        SyncRotations(transform, objectToSync);
    }

    internal void SyncRotations(Transform parent, Transform objectToSync)
    {
        objectToSync.transform.rotation = parent.rotation;
        for (int i = 0; i < objectToSync.childCount; i++)
        {

            if (parent.GetChild(i))
            {
                SyncRotations(parent.GetChild(i), objectToSync.GetChild(i));
            }
        }
    }

    internal void SyncPositions(Transform parent, Transform objectToSync)
    {
        Debug.Log(parent.name + " " + parent.childCount + " " + objectToSync.name + " " + objectToSync.childCount);
        objectToSync.transform.position = parent.position;
        for (int i = 0; i < objectToSync.childCount; i++)
        {

            if (parent.GetChild(i))
            {
                SyncPositions(parent.GetChild(i), objectToSync.GetChild(i));
            }
        }
    }
    
    internal void SyncPositionTransformChildren()
    {
        transform.GetComponent<SyncPosToTransform>().SyncObject();
        SyncPosToTransform[] syncPositions = transform.GetComponentsInChildren<SyncPosToTransform>();
        for (int i = 0; i < syncPositions.Length; i++)
            {
            syncPositions[i].SyncObject();
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(SyncPos))]
public class SyncPosEditor:Editor
{
    public SyncPos _target;

    private void OnEnable()
    {
        _target = (SyncPos)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Sync Children"))
        {
            _target.SyncPositionTransformChildren();
        }
        /*if (GUILayout.Button("Apply Transform"))
        {   
            _target.SyncPositions(_target.transform, _target.objectToSync);
        }
        if (GUILayout.Button("Apply Rotation"))
        {
            _target.SyncRotations(_target.transform, _target.objectToSync);
        }*/
    }

    private void LogChildren(Transform objectToSync)
    {
        Debug.Log(objectToSync .name + " " + objectToSync.childCount);
        for (int i = 0; i < objectToSync.childCount; i++)
        {
            LogChildren(objectToSync.GetChild(i));
        }
    }
}

#endif