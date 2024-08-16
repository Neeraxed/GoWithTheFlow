using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public static int PortalLayer => LayerMask.NameToLayer("Portal");
    public static event Action PortalTriggered;

    [SerializeField] private Portal connectedPortal;
    [SerializeField] private Transform spawnPoint;

    private Vector3 SpawnPosition => spawnPoint.position;
    private Quaternion SpawnRotation => this.transform.rotation;

    public void Teleport(Transform requested)
    {
        var destination = connectedPortal.SpawnPosition;
        var rotation = connectedPortal.SpawnRotation;

        requested.position = destination;
        requested.rotation = rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        PortalTriggered?.Invoke();
    }
}