using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static bool IsSoundOff;

    [SerializeField] private AudioSource backgroundSoundtrack;
    [SerializeField] private AudioSource clickSound;

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "Click":
                clickSound.Play();
                break;
            case "Background":
                backgroundSoundtrack.Play();
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