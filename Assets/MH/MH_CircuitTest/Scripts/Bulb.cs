using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulb : MonoBehaviour
{
    public GameObject yellowSphere;
    NodeManager nodeManager;
    // Start is called before the first frame update
    void Start()
    {
        yellowSphere.SetActive(false);
        nodeManager = gameObject.GetComponent<NodeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (CircuitManager.instance.isCircuitActivated == true
            && nodeManager.isPartReady == true)
        {
            yellowSphere.SetActive(true);
        }
        else
        {
            yellowSphere.SetActive(false);
        }
    }
}
