using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundSoundtrack;
    [SerializeField] private AudioSource _clickSound;

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "Click":
                _clickSound.Play();
                break;
            case "Background":
                _backgroundSoundtrack.Play();
                break;
        }
    }
    
    public void StopPlayEverything()
    {
        AudioListener.pause = !AudioListener.pause;
    }
}
