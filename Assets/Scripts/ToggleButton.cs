using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public RectTransform cardsGroup;
    private Vector3 originalPosition;
    public Button showButton;
    public Button hideButton;

    void Start()
    {
        originalPosition = new Vector3(-650f, -5f, 0f);
        showButton.gameObject.SetActive(true);
        hideButton.gameObject.SetActive(false);

        showButton.onClick.AddListener(Show);
        hideButton.onClick.AddListener(Hide);
    }

    void Show()
    {
        showButton.gameObject.SetActive(false);
        hideButton.gameObject.SetActive(true);
    }

    void Hide()
    {
        hideButton.gameObject.SetActive(false);
        showButton.gameObject.SetActive(true);
    }

    public void GoUp()
    {
        cardsGroup.anchoredPosition = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z);
    }

    public void GoDown()
    {
        cardsGroup.anchoredPosition = new Vector3(originalPosition.x, originalPosition.y - 125f, originalPosition.z);
    }
}