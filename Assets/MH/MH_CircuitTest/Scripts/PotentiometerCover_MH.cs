using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotentiometerCover_MH : MonoBehaviour
{
    public GameObject cover;
    public Transform[] controllerMoveLimit;
    Transform min_point;
    Transform max_point;
    public Transform[] points;
    Transform point_Start;
    Transform point_End;
    public Transform controllerPoint;
    void Start()
    {
        cover = gameObject;
        min_point = controllerMoveLimit[0];
        max_point = controllerMoveLimit[1];

        point_Start = points[0];
        point_End = points[1];
    }

    // Update is called once per frame
    void Update()
    {
        point_End.position = controllerPoint.position;

        cover.transform.localPosition = (point_Start.localPosition + point_End.localPosition) * 0.5f;
        
        if (point_End.localPosition.y > max_point.localPosition.y)
        {
            point_End.localPosition = max_point.localPosition;
        }
        else if (point_End.localPosition.y < min_point.localPosition.y)
        {
            point_End.localPosition = min_point.localPosition;
        }

        cover.transform.localScale = new Vector3(cover.transform.localScale.x,
                                                 Vector3.Distance(point_Start.localPosition, point_End.localPosition),
                                                 cover.transform.localScale.z);

    }
}
