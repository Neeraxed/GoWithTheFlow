using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _groundTiles;

    private GameObject _currentTile;

    public void SpawnTile()
    {
        _currentTile.SetActive(false);
        _currentTile = _groundTiles[Random.Range(0, _groundTiles.Length)];
        _currentTile.SetActive(true);
    }

    private void Start()
    {
        _currentTile = _groundTiles[Random.Range(0, _groundTiles.Length)];
        _currentTile.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
            SpawnTile();
    }
}
