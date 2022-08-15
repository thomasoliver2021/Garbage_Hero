using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipScroll : MonoBehaviour
{
    private float starttime;
    private void Start()
    {
        starttime = Time.time;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("GameScene");
        if (Time.time - starttime >= 34f) GameObject.FindGameObjectWithTag("story").SetActive(false);
    }
}
