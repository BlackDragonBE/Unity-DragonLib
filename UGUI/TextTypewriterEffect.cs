using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextTypewriterEffect : MonoBehaviour
{
    public float TimeBetweenLetters = 0.05f;
    public bool StartTypingOnStart = false;

    [Header("Optional")]
    public AudioClip SoundOnEachType;

    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Start()
    {
        if (StartTypingOnStart)
        {
            StartTypeWriter();
        }
    }

    public void StartTypeWriter()
    {
        StartCoroutine(DoTyping());
    }

    private IEnumerator DoTyping()
    {
        char[] bytes = _text.text.ToCharArray();

        _text.text = "";

        foreach (char c in bytes)
        {
            _text.text += c;

            if (SoundOnEachType)
            {
                AudioSource.PlayClipAtPoint(SoundOnEachType, transform.position);
            }

            yield return new WaitForSeconds(TimeBetweenLetters);
        }
    }
}