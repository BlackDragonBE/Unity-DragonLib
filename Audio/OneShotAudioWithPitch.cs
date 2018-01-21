using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class OneShotAudioWithPitch : MonoBehaviour
{
    public AudioClip Clip;
    public float Pitch;

    private void Start()
    {
        if (Clip)
        {
            GetComponent<AudioSource>().pitch = Pitch;
            GetComponent<AudioSource>().clip = Clip;
            GetComponent<AudioSource>().loop = false;
            GetComponent<AudioSource>().Play();
        }
    }

    private void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
    }
}