using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulb_MH : MonoBehaviour
{
    public float resistanceValue;
    NodeManager_MH nodeManager;
    Resist_MH resist;

    Node_MH node1;
    Node_MH node2;

    public GameObject bulbLight;

    void Start()
    {
        nodeManager = GetComponent<NodeManager_MH>();
        resist = GetComponent<Resist_MH>();
        resist.resistance = resistanceValue;
        node1 = nodeManager.nodes[0];
        node2 = nodeManager.nodes[1];

        bulbLight.SetActive(false);
    }

    void Update()
    {
        ConnectCheck();
        NodeInteraction();
        ContactNodeInteraction();

        TurnOnLight();
    }

    void ConnectCheck()
    {
        if (node1.CompareTag("PlusNode") && node2.CompareTag("MinusNode"))
        {
            nodeManager.isPartReady = true;
        }
        else if (node1.CompareTag("MinusNode") && node2.CompareTag("PlusNode"))
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
        #region #���ʿ��� �ٸ������� ��������
        if (node1.isPowerSupplied)
        {
            node2.isPowerSupplied = node1.isPowerSupplied;
        }
        if (node1.isGrounded)
        {
            node2.isGrounded = node1.isGrounded;
        }

        if (node2.isPowerSupplied)
        {
            node1.isPowerSupplied = node2.isPowerSupplied;
        }
        if (node2.isGrounded)
        {
            node1.isGrounded = node2.isGrounded;
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

    void TurnOnLight()
    {
        if (CircuitManager_MH.instance.isCircuitActivated)
        {
            bulbLight.SetActive(true);
        }
        else
        {
            bulbLight.SetActive(false);
        }

    }

    void TransNodeType()
    {

    }

}