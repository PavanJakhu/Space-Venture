using UnityEngine;
using System.Collections;

public class PlaySoundAtPoint : MonoBehaviour
{
    public AudioClip clip;
    public bool playOnAwake;
    [Range(0.0f, 1.0f)]
    public float volume;

    // Use this for initialization
    void Start()
    {
        if (playOnAwake)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        }
    }

    void Play()
    {
        AudioSource.PlayClipAtPoint(clip, transform.position, volume);
    }
}
