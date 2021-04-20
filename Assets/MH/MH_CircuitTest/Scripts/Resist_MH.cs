using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resist_MH : MonoBehaviour
{
    public bool isParallel;

    public float resistance;
    public float fixedResistance;

    NodeManager_MH nodeManager;
    Node_MH node1;
    Node_MH node2;

    void Start()
    {
        nodeManager = GetComponent<NodeManager_MH>();
        node1 = nodeManager.nodes[0];
        node2 = nodeManager.nodes[1];
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Node_MH node in nodeManager.nodes)
        {
            if (node.ContactNodes != null)
            {

            }
        }
        if (node1.ContactNodes != null)
        {
            NodeManager_MH[] temp_serchedNM1 = node1.ContactNodes.Select(nm => nm.nm_part).Where(comptag => comptag.CompareTag("Resistance")).ToArray();
            if (temp_serchedNM1 != null && temp_serchedNM1.Length > 0)
            {
                if (node2.ContactNodes != null)
                {
                    NodeManager_MH[] temp_serchedNM2 = node1.ContactNodes.Select(nm => nm.nm_part).Where(comptag => comptag.CompareTag("Resistance")).ToArray();
                    if (temp_serchedNM2 != null && temp_serchedNM2.Length > 0)
                    {

                    }
                }
            }
        }
    }
}
