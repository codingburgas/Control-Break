using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    private RawImage Background;

    private Vector2 ScrollSpeed;

    void Start()
    {
        Background = GetComponent<RawImage>();

        ScrollSpeed = new Vector2( 0.03f, 0f );
    }

    void Update()
    {
        ScrollBackground();
    }

    void ScrollBackground()
    {
        Background.uvRect = new Rect(Background.uvRect.position + ScrollSpeed * Time.deltaTime, Background.uvRect.size);
    }
}
