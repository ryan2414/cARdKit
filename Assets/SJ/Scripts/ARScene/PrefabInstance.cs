using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���� ��ġ������ Ư�� �̸��� ���� ��ü�� ������
//�� �̸��� ���� �̸��� ���� �������� ���� ��Ű�� �ʹ�.

public class PrefabInstance : MonoBehaviour
{
    public GameObject createObj;
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
            StopCoroutine(FalsePrefab(other.gameObject));
            StartCoroutine(ActivePrefab(other.gameObject));
        }

    }

    IEnumerator ActivePrefab(GameObject _other)
    {

        for (int i = 0; i < 3 && isStay == true; i++)
        {
            if (i >= 2)
            {
                createObj.SetActive(true);
                gameObject.GetComponent<Renderer>().enabled = false;

                //��Ŀ ���� �ִ� ������Ʈ�� �ڽĵ��� ���� �ʹ�.
                //��Ŀ�� ���� �ʴ� ������ TriggerExit�� �۵��ϱ� ����
                //������Ʈ�� ������ ���� �ʰ� �������� ���� ������ �ٽ� �Ӷ� �ڽ� ������Ʈ�� ������ �ʾƼ� 
                MeshRenderer[] _otherMeshRenderer = _other.GetComponentsInChildren<MeshRenderer>();
               // _otherTransform = _other.GetComponentsInChildren<Transform>();
                for (int j = 0; j < _otherMeshRenderer.Length; j++)
                {
                    _otherMeshRenderer[j].enabled = false;
                }
            }
            yield return new WaitForSeconds(1f);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == gameObject.name)
        {
            isStay = false;

            StopCoroutine(ActivePrefab(other.gameObject));
            StartCoroutine(FalsePrefab(other.gameObject));
        }
    }

    IEnumerator FalsePrefab(GameObject _other)
    {
        for (int i = 0; i < 3 && !isStay == true; i++)
        {
            if (i >= 2)
            {
                createObj.SetActive(false);
                gameObject.GetComponent<Renderer>().enabled = true;
                //��Ŀ ���� �ִ� ������Ʈ�� �ڽĵ��� �Ѱ� �ʹ�.
                //0������ �θ��� ������ ���� ������ 1������ ������ �Ѵ�.
                MeshRenderer[] _otherMeshRenderer = _other.GetComponentsInChildren<MeshRenderer>();
                // _otherTransform = _other.GetComponentsInChildren<Transform>();
                for (int j = 0; j < _otherMeshRenderer.Length; j++)
                {
                    _otherMeshRenderer[j].enabled = true;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
