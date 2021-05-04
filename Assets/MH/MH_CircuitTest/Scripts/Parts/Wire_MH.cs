using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire_MH : MonoBehaviour
{
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
        NodeCheck();
        MergeContactNode();

        NodeInteraction();
        ContactNodeInteraction();
        ContactNodeTagShare();
    }

    public void NodeCheck()
    {
        if (node1.isConnected && node1.isGrounded && node1.isPowerSupplied
            && node2.isConnected && node2.isGrounded && node2.isPowerSupplied)
        {
            nodeManager.isPartReady = true;
        }
        else
        {
            nodeManager.isPartReady = false;
        }
    }

    private void NodeInteraction()
    {
        #region #한쪽에서 다른쪽으로 정보전달
        if (node1.isPowerSupplied)
        {
            node2.isPowerSupplied = node1.isPowerSupplied;
        }
        if (node1.isGrounded)
        {
            node2.isGrounded = node1.isGrounded;
        }
        if (!node1.CompareTag("Node") && node2.CompareTag("Node"))
        {
            node2.tag = node1.tag;
        }

        if (node2.isPowerSupplied)
        {
            node1.isPowerSupplied = node2.isPowerSupplied;
        }
        if (node2.isGrounded)
        {
            node1.isGrounded = node2.isGrounded;
        }
        if (!node2.CompareTag("Node") && node1.CompareTag("Node"))
        {
            node1.tag = node2.tag;
        }
        #endregion
    }

    void ContactNodeInteraction()
    {
        if (node1.ContactNodes != null && node1.ContactNodes.Length > 0)
        {
            if (node1.isPowerSupplied)
            {
                foreach (Node_MH contactnode in node1.ContactNodes)
                {
                    contactnode.isPowerSupplied = node1.isPowerSupplied;
                }
            }
            if (node1.isGrounded)
            {
                foreach (Node_MH contactnode in node1.ContactNodes)
                {
                    contactnode.isGrounded = node1.isGrounded;
                }
            }
        }

        if (node2.ContactNodes != null && node2.ContactNodes.Length > 0)
        {
            if (node2.isPowerSupplied)
            {
                foreach (Node_MH contactnode in node2.ContactNodes)
                {
                    contactnode.isPowerSupplied = node2.isPowerSupplied;
                }
            }
            if (node2.isGrounded)
            {
                foreach (Node_MH contactnode in node2.ContactNodes)
                {
                    contactnode.isGrounded = node2.isGrounded;
                }
            }
        }
    }

    void ContactNodeTagShare()
    {
        foreach (Node_MH node in nodeManager.nodes)
        {
            if (!node.CompareTag("Node"))
            {
                if (node.ContactNodes.Length > 0 && node.ContactNodes != null)
                {
                    foreach (Node_MH cont_node in node.ContactNodes)
                    {
                        if (cont_node.CompareTag("Node"))
                        {
                            cont_node.tag = node.tag;
                        }
                    }
                }
            }
        }
    }

    void MergeContactNode()
    {
        // 노드1번에 접촉한 Node가 있을 때 노드 2번의 접촉한 Node에 1번 노드를 끼워넣기
        if (node1.ContactNodes != null)
        {
            List<Node_MH> temp_node = new List<Node_MH>();
            if (node2.ContactNodes != null)
            {
                temp_node = node2.ContactNodes.ToList();
            }
            temp_node.AddRange(node1.ContactNodes);
            node2.ContactNodes = temp_node.Distinct().ToArray();
            node1.ContactNodes = temp_node.Distinct().ToArray();
        }

        // 노드2번에 접촉한 Node가 있을 때 노드 1번의 접촉한 Node에 2번 노드를 끼워넣기
        if (node2.ContactNodes != null)
        {
            List<Node_MH> temp_node = new List<Node_MH>();
            if (node1.ContactNodes != null)
            {
                temp_node = node1.ContactNodes.ToList();
            }
            temp_node.AddRange(node2.ContactNodes);
            node1.ContactNodes = temp_node.Distinct().ToArray();
            node2.ContactNodes = temp_node.Distinct().ToArray();
        }

    }
}
