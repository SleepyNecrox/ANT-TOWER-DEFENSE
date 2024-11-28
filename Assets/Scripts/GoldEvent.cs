using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GoldEvent : MonoBehaviour
{
    [SerializeField] private int goldReward;

    public void OnHit(int playerID)
    {
        Gold playerGold = FindPlayerGold(playerID);
        if (playerGold != null)
        {
            playerGold.AddGold(goldReward);
        }
    }

    private Gold FindPlayerGold(int playerID)
    {
        foreach (Gold gold in FindObjectsOfType<Gold>())
        {
            if (gold.GetComponent<PhotonView>().Owner.ActorNumber == playerID)
            {
                return gold;
            }
        }
        return null;
    }
}
