using UnityEngine;

public class InfiniteTileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tiles;
    [SerializeField] private PlayerBehaviour playerBehaviour;

    private Vector3 currentTileLocation = Vector3.zero;
    private GameObject prevTile;

    private void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        SpawnTile();

        playerBehaviour.reachedTileEnd.AddListener(DeleteTile);
        playerBehaviour.reachedTileEnd.AddListener(SpawnTile);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SpawnTile();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SpawnTile();
        }
    }

    private void SpawnTile()
    {
        prevTile = GameObject.Instantiate(SelectRandomGameobject(tiles), currentTileLocation, Quaternion.identity);
        currentTileLocation.z += prevTile.GetComponentInChildren<Renderer>().bounds.size.z;
    }

    private void DeleteTile()
    {
        Destroy(prevTile);
    }

    private GameObject SelectRandomGameobject(GameObject[] gameObjects)
    {
        if (gameObjects.Length == 0) return null;
        return gameObjects[Random.Range(0, gameObjects.Length)];
    }
}
