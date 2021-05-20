using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//페이더의 크기가 커지게 하고 싶다.
public class Fade_IN_ActionUI : MonoBehaviour
{

    public float speed;
    public float UIMinSize;
    public Animator Ani_SSH_Help;
    public Image IMG_Bulb;
    public Image IMG_Bulb_Fill;
    public Image _Img_Fader;

    bool once;
    private void Awake()
    {
        _Img_Fader.gameObject.SetActive(true);
        IMG_Bulb.gameObject.SetActive(true);
        IMG_Bulb_Fill.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        UIMinSize = 0;
        IMG_Bulb_Fill.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FillTheBulb();

        if (IMG_Bulb_Fill.fillAmount >= 1)
        {
            IMG_Bulb.gameObject.SetActive(false);
            IMG_Bulb_Fill.gameObject.SetActive(false);
            FadeINStart();
            FadeINSound();
        }
    }
    bool oneSound;
    private void FadeINSound()
    {
        if (!oneSound)
        {
            GameObject.Find("FaderInSound").GetComponent<AudioSource>().Play();

            oneSound = true;
        }
    }

    private void FillTheBulb()
    {
        IMG_Bulb_Fill.fillAmount += Time.deltaTime / 2;
    }

    private void FadeINStart()
    {
        if (UIMinSize < 3000)
        {
            UIMinSize += Time.deltaTime * speed;

            RectTransform rectHight = gameObject.GetComponent<RectTransform>();
            rectHight.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, UIMinSize);
            RectTransform rectWidth = gameObject.GetComponent<RectTransform>();
            rectWidth.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, UIMinSize);

        }
        else if (UIMinSize >= 3000 && !once)
        {
            gameObject.SetActive(false);
            Ani_SSH_Help.SetTrigger("isStart");

            GameObject.Find("BGM_AR").GetComponent<AudioSource>().Play();


            once = true;
        }
    }
}
