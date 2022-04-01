using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    void Start()
    {
        Vector3 origPos = transform.position;
        Quaternion origRot = transform.rotation;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        MeshFilter[] meshes = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] instances = new CombineInstance[meshes.Length];

        for (int i = 0; i < meshes.Length; i++)
        {
            if (meshes[i].gameObject.transform == transform) continue;
            instances[i].mesh = meshes[i].sharedMesh;

            instances[i].transform = meshes[i].transform.localToWorldMatrix;
            meshes[i].gameObject.SetActive(false);
        }

        MeshFilter combinedMesh = transform.GetComponent<MeshFilter>();
        combinedMesh.sharedMesh = new Mesh();
        combinedMesh.sharedMesh.CombineMeshes(instances);
        transform.position = origPos;
        transform.rotation = origRot;
    }
}
