using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.3F;
    [SerializeField] private Transform followingObject;

    private Vector3 _velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 targetPosition = followingObject.TransformPoint(new Vector3(0, 1f, -3f));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        transform.LookAt(followingObject);
    }
}
