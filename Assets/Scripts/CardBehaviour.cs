using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] GameObject antPrefab;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private float cooldown;
    [SerializeField] private int goldCost;
    private bool isCooldown;
    private HorizontalLayoutGroup parentLayoutGroup;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private int originalSiblingIndex;

    private Timer timer;

    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private GameObject infoBox;

    void Start()
    {
        timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        textMeshProUGUI.text = "";
        originalScale = transform.localScale;
        originalSiblingIndex = transform.GetSiblingIndex();
        parentLayoutGroup = GetComponentInParent<HorizontalLayoutGroup>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        parentLayoutGroup.enabled = false;
        transform.localScale = originalScale * 1.15f;
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
        transform.SetSiblingIndex(originalSiblingIndex);
        parentLayoutGroup.enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isCooldown) return;

            if (Timer.playerGold >= goldCost)
            {
                Timer.playerGold -= goldCost;
                timer.UpdateGold();
                Instantiate(antPrefab, spawnLocation.position, Quaternion.identity);
                StartCoroutine(StartCooldown());
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            infoBox.SetActive(true);
        }
        
    }

    private IEnumerator StartCooldown()
    {
        isCooldown = true;

        GetComponent<Image>().color = Color.gray;

        float remainingCooldown = cooldown;

        while (remainingCooldown > 0)
        {
            textMeshProUGUI.text = Mathf.CeilToInt(remainingCooldown).ToString();
            remainingCooldown -= Time.deltaTime;
            yield return null;
        }

        isCooldown = false;
        GetComponent<Image>().color = Color.white;
        textMeshProUGUI.text = "";
    }
}

