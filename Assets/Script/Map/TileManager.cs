using System.Collections.Generic;
using Script.Utilities;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float spawnPos = 0;
    public Bounds tileBound;
    public int numberOfTiles = 6; //no. of tiles want to be shown on the screen

    public Transform character;

    private int initialTileIndex;

    private List<GameObject> activeTiles = new List<GameObject>();

    private List<GameObject> tilePool = new List<GameObject>();

    public void Start()
    {
        InstantiateTiles();
        InitializeTiles();
    }

    private void InstantiateTiles()
    {
        // var initialTile = tilePrefabs[0];
        // Instantiate(initialTile);
        // initialTile.SetActive(false);

        foreach (var tile in tilePrefabs)
        {
            for (int i = 0; i < 3; i++)
            {
                var t = Instantiate(tile);
                t.SetActive(false);
                tilePool.Add(t);
            }
        }

        var initialTile = tilePool[0];

        tileBound = new Bounds(Vector3.zero, Vector3.zero);

        BoxCollider[] colliders = initialTile.GetComponentsInChildren<BoxCollider>();
        foreach (var collider in colliders)
        {
            tileBound.Encapsulate(collider.bounds);
        }

        tilePool = ListUtility.Shuffle(tilePool);
        initialTileIndex = tilePool.IndexOf(initialTile);
    }

    private void InitializeTiles()
    {
        SpawnTile(initialTileIndex);

        for (int i = 1; i < numberOfTiles; i++)
        {
            SpawnTile(Random.Range(0, tilePool.Count));
        }
    }

    void Update()
    {
        if (character.position.z - 35 > spawnPos - (numberOfTiles * tileBound.size.z))
        {
            SpawnTile(Random.Range(0, tilePool.Count));
            DeleteTile();    // delete the odd tile whenever a new tile has been created
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject gameObject = tilePool[tileIndex];
        tilePool.RemoveAt(tileIndex);

        activeTiles.Add(gameObject);
        var objectTransform = transform;
        gameObject.transform.position = objectTransform.forward * spawnPos;
        gameObject.transform.rotation = objectTransform.rotation;
        gameObject.SetActive(true);

        spawnPos += tileBound.size.z;
    }

    private void DeleteTile()
    {
        GameObject gameObject = activeTiles[0];
        gameObject.SetActive(false);

        activeTiles.RemoveAt(0);
        tilePool.Add(gameObject);

        Debug.Log($"{gameObject.name}");
    }
}
