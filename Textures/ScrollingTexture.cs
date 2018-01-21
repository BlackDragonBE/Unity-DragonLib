using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    public float scrollSpeed = .5f;
    public float offset;

    public float XScroll = 1f;
    public float YScroll = 1f;

    private void Update()
    {
        offset += (Time.deltaTime * scrollSpeed) / 10.0f;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset * XScroll, offset * YScroll));
    }
}