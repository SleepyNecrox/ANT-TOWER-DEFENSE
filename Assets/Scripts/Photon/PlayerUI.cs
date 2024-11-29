using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject blueUI;
    [SerializeField] private GameObject redUI;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject defeatScreen;
    [SerializeField] private GameObject tieScreen;

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

    public void BlueWin()
    {
        Time.timeScale = 0f;
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            blueUI.SetActive(false);
            victoryScreen.SetActive(true);
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            redUI.SetActive(false);
            defeatScreen.SetActive(true);
        }
    }

    public void RedWin()
    {
        Time.timeScale = 0f;
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            blueUI.SetActive(false);
            defeatScreen.SetActive(true);
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            redUI.SetActive(false);
            victoryScreen.SetActive(true);
        }
    }

    public void GoMenu()
    {
        Time.timeScale = 1f;
        PhotonNetwork.Disconnect();
        StartCoroutine(WaitForDisconnect());
    }

    private IEnumerator WaitForDisconnect()
    {
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }
        SceneManager.LoadScene(0);
    }

    public void TieGame()
    {
        Time.timeScale = 0f;
        tieScreen.SetActive(true);
    }

}
