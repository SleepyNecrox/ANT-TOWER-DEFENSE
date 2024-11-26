using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerTXT;
    [SerializeField] private TextMeshProUGUI player1GoldTXT;
    [SerializeField] private TextMeshProUGUI player2GoldTXT;
    private float currentTime;

    private Gold player1Gold;
    private Gold player2Gold;

    void Start()
    {
        Gold[] allGoldScripts = FindObjectsOfType<Gold>();
        foreach (Gold goldScript in allGoldScripts)
        {
            if (goldScript.playerID == 1) player1Gold = goldScript;
            if (goldScript.playerID == 2) player2Gold = goldScript;
        }

        UpdateGold();
        currentTime = 300f;;
        StartCoroutine(AddGold());
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            currentTime = 0;
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerTXT.text = $"{minutes:0}:{seconds:00}";
    }

    private IEnumerator AddGold()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(2f);
            player1Gold.AddGold(1);
            player2Gold.AddGold(1);
            UpdateGold();
        }
    }

    public void UpdateGold()
    {
        player1GoldTXT.text = $"{player1Gold.gold} Gold";
        player2GoldTXT.text = $"{player2Gold.gold} Gold";
    }
}
