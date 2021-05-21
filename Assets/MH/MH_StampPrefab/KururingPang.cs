using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KururingPang : MonoBehaviour
{
    public RectTransform startPosition;
    public RectTransform endPosition;

    public GameObject Pang;

    public float smallSize;
    float currnetSize;
    public float largeSize;
    float targetSizeUp;
    float targetSizeDown;

    public int turnCount;
    public float resizeUpSpeed;
    public float resizeDownSpeed;
    public float moveSpeed;

    Vector3 targetRotation;
    Vector3 currentRotation;

    RectTransform rectTransform;

    bool isSizeUpping;

    bool isAnimationEnd = true;
    bool isPangOn;

    //���� �߰�
    public bool isStartAnim;
    public GameObject markerInBox;
    string didPlay;
    //

    // ��� �̹��� �߰�
    public GameObject backgroundImg;
    //

    void Start()
    {
        //�����߰�
        didPlay = PlayerPrefs.GetString(transform.parent.gameObject.name);
        print($"{transform.parent.gameObject.name}, {didPlay}");
        if (didPlay == "DidAnim")
        {
            markerInBox.SetActive(true);
            backgroundImg.SetActive(true);
        }
        else
        {
            markerInBox.SetActive(false);
            backgroundImg.SetActive(false);
        }
        //

        isSizeUpping = true;

        Pang.SetActive(false);
        targetRotation = new Vector3(0, 0, turnCount * -360);
        currentRotation = new Vector3(0, 0, 0);

        rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.position = startPosition.position;
        rectTransform.sizeDelta = new Vector2(smallSize, smallSize);

        targetSizeUp = Mathf.Abs(largeSize - smallSize);
        targetSizeDown = Mathf.Abs(smallSize - largeSize);
    }

    void Update()
    {
        //���� �߰�
        if (isStartAnim && didPlay != "DidAnim")
        //
        {
            rectTransform.position = Vector3.MoveTowards(rectTransform.position, endPosition.position, moveSpeed * Time.deltaTime);

            if (!isAnimationEnd)
            {
                // ȸ��
                currentRotation += targetRotation * Time.deltaTime / resizeDownSpeed;
                rectTransform.localEulerAngles = currentRotation;
            }
            else
            {
                rectTransform.localEulerAngles = Vector3.zero;
            }


            // ũ�� ����
            // Ŀ����
            if (isSizeUpping)
            {
                currnetSize += targetSizeUp * Time.deltaTime / resizeUpSpeed;
                float resize = smallSize + currnetSize;
                rectTransform.sizeDelta = new Vector2(resize, resize);
                if (resize >= largeSize)
                {
                    rectTransform.sizeDelta = new Vector2(largeSize, largeSize);
                    currnetSize = largeSize;

                    isAnimationEnd = false;
                    isSizeUpping = false;
                }
            }
            // �۾�����
            else
            {

                currnetSize -= targetSizeDown * Time.deltaTime / resizeDownSpeed;
                float resize = currnetSize;
                rectTransform.sizeDelta = new Vector2(resize, resize);
                if (resize <= smallSize)
                {
                    rectTransform.sizeDelta = new Vector2(smallSize, smallSize);
                    if (!isPangOn)
                    {
                        Pang.transform.position = transform.position;
                        Pang.SetActive(true);
                        isPangOn = true;
                        isAnimationEnd = true;

                        //���� �߰�
                        PlayerPrefs.SetString(transform.parent.gameObject.name, "DidAnim");
                        backgroundImg.SetActive(true);
                        //
                    }
                }
            }
        }

    }
}
