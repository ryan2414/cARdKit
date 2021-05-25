using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_SJ : MonoBehaviour
{
    Transform trn_cam;
    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        trn_cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Y���� �鸮�� �ʰ� ȸ���� �ϱ⸦ ����
        targetPos = new Vector3(trn_cam.transform.position.x, transform.position.y, trn_cam.transform.position.z);
        transform.LookAt(targetPos);
    }
}
