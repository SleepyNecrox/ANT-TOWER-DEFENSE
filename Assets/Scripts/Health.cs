using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] internal int maxHealth = 30;
    [SerializeField] internal int currentHealth;
    private Slider healthSlider;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider = GetComponentInChildren<Slider>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            StartCoroutine(FlashRed());
        }

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

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
