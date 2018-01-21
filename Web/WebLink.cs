using UnityEngine;

public class WebLink : MonoBehaviour
{
    public string URL;

    public void OpenUrl()
    {
        Application.OpenURL(URL);
    }

    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }
}