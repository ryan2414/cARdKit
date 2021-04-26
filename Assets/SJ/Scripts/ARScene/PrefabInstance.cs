using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//만약 터치센서에 특정 이름을 가진 물체가 닿으면
//그 이름과 같은 이름을 가진 프리팹을 실행 시키고 싶다.

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

                //마커 위에 있는 오브젝트의 자식들을 끄고 싶다.
                //마커를 끄지 않는 이유는 TriggerExit을 작동하기 위함
                //오브젝트를 완전히 끄지 않고 렌더러만 끄는 이유는 다시 켤때 자식 오브젝트가 켜지지 않아서 
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
                //마커 위에 있는 오브젝트의 자식들을 켜고 싶다.
                //0번에는 부모의 정보가 들어가기 때문에 1번부터 시작을 한다.
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
