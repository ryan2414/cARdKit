using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

//버튼을 누르면 스킵을 하게 하고 싶다.
public class Scenemanager_Intro : MonoBehaviour
{
    public VideoPlayer videoClip;
    public Image IMG_FillBulb;
    public GameObject UI_Background;
    public CanvasGroup IMG_Fader;
    public GameObject BTN_Skip;

    public float fadeTime;
    bool IWantToPlayOnce;

    private void Awake()
    {
        videoClip.Prepare();
        BTN_Skip.SetActive(false);
    }

    private void Start()
    {
        //전구를 2초간 채워준다
        IMG_FillBulb.fillAmount = 0;

        //페이더를 위한 이미지
        IMG_Fader.alpha = 0;



    }

    private void Update()
    {
        FillTheBulb();

    }

    public void FillTheBulb()
    {
        IMG_FillBulb.fillAmount += Time.deltaTime / 2;

        if (!IWantToPlayOnce && IMG_FillBulb.fillAmount >= 1)
        {
            IWantToPlayOnce = true;
            BTN_Skip.SetActive(true);
            VideoStart();
        }

        //영상 끝났을 때 씬 넘어가는 거
        if (videoClip.isPaused == true)
        {
            StartCoroutine(IEShowBoard(IMG_Fader));
        }
    }

    public void VideoStart()
    {
        UI_Background.SetActive(false);
        videoClip.Play();
    }

    public void OnClickSkip()
    {
        videoClip.Stop();
        StartCoroutine(IEShowBoard(IMG_Fader));
    }


    public IEnumerator IEShowBoard(CanvasGroup canvasGroup)
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / fadeTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        canvasGroup.alpha = 1;

        SceneManager.LoadScene("1SJ_StartScene");
    }

    public IEnumerator IEHideBoard(CanvasGroup canvasGroup)
    {
        while (0 < canvasGroup.alpha)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        canvasGroup.alpha = 0;

        SceneManager.LoadScene("1SJ_StartScene");

    }
}
