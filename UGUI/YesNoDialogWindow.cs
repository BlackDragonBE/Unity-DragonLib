using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Reusable Yes/No Dialog
/// Attach to a window with 2 buttons , a question Text and an optional title Text
/// </summary>
public class YesNoDialogWindow : MonoBehaviour
{
    //References

    //Public
    public Button BtnYes;
    public Button BtnNo;

    public Text TxtTitle;
    public Text TxtMessageText;

    private Action _onYesAction;
    private Action _onNoAction;

    //Private

    void Awake()
    {
    }

    void Start()
    {
        BtnYes.onClick.AddListener(OnYes);
        BtnNo.onClick.AddListener(OnNo);
    }

    public void Show(string title, string message, string yesText = "Yes", string noText = "No")
    {
        if (TxtTitle)
        {
            TxtTitle.text = title;
        }

        TxtMessageText.text = message;
        BtnYes.transform.Find("Text").GetComponent<Text>().text = yesText;
        BtnNo.transform.Find("Text").GetComponent<Text>().text = noText;

        gameObject.SetActive(true);

    }

    /// <summary>
    /// Sets what happens when either Yes or No is pressed
    /// </summary>
    /// <param name="onYes">What happens when Yes is pressed</param>
    /// <param name="onNo">What happens when No is pressed</param>
    public void SetYesNoActions(Action onYes, Action onNo)
    {
        _onYesAction = null;
        _onNoAction = null;

        _onYesAction = onYes;
        _onNoAction = onNo;
    }

    private void OnYes()
    {
        if (_onYesAction != null && _onYesAction.GetInvocationList().Length > 0)
        {
            _onYesAction.Invoke();
        }

        gameObject.SetActive(false);
    }

    private void OnNo()
    {
        if (_onNoAction != null && _onNoAction.GetInvocationList().Length > 0)
        {
            _onNoAction.Invoke();
        }

        gameObject.SetActive(false);
    }
}
