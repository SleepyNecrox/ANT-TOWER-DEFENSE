using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Health : MonoBehaviourPunCallbacks
{
    [SerializeField] private int goldReward;
    [SerializeField] internal int currentHealth;
    private Slider healthSlider;
    private SpriteRenderer spriteRenderer;
    private AntStats antStats;
    private PlayerUI playerUI;

    void Start()
    {
        playerUI = FindObjectOfType<PlayerUI>();
        antStats = GetComponent<AntStats>();
        currentHealth = antStats.health;
        healthSlider = GetComponentInChildren<Slider>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (healthSlider != null)
        {
            healthSlider.maxValue = antStats.health;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if (photonView.IsMine)
        {
            photonView.RPC("SyncTakeDamage", RpcTarget.All, damage);
        }
    }

    [PunRPC]
    private void SyncTakeDamage(int damage)
    {
        currentHealth -= damage;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            // i am dumb but it works lol
            if (gameObject.layer == LayerMask.NameToLayer("BlueBase"))
            {
                playerUI.RedWin();
            }
            else if (gameObject.layer == LayerMask.NameToLayer("RedBase"))
            {
                playerUI.BlueWin();
            }

            RewardGoldToPlayer();
            Destroy(gameObject);
        }
    }

    private IEnumerator FlashRed()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

     private void RewardGoldToPlayer()
    {
        int rewardedPlayerID = (tag == "Blue") ? 2 : 1;

        Gold[] allPlayers = FindObjectsOfType<Gold>();
        foreach (Gold playerGold in allPlayers)
        {
            if (playerGold.playerID == rewardedPlayerID)
            {
                playerGold.AddGold(goldReward);
                return;
            }
        }

    }


}
