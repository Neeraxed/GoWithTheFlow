using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static bool IsSoundOff;

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
        IsSoundOff = !IsSoundOff;
    }

    public void StopPlayEverything(bool par)
    {
        AudioListener.pause = par;
        IsSoundOff = par;
    }
}
