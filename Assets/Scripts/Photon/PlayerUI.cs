using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject blueUI;
    [SerializeField] private GameObject redUI;

    void Start()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            blueUI.SetActive(true);
            redUI.SetActive(false);
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            blueUI.SetActive(false);
            redUI.SetActive(true);
        }
    }
}
