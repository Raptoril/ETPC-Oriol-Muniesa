using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Transform tilePrefab;
    public int tileCount = 15;

    private List<Transform> _tiles;

    void Awake()
    {
        _tiles = new List<Transform>();
        Vector3 spawnPosition = Vector3.zero;

        for (int i = 0; i < tileCount; i++)
        {
            Transform tile = Instantiate(tilePrefab, transform);
            tile.transform.position = spawnPosition;
            
            TileController tileCtr = tile.GetComponent<TileController>();
            spawnPosition = tileCtr.pivotBack.position;
            _tiles.Add(tile);
        }
    }

    void Update()
    {
        
    }
}
