using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//노드에 등록된 소자들이 켜지면 와이어가 켜지게 하고 싶다.
public class CheckWire_SJ : MonoBehaviour
{
    public GameObject Go_Wire;
    public GameObject Go_WirePlane;
    public List<GameObject> Go_PartList = new List<GameObject>();

    private void Update()
    {
        if (Go_PartList[0].activeSelf == true && Go_PartList[1].activeSelf == true)
        {
            Go_Wire.SetActive(true);
            Go_WirePlane.SetActive(false);
        }
        else
        {
            Go_Wire.SetActive(false);
            Go_WirePlane.SetActive(true);

        }

    }
}
