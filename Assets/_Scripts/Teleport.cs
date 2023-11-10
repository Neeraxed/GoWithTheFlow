using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public static bool canTeleport = true;
    [SerializeField] private CharacterController cc;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float teleportationTimer = 1f;
    
    private void OnTriggerEnter(Collider _col)
    {
        if (_col.gameObject.CompareTag("Player") && canTeleport)
        {
            cc.enabled = false;
            cc.gameObject.transform.localPosition = targetTransform.localPosition;
            //cc.gameObject.transform.localRotation = targetTransform.localRotation;
            canTeleport = false;
            StartCoroutine(PauseBetweenTeleportations(teleportationTimer));
            cc.enabled = true;
        }
    }

    private IEnumerator PauseBetweenTeleportations(float timeinSeconds)
    {
        yield return new WaitForSeconds(timeinSeconds);
        canTeleport = true;
    }
}
