using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal connectedPortal;
    public Transform spawnPoint;
    private Vector3 SpawnPosition => spawnPoint.position;
    private Quaternion SpawnRotation => this.transform.rotation;

    public static int PortalLayer => LayerMask.NameToLayer("Portal");

    public void Teleport(Transform requested)
    {
        var destination = connectedPortal.SpawnPosition;
        var rotation = connectedPortal.SpawnRotation;

        requested.position = destination;
        requested.rotation = rotation;
    }
}
