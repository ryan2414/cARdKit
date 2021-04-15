using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 접촉한 Node가 한개가 아니라면??? -> 기존: Node, 변경: Node[]
// 접속을 Wire 또는 소자들이 직접하므로 변경필요 -> 기존 Node[], 변경: NodeManager[]
// 어차피 접속한 Node들의 연쇄작용을 통해 모든 Node가 초기화 되어야 함 -> 기존: 연결되어있는것을 찾아서 초기화, 변경: CircuitManager에서 모든 Node를 초기화

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

            // 외부의 Node가 Node들에 중복으로 붙어있는 곳에 붙을 경우 붙은곳에 있는 bool값 초기화
            if (isSeparated == true)
            {
                for (int i = 0; i < ContactedNodes.Length; i++)
                {
                    ContactedNodes[i].Initialize();
                }
                isSeparated = false;
            }

            // 내 태그는 Node고 들어온Tag는 Node가 아닐때
            if (gameObject.CompareTag("Node") && !other.gameObject.CompareTag("Node"))
            {
                gameObject.tag = other.gameObject.tag;
            }


            // 다른 Node와 접촉함
            isConnected = true;

            // PlusNode와 접촉할 경우 ++++++
            if (other.gameObject.CompareTag("PlusNode"))
            {
                isPowerSupplied = true;
            }
            // MinusNode와 접촉할 경우 ------
            if (other.gameObject.CompareTag("MinusNode"))
            {
                isGrounded = true;
            }
            // 다른 Node들과 접촉할 경우
            if (other.gameObject.CompareTag("Node"))
            {
                Node contactNode = other.gameObject.GetComponent<Node>();
                // 접촉한 Node가 +와 연결되어있을 경우 +++++++
                if (contactNode.isPowerSupplied == true)
                {
                    isPowerSupplied = true;
                }
                // 접촉한 Node가 -와 연결되어있을 경우 -------
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
