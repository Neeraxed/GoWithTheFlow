using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerBehaviour : MonoBehaviour
{
    public static int CollectedAmount;

    [SerializeField] private TextMeshProUGUI finishText;
    [SerializeField] private TextMeshProUGUI collectedAmount;
    [SerializeField] private TextMeshProUGUI highScore;

    public UnityEvent reachedTileEnd;

    public void ChangeCollectedAmount()
    {
        collectedAmount.text = "Частиц собрано: " + CollectedAmount;
        CheckHighScore();
    }
    private void OnEnable()
    {
        UpdateHighScore();
        CollectableMan.CollectedAmountChanged += ChangeCollectedAmount;
    }
    private void OnDisable()
    {
        CollectableMan.CollectedAmountChanged -= ChangeCollectedAmount;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TileBound") && other.transform.position.z > transform.position.z)
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void CheckHighScore()
    {
        if (CollectedAmount > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", CollectedAmount);
            UpdateHighScore();
        }
    }
    private void UpdateHighScore() => highScore.text = "Рекорд: " + PlayerPrefs.GetInt("HighScore", 0);
}
