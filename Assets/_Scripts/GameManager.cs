using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class GameManager : MonoBehaviour
{
    public static int CollectedAmount;

    [SerializeField] private Text _collectedAmount;
    [SerializeField] private GameObject _restartScreen;
    [SerializeField] private GameObject _renderedParts;
    [SerializeField] private float _timeOfVulnerability;
    [SerializeField] private ParticleSystem _explosionPaticleSystem;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private GameObject _finishText;
    [SerializeField] private Text _highScore;

    private const string _YandexLeaderBoardName = "Score";
    
    public void RestartScene()
    {
        Debug.Log("RestartScene");
        _restartScreen.SetActive(false);
        _playerMovement.enabled = true;
        CollectedAmount = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueScene()
    {
        Debug.Log("ContinueScene");
        _restartScreen.SetActive(false);
        _playerMovement.enabled = true;
        Time.timeScale = 0.7f;
        _playerBehaviour.IsInvulnerable = true;
        StartCoroutine(BecomeVulnerable());
        StartCoroutine(Blink());
        StartCoroutine(SetTimeScaleToNormal());
    }

    private void OnEnable()
    {
        UpdateHighScore();
        CollectableMan.CollectedAmountChanged += ChangeCollectedAmount;
        _playerBehaviour.PlayerDied.AddListener(PlayerDied);
        _playerBehaviour.ReachedFinish.AddListener(PlayerReachedFinish);
    }

    private void OnDisable()
    {
        CollectableMan.CollectedAmountChanged -= ChangeCollectedAmount;
        _playerBehaviour.PlayerDied.RemoveListener(PlayerDied);
        _playerBehaviour.ReachedFinish.RemoveListener(PlayerReachedFinish);
    }

    private IEnumerator BecomeVulnerable()
    {
        yield return new WaitForSeconds(_timeOfVulnerability);
        _playerBehaviour.IsInvulnerable = false;
    }

    private IEnumerator SetTimeScaleToNormal()
    {
        while (Time.timeScale < 1f)
        {
            yield return new WaitForSeconds(_timeOfVulnerability);
            Time.timeScale += 0.2f;
        }
    }

    private IEnumerator Blink()
    {
        for (int i = 0; i < _timeOfVulnerability * 5f; i++)
        {
            _renderedParts.SetActive((!_renderedParts.activeSelf));
            yield return new WaitForSeconds(0.2f);
        }
        _renderedParts.SetActive(true);
    }

    private void PlayerDied()
    {
        _explosionPaticleSystem.Emit(1000);
        _playerMovement.enabled = false;
        _restartScreen.SetActive(true);
    }

    private void PlayerReachedFinish()
    {
        _finishText.gameObject.SetActive(true);
    }

    private void ChangeCollectedAmount()
    {
        _collectedAmount.text = CollectedAmount.ToString();
        CheckHighScore();
    }

    private void CheckHighScore()
    {
        if (CollectedAmount > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", CollectedAmount);
            UpdateHighScore();
            YandexGame.NewLeaderboardScores(_YandexLeaderBoardName,CollectedAmount);
        }
    }
    
    private void UpdateHighScore() => _highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
}