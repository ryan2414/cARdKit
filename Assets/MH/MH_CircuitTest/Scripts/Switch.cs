using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject switchObject;
    public Transform[] switchTransforms;

    public NodeManager nodeManager;
    bool isStateChanged;

    private void OnEnable()
    {
        nodeManager = GetComponent<NodeManager>();
        isStateChanged = true;
    }

    Ray ray;
    bool rayCast;
    RaycastHit hitinfo;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layer = 1 << LayerMask.NameToLayer("Switch");
        rayCast = Physics.Raycast(ray, out hitinfo, float.MaxValue, layer);

        if (Input.GetMouseButtonDown(0) && rayCast == true)
        {
            NodeManager nm = hitinfo.transform.gameObject.GetComponentInParent<NodeManager>();
            nm.isBlock = !nm.isBlock;
            isStateChanged = true;
        }

        if (nodeManager.isBlock == false)
        {
            switchObject.transform.position = switchTransforms[0].position;

            print("스위치 작동 중");
            NodeTagTrans();
        }
        else if (nodeManager.isBlock == true)
        {
            switchObject.transform.position = switchTransforms[1].position;
            if (isStateChanged == true)
            {
                isStateChanged = false;
                print("스위치 작동 중지");
                CircuitManager.instance.NodeInitialize();
            }
        }
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
