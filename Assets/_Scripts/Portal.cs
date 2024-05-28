using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public static int PortalLayer => LayerMask.NameToLayer("Portal");
    public static event Action PortalTriggered;

    [SerializeField] private Portal _connectedPortal;
    [SerializeField] private Transform _spawnPoint;

    private Vector3 SpawnPosition => _spawnPoint.position;
    private Quaternion SpawnRotation => this.transform.rotation;

    public void Teleport(Transform requested)
    {
        var destination = _connectedPortal.SpawnPosition;
        var rotation = _connectedPortal.SpawnRotation;

        requested.position = destination;
        requested.rotation = rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        PortalTriggered?.Invoke();
    }
}
