using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    Sprite explosion2;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.sprite = explosion2;

        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
