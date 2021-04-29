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
    public static StageManager_WorldMap instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject UI_Finish;


    public GameObject FireWorksFactory;
    public GameObject FireWorksWholeMapFactory;

    //�������� ��ư ������ ������ �´�
    public List<Button> btnStage = new List<Button>();
    public List<Button> btnStage2 = new List<Button>();
    List<bool> stage1ClearBool = new List<bool>();
    List<bool> stage2ClearBool = new List<bool>();

    //���� ĵ����
    public CanvasGroup CloudCanvasGroup;
    public float fadeSpeed;

    int stageTotalNumber = 8;

    bool stage1Clear;
    bool stage2Clear;

    private void Start()
    {
        //1-1�� ������ ������ �������� ��ư�� ����
        for (int i = 0; i < btnStage.Count; i++)
        {
            if (i > 0) btnStage[i].interactable = false;

            btnStage2[i].interactable = false;
            stage1ClearBool.Add(btnStage[i].interactable);
            stage2ClearBool.Add(btnStage2[i].interactable);
        }

        UI_Finish.SetActive(false);

    }

    private void Update()
    {
        ButtonColorChange();
        FireWorks();
    }


    public void FireWorks()
    {
        if (!stage1Clear && !stage2Clear && !stage1ClearBool.Contains(false))
        {
            //1-4�ܰ谡 ���̳��� �Ҳɳ��� ��ƼŬ�� �����ϰ�
            //�Ȱ��� ������
            //2�ܰ� ���������� ������� ������ �ϰ� �ʹ�.

            stage1Clear = true;
            GameObject firework = Instantiate(FireWorksFactory);
            firework.GetComponent<Animator>().SetTrigger("ParticleStart");
            StartCoroutine(DisapearCloud());
        }

        if (stage1Clear && !stage2Clear && !stage2ClearBool.Contains(false))
        {
            //�������� 2���� �Ϸᰡ �Ǹ� �ȳ� �˾��� ����.
            //�������� 2 Ŭ����� ���� ������ �߰�
            GameObject firwork = Instantiate(FireWorksWholeMapFactory);
            firwork.GetComponent<Animator>().SetTrigger("Stage2Clear");
            StartCoroutine(FinishStage());
            stage2Clear = true;
        }


    }
    IEnumerator DisapearCloud()
    {
        yield return new WaitForSeconds(2f);
        //�Ҳɳ��̰� ������ 
        //UIȭ���� �������� ������� �ϰ� �ʹ�.
        while (CloudCanvasGroup.alpha > 0)
        {
            CloudCanvasGroup.alpha -= Time.deltaTime / fadeSpeed;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return new WaitForSeconds(0.5f);

        //2���������� ��ư UI�� ��Ÿ���� �Ѵ�.
        //2-1�� ������ ������ ���������� ������ ������ �����.
        for (int i = 0; i < btnStage2.Count; i++)
        {
            if (i > 0)
            {
                btnStage2[i].interactable = false;
            }
            btnStage2[0].interactable = true;
            btnStage2[i].gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator FinishStage()
    {
        yield return new WaitForSeconds(2f);

        CanvasGroup finishUI = UI_Finish.GetComponent<CanvasGroup>();
        finishUI.alpha = 0;
        UI_Finish.SetActive(true);
        while (finishUI.alpha <= 1)
        {
            finishUI.alpha += Time.deltaTime / fadeSpeed;
            yield return new WaitForSeconds(Time.deltaTime);
        }

    }

    public void ButtonColorChange()
    {
        ///�׽�Ʈ �� ������ ����� �ϱ�///
        ///Ondistroy Object�� ���Ͽ� ������ �߻��� �� ����///
        ///

        //�ε��� ���� �޾Ƽ�
        //�� ���������� ��ư�� Ȱ��ȭ ���ְ�
        //Ŭ������ ���������� ���� �����ϰ� �ʹ�.
        for (int i = 0; i < stageTotalNumber; i++)
        {
            if (i < 4 && FlagManager.instance.clearBool[i] == true)
            {
                btnStage[i].gameObject.GetComponent<Image>().color = Color.yellow;
                stage1ClearBool[i] = true;
                if (i < 3) btnStage[i + 1].interactable = true;
            }
            else if (i >= 4 && FlagManager.instance.clearBool[i] == true)
            {
                int j = i - 4;
                btnStage2[j].gameObject.GetComponent<Image>().color = Color.yellow;
                stage2ClearBool[j] = true;
                if (j < 3) btnStage2[j + 1].interactable = true;
            }
        }
    }

    public void TestButton()
    {
        for (int i = 0; i < btnStage.Count; i++)
        {
            btnStage[i].interactable = true;
        }
    }

    public void Test2Button()
    {
        for (int i = 0; i < btnStage2.Count; i++)
        {
            btnStage2[i].interactable = true;
        }
    }

}
