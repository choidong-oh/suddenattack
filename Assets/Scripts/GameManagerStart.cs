using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerStart : MonoBehaviour
{ 
    [SerializeField] private GameObject uiSetting;
    [SerializeField] private GameObject uiSound;

    public void SettingSetactive()
    {
        uiSetting.SetActive(true);
    }
    public void SoundSetactive()
    {
        uiSound.SetActive(true);
    }
    public void BackSetactive()
    {
        uiSetting.SetActive(false);
    }
    public void SoundSetactiveFalse()
    {
        uiSound.SetActive(false);
    }


}
