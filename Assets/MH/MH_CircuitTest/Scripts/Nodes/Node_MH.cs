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

    // 이 Node의 부모 소자
    public NodeManager_MH nm_part;
    // 이 Node와 같은 부모의 다른 소자
    public Node_MH oppositeNode;

    // 이 Node와 접촉해있는 Node들
    public Node_MH[] ContactNodes;
    // 이 Node와 접촉해있는 Node들의 반대쪽 Node들
    public Node_MH[] ContactOppositeNodes;

    public GameObject checker;

    // 나중에 사용 할 수도 있는 소자 전압
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

    // 최초 실행 시 소자에서 이 함수를 실행해서 Node_MH에 소자 NodeManager_MH정보가 담겨짐
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
