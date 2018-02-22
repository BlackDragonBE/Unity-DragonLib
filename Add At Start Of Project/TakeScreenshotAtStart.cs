using System;
using System.Collections;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class TakeScreenshotAtStart : MonoBehaviour
{
    public float MinDelayBeforeScreenshot = 1f;
    public float MaxDelayBeforeScreenshot = 1f;

    public int MaximumAmountOfScreenshotsPerPlaySession = 1;

    private void Start()
    {
        if (!Directory.Exists("Screenshots"))
        {
            Directory.CreateDirectory("Screenshots");
        }

        StartCoroutine(TakeScreenshots());
    }

    private IEnumerator TakeScreenshots()
    {
        for (int i = 0; i < MaximumAmountOfScreenshotsPerPlaySession; i++)
        {
            yield return new WaitForSeconds(Random.Range(MinDelayBeforeScreenshot, MaxDelayBeforeScreenshot));
            CreateScreenshot();
        }
    }

    [ContextMenu("Take Screenshot")]
    public void CreateScreenshot()
    {
        ScreenCapture.CaptureScreenshot("Screenshots/" + "screenshot" + DateTime.Now.ToString("yyyy-MM-dd.hh-mm") + ".png");
    }
}