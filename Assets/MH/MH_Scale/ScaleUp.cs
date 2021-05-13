using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{
    public float smallScale;
    float currnetScale;
    public float largeScale;
    float targetScale_Up;

    public float rescaleUpSpeed;
    public float moveSpeed;

    public RectTransform startPosition;
    public RectTransform endPosition;

    RectTransform rectTransform;
    private void OnEnable()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.position = startPosition.position;
        rectTransform.localScale = new Vector3(smallScale, smallScale, smallScale);

        targetScale_Up = Mathf.Abs(largeScale - smallScale);

        StopCoroutine("IEScaleUp");
        StartCoroutine("IEScaleUp");

    }

    IEnumerator IEScaleUp()
    {
        while (true)
        {
            rectTransform.position = Vector3.MoveTowards(rectTransform.position, endPosition.position, moveSpeed * Time.deltaTime);

            currnetScale += targetScale_Up * Time.deltaTime / rescaleUpSpeed;
            float reScale = smallScale + currnetScale;
            rectTransform.localScale = new Vector3(reScale, reScale, reScale);

            if (reScale >= largeScale)
            {
                rectTransform.localScale = new Vector3(largeScale, largeScale, largeScale);
                currnetScale = 0;

                this.enabled = false;
                break;
            }

            yield return new WaitForSeconds(Time.deltaTime);

        }

    }

}
