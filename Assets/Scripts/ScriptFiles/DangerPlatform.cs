using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerPlatform : MonoBehaviour
{
    public float timing = 3.0f;
    private Collider2D _platformCollider;
    private SpriteRenderer _platformSprite;
    private Rigidbody2D _platformRigidbody2D;

    private void Start()
    {
        _platformCollider = GetComponent<Collider2D>();
        _platformSprite = GetComponent<SpriteRenderer>();
        _platformRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(_dissapear());
        }
    }

    private IEnumerator _dissapear()
    {
        yield return new WaitForSeconds(timing);
        _platformCollider.enabled = false;
        _platformSprite.enabled = false;
        _platformRigidbody2D.isKinematic = true;

        yield return new WaitForSeconds(timing);

        _platformCollider.enabled = true;
        _platformSprite.enabled = true;
        _platformRigidbody2D.isKinematic = false;
    }
        
}
