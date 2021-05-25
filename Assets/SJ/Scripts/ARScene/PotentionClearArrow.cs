using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotentionClearArrow : MonoBehaviour
{
    [SerializeField]
    Switch_MH switch_MH;

    [SerializeField]
    GameObject canvas_ClearArrow;

    // Start is called before the first frame update
    void Start()
    {
        canvas_ClearArrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //스위치 버튼을 누르면 스위치에 화살표를 끄고 가변저항의 스위치를 활성화 한다.
        if (switch_MH.isOn == true)
        {
            switch_MH.GetComponent<Transform>().Find("Canvas_ClearArrow").gameObject.SetActive(false);
            canvas_ClearArrow.SetActive(true);
        }
        //최종 클리어가 되면 가변저항의 스위치도 끈다.
        if (ClearUIActive_SJ.instance.isStageClear == true)
        {
            canvas_ClearArrow.SetActive(false);

        }
    }
}
