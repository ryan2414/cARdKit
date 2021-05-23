using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAnim_MH : MonoBehaviour
{
    public GameObject touchAnimation;
    Animator anim;
    void Start()
    {
        touchAnimation.SetActive(false);
        anim = touchAnimation.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                touchAnimation.SetActive(false);
                anim.StopPlayback();
                touchAnimation.transform.position = touch.position;
                touchAnimation.SetActive(true);
            }
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    touchAnimation.SetActive(false);
        //    anim.StopPlayback();
        //    touchAnimation.transform.position = Input.mousePosition;
        //    touchAnimation.SetActive(true);
        //}
    }
}
