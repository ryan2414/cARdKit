using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 해당 소자가 가지고 있는 Node들의 정보를 가져와 저장

public class NodeManager_MH : MonoBehaviour
{
    public bool isPartReady;

    public Node_MH[] nodes;

    private void OnEnable()
    {
        nodes = GetComponentsInChildren<Node_MH>();
        for (int i = 0; i <nodes.Length; i++)
        {
            nodes[i].CallNodeManager(this);
        }
    }
}
