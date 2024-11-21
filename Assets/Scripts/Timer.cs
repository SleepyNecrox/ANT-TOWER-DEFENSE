using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerTXT;
    [SerializeField] private TextMeshProUGUI goldTXT;
    [SerializeField] private int startTime = 5;
    private float currentTime;
    public static int playerGold = 10; 

    void Start()
    {
        UpdateGold();
        currentTime = startTime * 60;
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
            playerGold += 1;
            UpdateGold();
        }
    }

    public void UpdateGold()
    {
        goldTXT.text = $"{playerGold} Gold";
    }
}