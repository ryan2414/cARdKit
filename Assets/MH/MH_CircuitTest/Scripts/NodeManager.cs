using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ���ڿ� ���� �ϴ� ��ũ��Ʈ

public class NodeManager : MonoBehaviour
{
    public Node[] nodes;
    public bool isConnectRight;
    public bool isPartReady;

    // Switch���ڰ� �۵��Ҷ� ������ �� �ִ� ����
    public bool isBlock;

    private void OnEnable()
    {
        nodes = GetComponentsInParent<Transform>().Last().GetComponentsInChildren<Node>();
        isConnectRight = false;

        if (gameObject.CompareTag("Switch"))
        {
            isBlock = true;
        }
        else
        {
            isBlock = false;
        }
    }

    private void Update()
    {
        if (nodes.Length == 2)
        {
            ConnectCheck();

            if (nodes[0].isPowerSupplied == true)
            {
                if (nodes[1].isGrounded == true && isConnectRight == true)
                {
                    isPartReady = true;
                }
                //nodes[1]�� ������ ���ִٸ� �׳𿡰� node[0]�� isPowerSupplied
                if (nodes[1].isConnected == true && isBlock == false)
                {
                    for (int i = 0; i < nodes[1].ContactedNodes.Length; i++)
                    {
                        nodes[1].ContactedNodes[i].isPowerSupplied = true;
                    }

                }
            }
            else { isPartReady = false; }

            if (nodes[0].isGrounded == true)
            {
                if (nodes[1].isPowerSupplied == true && isConnectRight == true)
                {
                    isPartReady = true;
                }
                //nodes[1]�� ������ ���ִٸ� �׳𿡰� node[0]�� isGrounded
                if (nodes[1].isConnected == true && isBlock == false)
                {
                    for (int i = 0; i < nodes[1].ContactedNodes.Length; i++)
                    {
                        nodes[1].ContactedNodes[i].isGrounded = true;
                    }

                }
            }
            else { isPartReady = false; }

            if (nodes[1].isPowerSupplied == true)
            {
                if (nodes[0].isGrounded == true && isConnectRight == true)
                {
                    isPartReady = true;
                }
                //nodes[0]�� ������ ���ִٸ� �׳𿡰� node[1]�� isPowerSupplied
                if (nodes[0].isConnected == true && isBlock == false)
                {
                    for (int i = 0; i < nodes[0].ContactedNodes.Length; i++)
                    {
                        nodes[0].ContactedNodes[i].isPowerSupplied = true;
                    }

                }
            }
            else { isPartReady = false; }

            if (nodes[1].isGrounded == true)
            {
                if (nodes[0].isPowerSupplied == true && isConnectRight == true)
                {
                    isPartReady = true;
                }
                //nodes[0]�� ������ ���ִٸ� �׳𿡰� node[1]�� isGrounded
                if (nodes[0].isConnected == true && isBlock == false)
                {
                    for (int i = 0; i < nodes[0].ContactedNodes.Length; i++)
                    {
                        nodes[0].ContactedNodes[i].isGrounded = true;
                    }
                }
            }
            else { isPartReady = false; }
        }
    }

    void ConnectCheck()
    {
        if (nodes[0].CompareTag("PlusNode"))
        {
            if (nodes[1].CompareTag("MinusNode")) { isConnectRight = true; }
            else { isConnectRight = false; }
        }
        else if (nodes[0].CompareTag("MinusNode"))
        {
            if (nodes[1].CompareTag("PlusNode")) { isConnectRight = true; }
            else { isConnectRight = false; }
        }
        if (nodes[1].CompareTag("PlusNode"))
        {
            if (nodes[0].CompareTag("MinusNode")) { isConnectRight = true; }
            else { isConnectRight = false; }
        }
        else if (nodes[1].CompareTag("MinusNode"))
        {
            if (nodes[0].CompareTag("PlusNode")) { isConnectRight = true; }
            else { isConnectRight = false; }
        }
        else
        {
            isConnectRight = false;
        }
    }
}
