using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    public Node[] nodes;

    void Start()
    {
        nodes = GetComponentsInParent<Transform>().Last().GetComponentsInChildren<Node>();
    }

    void Update()
    {
        // nodes 2개중 하나만 isPowerSupplied == true && isGrounded == true 이면 배터리는 작동 가능
        if (nodes[0].isPowerSupplied == true
            && nodes[0].isGrounded == true)
        {
            CircuitManager.instance.isCircuitActivated = true;
        }
        else
        {
            CircuitManager.instance.isCircuitActivated = false;
        }
    }
}
