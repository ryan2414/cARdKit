using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronCreate : MonoBehaviour
{
    public GameObject electronPrefab;

    public List<Transform> movePoints = new List<Transform>();
    //public List<GameObject> electrons = new List<GameObject>();

    [SerializeField]
    float timer = 3f;
    float currentTime;

    void Start()
    {
        currentTime = timer / 3;
    }

    void Update()
    {
        if (CircuitManager_MH.instance.isCircuitActivated)
        {
            currentTime += Time.deltaTime;
            if (currentTime > timer)
            {
                GameObject tempGo = Instantiate(electronPrefab);
                tempGo.transform.position = movePoints[0].position;
                tempGo.transform.parent = gameObject.transform;
                tempGo?.GetComponent<ElectronMove>().GetDepart(movePoints);

                //electrons.Add(tempGo);
                currentTime = 0;
            }
        }
    }
}
