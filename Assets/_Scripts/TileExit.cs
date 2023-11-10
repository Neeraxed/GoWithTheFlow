using UnityEngine;

public class TileExit : MonoBehaviour
{
    [SerializeField] private TileManager tm;
    [SerializeField] private PlayerMovement pm;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter tileExit");
        tm.SpawnTile();
        pm.Rotate(Vector2.one, 0.1f);
    }
}
