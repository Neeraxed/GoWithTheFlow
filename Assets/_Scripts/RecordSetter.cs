using UnityEngine;
using UnityEngine.UI;

public class RecordSetter : MonoBehaviour
{
    [SerializeField] private Text _highScore;

    private void Awake()
    {
        _highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
