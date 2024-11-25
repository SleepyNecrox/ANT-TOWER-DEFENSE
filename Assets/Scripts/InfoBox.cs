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
    private Timer timer;

    void Start()
    {
        timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
    }
    public void ShowInfo(AntStats antStats, CardBehaviour cardBehaviour)
    {
        currentCard = cardBehaviour;
        currentAntStats = antStats;
        infoBox.SetActive(true);
    }

    public void Close()
    {
        infoBox.SetActive(false);
    }

    public void UpgradeAnt()
    {
        if (Timer.playerGold >= currentAntStats.upgradeCost && currentCard.level < 3)
        {
            Timer.playerGold -= Mathf.RoundToInt(currentAntStats.upgradeCost);
            timer.UpdateGold();
            currentCard.Upgrade();

            currentCard.UpdateInfoBox();
        }
    }
}
