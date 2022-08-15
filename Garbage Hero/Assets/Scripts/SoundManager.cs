using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private static int muted = 0;

    [SerializeField]
    Sprite normal;
    [SerializeField]
    Sprite soundoff;

    private void Start()
    {
        Image[] gettem = gameObject.GetComponentsInChildren<Image>();
        foreach (Image img in gettem)
        {
            if (img.gameObject.GetInstanceID() != gameObject.GetInstanceID()) img.sprite = (muted == 0) ? normal: soundoff;
        }
    }

    public void ToggleSound()
    {
        if (muted == 0)
        {
            AudioListener.pause = true;
            muted = 1;
            Image[] gettem = gameObject.GetComponentsInChildren<Image>();
            foreach (Image img in gettem) {
                if(img.gameObject.GetInstanceID() != gameObject.GetInstanceID()) img.sprite = soundoff;
            }
        }
        else
        {
            AudioListener.pause = false;
            muted = 0;
            Image[] gettem = gameObject.GetComponentsInChildren<Image>();
            foreach (Image img in gettem)
            {
                if (img.gameObject.GetInstanceID() != gameObject.GetInstanceID()) img.sprite = normal;
            }
        }
    }
}
