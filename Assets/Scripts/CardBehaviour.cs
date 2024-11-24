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

    [SerializeField] private TextMeshProUGUI cdText;
    [SerializeField] private GameObject infoBox;

    [SerializeField] private TextMeshProUGUI nameTXT;
    [SerializeField] private TextMeshProUGUI hpTXT;
    [SerializeField] private TextMeshProUGUI dmgTXT;
    [SerializeField] private TextMeshProUGUI spdTXT;
    [SerializeField] private TextMeshProUGUI upgradeTXT;

    [SerializeField] private Button upgradeButton;
    private AntStats antStats;

    void Start()
    {
        timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        cdText.text = "";
        originalScale = transform.localScale;
        originalSiblingIndex = transform.GetSiblingIndex();
        parentLayoutGroup = GetComponentInParent<HorizontalLayoutGroup>();

        antStats = antPrefab.GetComponent<AntStats>();

        upgradeButton.onClick.AddListener(() =>
        {
            InfoBox infoBoxScript = infoBox.GetComponent<InfoBox>();
            infoBoxScript.UpgradeAnt();
        });
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
            InfoBox infoBoxScript = infoBox.GetComponent<InfoBox>();
            infoBoxScript.ShowInfo(antStats);
            UpdateInfoBox();
        }
        
    }

    public void UpdateInfoBox()
    {
        antStats.upgradeCost = Mathf.Ceil(antStats.upgradeCost);
        nameTXT.text = antStats.antName;
        hpTXT.text = $"HP: {antStats.health}";
        dmgTXT.text = $"DMG: {antStats.damage}";
        spdTXT.text = $"SPD: {antStats.speed}";
        upgradeTXT.text = $" UPGRADE: {antStats.upgradeCost}";
    }

    private IEnumerator StartCooldown()
    {
        isCooldown = true;

        GetComponent<Image>().color = Color.gray;

        float remainingCooldown = cooldown;

        while (remainingCooldown > 0)
        {
            cdText.text = Mathf.CeilToInt(remainingCooldown).ToString();
            remainingCooldown -= Time.deltaTime;
            yield return null;
        }

        isCooldown = false;
        GetComponent<Image>().color = Color.white;
        cdText.text = "";
    }
}

