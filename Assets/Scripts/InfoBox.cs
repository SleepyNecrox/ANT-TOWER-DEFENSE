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
    private Timer timer;

    void Start()
    {
        timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
    }

    public void ShowInfo(AntStats antStats)
    {
        currentAntStats = antStats;
        infoBox.SetActive(true);
    }

    public void Close()
    {
        infoBox.SetActive(false);
    }

    public void UpgradeAnt()
    {
        if (currentAntStats != null && Timer.playerGold >= currentAntStats.upgradeCost)
        {
            Timer.playerGold -= Mathf.RoundToInt(currentAntStats.upgradeCost);
            timer.UpdateGold();
            currentAntStats.Upgrade();

            CardBehaviour card = currentAntStats.GetComponent<CardBehaviour>();
            card.UpdateInfoBox();
        }
    }
}
