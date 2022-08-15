using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBulletBehavior : MonoBehaviour
{
    private float speed;
    private Renderer renderer;

    void Start()
    {
        speed = 2.2f;
        renderer = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        gameObject.transform.Translate(gameObject.transform.up * Time.deltaTime * speed, Space.World);
        if (!renderer.isVisible) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
