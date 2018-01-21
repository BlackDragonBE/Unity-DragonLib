using UnityEngine;
using System.Collections;

public class SendMail : MonoBehaviour
{

    public void EmailUs()
    {
        //email Id to send the mail to
        string email = "black.dragon.be@gmail.com";
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
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }

    string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }
}