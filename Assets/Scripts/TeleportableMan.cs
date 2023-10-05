using UnityEngine;

public class TeleportableMan : MonoBehaviour
{
    public bool AlreadyTeleported { get; private set; }
    public float portalActivationRadius = 2f;
    private Collider[] portalCollidersBuffer = new Collider[1];

    void Update()
    {
        DetectPortal();
    }

    private void DetectPortal()
    {
        var hits = Physics.OverlapSphereNonAlloc(this.transform.position, portalActivationRadius, portalCollidersBuffer, 1 << Portal.PortalLayer);

        if (hits > 0)
        {
            if (!AlreadyTeleported)
            {
                var portal = portalCollidersBuffer[0].GetComponent<Portal>();
                portal.Teleport(transform);
                AlreadyTeleported = true;
            }
        }
        else
        {
            AlreadyTeleported = false;
        }
    }
}
