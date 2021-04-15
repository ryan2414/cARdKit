using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ȸ�ο� ����(Power)�� �ִ����� Ȯ���ϰ�
// ������ ȸ�� ������ Plus �� Minus�� Node�� ����Ǿ� �ִ����� Ȯ���ؼ�
// ���� �Ǿ����� �� ����Ǿ��ִ� ���ڵ��� �⵿ �� �� �ֵ��� �Ѵ�.

// 1. ������ �˻� 
// 2. -> ���� �� ������ Node�� ���� 
// 3. -> isPowerSupplied && isGrounded �� true�ϰ�� 
// 4. -> CircuitManager�� ���� ����� Ȱ��ȭ

// ** �Ǵ�, �̱����� ���� �������� ������ ���� �� CircuitManager�� ���� ����� Ȱ��ȭ

public class CircuitManager : MonoBehaviour
{
    #region #�̱���
    public static CircuitManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public bool isCircuitActivated;

    void Start()
    {

    }


    void Update()
    {

    }

    public void NodeInitialize()
    {
        Node[] nodes = GameObject.FindGameObjectsWithTag("Node").Select(obj => obj.GetComponent<Node>()).ToArray();
        Node[] plusNodes = GameObject.FindGameObjectsWithTag("PlusNode").Select(obj => obj.GetComponent<Node>()).ToArray();
        Node[] minusNodes = GameObject.FindGameObjectsWithTag("MinusNode").Select(obj => obj.GetComponent<Node>()).ToArray();

        if (nodes != null)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].Initialize();
            }
        }
        if (plusNodes != null)
        {
            for (int i = 0; i < plusNodes.Length; i++)
            {
                plusNodes[i].Initialize();
            }
        }
        if (minusNodes != null)
        {
            for (int i = 0; i < minusNodes.Length; i++)
            {
                minusNodes[i].Initialize();
            }
        }
    }

}
