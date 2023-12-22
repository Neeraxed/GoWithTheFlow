using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TurnGameObjectOnOff(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void SoundTurnOnOff()
    {
        AudioListener.pause = !AudioListener.pause;
    }
}
