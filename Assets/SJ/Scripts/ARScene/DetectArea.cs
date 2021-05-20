using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//Ư�� ����Ʈ�� �������� �� �ȿ� Ư�� ������Ʈ�� ����
//Ʈ�縦 ǥ���Ѵ�.
public class DetectArea : MonoBehaviour
{
    //���� ��Ŀ ���� �ִ� ������Ʈ
    public GameObject interactionGameObject;
    public Vector2 interactionGameObjectPoint;

    //���� ���� �ִ� ������Ʈ�� �ξƿ� ���θ� Ȯ���ϴ� ����Ʈ
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

    //ȸ�� ���� �����ų ������Ʈ
    public GameObject electricObj;

    bool objectActive;
    //isIn�� Ʈ���̸� ���� ������Ʈ�� Ʈ��� �ٲ��ش�.
    void ActiveObj(bool isIn)
    {
        if (isIn == true)
        {
            electricObj.SetActive(true);//ȸ�� ���� ���� Ȱ��ȭ
            interactionGameObject.SetActive(false);//���� ��Ŀ ���� ���� ����
            transform.GetComponentInChildren<MeshRenderer>().enabled = false;//ȸ�� ��Ŀ �� Ȱ��ȭ
            objectActive = true;
        }
        else
        {
            if (objectActive)
            {
                //isIn�� false��  ������Ʈ�� �������.
                electricObj.SetActive(false);//ȸ�� ���� ���� ��Ȱ��ȭ
                transform.GetComponentInChildren<MeshRenderer>().enabled = true;//ȸ�� ��Ŀ Ȱ��ȭ
            }
        }
    }
}