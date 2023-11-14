using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerBehaviour : MonoBehaviour
{
    public static int CollectedAmount;

    [SerializeField] private TextMeshProUGUI finishText;
    [SerializeField] private TextMeshProUGUI collectedAmount;

    public UnityEvent reachedTileEnd;

    private void Awake()
    {
        CollectableMan.CollectedAmountChanged += ChangeCollectedAmount;
    }

    public void ChangeCollectedAmount()
    {
        collectedAmount.text = "Souls collected: " + CollectedAmount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TileBound"))
        {
            reachedTileEnd?.Invoke();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Entered collision with " + hit.gameObject.name);

        if (hit.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            Die();
        else if (hit.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            finishText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);

        //TODO restart screen + адекватный сброс CollectedAmount

        CollectedAmount = 0;

        SceneManager.LoadScene(0);
    }
}
