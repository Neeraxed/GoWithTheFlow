using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public static bool canTeleport = true;

    [SerializeField] private CharacterController _cc;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _teleportationTimer = 1f;
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && canTeleport)
        {
            _cc.enabled = false;
            _cc.gameObject.transform.localPosition = _targetTransform.localPosition;
            canTeleport = false;
            StartCoroutine(PauseBetweenTeleportations(_teleportationTimer));
            _cc.enabled = true;
        }
    }

    private IEnumerator PauseBetweenTeleportations(float timeinSeconds)
    {
        yield return new WaitForSeconds(timeinSeconds);
        canTeleport = true;
    }
}
