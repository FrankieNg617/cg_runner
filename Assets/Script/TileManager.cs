using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float spawnPos = 0;
    public float tileLength = 30;
    public int numberOfTiles = 6; //no. of tiles want to be shown on the screen

    public Transform character;
    private List<GameObject> activeTiles = new List<GameObject>();
    
    async void Start()
    {
        for(int i = 0; i < numberOfTiles; i++)
        {
            if(i == 0)
               SpawnTile(0);
            else
               SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
       
    }

    
    void Update()
    {
        if(character.position.z -35 > spawnPos - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();    //delete the odd tile whenever a new tile has been created
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go =  Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPos, transform.rotation);
        activeTiles.Add(go);
        spawnPos += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
