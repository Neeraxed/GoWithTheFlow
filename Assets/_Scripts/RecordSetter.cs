using UnityEngine;
using UnityEngine.UI;

public class RecordSetter : MonoBehaviour
{
    [SerializeField] private Text highScore;

    private void Awake()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}