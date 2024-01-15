using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
public class GameManager : MonoBehaviour
{
    public static int CollectedAmount;

    [SerializeField] private TextMeshProUGUI collectedAmount;
    [SerializeField] private GameObject restartScreen;
    [SerializeField] private GameObject renderedParts;
    [SerializeField] private float timeOfVulnerability;
    [SerializeField] private ParticleSystem explosionPaticleSystem;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerBehaviour playerBehaviour;
    [SerializeField] private TextMeshProUGUI finishText;
    [SerializeField] private TextMeshProUGUI highScore;
    private const string YandexLeaderBoardName = "Score";
    
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
        playerBehaviour.isInvulnerable = true;
        //transform.localPosition += Vector3.back * 3f;
        StartCoroutine(BecomeVulnerable());
        StartCoroutine(Blink());
        StartCoroutine(SetTimeScaleToNormal());
    }
    private void OnEnable()
    {
        UpdateHighScore();
        CollectableMan.CollectedAmountChanged += ChangeCollectedAmount;
        playerBehaviour.PlayerDied.AddListener(PlayerDied);
        playerBehaviour.ReachedFinish.AddListener(PlayerReachedFinish);
    }
    private void OnDisable()
    {
        CollectableMan.CollectedAmountChanged -= ChangeCollectedAmount;
        playerBehaviour.PlayerDied.RemoveListener(PlayerDied);
        playerBehaviour.ReachedFinish.RemoveListener(PlayerReachedFinish);
    }
    private IEnumerator BecomeVulnerable()
    {
        yield return new WaitForSeconds(timeOfVulnerability);
        playerBehaviour.isInvulnerable = false;
    }

    private IEnumerator SetTimeScaleToNormal()
    {
        while (Time.timeScale < 1f)
        {
            yield return new WaitForSeconds(timeOfVulnerability);
            Time.timeScale += 0.2f;
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

    private void PlayerDied()
    {
        explosionPaticleSystem.Emit(1000);
        playerMovement.enabled = false;
        restartScreen.SetActive(true);
    }

    private void PlayerReachedFinish()
    {
        finishText.gameObject.SetActive(true);
    }

    private void ChangeCollectedAmount()
    {
        collectedAmount.text = "Частиц собрано: " + CollectedAmount;
        CheckHighScore();
    }

    private void CheckHighScore()
    {
        if (CollectedAmount > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", CollectedAmount);
            UpdateHighScore();
            YandexGame.NewLeaderboardScores(YandexLeaderBoardName,CollectedAmount);
        }
    }
    
    private void UpdateHighScore() => highScore.text = "Рекорд: " + PlayerPrefs.GetInt("HighScore", 0);
}