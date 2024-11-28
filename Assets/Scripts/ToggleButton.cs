using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{

    public Animator cardsUI;
    public RectTransform cardsGroup;
    private Vector3 originalPosition;
    public Button showButton;
    public Button hideButton;

    void Start()
    {
        showButton.gameObject.SetActive(true);
        hideButton.gameObject.SetActive(false);

        showButton.onClick.AddListener(Show);
        hideButton.onClick.AddListener(Hide);
    }

    public void Show()
    {
        cardsUI.SetBool("Show", true);
        showButton.gameObject.SetActive(false);
        hideButton.gameObject.SetActive(true);
    }

    public void Hide()
    {
        cardsUI.SetBool("Show", false);
        hideButton.gameObject.SetActive(false);
        showButton.gameObject.SetActive(true);
    }

   
}