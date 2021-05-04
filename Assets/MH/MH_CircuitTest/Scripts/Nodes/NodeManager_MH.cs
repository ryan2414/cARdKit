using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 해당 소자가 가지고 있는 Node들의 정보를 가져와 저장

public class NodeManager_MH : MonoBehaviour
{
    public bool isPartReady;

    public Node_MH[] nodes;

    private void Awake()
    {
        nodes = GetComponentsInChildren<Node_MH>();
        for (int i = 0; i <nodes.Length; i++)
        {
            nodes[i].CallNodeManager(this);
        }
    }

    private void Update()
    {
        //NodeColorChange();
    }

    void NodeColorChange()
    {
        if (!gameObject.CompareTag("Power"))
        {
            if (isPartReady)
            {
                foreach (MeshRenderer mr in nodes.Select(m => m.GetComponent<MeshRenderer>()))
                {
                    mr.material.color = new Color(0.9811321f, 0.4674262f, 0.9589713f);
                }
            }
            else
            {
                foreach (MeshRenderer mr in nodes.Select(m => m.GetComponent<MeshRenderer>()))
                {
                    mr.material.color = Color.white;
                }
            }
        }
    }

}
