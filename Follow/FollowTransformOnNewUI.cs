using UnityEngine;
using System.Collections;

public class FollowTransformOnNewUI : MonoBehaviour
{
    public Transform Target;
    public Vector3 ScreenOffset;

    private Canvas _currentCanvas;
    private Camera _cam;

    // Use this for initialization
    void Start()
    {
        _currentCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().anchoredPosition = GetScreenPosition(Target, _currentCanvas, _cam) + ScreenOffset;
    }

    public Vector3 GetScreenPosition(Transform transform, Canvas canvas, Camera cam)
    {
        Vector3 pos;
        float width = canvas.GetComponent<RectTransform>().sizeDelta.x;
        float height = canvas.GetComponent<RectTransform>().sizeDelta.y;
        float x = Camera.main.WorldToScreenPoint(transform.position).x / Screen.width;
        float y = Camera.main.WorldToScreenPoint(transform.position).y / Screen.height;
        pos = new Vector3(width * x - width / 2, y * height - height / 2);
        return pos;
    }
}
