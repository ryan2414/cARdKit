using UnityEngine;

public class DetectArea : MonoBehaviour
{
    //인식 당하는거
    public GameObject interactionGameObject;
    public Vector2 interactionGameObjectPoint;

    //뽑아 내는거
    public GameObject[] targetPoints;
    public Vector2[] targetPolygons;

    [SerializeField]
    bool isIn;

    // Start is called before the first frame update
    void Start()
    {
        targetPolygons = new Vector2[targetPoints.Length];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < targetPoints.Length; i++)
        {
            targetPolygons[i] = Camera.main.WorldToScreenPoint(targetPoints[i].transform.position);
        }
        interactionGameObjectPoint = Camera.main.WorldToScreenPoint(interactionGameObject.transform.position);

        isIn = PolyUtil.IsPointInPolygon(interactionGameObjectPoint, targetPolygons);
    }
}