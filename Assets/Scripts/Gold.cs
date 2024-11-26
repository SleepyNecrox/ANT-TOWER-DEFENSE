using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField] internal float gold = 15.0f;
    [SerializeField] internal int playerID;

    public bool CanAfford(float amount)
    {
        return gold >= amount;
    }

    public void SpendGold(int amount)
    {
        if (CanAfford(amount))
        {
            gold -= amount;
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }
}
