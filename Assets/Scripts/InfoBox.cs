using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoBox : MonoBehaviour
{
    public GameObject infoBox;
    public TextMeshProUGUI goldText;
    private AntStats currentAntStats;
    private CardBehaviour currentCard;

    private Gold playerGold;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }

    public void ShowInfo(AntStats antStats, CardBehaviour cardBehaviour, Gold playerGoldRef)
    {
        currentCard = cardBehaviour;
        currentAntStats = antStats;
        playerGold = playerGoldRef; 
        infoBox.SetActive(true);
        UpdateGoldText();
    }

    public void Close()
    {
        audioManager.PlaySFX(audioManager.InfoBoxPopUp);
        infoBox.SetActive(false);
    }

    public void UpgradeAnt()
    {
        if (playerGold.CanAfford(currentAntStats.upgradeCost) && currentCard.level < 3)
        {
            playerGold.SpendGold(Mathf.RoundToInt(currentAntStats.upgradeCost));
            currentCard.Upgrade();
            currentCard.UpdateInfoBox();
            UpdateGoldText();
        }
    
    }

    private void UpdateGoldText()
    {
        goldText.text = $"{playerGold.gold}";
    }
}
