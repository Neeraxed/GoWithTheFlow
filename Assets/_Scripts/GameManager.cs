using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class GameManager : MonoBehaviour
{
    public static int CollectedAmount;

    [SerializeField] private Text collectedAmount;
    [SerializeField] private GameObject restartScreen;
    [SerializeField] private GameObject renderedObjects;
    [SerializeField] private float timeOfInvulnerability = 3f;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerBehaviour playerBehaviour;
    [SerializeField] private GameObject finishText;
    [SerializeField] private Text highScore;

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
        playerBehaviour.IsInvulnerable = true;
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
        yield return new WaitForSeconds(timeOfInvulnerability);
        playerBehaviour.IsInvulnerable = false;
    }

    private IEnumerator SetTimeScaleToNormal()
    {
        while (Time.timeScale < 1f)
        {
            yield return new WaitForSeconds(timeOfInvulnerability);
            Time.timeScale += 0.2f;
        }
    }

    private IEnumerator Blink()
    {
        for (int i = 0; i < timeOfInvulnerability * 5f; i++)
        {
            renderedObjects.SetActive((!renderedObjects.activeSelf));
            yield return new WaitForSeconds(0.2f);
        }
        renderedObjects.SetActive(true);
    }

    private void PlayerDied()
    {
        explosion.Emit(1000);
        playerMovement.enabled = false;
        restartScreen.SetActive(true);
    }

    private void PlayerReachedFinish()
    {
        finishText.gameObject.SetActive(true);
    }

    private void ChangeCollectedAmount()
    {
        collectedAmount.text = CollectedAmount.ToString();
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
    
    private void UpdateHighScore() => highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
}