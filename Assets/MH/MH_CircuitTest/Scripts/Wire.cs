using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    NodeManager nodeManager;
    // Start is called before the first frame update
    void Start()
    {
        nodeManager = GetComponent<NodeManager>();
    }

    void Update()
    {
        NodeTagTrans();
    }

    void NodeTagTrans()
    {
        if (!nodeManager.nodes[0].CompareTag("Node"))
        {
            nodeManager.nodes[1].tag = nodeManager.nodes[0].tag;
        }
        else if (!nodeManager.nodes[1].CompareTag("Node"))
        {
            nodeManager.nodes[0].tag = nodeManager.nodes[1].tag;
        }
    }

}
