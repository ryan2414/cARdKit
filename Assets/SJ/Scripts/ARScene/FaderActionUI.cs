using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//���̴��� ũ�Ⱑ �پ� ��� �ϰ� �ʹ�.
public class FaderActionUI : MonoBehaviour
{
    public float speed;
    public float UIMaxSize;

    // Start is called before the first frame update
    void Start()
    {
        UIMaxSize = 3000;
    }

    // Update is called once per frame
    void Update()
    {
        FaderMakeSmall();
    }

    void FaderMakeSmall()
    {
        if (UIMaxSize > 0)
        {
            UIMaxSize -= speed * Time.deltaTime;

            RectTransform rectHight = gameObject.GetComponent<RectTransform>();
            rectHight.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, UIMaxSize);
            RectTransform rectWidth = gameObject.GetComponent<RectTransform>();
            rectWidth.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, UIMaxSize);
           
        }

        //���� �޴����� ���� ���� �ҷ����� �Լ��� ȣ���Ѵ�.
        if (UIMaxSize < 0)
            GameObject.Find("GameManager").GetComponent<Scenemanager_Story>().OnClickNextScene();
    }
}
