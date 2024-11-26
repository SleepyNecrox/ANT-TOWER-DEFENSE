using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Health : MonoBehaviourPunCallbacks
{
    [SerializeField] internal int currentHealth;
    private Slider healthSlider;
    private SpriteRenderer spriteRenderer;

    private AntStats antStats;

    void Start()
    {
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
}
