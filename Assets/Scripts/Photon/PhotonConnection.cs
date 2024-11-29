using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonConnection : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject connectingTXT;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Creating a new room...");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Joined Room: {PhotonNetwork.CurrentRoom.Name}");
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"Room Created: {PhotonNetwork.CurrentRoom.Name}");
    }

    public void Connect()
    {
        audioManager.PlaySFX(audioManager.Button);
        connectingTXT.SetActive(true);
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Already connected");
            return;
        }

        PhotonNetwork.ConnectUsingSettings();
    }
}
