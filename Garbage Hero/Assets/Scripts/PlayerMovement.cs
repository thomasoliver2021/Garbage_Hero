using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    void Update(){
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dir = mousePosition - transform.position;
        var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
