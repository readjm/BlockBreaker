using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour {

    public Sprite muteOn;
    public Sprite muteOff;

    private static bool mute = false;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Mute);
    }

    public void Mute()
    {
        if (mute == false)
        {
            mute = true;
            AudioListener.volume = 0.0f;
            GetComponent<Image>().sprite = muteOn;
        }
        else
        {
            mute = false;
            AudioListener.volume = 1.0f;
            GetComponent<Image>().sprite = muteOff;
        }

    }
}
