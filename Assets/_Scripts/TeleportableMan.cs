using UnityEngine;

public class TeleportableMan : MonoBehaviour
{
    public bool AlreadyTeleported { get; private set; }

    [SerializeField] private float _portalActivationRadius = 2f;

    private Collider[] _portalCollidersBuffer = new Collider[1];

    private void DetectPortal()
    {
        var hits = Physics.OverlapSphereNonAlloc(this.transform.position, _portalActivationRadius, _portalCollidersBuffer, 1 << Portal.PortalLayer);

        if (hits > 0)
        {
            if (!AlreadyTeleported)
            {
                var portal = _portalCollidersBuffer[0].GetComponent<Portal>();
                portal.Teleport(transform);
                AlreadyTeleported = true;
            }
        }
        else
        {
            AlreadyTeleported = false;
        }
    }

    private void OnEnable()
    {
        Portal.PortalTriggered += DetectPortal;
    }

    private void OnDisable()
    {
        Portal.PortalTriggered -= DetectPortal;
    }
}
