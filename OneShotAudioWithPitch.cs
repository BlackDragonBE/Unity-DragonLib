using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class OneShotAudioWithPitch : MonoBehaviour
{
    public AudioClip Clip;
    public float Pitch;

    //Setup of things that depend on other components
    void Start()
    {
        if (Clip)
        {
            GetComponent<AudioSource>().pitch = Pitch;
            GetComponent<AudioSource>().clip = Clip;
            GetComponent<AudioSource>().loop = false;
            GetComponent<AudioSource>().Play();
        }
    }

    void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
