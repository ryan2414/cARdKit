using UnityEngine;

public class DetectArea : MonoBehaviour
{
    //�ν� ���ϴ°�
    public GameObject interactionGameObject;
    public Vector2 interactionGameObjectPoint;

    //�̾� ���°�
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