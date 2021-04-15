using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 소자에 들어가야 하는 스크립트

public class NodeManager : MonoBehaviour
{
    public Node[] nodes;
    public bool isConnectRight;
    public bool isPartReady;

    // Switch소자가 작동할때 조작할 수 있는 변수
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
                //nodes[1]에 접촉한 놈있다면 그놈에게 node[0]의 isPowerSupplied
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
                //nodes[1]에 접촉한 놈있다면 그놈에게 node[0]의 isGrounded
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
                //nodes[0]에 접촉한 놈있다면 그놈에게 node[1]의 isPowerSupplied
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
                //nodes[0]에 접촉한 놈있다면 그놈에게 node[1]의 isGrounded
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
