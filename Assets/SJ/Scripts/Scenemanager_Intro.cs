using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

//��ư�� ������ ��ŵ�� �ϰ� �ϰ� �ʹ�.
public class Scenemanager_Intro : MonoBehaviour
{
    public VideoPlayer videoClip;
    public Image IMG_FillBulb;
    public GameObject UI_Background;
    public CanvasGroup IMG_Fader;
    public float fillTime;
    bool IWantToPlayOnce;

    private void Awake()
    {
        videoClip.Prepare();
    }
    private void Start()
    {
        //������ 2�ʰ� ä���ش�
        IMG_FillBulb.fillAmount = 0;

        IMG_Fader.alpha = 0;

        // Invoke("VideoStart", 2f);
    }

    private void Update()
    {
        //if (!IWantToPlayOnce && videoClip.isPrepared == true)
        //{
        //    videoClip.Play();
        //    IWantToPlayOnce = true;
        //}

        IMG_FillBulb.fillAmount += Time.deltaTime / 2;

        if (!IWantToPlayOnce && IMG_FillBulb.fillAmount >= 1)
        {
            IWantToPlayOnce = true;
            VideoStart();
        }
        //���� ������ �� �� �Ѿ�� ��
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
        StartCoroutine(IEHideBoard(IMG_Fader));

    }


    public IEnumerator IEShowBoard(CanvasGroup canvasGroup)
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / fillTime;
            yield return new WaitForSeconds(Time.deltaTime / fillTime);
        }
        canvasGroup.alpha = 1;

        SceneManager.LoadScene("1SJ_StartScene");
    }

    public IEnumerator IEHideBoard(CanvasGroup canvasGroup)
    {
        while (0 < canvasGroup.alpha)
        {
            canvasGroup.alpha -= Time.deltaTime / fillTime;
            yield return new WaitForSeconds(Time.deltaTime / fillTime);
        }
        canvasGroup.alpha = 0;

        SceneManager.LoadScene("1SJ_StartScene");

    }
}
