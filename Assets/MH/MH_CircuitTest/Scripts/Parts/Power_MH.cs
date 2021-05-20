using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_MH : MonoBehaviour
{
    NodeManager_MH nodeManager;

    Node_MH plusNode;
    Node_MH minusNode;

    public float powerVoltage;

    void Start()
    {
        nodeManager = GetComponent<NodeManager_MH>();

        plusNode = nodeManager.nodes.Where(plus => plus.gameObject.CompareTag("PlusNode")).ToArray()[0];
        minusNode = nodeManager.nodes.Where(plus => plus.gameObject.CompareTag("MinusNode")).ToArray()[0];

        plusNode.nodeVoltage = powerVoltage;
        minusNode.nodeVoltage = 0f;
    }

    void Update()
    {
        plusNode.isPowerSupplied = true;
        minusNode.isGrounded = true;

        NodeInteraction();
    }

    private void NodeInteraction()
    {
        #region # 전원부 Node연결 상호작용
        if (plusNode.ContactNodes != null)
        {
            foreach (Node_MH changeParams in plusNode.ContactNodes)
            {
                if (changeParams.CompareTag("Node"))
                {
                    changeParams.tag = plusNode.gameObject.tag;
                    changeParams.nodeVoltage = plusNode.nodeVoltage;
                    changeParams.isPowerSupplied = true;
                }

                if (changeParams.isGrounded)
                {
                    plusNode.isGrounded = true;
                }
            }
        }
        if (minusNode.ContactNodes != null)
        {
            foreach (Node_MH changeParams in minusNode.ContactNodes)
            {
                if (changeParams.CompareTag("Node"))
                {
                    changeParams.tag = minusNode.gameObject.tag;
                    changeParams.nodeVoltage = minusNode.nodeVoltage;
                    changeParams.isGrounded = true;
                }
                if (changeParams.isPowerSupplied)
                {
                    plusNode.isPowerSupplied = true;
                }
            }
        }

        if (plusNode.isConnected && plusNode.isPowerSupplied && plusNode.isGrounded)
        {
            nodeManager.isPartReady = true;
            CircuitManager_MH.instance.isCircuitActivated = true;
        }
        else if (minusNode.isConnected && minusNode.isPowerSupplied && minusNode.isGrounded)
        {
            nodeManager.isPartReady = true;
            CircuitManager_MH.instance.isCircuitActivated = true;
        }
        else
        {
            nodeManager.isPartReady = false;
            CircuitManager_MH.instance.isCircuitActivated = false;
        }
        #endregion
    }

    private void OnDisable()
    {
        CircuitManager_MH.instance.NodeInitialize();
        CircuitManager_MH.instance.isCircuitActivated = false;
    }
}
