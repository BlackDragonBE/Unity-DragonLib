using UnityEngine;

public class SendMail : MonoBehaviour
{
    public string EmailAdress;

    public void EmailUs()
    {
        //subject of the mail
        string subject = MyEscapeURL("FEEDBACK/SUGGESTION");
        //body of the mail which consists of Device Model and its Operating System
        string body = MyEscapeURL("- Please enter your message here -\n\n\n\n" +
         "________" +
         "\n\nPlease do not modify this:\n\n" +
         "Model: " + SystemInfo.deviceModel + "\n\n" +
            "OS: " + SystemInfo.operatingSystem + "\n\n" +
         "________");
        //Open the Default Mail App
        Application.OpenURL("mailto:" + EmailAdress + "?subject=" + subject + "&body=" + body);
    }

    private string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }
}