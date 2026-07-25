using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
