using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ Node�� �Ѱ��� �ƴ϶��??? -> ����: Node, ����: Node[]
// ������ Wire �Ǵ� ���ڵ��� �����ϹǷ� �����ʿ� -> ���� Node[], ����: NodeManager[]
// ������ ������ Node���� �����ۿ��� ���� ��� Node�� �ʱ�ȭ �Ǿ�� �� -> ����: ����Ǿ��ִ°��� ã�Ƽ� �ʱ�ȭ, ����: CircuitManager���� ��� Node�� �ʱ�ȭ

public class Node : MonoBehaviour
{
    public bool isConnected;
    public bool isPowerSupplied;
    public bool isGrounded;

    public bool isSeparated;

    public Node[] ContactedNodes;
    public GameObject Checker;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Node"))
        {

            int layer = 1 << LayerMask.NameToLayer("Node");
            ContactedNodes = Physics.OverlapBox(Checker.transform.position, Checker.transform.lossyScale / 2, Quaternion.identity, layer)
                                    .Select(cols => cols.gameObject.GetComponent<Node>())
                                    .ToArray();

            // �ܺ��� Node�� Node�鿡 �ߺ����� �پ��ִ� ���� ���� ��� �������� �ִ� bool�� �ʱ�ȭ
            if (isSeparated == true)
            {
                for (int i = 0; i < ContactedNodes.Length; i++)
                {
                    ContactedNodes[i].Initialize();
                }
                isSeparated = false;
            }

            // �� �±״� Node�� ����Tag�� Node�� �ƴҶ�
            if (gameObject.CompareTag("Node") && !other.gameObject.CompareTag("Node"))
            {
                gameObject.tag = other.gameObject.tag;
            }


            // �ٸ� Node�� ������
            isConnected = true;

            // PlusNode�� ������ ��� ++++++
            if (other.gameObject.CompareTag("PlusNode"))
            {
                isPowerSupplied = true;
            }
            // MinusNode�� ������ ��� ------
            if (other.gameObject.CompareTag("MinusNode"))
            {
                isGrounded = true;
            }
            // �ٸ� Node��� ������ ���
            if (other.gameObject.CompareTag("Node"))
            {
                Node contactNode = other.gameObject.GetComponent<Node>();
                // ������ Node�� +�� ����Ǿ����� ��� +++++++
                if (contactNode.isPowerSupplied == true)
                {
                    isPowerSupplied = true;
                }
                // ������ Node�� -�� ����Ǿ����� ��� -------
                if (contactNode.isGrounded == true)
                {
                    isGrounded = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Node"))
        {
            CircuitManager.instance.NodeInitialize();
            ContactedNodes = null;
        }
        isSeparated = true;
    }

    public void Initialize()
    {
        isConnected = false;
        isPowerSupplied = false;
        isGrounded = false;

        if (GetComponentsInParent<Transform>().Last().gameObject.CompareTag("Power") == false)
        {
            gameObject.tag = "Node";
        }
    }

}
