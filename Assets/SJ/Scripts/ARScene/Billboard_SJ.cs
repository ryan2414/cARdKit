using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_SJ : MonoBehaviour
{
    public GameObject Target;
    public float speed;
    float deltaSpeed;

    //Y축이 들리지 않고 회전만 하기를 원함
    void Update()
    {
        deltaSpeed += speed * Time.deltaTime;

        transform.localPosition = Target.transform.localPosition + new Vector3 (0,- 50,-0.003f);

        transform.localEulerAngles = new Vector3(0, deltaSpeed, 0);

    }
}
