using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SyncPosToTransform : MonoBehaviour {

    public bool syncRotation = true;
    public bool syncPosition = true;

    public Transform parent;

    public void Update()
    {
        SyncObject();
    }

    public void SyncObject()
    {
        if(syncPosition)
            SyncPosition(parent,transform);
        if(syncRotation)
            SyncRotation(parent, transform);
    }

    internal void SyncRotation(Transform parent, Transform objectToSync)
    {
        objectToSync.transform.rotation = parent.rotation;
    }

    internal void SyncPosition(Transform parent, Transform objectToSync)
    {
        objectToSync.transform.position = parent.position;
    }

    internal void SyncObjects()
    {
        SyncObject();
        SyncPosToTransform[] allComponents = transform.GetComponentsInChildren<SyncPosToTransform>();
        for (int i = 0; i < allComponents.Length; i++)
        {
            allComponents[i].SyncObject();
        }
    }
    
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(SyncPosToTransform))]
public class SyncPosToTransformEditor:Editor
{
    public SyncPosToTransform _target;

    private void OnEnable()
    {
        _target = (SyncPosToTransform)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Sync Objects"))
        {   
            _target.SyncObjects();
        }
        if (GUILayout.Button("Apply Transform"))
        {   
            _target.SyncPosition(_target.transform, _target.parent);
        }
        if (GUILayout.Button("Apply Rotation"))
        {
            _target.SyncRotation(_target.transform, _target.parent);
        }
    }
}

#endif

