using UnityEngine;
using UnityEngine.Events;

public class PlayerBehaviour : MonoBehaviour
{
    public UnityEvent ReachedTileEnd;
    public UnityEvent PlayerDied;
    public UnityEvent ReachedFinish;

    private bool isInvulnerable = false;

    public bool IsInvulnerable { get { return isInvulnerable; } set { isInvulnerable = value; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TileBound") &&
            other.transform.position.z > transform.position.z)
        {
            ReachedTileEnd?.Invoke();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isInvulnerable) return;
        
        Debug.Log("Entered collision with " + hit.gameObject.name);

        if (hit.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            Die();
        else if (hit.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            ReachedFinish?.Invoke();
            Time.timeScale = 0;
        }
    }

    private void Die()
    {
        PlayerDied?.Invoke();
    }
}
