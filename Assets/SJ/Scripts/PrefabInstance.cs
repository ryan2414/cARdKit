using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���� ��ġ������ Ư�� �̸��� ���� ��ü�� ������
//�� �̸��� ���� �̸��� ���� �������� ���� ��Ű�� �ʹ�.

public class PrefabInstance : MonoBehaviour
{
    public GameObject createObj;
    GameObject item;
    bool isStay;

    private void Start()
    {
        createObj.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == gameObject.name)
        {
            isStay = true;
            StartCoroutine(ActivePrefab());
        }

    }

    IEnumerator ActivePrefab()
    {
        
        for (int i = 0; i < 3 && isStay == true; i++)
        {
            if (i >= 2)
            {
                createObj.SetActive(true);
                gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(1f);
        }

    }



    private void OnTriggerExit(Collider other)
    {
        isStay = false;

        StopCoroutine(ActivePrefab());
    }
}
