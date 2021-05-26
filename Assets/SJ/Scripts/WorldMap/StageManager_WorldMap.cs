using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//만약 AR 씬에서 스테이지를 클리어 했다면
//AR 씬에서 클리어한 정보를 주고
//월드맵에서 스테이지 버튼의 색이 변하게 하고 싶다.
//
public class StageManager_WorldMap : MonoBehaviour
{
    public GameObject UI_Finish;
    public Animator anim;

    //스테이지 버튼 정보를 가지고 온다
    public List<Button> btnStage = new List<Button>();
    public List<GameObject> clearStamp = new List<GameObject>();


    #region 안쓰는거

    //불놀이 오브젝트 
    //public GameObject FireWorksFactory;
    //public GameObject FireWorksWholeMapFactory;

    //List<bool> stageClearBool = new List<bool>();
    //public List<Button> btnStage2 = new List<Button>();
    //// List<bool> stage2ClearBool = new List<bool>();

    ////구름 캔버스
    ////public CanvasGroup CloudCanvasGroup;
    ////public float fadeSpeed;

    //int stageTotalNumber = 8;

    //bool stage1Clear;
    //bool stage2Clear;
    #endregion


    private void Start()
    {
        //1-1을 제외한 나머지 스테이지 버튼을 끈다
        for (int i = 0; i < btnStage.Count; i++)
        {
            if (i > 0) btnStage[i].interactable = false;
        }
        UI_Finish.SetActive(false);

        soundClick = GameObject.Find("ClickSound").GetComponent<AudioSource>();

    }

    private void Update()
    {
        ButtonColorChange();
    }

    public void ButtonColorChange()
    {
        //인덱스 값을 받아서
        //그 스테이지의 버튼을 활성화 해주고
        //클리어한 스테이지의 색을 변경하고 싶다.
        for (int i = 0; i < btnStage.Count; i++)
        {
            if (FlagManager.instance.clearBool[i] == true)
            {
                //애니메이션은 데이터 남아있는 동안 한번만 실행하고 싶은데
                clearStamp[i].GetComponentInChildren<KururingPang>().isStartAnim = true;
                if (i < btnStage.Count - 1)
                {
                    btnStage[i + 1].interactable = true;
                }
                if (FlagManager.instance.clearBool[3] == true && PlayerPrefs.GetInt("ChapterClear") < 1)
                {
                    anim.SetTrigger("isStageClear");
                    PlayerPrefs.SetInt("ChapterClear", 1);
                }
                if (FlagManager.instance.clearBool[6] == true && PlayerPrefs.GetInt("ChapterClear") == 1)
                {
                    anim.SetTrigger("isStageClear");
                    PlayerPrefs.SetInt("ChapterClear", 2);
                    isAllChapterClear = true;
                }

            }
        }
    }




    #region 불꽃놀이
    //public void FireWorks()
    //{
    //    if (!stage1Clear && !stage2Clear && !stage1ClearBool.Contains(false))
    //    {
    //        //1-4단계가 끝이나면 불꽃놀이 파티클을 실행하고
    //        //안개가 걷히고
    //        //2단계 스테이지가 순서대로 열리게 하고 싶다.

    //        stage1Clear = true;
    //        //GameObject firework = Instantiate(FireWorksFactory);
    //        //firework.GetComponent<Animator>().SetTrigger("ParticleStart");
    //        //StartCoroutine(DisapearCloud());
    //    }

    //    //if (stage1Clear && !stage2Clear && !stage2ClearBool.Contains(false))
    //    //{
    //    //    //스테이지 2까지 완료가 되면 안내 팝업을 띄운다.
    //    //    //스테이지 2 클리어시 폭죽 이펙터 추가
    //    //    GameObject firwork = Instantiate(FireWorksWholeMapFactory);
    //    //    firwork.GetComponent<Animator>().SetTrigger("Stage2Clear");
    //    //    StartCoroutine(FinishStage());
    //    //    stage2Clear = true;
    //    //}
    //}
    //IEnumerator DisapearCloud()
    //{
    //    yield return new WaitForSeconds(2f);
    //    //불꽃놀이가 끝나면 
    //    //UI화면의 구름들이 사라지게 하고 싶다.
    //    while (CloudCanvasGroup.alpha > 0)
    //    {
    //        CloudCanvasGroup.alpha -= Time.deltaTime / fadeSpeed;
    //        yield return new WaitForSeconds(Time.deltaTime);
    //    }

    //    yield return new WaitForSeconds(0.5f);

    //    //2스테이지의 버튼 UI가 나타나게 한다.
    //    //2-1을 제외한 나머지 스테이지를 누를수 없도록 만든다.
    //    for (int i = 0; i < btnStage2.Count; i++)
    //    {
    //        if (i > 0)
    //        {
    //            btnStage2[i].interactable = false;
    //        }
    //        btnStage2[0].interactable = true;
    //        btnStage2[i].gameObject.SetActive(true);

    //        yield return new WaitForSeconds(0.5f);
    //    }
    //}

    //IEnumerator FinishStage()
    //{
    //    yield return new WaitForSeconds(2f);

    //    CanvasGroup finishUI = UI_Finish.GetComponent<CanvasGroup>();
    //    finishUI.alpha = 0;
    //    UI_Finish.SetActive(true);
    //    while (finishUI.alpha <= 1)
    //    {
    //        finishUI.alpha += Time.deltaTime / fadeSpeed;
    //        yield return new WaitForSeconds(Time.deltaTime);
    //    }

    //}
    #endregion



    public void TestButton()
    {
        for (int i = 0; i < btnStage.Count; i++)
        {
            btnStage[i].interactable = true;
        }
    }


    // MH추가

    public GameObject announcePanel;
    public AudioSource soundClick;
    bool isMenuOn;
    bool isAllChapterClear;
    public void AnnounceThankyou()
    {
        if (!isMenuOn)
        {
            if (isAllChapterClear)
            {
                soundClick.Play();
                isMenuOn = true;
                announcePanel.SetActive(true);
                //OptionPanel.GetComponent<ScaleUp>().enabled = true;
            }
        }
    }

    public void OnClickX()
    {
        if (announcePanel.activeSelf == true)
        {
            soundClick.Play();
            isMenuOn = false;
            //OptionPanel.GetComponent<ScaleDown>().enabled = true;
        }
        else
        {
            soundClick.Play();
            isMenuOn = false;
            //HelpPanel.GetComponent<ScaleDown>().enabled = true;
        }
    }

}
