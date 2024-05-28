using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InfiniteTileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _tiles;
    [SerializeField] private GameObject _startTile;
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private int _startingTileCount;

    private Vector3 _currentTileLocation = Vector3.zero;
    private GameObject _prevTile;
    private Queue<GameObject> _existingTiles = new Queue<GameObject>();

    private void OnEnable()
    {
        _playerBehaviour.ReachedTileEnd.AddListener(DeleteTile);
        _playerBehaviour.ReachedTileEnd.AddListener(SpawnTile);
    }

    private void OnDisable()
    {
        _playerBehaviour.ReachedTileEnd.RemoveListener(DeleteTile);
        _playerBehaviour.ReachedTileEnd.RemoveListener(SpawnTile);
    }

    private void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        SpawnStartTile();

        for (int i = 0; i < _startingTileCount; i++)
        {
            SpawnTile();
        }
    }

    private void SpawnTile()
    {
        _prevTile = Instantiate(SelectRandomGameobject(_tiles), _currentTileLocation, Quaternion.identity);
        _existingTiles.Enqueue(_prevTile);
        _currentTileLocation.z += _prevTile.GetComponentInChildren<Renderer>().bounds.size.z;
    }

    private void SpawnStartTile()
    {
        _prevTile = Instantiate(_startTile, _currentTileLocation, Quaternion.identity);
        _existingTiles.Enqueue(_prevTile);
        _currentTileLocation.z += _prevTile.GetComponentInChildren<Renderer>().bounds.size.z;
    }

    private void DeleteTile()
    {
        if(_existingTiles.Count > 4)
            Destroy(_existingTiles.Dequeue());
    }

    private GameObject SelectRandomGameobject(GameObject[] gameObjects)
    {
        if (gameObjects.Length == 0) return null;
        return gameObjects[Random.Range(0, gameObjects.Length)];
    }
}
