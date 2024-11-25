using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
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
            healthSlider.maxValue =antStats.health;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
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
