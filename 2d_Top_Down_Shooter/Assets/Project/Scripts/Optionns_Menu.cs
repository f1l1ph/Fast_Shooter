using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Optionns_Menu : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider volume_Slider;
    [SerializeField] private TMP_Dropdown quality_DropDown;

    private string quality_Key = "quality";
    private string volume_Key = "volume";

    private void Start()
    {
        mixer.SetFloat(volume_Key, PlayerPrefs.GetFloat(volume_Key));
        volume_Slider.value = PlayerPrefs.GetFloat(volume_Key);

        quality_DropDown.value = PlayerPrefs.GetInt(quality_Key);
    }

    public void Set_Volume(float volume)
    {
        mixer.SetFloat(volume_Key, volume);
        PlayerPrefs.SetFloat(volume_Key, volume);
    }

    public void Set_Quality(int quality_Index)
    {
        QualitySettings.SetQualityLevel(quality_Index);
        PlayerPrefs.SetInt(quality_Key, quality_Index);
    }
}
