using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] groundTiles;

    private GameObject currentTile;

    private Vector3 nextSpawnPoint;

    private void Start()
    {
        currentTile = groundTiles[Random.Range(0, groundTiles.Length)];
        currentTile.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
            SpawnTile();
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject temp = Instantiate(groundTiles[tileIndex], nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(0).transform.position;
    }

    public void SpawnTile()
    {
        currentTile.SetActive(false);
        currentTile = groundTiles[Random.Range(0, groundTiles.Length)];
        currentTile.SetActive(true);
    }
}
