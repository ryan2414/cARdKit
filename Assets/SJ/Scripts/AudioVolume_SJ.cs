using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//볼륨의 처음 시작 값을 1로 정하고 싶다.

public class AudioVolume_SJ : MonoBehaviour
{
    public Slider volumeSlider;
    private void Start()
    {
        if (PlayerPrefs.GetInt("VolumeInitial") == 0)
        {
            PlayerPrefs.SetFloat("SoundVolume", 1);
            PlayerPrefs.SetInt("VolumeInitial", 1);
        }

        //소리 크기를 동기화
        volumeSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        
    }


    private void Update()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("SoundVolume", AudioListener.volume);
        print(volumeSlider.value);
    }
}
