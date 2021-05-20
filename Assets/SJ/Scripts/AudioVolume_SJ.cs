using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//������ ó�� ���� ���� 1�� ���ϰ� �ʹ�.

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

        //�Ҹ� ũ�⸦ ����ȭ
        volumeSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        
    }


    private void Update()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("SoundVolume", AudioListener.volume);
        print(volumeSlider.value);
    }
}
