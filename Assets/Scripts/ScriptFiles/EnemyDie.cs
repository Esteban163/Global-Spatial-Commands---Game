using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 5;
    public Color hitColor = Color.red; // impact color
    public float feedbackDuration = 0.2f; // duration of feedback


    private int currentHealth;
    private SpriteRenderer spriteRenderer; // for color change
    private Color originalColor; // store original color of sprite

    void Start()
    {
        currentHealth = maxHealth;

        // get the SpriteRenderer for control of the feedback
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogError("Sprite Renderer error, chequear que el enemigo tenga Sprite Renderer");
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} recibió daño, salud restante: {currentHealth}");

        // show visual feedback
        if (spriteRenderer != null)
        {
            StartCoroutine(ShowHitFeedback());
        }

        if (currentHealth <= 0)
        {
            EnemyDie();
        }
    }

    private System.Collections.IEnumerator ShowHitFeedback()
    {
        spriteRenderer.color = hitColor; // change impact color
        yield return new WaitForSeconds(feedbackDuration);
        spriteRenderer.color = originalColor; // restore original color
    }

    private void EnemyDie()
    {
        Debug.Log($"{gameObject.name} ha muerto");
        Destroy(gameObject);
    }
}