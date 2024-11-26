using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class ReadyScreen : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button readyButton;
    [SerializeField] private TextMeshProUGUI player1Username;
    [SerializeField] private TextMeshProUGUI player2Username;
    [SerializeField] private TextMeshProUGUI player1ReadyStatus;
    [SerializeField] private TextMeshProUGUI player2ReadyStatus;

    private void Start()
    {
        readyButton.onClick.AddListener(ReadyButtonClicked);
        readyButton.interactable = true;
        UpdatePlayerInfo();
    }

    public override void OnJoinedRoom()
    {
        UpdatePlayerInfo();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerInfo();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerInfo();
    }

    private void UpdatePlayerInfo()
    {
        Player player1 = PhotonNetwork.PlayerList.FirstOrDefault(p => p.ActorNumber == 1);
        Player player2 = PhotonNetwork.PlayerList.FirstOrDefault(p => p.ActorNumber == 2);

        player1Username.text = player1 != null ? player1.NickName : "Waiting...";
        player1ReadyStatus.text = player1 != null ? "Ready" : "Waiting for Player 1...";

        player2Username.text = player2 != null ? player2.NickName : "Waiting...";
        player2ReadyStatus.text = player2 != null ? "Ready" : "Waiting for Player 2...";
    }

    public void ReadyButtonClicked()
    {
        Player player1 = PhotonNetwork.PlayerList.FirstOrDefault(p => p.ActorNumber == 1);
        Player player2 = PhotonNetwork.PlayerList.FirstOrDefault(p => p.ActorNumber == 2);

        if (player1 != null && player2 != null)
        {
            photonView.RPC("StartGame", RpcTarget.All);
        }
    }

    [PunRPC]
    void StartGame()
    {
        SceneManager.LoadScene(2);
    }
}
