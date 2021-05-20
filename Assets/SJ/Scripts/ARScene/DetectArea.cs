using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//특정 포인트를 기준으로 그 안에 특정 오브젝트가 들어면
//트루를 표시한다.
public class DetectArea : MonoBehaviour
{
    //종이 마커 위에 있는 오브젝트
    public GameObject interactionGameObject;
    public Vector2 interactionGameObjectPoint;

    //종이 위에 있는 오브젝트의 인아웃 여부를 확인하는 포인트
    public GameObject[] targetPoints;
    public Vector2[] targetPolygons;

    [SerializeField]
    bool isIn;


    // Start is called before the first frame update
    void Start()
    {
        targetPolygons = new Vector2[targetPoints.Length];
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < targetPoints.Length; i++)
        {
            targetPolygons[i] = Camera.main.WorldToScreenPoint(targetPoints[i].transform.position);
        }
        interactionGameObjectPoint = Camera.main.WorldToScreenPoint(interactionGameObject.transform.position);

        isIn = PolyUtil.IsPointInPolygon(interactionGameObjectPoint, targetPolygons);

        ActiveObj(isIn);
    }

    //회로 위에 실행시킬 오브젝트
    public GameObject electricObj;

    bool objectActive;
    //isIn이 트루이면 게임 오브젝트를 트루로 바꿔준다.
    void ActiveObj(bool isIn)
    {
        if (isIn == true)
        {
            electricObj.SetActive(true);//회로 위의 소자 활성화
            interactionGameObject.SetActive(false);//종이 마커 위의 소자 끄기
            transform.GetComponentInChildren<MeshRenderer>().enabled = false;//회로 마커 비 활성화
            objectActive = true;
        }
        else
        {
            if (objectActive)
            {
                //isIn이 false면  오브젝트가 사라진다.
                electricObj.SetActive(false);//회로 위의 소자 비활성화
                transform.GetComponentInChildren<MeshRenderer>().enabled = true;//회로 마커 활성화
            }
        }
    }
}