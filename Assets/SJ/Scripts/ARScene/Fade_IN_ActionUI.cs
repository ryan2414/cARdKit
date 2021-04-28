using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//���̴��� ũ�Ⱑ Ŀ���� �ϰ� �ʹ�.
public class Fade_IN_ActionUI : MonoBehaviour
{
    public float speed;
    public float UIMinSize;
    public Animator Ani_SSH_Help;

    bool once;

    // Start is called before the first frame update
    void Start()
    {
        UIMinSize = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (UIMinSize < 3000)
        {
            UIMinSize += speed * Time.deltaTime;

            RectTransform rectHight = gameObject.GetComponent<RectTransform>();
            rectHight.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, UIMinSize);
            RectTransform rectWidth = gameObject.GetComponent<RectTransform>();
            rectWidth.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, UIMinSize);

        }else if(UIMinSize >= 3000 && !once)
        {
            gameObject.SetActive(false);
            Ani_SSH_Help.SetTrigger("isStart");
            once = true;
        }

    }

    //void FaderMakeSmall()
    //{
        

    //    //���� �޴����� ���� ���� �ҷ����� �Լ��� ȣ���Ѵ�.
    //    if (UIMinSize < 0)
    //        GameObject.Find("GameManager").GetComponent<Scenemanager_Story>().OnClickNextScene();
    //}
}
