using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteController_MH : MonoBehaviour
{
    public float weekWind;
    public float middleWind;
    public float strongWind;

    NodeManager_MH nodeManager;
    Node_MH node1;
    Node_MH node2;

    Resist_MH resist;
    public float resistanceValue;

    // 0 -> 정지, 1 -> 약, 2 -> 중, 3 -> 강
    public GameObject[] buttons;

    public bool isStateChange;
    bool isRemoteOn;
    bool isDisconnect;

    // 0 -> off, 1 -> on
    public List<Vector3> button0_trn = new List<Vector3>();
    public List<Vector3> button1_trn = new List<Vector3>();
    public List<Vector3> button2_trn = new List<Vector3>();
    public List<Vector3> button3_trn = new List<Vector3>();

    [SerializeField]
    List<RC_Button_MH> isButtonPushed = new List<RC_Button_MH>();

    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            isButtonPushed.Add(buttons[i].GetComponent<RC_Button_MH>());
        }
    }
    void Start()
    {
        nodeManager = GetComponent<NodeManager_MH>();
        resist = GetComponent<Resist_MH>();

        node1 = nodeManager.nodes[0];
        node2 = nodeManager.nodes[1];

        button0_trn.Add(buttons[0].transform.localPosition);
        button0_trn.Add(buttons[0].transform.localPosition - new Vector3(0, 0.03f, 0));
        button1_trn.Add(buttons[1].transform.localPosition);
        button1_trn.Add(buttons[1].transform.localPosition - new Vector3(0, 0.03f, 0));
        button2_trn.Add(buttons[2].transform.localPosition);
        button2_trn.Add(buttons[2].transform.localPosition - new Vector3(0, 0.03f, 0));
        button3_trn.Add(buttons[3].transform.localPosition);
        button3_trn.Add(buttons[3].transform.localPosition - new Vector3(0, 0.03f, 0));
    }

    private void OnEnable()
    {
        isButtonPushed[0].isOn = false;
        isButtonPushed[1].isOn = false;
        isButtonPushed[2].isOn = false;
        isButtonPushed[3].isOn = false;
        isStateChange = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ResetButtonPositions();
            isStateChange = true;
            isButtonPushed[0].isOn = true;
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            isButtonPushed[0].isPushing = true;
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            isButtonPushed[0].isPushing = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ResetButtonPositions();
            isStateChange = true;
            isButtonPushed[1].isOn = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ResetButtonPositions();
            isStateChange = true;
            isButtonPushed[2].isOn = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ResetButtonPositions();
            isStateChange = true;
            isButtonPushed[3].isOn = true;
        }

        // off
        if (isButtonPushed[0].isOn)
        {
            if (isStateChange)
            {
                ResetButtonPositions();
                isButtonPushed[0].isOn = true;
                isStateChange = false;
                nodeManager.isPartReady = false;
                isDisconnect = true;
            }
            isRemoteOn = false;

            resistanceValue = 9999f;
            resist.R = resistanceValue;

            if (isButtonPushed[0].isPushing)
            {
                buttons[0].transform.localPosition = button0_trn[1];
            }
            else
            {
                buttons[0].transform.localPosition = Vector3.Lerp(buttons[0].transform.localPosition, button0_trn[0], 0.1f);
            }
        }
        else
        {
            buttons[0].transform.localPosition = Vector3.Lerp(buttons[0].transform.localPosition, button0_trn[0], 0.1f);
        }
        // 약
        if (isButtonPushed[1].isOn)
        {
            if (isStateChange)
            {
                ResetButtonPositions();
                isButtonPushed[1].isOn = true;
                isStateChange = false;
            }
            isRemoteOn = true;
            buttons[1].transform.localPosition = button1_trn[1];
            resistanceValue = weekWind;
            resist.R = resistanceValue;
        }
        else
        {
            buttons[1].transform.localPosition = Vector3.Lerp(buttons[1].transform.localPosition, button1_trn[0], 0.1f);
        }
        // 중
        if (isButtonPushed[2].isOn)
        {
            if (isStateChange)
            {
                ResetButtonPositions();
                isButtonPushed[2].isOn = true;
                isStateChange = false;
            }
            isRemoteOn = true;
            resistanceValue = middleWind;
            resist.R = resistanceValue;
            buttons[2].transform.localPosition = button2_trn[1];
        }
        else
        {
            buttons[2].transform.localPosition = Vector3.Lerp(buttons[2].transform.localPosition, button2_trn[0], 0.1f);
        }
        // 강
        if (isButtonPushed[3].isOn)
        {
            if (isStateChange)
            {
                ResetButtonPositions();
                isButtonPushed[3].isOn = true;
                isStateChange = false;
            }
            isRemoteOn = true;
            resistanceValue = strongWind;
            resist.R = resistanceValue;
            buttons[3].transform.localPosition = button3_trn[1];
        }
        else
        {
            buttons[3].transform.localPosition = Vector3.Lerp(buttons[3].transform.localPosition, button3_trn[0], 0.1f);
        }

        if (isRemoteOn)
        {
            MergeContactNode();

            NodeInteraction();
            ContactNodeInteraction();
            ContactNodeTagShare();

            NodeCheck();
        }
        else if (!isRemoteOn)
        {
            if (isDisconnect)
            {
                isDisconnect = false;
                CircuitManager_MH.instance.NodeInitialize();
            }
        }
    }

    void MergeContactNode()
    {
        // 노드1번에 접촉한 Node가 있을 때 노드 2번의 접촉한 Node에 1번 노드를 끼워넣기
        if (node1.ContactNodes != null)
        {
            List<Node_MH> temp_node = new List<Node_MH>();
            if (node2.ContactNodes != null)
            {
                temp_node = node2.ContactNodes.ToList();
            }
            temp_node.AddRange(node1.ContactNodes);
            node2.ContactNodes = temp_node.Distinct().ToArray();
            node1.ContactNodes = temp_node.Distinct().ToArray();
        }

        // 노드2번에 접촉한 Node가 있을 때 노드 1번의 접촉한 Node에 2번 노드를 끼워넣기
        if (node2.ContactNodes != null)
        {
            List<Node_MH> temp_node = new List<Node_MH>();
            if (node1.ContactNodes != null)
            {
                temp_node = node1.ContactNodes.ToList();
            }
            temp_node.AddRange(node2.ContactNodes);
            node1.ContactNodes = temp_node.Distinct().ToArray();
            node2.ContactNodes = temp_node.Distinct().ToArray();
        }

    }

    private void NodeInteraction()
    {
        #region #한쪽에서 다른쪽으로 정보전달
        if (node1.isPowerSupplied)
        {
            node2.isPowerSupplied = node1.isPowerSupplied;
        }
        if (node1.isGrounded)
        {
            node2.isGrounded = node1.isGrounded;
        }
        if (!node1.CompareTag("Node") && node2.CompareTag("Node"))
        {
            node2.tag = node1.tag;
        }

        if (node2.isPowerSupplied)
        {
            node1.isPowerSupplied = node2.isPowerSupplied;
        }
        if (node2.isGrounded)
        {
            node1.isGrounded = node2.isGrounded;
        }
        if (!node2.CompareTag("Node") && node1.CompareTag("Node"))
        {
            node1.tag = node2.tag;
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

    void ContactNodeTagShare()
    {
        foreach (Node_MH node in nodeManager.nodes)
        {
            if (!node.CompareTag("Node"))
            {
                if (node.ContactNodes.Length > 0 && node.ContactNodes != null)
                {
                    foreach (Node_MH cont_node in node.ContactNodes)
                    {
                        if (cont_node.CompareTag("Node"))
                        {
                            cont_node.tag = node.tag;
                        }
                    }
                }
            }
        }
    }

    public void NodeCheck()
    {
        if (node1.isConnected && node1.isGrounded && node1.isPowerSupplied
            && node2.isConnected && node2.isGrounded && node2.isPowerSupplied)
        {
            nodeManager.isPartReady = true;
        }
        else
        {
            nodeManager.isPartReady = false;
        }
    }

    public void ResetButtonPositions()
    {
        isButtonPushed[0].isOn = false;
        isButtonPushed[1].isOn = false;
        isButtonPushed[2].isOn = false;
        isButtonPushed[3].isOn = false;
    }
}
