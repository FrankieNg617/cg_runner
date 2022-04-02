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

        AdvanceMerge();
        //BasicMerge();
        transform.position = origPos;
        transform.rotation = origRot;
    }

    private void AdvanceMerge()
    {
        /*
            1. Get all mesh from children
            2. Filter out specific material and its mesh
            3. For every material, make a sub mesh
            4. Merge all submesh
        */
        //  1.
        MeshFilter[] meshes = GetComponentsInChildren<MeshFilter>(false);

        //  2.
        Dictionary<Material, List<MeshFilter>> materialDict = new Dictionary<Material, List<MeshFilter>>();
        foreach (var mesh in meshes)
        {
            if (mesh.transform == transform) continue;
            Material meshMat = mesh.GetComponent<MeshRenderer>()?.sharedMaterial;
            if (!materialDict.ContainsKey(meshMat)) materialDict[meshMat] = new List<MeshFilter>();
            materialDict[meshMat].Add(mesh);
        }

        //  3.
        List<Mesh> subMeshes = new List<Mesh>();
        foreach (var mat in materialDict.Keys)
        {
            List<MeshFilter> matMeshes = materialDict[mat];
            CombineInstance[] subCombines = new CombineInstance[matMeshes.Count];

            for (int i = 0; i < matMeshes.Count; i++)
            {
                subCombines[i].mesh = matMeshes[i].sharedMesh;
                subCombines[i].transform = matMeshes[i].transform.localToWorldMatrix;
                subCombines[i].subMeshIndex = i;

                matMeshes[i].gameObject.SetActive(false);
            }
            Mesh subMesh = new Mesh();
            subMesh.CombineMeshes(subCombines);
            subMeshes.Add(subMesh);
        }

        //  4.
        CombineInstance[] combines = new CombineInstance[subMeshes.Count];
        for (int i = 0; i < subMeshes.Count; i++)
        {
            combines[i].mesh = subMeshes[i];
            combines[i].transform = Matrix4x4.identity;
        }
        MeshFilter rootMesh = transform.GetComponent<MeshFilter>();
        rootMesh.sharedMesh = new Mesh();
        rootMesh.sharedMesh.CombineMeshes(combines, false);
    }

    private void BasicMerge()
    {
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
    }
}
