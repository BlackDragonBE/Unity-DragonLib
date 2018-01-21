using System;
using System.IO;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class TakeScreenshotAtStart : MonoBehaviour
{
    public float MinDelayBeforeScreenshot = 1f;
    public float MaxDelayBeforeScreenshot = 1f;

    private void Start()
    {
        if (!Directory.Exists("Screenshots"))
        {
            Directory.CreateDirectory("Screenshots");
        }

        StartCoroutine(TakeScreenshot());
    }

    private IEnumerator TakeScreenshot()
    {
        yield return new WaitForSeconds(Random.Range(MinDelayBeforeScreenshot, MaxDelayBeforeScreenshot));
        ScreenCapture.CaptureScreenshot("Screenshots/" + "screenshot" + DateTime.Now.ToString("yyMMdd") + ".png");
    }
}