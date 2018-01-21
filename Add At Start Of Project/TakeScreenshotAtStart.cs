using System;
using System.IO;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class TakeScreenshotAtStart : MonoBehaviour
{
    //References

    //Public
    public float MinDelayBeforeScreenshot = 1f;
    public float MaxDelayBeforeScreenshot = 1f;

    //Private

    void Start()
    {
        if (!Directory.Exists("screenshots"))
        {
            Directory.CreateDirectory("screenshots");
        }

        StartCoroutine(TakeScreenshot());
    }

    private IEnumerator TakeScreenshot()
    {
        yield return new WaitForSeconds(Random.Range(MinDelayBeforeScreenshot,MaxDelayBeforeScreenshot));
        ScreenCapture.CaptureScreenshot("screenshots/" + "screenshot" + DateTime.Now.ToString("yyMMdd") + ".png");
        //Debug.Log("Screenshot made");
    }
}
