using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_MH : MonoBehaviour
{
    public bool isConnected;
    public bool isPowerSupplied;
    public bool isGrounded;

    public bool isSeparated;

    // �� Node�� �θ� ����
    public NodeManager_MH nm_part;
    // �� Node�� ���� �θ��� �ٸ� ����
    public Node_MH oppositeNode;

    // �� Node�� �������ִ� Node��
    public Node_MH[] ContactNodes;
    // �� Node�� �������ִ� Node���� �ݴ��� Node��
    public Node_MH[] ContactOppositeNodes;

    public GameObject checker;

    // ���߿� ��� �� ���� �ִ� ���� ����
    public float nodeVoltage;

    private void Awake()
    {

    }

    void Start()
    {
        checker = GetComponentInChildren<Checker_MH>().checker;

        oppositeNode = nm_part.nodes.Where(opp => opp != this).ToArray()[0];
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Node"))
        {
            int layer = 1 << LayerMask.NameToLayer("Node");
            ContactNodes = Physics.OverlapBox(checker.transform.position, checker.transform.lossyScale / 2, Quaternion.identity, layer)
                                    .Select(obj => obj.gameObject)
                                    .Where(notThis => notThis != this.gameObject)
                                    .Select(cols => cols.gameObject.GetComponent<Node_MH>())
                                    .ToArray();

            if (ContactNodes != null && ContactNodes.Length > 0)
            {
                ContactOppositeNodes = ContactNodes.Select(opp => opp.oppositeNode).ToArray();
            }

            if (isSeparated == true)
            {
                for (int i = 0; i < ContactNodes.Length; i++)
                {
                    ContactNodes[i].Initialize();
                }
                isSeparated = false;
            }
            isConnected = true;


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Node"))
        {
            CircuitManager_MH.instance.NodeInitialize();
            isSeparated = true;
        }
    }

    // ���� ���� �� ���ڿ��� �� �Լ��� �����ؼ� Node_MH�� ���� NodeManager_MH������ �����
    public void CallNodeManager(NodeManager_MH target)
    {
        nm_part = target;
    }

    public void Initialize()
    {
        isConnected = false;
        isPowerSupplied = false;
        isGrounded = false;

        if (nm_part.gameObject.layer != LayerMask.NameToLayer("Power"))
        {
            gameObject.tag = "Node";
            nodeVoltage = 0;
        }

        ContactNodes = null;
        ContactOppositeNodes = null;
    }

}
