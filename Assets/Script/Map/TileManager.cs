using System.Collections.Generic;
using Script.Utilities;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Tile[] tilePrefabs;
    public float spawnPos = 0;
    // public Bounds tileBound;
    public int numberOfTiles = 10; //no. of tiles want to be shown on the screen

    public Transform character;

    private int initialTileIndex;

    private List<Tile> activeTiles = new List<Tile>();

    private List<Tile> tilePool = new List<Tile>();


    //  Only used for multiplayer purpose
    private List<Transform> networkCharacterTransforms;

    public void Start()
    {
        InstantiateTiles();
        InitializeTiles();
    }

    private void InstantiateTiles()
    {
        foreach (var tile in tilePrefabs)
        {
            for (int i = 0; i < 3; i++)
            {
                Tile t = Instantiate(tile);
                t.gameObject.SetActive(false);
                tilePool.Add(t);
            }
        }

        var initialTile = tilePool[0];

        /*
        tileBound = new Bounds(Vector3.zero, Vector3.zero);

        BoxCollider[] colliders = initialTile.GetComponentsInChildren<BoxCollider>();
        foreach (var collider in colliders)
        {
            tileBound.Encapsulate(collider.bounds);
        }
        */

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
        float totalTileLength = 0;
        foreach (var activeTile in activeTiles)
        {
            totalTileLength += activeTile.GetLength();
        }


        if (GetFarthestCharacter().position.z - 70 > spawnPos - totalTileLength)
        {
            SpawnTile(Random.Range(0, tilePool.Count));
            DeleteTile();    // delete the odd tile whenever a new tile has been created
        }
    }

    public void SpawnTile(int tileIndex)
    {
        Tile tile = tilePool[tileIndex];
        tilePool.RemoveAt(tileIndex);

        activeTiles.Add(tile);
        var objectTransform = transform;
        tile.transform.position = objectTransform.forward * spawnPos;
        tile.transform.rotation = objectTransform.rotation;
        tile.gameObject.SetActive(true);

        spawnPos += tile.GetLength();
    }

    private void DeleteTile()
    {
        Tile tile = activeTiles[0];
        tile.gameObject.SetActive(false);

        activeTiles.RemoveAt(0);
        tilePool.Add(tile);

        // Debug.Log($"TileManager: {gameObject.name}");
    }

    private Transform GetFarthestCharacter()
    {
        if (character != null) return character;

        //  FIX: Hard-code approach to handle racing condition between network character init & tile manager Awake
        if (networkCharacterTransforms == null || networkCharacterTransforms.Count == 0)
        {
            networkCharacterTransforms = new List<Transform>();
            var players = FindObjectOfType<NetworkGameManager>()?.playersInGame;
            for (int i = 0; players != null && i < players.Count; i++)
            {
                networkCharacterTransforms.Add(players[i].transform);
            }
            return GameObject.FindWithTag("Player").transform;
        }
        Transform farthest = networkCharacterTransforms[0];
        print(networkCharacterTransforms.Count);
        for (int i = 1; i < networkCharacterTransforms.Count; i++)
        {
            if (networkCharacterTransforms[i].position.z > farthest.position.z)
            {
                farthest = networkCharacterTransforms[i];
            }
        }
        return farthest;
    }
}
