using System;
using System.Collections;
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
    [SerializeField] private GameObject restartScreen;
    [SerializeField] private GameObject renderedParts;
    [SerializeField] private float timeOfVulnerability;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private PlayerMovement playerMovement;
    
    private bool isInvulnerable = false;

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
        if (other.gameObject.layer == LayerMask.NameToLayer("TileBound") &&
            other.transform.position.z > transform.position.z)
        {
            reachedTileEnd?.Invoke();
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
            finishText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void RestartScene()
    {
        Debug.Log("RestartScene");
        restartScreen.SetActive(false);
        playerMovement.enabled = true;
        CollectedAmount = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueScene()
    {
        Debug.Log("ContinueScene");
        restartScreen.SetActive(false);
        playerMovement.enabled = true;
        Time.timeScale = 0.7f;
        isInvulnerable = true;
        //transform.localPosition += Vector3.back * 3f;
        StartCoroutine(BecomeVulnerable());
        StartCoroutine(Blink());
        StartCoroutine(SetTimeScaleToNormal());
    }

    private IEnumerator BecomeVulnerable()
    {
        yield return new WaitForSeconds(timeOfVulnerability);
        isInvulnerable = false;
    }       
    private IEnumerator SetTimeScaleToNormal()
    {
        while (Time.timeScale < 1f)
        {
            yield return new WaitForSeconds(timeOfVulnerability);
            Time.timeScale += 0.2f;
            Debug.Log(Time.timeScale);
        }
    }   
    
    private IEnumerator Blink()
    {
        for (int i = 0; i < timeOfVulnerability * 5f; i++)
        {
            renderedParts.SetActive((!renderedParts.activeSelf));
            yield return new WaitForSeconds(0.2f);
        }
        renderedParts.SetActive(true);
    }

    private void Die()
    {
        //TODO shaking screen + particle system
        particleSystem.Emit(150);
        playerMovement.enabled = false;
        restartScreen.SetActive(true);
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
