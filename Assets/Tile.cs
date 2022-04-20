using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private readonly float unit = 25;

    [SerializeField] float unitSize = 1;

    Bounds tileBound;

    public Bounds GetBounds()
    {
        if (tileBound.center == Vector3.zero)
        {
            tileBound = new Bounds(Vector3.zero, Vector3.zero);

            BoxCollider[] colliders = GetComponentsInChildren<BoxCollider>();
            foreach (var collider in colliders)
            {
                tileBound.Encapsulate(collider.bounds);
            }
        }

        return tileBound;
    }

    public float GetLength()
    {
        return unit * unitSize;
    }
}
