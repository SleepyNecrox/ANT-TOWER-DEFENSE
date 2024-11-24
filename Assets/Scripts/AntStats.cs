using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntStats : MonoBehaviour
{
    [SerializeField] internal string antName;
    [SerializeField] internal int damage;
    [SerializeField] internal int health;
    [SerializeField] internal float speed;
    [SerializeField] internal float upgradeCost;

    public void Upgrade()
    {
        damage += 5;
        health += 10;
        speed += 0.5f;

        upgradeCost *= 1.5f;
    }
}
