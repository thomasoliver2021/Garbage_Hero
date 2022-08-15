using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Change the speed of the ship
    float speed = 0.9f;
    void Update(){
        // Get mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate angle between ship and mouse
        var dir = mousePosition - transform.position;
        var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;

        // Calculate how much the ship should accelerate
        // The further the mouse, the faster the ship will go
        float acceleration = Vector2.Distance(mousePosition, transform.position) * speed;

        // Rotate ship to face mouse
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // Move the ship towards the mouse
        transform.position = Vector2.MoveTowards(transform.position, mousePosition, acceleration * Time.deltaTime);
    }
}
