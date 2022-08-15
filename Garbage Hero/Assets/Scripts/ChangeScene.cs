using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    AudioSource btnnoise;
    public void SceneChange(string name)
    {
        StartCoroutine(_SceneChange(name));
    }

    IEnumerator _SceneChange(string name)
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(name);
    }
    public void ClickButton()
    {
        btnnoise.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
