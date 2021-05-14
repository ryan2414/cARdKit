using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SHIntro_SJ : MonoBehaviour
{
    public GameObject img_SH;
    public GameObject go_Sh_MoveTarget;
    public GameObject txt_Sh;

    public float moveSpeed;
    public float dialogSpeed;

    float timer;
    float deltaDis;
    Vector3 sh_OriginPos;

    // Start is called before the first frame update
    void Start()
    {
        txt_Sh.SetActive(false);
        txt_Sh.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        txt_Sh.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 0);

        sh_OriginPos = img_SH.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        deltaDis = moveSpeed * Time.deltaTime;

        //승형이가 왼쪽에서 나타난다
        img_SH.transform.position = Vector3.MoveTowards(img_SH.transform.position, go_Sh_MoveTarget.transform.position, deltaDis);

        if (txt_Sh.GetComponent<Image>().color.a < 1)
        {
            txt_Sh.gameObject.SetActive(true);
            txt_Sh.GetComponent<Image>().color += new Color(0, 0, 0, Time.deltaTime / dialogSpeed);
            txt_Sh.GetComponentInChildren<Text>().color += new Color(0, 0, 0, Time.deltaTime / dialogSpeed);
        }

        txt_Sh.GetComponent<Image>().color -= new Color(0, 0, 0, Time.deltaTime / dialogSpeed);
        txt_Sh.GetComponentInChildren<Text>().color -= new Color(0, 0, 0, Time.deltaTime / dialogSpeed);

        if (txt_Sh.GetComponent<Image>().color.a <= 0)
        {
            img_SH.transform.position = Vector3.MoveTowards(img_SH.transform.position, sh_OriginPos, deltaDis);
        }
    }
}
