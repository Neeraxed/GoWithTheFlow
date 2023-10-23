using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] groundTiles;

    private Vector3 nextSpawnPoint;

    private void Start()
    {
        SpawnTile(0);        
        SpawnTile(2);        
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject temp = Instantiate(groundTiles[tileIndex], nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(0).transform.position;
    }


}
