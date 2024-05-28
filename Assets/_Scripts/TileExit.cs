using UnityEngine;

public class TileExit : MonoBehaviour
{
    [SerializeField] private TileManager _tileManager;
    [SerializeField] private PlayerMovement _playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter tileExit");
        _tileManager.SpawnTile();
        _playerMovement.gameObject.transform.position = Vector3.zero;
        _playerMovement.gameObject.transform.rotation = Quaternion.identity;
    }
}
