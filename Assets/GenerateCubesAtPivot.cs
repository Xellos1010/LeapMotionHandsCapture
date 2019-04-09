using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

    public class GenerateCubesAtPivot : MonoBehaviour {

    //public bvhAnchor[] anchorPoints; 
    public PrimitiveType primitiveType;
    public GameObject prefabToGenerate;

        internal void GenerateCubes()
        {
            GenerateCubes(transform);
        }

        internal void GenerateCubes(Transform t)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                Transform child = t.GetChild(i);
                if (t.GetChild(i).childCount > 0)
                    GenerateCubes(child);
                //Generate Anchor
                //Generate Mesh 
                GeneratePrimitive(primitiveType, child);
            }
        }

        private GameObject GeneratePrimitive(PrimitiveType primitiveToGenerate)
        {
            return GameObject.CreatePrimitive(primitiveToGenerate);
        }

        private void GeneratePrimitive(PrimitiveType primitiveToGenerate, Transform parent)
        {
            GameObject child = GeneratePrimitive(primitiveToGenerate);
            child.transform.parent = parent;
            child.transform.position = Vector3.zero;
            child.transform.rotation = Quaternion.identity;
            child.transform.localScale = Vector3.one;
        }

    internal void GenerateCubesWithPrefab()
    {
        GenerateCubesWithPrefab(transform);
    }

    internal void GenerateCubesWithPrefab(Transform t)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            Transform child = t.GetChild(i);
            if (t.GetChild(i).childCount > 0)
                GenerateCubesWithPrefab(child);
            //Generate Anchor
            //Generate Mesh 
            StartCoroutine(GeneratePrefab(prefabToGenerate, child));
        }
    }

    private IEnumerator GeneratePrefab(GameObject prefabToGenerate, Transform parent)
    {
        GameObject generatedPrefab;
        generatedPrefab = (GameObject)Instantiate(prefabToGenerate, transform.position, transform.rotation,parent) as GameObject;
        yield return new WaitForSeconds(.1f);
        generatedPrefab.transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(.1f);
        generatedPrefab.transform.localRotation = Quaternion.identity;
        yield return new WaitForSeconds(.1f);
        generatedPrefab.transform.localScale = Vector3.one;
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(GenerateCubesAtPivot))]
[CanEditMultipleObjects]
public class GenerateCubesAtPivotEditor : Editor
{
    GenerateCubesAtPivot _target;

    private void OnEnable()
    {
        _target = (GenerateCubesAtPivot)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate Box's @ Anchor"))
        {
            _target.GenerateCubes();
        }
        if (GUILayout.Button("Generate prefab @ Anchor"))
        {
            _target.GenerateCubesWithPrefab();
        }
    }

}
#endif