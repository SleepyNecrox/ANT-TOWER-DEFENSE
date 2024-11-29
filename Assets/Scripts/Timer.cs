using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Timer : MonoBehaviourPun
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
        currentTime = 300f;

        StartCoroutine(AddGold());

        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SyncTimer", RpcTarget.AllBuffered, currentTime); 
        }
    }

    void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return; 

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
            photonView.RPC("SyncTimer", RpcTarget.Others, currentTime);
        }
        else
        {
            currentTime = 0;
            UpdateTimerDisplay();
            if (PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("GameEndTie", RpcTarget.All);
            }
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
        player1GoldTXT.text = $"{player1Gold.gold}";
        player2GoldTXT.text = $"{player2Gold.gold}";
    }

    [PunRPC]
    void SyncTimer(float syncedTime)
    {
        currentTime = syncedTime;
        UpdateTimerDisplay();
    }

    [PunRPC]
    void GameEndTie()
    {
        PlayerUI playerUI = FindObjectOfType<PlayerUI>();

        if (playerUI != null)
        {
            playerUI.TieGame();
        }
    }
}
