using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCursor : MonoBehaviour
{
    public Texture2D[] cursorTextures;
    public float animationInterval = 1f;
    private int currentCursorIndex = 0;

    void Start()
    {
        InvokeRepeating(nameof(AnimateCursor), 0f, animationInterval);
    }

    void AnimateCursor()
    {
        Cursor.SetCursor(cursorTextures[currentCursorIndex], Vector2.zero, CursorMode.Auto);

        currentCursorIndex = (currentCursorIndex + 1) % cursorTextures.Length;
    }
}
