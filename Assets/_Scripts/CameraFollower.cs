using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform followingObject;

    public float smoothTime = 0.3F;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 targetPosition = followingObject.TransformPoint(new Vector3(0, 1.5f, -1.5f));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.LookAt(followingObject);
    }
}
