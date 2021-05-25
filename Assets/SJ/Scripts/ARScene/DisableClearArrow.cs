using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableClearArrow : MonoBehaviour
{
    [SerializeField]
    GameObject canvas_ClearArrow;

    // Update is called once per frame
    void Update()
    {
        //최종 클리어가 되면 가변저항의 스위치도 끈다.
        if (ClearUIActive_SJ.instance.isStageClear == true)
        {
            canvas_ClearArrow.SetActive(false);

        }
    }
}
