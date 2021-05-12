using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pang : MonoBehaviour
{
    public float startSize;
    float currentSize;

    public float resizeSpeed;

    RectTransform rectTransform;

    Image controlAlpha;

    public float fadeTime;

    [SerializeField, Range(0, 1)]
    float fadeInRatio;
    float fadeOutRatio;
    float fadeinTime;
    float fadeoutTime;

    bool isFadeOut;

    void OnEnable()
    {
        controlAlpha = GetComponent<Image>();
        controlAlpha.color = new Color(controlAlpha.color.r, controlAlpha.color.g, controlAlpha.color.b, (1 - fadeInRatio));
        rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(startSize, startSize);

        fadeOutRatio = 1 - fadeInRatio;
        fadeinTime = fadeTime * fadeInRatio;
        fadeoutTime = fadeTime * fadeOutRatio;
    }
    float Timeer;
    void Update()
    {
        Timeer += Time.deltaTime;
        // ũ�� ����
        currentSize += resizeSpeed * Time.deltaTime;
        float resize = startSize + currentSize;
        rectTransform.sizeDelta = new Vector2(resize, resize);

        if (!isFadeOut)
        {
            // ���� ������ resizeSpeed * fadeInRatio �ð� ���� Alpha���� 1�� ������־����
            controlAlpha.color += new Color(0, 0, 0, (Time.deltaTime * fadeInRatio) / (fadeinTime));

            if (controlAlpha.color.a >= 1)
            {
                controlAlpha.color = new Color(controlAlpha.color.r, controlAlpha.color.g, controlAlpha.color.b, 1);
                isFadeOut = true;
            }
        }
        else
        {
            // ���� ������ fadeTime * (1 - fadeInRatio) �ð� ���� Alpha���� 1�� ������־����
            controlAlpha.color -= new Color(0, 0, 0, (Time.deltaTime / fadeoutTime));
            if (controlAlpha.color.a <= 0)
            {
                controlAlpha.color = new Color(controlAlpha.color.r, controlAlpha.color.g, controlAlpha.color.b, 0);
                gameObject.SetActive(false);
            }
        }
    }
}