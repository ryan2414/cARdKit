using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ش� ���ڰ� ������ �ִ� Node���� ������ ������ ����

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
