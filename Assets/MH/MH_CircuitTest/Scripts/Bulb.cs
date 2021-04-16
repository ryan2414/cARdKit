using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulb : MonoBehaviour
{
    public GameObject yellowSphere;
    public GameObject yellowLight;
    NodeManager nodeManager;
    // Start is called before the first frame update
    void Start()
    {
        yellowSphere.SetActive(false);
        yellowLight.SetActive(false);
        nodeManager = gameObject.GetComponent<NodeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (CircuitManager.instance.isCircuitActivated == true
            && nodeManager.isPartReady == true)
        {
            yellowSphere.SetActive(true);
            yellowLight.SetActive(true);
        }
        else
        {
            yellowSphere.SetActive(false);
            yellowLight.SetActive(false);
        }
    }
}
