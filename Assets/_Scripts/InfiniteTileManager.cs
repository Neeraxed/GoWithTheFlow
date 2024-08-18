using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InfiniteTileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tiles;
    [SerializeField] private GameObject startTile;
    [SerializeField] private PlayerBehaviour playerBehaviour;
    [SerializeField] private int startingTileCount;

    private Vector3 currentTileLocation = Vector3.zero;
    private GameObject prevTile;
    private Queue<GameObject> existingTiles = new Queue<GameObject>();

    private void OnEnable()
    {
        playerBehaviour.ReachedTileEnd.AddListener(DeleteTile);
        playerBehaviour.ReachedTileEnd.AddListener(SpawnTile);
    }

    private void OnDisable()
    {
        playerBehaviour.ReachedTileEnd.RemoveListener(DeleteTile);
        playerBehaviour.ReachedTileEnd.RemoveListener(SpawnTile);
    }

    private void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        SpawnStartTile();

        for (int i = 0; i < startingTileCount; i++)
        {
            SpawnTile();
        }
    }

    private void SpawnTile()
    {
        prevTile = Instantiate(SelectRandomGameobject(tiles), currentTileLocation, Quaternion.identity);
        existingTiles.Enqueue(prevTile);
        currentTileLocation.z += prevTile.GetComponentInChildren<Renderer>().bounds.size.z;
    }

    private void SpawnStartTile()
    {
        prevTile = Instantiate(startTile, currentTileLocation, Quaternion.identity);
        existingTiles.Enqueue(prevTile);
        currentTileLocation.z += prevTile.GetComponentInChildren<Renderer>().bounds.size.z;
    }

    private void DeleteTile()
    {
        if(existingTiles.Count > 4)
            Destroy(existingTiles.Dequeue());
    }

    private GameObject SelectRandomGameobject(GameObject[] gameObjects)
    {
        if (gameObjects.Length == 0) return null;
        return gameObjects[Random.Range(0, gameObjects.Length)];
    }
}