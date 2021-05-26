using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���� AR ������ ���������� Ŭ���� �ߴٸ�
//AR ������ Ŭ������ ������ �ְ�
//����ʿ��� �������� ��ư�� ���� ���ϰ� �ϰ� �ʹ�.
//
public class StageManager_WorldMap : MonoBehaviour
{
    public GameObject UI_Finish;
    public Animator anim;

    //�������� ��ư ������ ������ �´�
    public List<Button> btnStage = new List<Button>();
    public List<GameObject> clearStamp = new List<GameObject>();


    #region �Ⱦ��°�

    //�ҳ��� ������Ʈ 
    //public GameObject FireWorksFactory;
    //public GameObject FireWorksWholeMapFactory;

    //List<bool> stageClearBool = new List<bool>();
    //public List<Button> btnStage2 = new List<Button>();
    //// List<bool> stage2ClearBool = new List<bool>();

    ////���� ĵ����
    ////public CanvasGroup CloudCanvasGroup;
    ////public float fadeSpeed;

    //int stageTotalNumber = 8;

    //bool stage1Clear;
    //bool stage2Clear;
    #endregion


    private void Start()
    {
        //1-1�� ������ ������ �������� ��ư�� ����
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
        //�ε��� ���� �޾Ƽ�
        //�� ���������� ��ư�� Ȱ��ȭ ���ְ�
        //Ŭ������ ���������� ���� �����ϰ� �ʹ�.
        for (int i = 0; i < btnStage.Count; i++)
        {
            if (FlagManager.instance.clearBool[i] == true)
            {
                //�ִϸ��̼��� ������ �����ִ� ���� �ѹ��� �����ϰ� ������
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




    #region �Ҳɳ���
    //public void FireWorks()
    //{
    //    if (!stage1Clear && !stage2Clear && !stage1ClearBool.Contains(false))
    //    {
    //        //1-4�ܰ谡 ���̳��� �Ҳɳ��� ��ƼŬ�� �����ϰ�
    //        //�Ȱ��� ������
    //        //2�ܰ� ���������� ������� ������ �ϰ� �ʹ�.

    //        stage1Clear = true;
    //        //GameObject firework = Instantiate(FireWorksFactory);
    //        //firework.GetComponent<Animator>().SetTrigger("ParticleStart");
    //        //StartCoroutine(DisapearCloud());
    //    }

    //    //if (stage1Clear && !stage2Clear && !stage2ClearBool.Contains(false))
    //    //{
    //    //    //�������� 2���� �Ϸᰡ �Ǹ� �ȳ� �˾��� ����.
    //    //    //�������� 2 Ŭ����� ���� ������ �߰�
    //    //    GameObject firwork = Instantiate(FireWorksWholeMapFactory);
    //    //    firwork.GetComponent<Animator>().SetTrigger("Stage2Clear");
    //    //    StartCoroutine(FinishStage());
    //    //    stage2Clear = true;
    //    //}
    //}
    //IEnumerator DisapearCloud()
    //{
    //    yield return new WaitForSeconds(2f);
    //    //�Ҳɳ��̰� ������ 
    //    //UIȭ���� �������� ������� �ϰ� �ʹ�.
    //    while (CloudCanvasGroup.alpha > 0)
    //    {
    //        CloudCanvasGroup.alpha -= Time.deltaTime / fadeSpeed;
    //        yield return new WaitForSeconds(Time.deltaTime);
    //    }

    //    yield return new WaitForSeconds(0.5f);

    //    //2���������� ��ư UI�� ��Ÿ���� �Ѵ�.
    //    //2-1�� ������ ������ ���������� ������ ������ �����.
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


    // MH�߰�

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
