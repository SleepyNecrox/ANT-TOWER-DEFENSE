using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class CardBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] GameObject antPrefab;
    [SerializeField] GameObject antPrefab2;
    [SerializeField] GameObject antPrefab3;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private float cooldown;
    [SerializeField] private int goldCost;
    private bool isCooldown;
    private HorizontalLayoutGroup parentLayoutGroup;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private int originalSiblingIndex;
    private Timer timer;
    private Gold playerGold;
    [SerializeField] private TextMeshProUGUI cdText;
    [SerializeField] private GameObject infoBox;

    [SerializeField] private TextMeshProUGUI nameTXT;
    [SerializeField] private TextMeshProUGUI hpTXT;
    [SerializeField] private TextMeshProUGUI dmgTXT;
    [SerializeField] private TextMeshProUGUI spdTXT;
    [SerializeField] private TextMeshProUGUI upgradeTXT;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private GameObject button;
    private AntStats antStats;
    [SerializeField] internal int level = 1;
    [SerializeField] private int playerID;

    [SerializeField] private GameObject rightClickIcon;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        Gold[] allGoldScripts = FindObjectsOfType<Gold>();
        foreach (Gold goldScript in allGoldScripts)
        {
            if (goldScript.playerID == playerID)
            {
                playerGold = goldScript;
                break;
            }
        }

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
        audioManager.PlaySFX(audioManager.CardHover);
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

            audioManager.PlaySFX(audioManager.Retro);
            if (level == 1 && playerGold.CanAfford(goldCost))
            {
                playerGold.gold -= goldCost;
                timer.UpdateGold();
                PhotonNetwork.Instantiate(antPrefab.name, spawnLocation.position, Quaternion.identity);
                StartCoroutine(StartCooldown());
            }
            else if (level == 2 && playerGold.CanAfford(goldCost))
            {
                playerGold.gold -= goldCost;
                timer.UpdateGold();
                PhotonNetwork.Instantiate(antPrefab2.name, spawnLocation.position, Quaternion.identity);
                StartCoroutine(StartCooldown());
            }
            else if (level == 3 && playerGold.CanAfford(goldCost))
            {
                playerGold.gold -= goldCost;
                timer.UpdateGold();
                PhotonNetwork.Instantiate(antPrefab3.name, spawnLocation.position, Quaternion.identity);
                StartCoroutine(StartCooldown());
            }
        }

    if (eventData.button == PointerEventData.InputButton.Right)
    {
        audioManager.PlaySFX(audioManager.Select);
        rightClickIcon.SetActive(false);
        infoBox.SetActive(true);
        InfoBox infoBoxScript = infoBox.GetComponent<InfoBox>();
        infoBoxScript.ShowInfo(antStats, this, playerGold); 
        UpdateInfoBox();
    }
    }

    public void Upgrade()
    {
        audioManager.PlaySFX(audioManager.Button);
        level += 1;
    }

    public void UpdateInfoBox()
    {
        if (level == 1)
        {
            antStats = antPrefab.GetComponent<AntStats>();
            button.SetActive(true);
        }
        else if (level == 2)
        {
            antStats = antPrefab2.GetComponent<AntStats>();
            button.SetActive(true);
        }
        else if (level == 3)
        {
            antStats = antPrefab3.GetComponent<AntStats>();
            button.SetActive(false);
        }

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

