using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDown : MonoBehaviour
{
    public float smallScale;
    float currnetScale;
    public float largeScale;
    float targetScale_Down;

    public float rescaleDownSpeed;
    public float moveSpeed;

    public RectTransform startPosition;
    public RectTransform endPosition;

    RectTransform rectTransform;
    private void OnEnable()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.position = startPosition.position;
        rectTransform.localScale = new Vector3(largeScale, largeScale, largeScale);

        targetScale_Down = Mathf.Abs(largeScale - smallScale);

        StopCoroutine("IEScaleDown");
        StartCoroutine("IEScaleDown");

    }

    IEnumerator IEScaleDown()
    {
        while (true)
        {
            rectTransform.position = Vector3.MoveTowards(rectTransform.position, endPosition.position, moveSpeed * Time.deltaTime);

            currnetScale -= targetScale_Down * Time.deltaTime / rescaleDownSpeed;
            float reScale = largeScale + currnetScale;
            rectTransform.localScale = new Vector3(reScale, reScale, reScale);

            if (reScale <= smallScale)
            {
                rectTransform.localScale = new Vector3(smallScale, smallScale, smallScale);
                currnetScale = smallScale;

                this.enabled = false;
                gameObject.SetActive(false);
                break;
            }

            yield return new WaitForSeconds(Time.deltaTime);

        }

    }
}
