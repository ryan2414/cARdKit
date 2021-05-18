using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//페이더의 크기가 줄어 들게 하고 싶다.
public class FaderActionUI : MonoBehaviour
{
    public float speed;
    public GameObject BlackPaper;

    float UIMaxSize;

    private void Start()
    {
        BlackPaper.SetActive(false);
        GameObject.Find("FaderSound").GetComponent<AudioSource>().Play();
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

        //게임 메니져의 다음 씬을 불러오는 함수를 호출한다.
        else if (UIMaxSize < 0)
        {
            BlackPaper.SetActive(true);
            GameObject.Find("GameManager").GetComponent<Scenemanager_Story>().OnClickNextScene();
        }
            
    }
}
