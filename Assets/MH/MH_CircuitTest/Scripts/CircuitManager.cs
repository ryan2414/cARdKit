using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 회로에 전원(Power)이 있는지를 확인하고
// 있으면 회로 전원의 Plus 와 Minus가 Node가 연결되어 있는지를 확인해서
// 연결 되어있을 시 연결되어있는 소자들이 기동 할 수 있도록 한다.

// 1. 전원을 검색 
// 2. -> 있을 시 전원의 Node에 접근 
// 3. -> isPowerSupplied && isGrounded 가 true일경우 
// 4. -> CircuitManager의 동작 기능을 활성화

// ** 또는, 싱글턴을 통해 전원에서 조건을 충족 시 CircuitManager의 동작 기능을 활성화

public class CircuitManager : MonoBehaviour
{
    #region #싱글턴
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
