using System.Collections;
using System.Collections.Generic;
using Script.Utilities;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float spawnPos = 0;
    public float tileLength = 30;
    public int numberOfTiles = 6; //no. of tiles want to be shown on the screen

    public Transform character;
    private List<GameObject> activeTiles = new List<GameObject>();
    private List<GameObject> tilePool = new List<GameObject>();

    public void Start()
    {
        InstantiateTiles();
        InitializeTiles();
    }

    private void InstantiateTiles()
    {
        foreach(var tile in tilePrefabs)
        {
            for (int i = 0; i < 2; i++)
            {
                var t = Instantiate(tile);
                t.SetActive(false);
                tilePool.Add(t);
            }
        }

        tilePool = ListUtility.Shuffle(tilePool);
    }

    private void InitializeTiles()
    {
        for(int i = 0; i < numberOfTiles; i++)
        {
            if(i == 0)
               SpawnTile(0);
            else
               SpawnTile(Random.Range(1, tilePool.Count));
        }
    }

    
    void Update()
    {
        if(character.position.z - 35 > spawnPos - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(1, tilePool.Count));
            DeleteTile();    // delete the odd tile whenever a new tile has been created
        }
    }

    public void SpawnTile(int tileIndex)
    {
        Debug.Log("SpawnTile " + tileIndex);
        Debug.Log(tilePool.Count);
        
        GameObject gameObject = tilePool[tileIndex];
        tilePool.RemoveAt(tileIndex);
        
        activeTiles.Add(gameObject);
        var objectTransform = transform;
        gameObject.transform.position = objectTransform.forward * spawnPos;
        gameObject.transform.rotation = objectTransform.rotation;
        gameObject.SetActive(true);

        spawnPos += tileLength;
    }

    private void DeleteTile()
    {
        //Destroy(activeTiles[0]);
        GameObject gameObject = activeTiles[0];
        activeTiles.RemoveAt(0);
        tilePool.Add(gameObject);
    }
}
