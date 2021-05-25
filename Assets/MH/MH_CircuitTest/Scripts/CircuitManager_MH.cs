using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitManager_MH : MonoBehaviour
{
    #region #싱글턴
    public static CircuitManager_MH instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public bool isCircuitActivated;
    public bool isStageClearSatisfied;

    public float V_Total;
    public float R_Total;
    public float I_Total;

    RaycastHit hitinfo;

    GameObject[] interactionObject;
    int switchId;
    int potentiometerId;
    int RemoteControllerId;

    private void Start()
    {
        isStageClearSatisfied = false;
        interactionObject = new GameObject[10];
        FingerIdResetTo99(ref switchId);
        FingerIdResetTo99(ref potentiometerId);
        FingerIdResetTo99(ref RemoteControllerId);
    }

    private void Update()
    {
        TouchInteraction();

        if (isCircuitActivated)
        {
            ValueSet();
        }
        else
        {
            ValueReset();
        }
    }

    void ValueSet()
    {
        R_Total = GameObject.FindGameObjectsWithTag("Resistance")
                       .Select(nm => nm.GetComponent<NodeManager_MH>())
                       .Where(ready => ready.isPartReady)
                       .Select(resist => resist.gameObject.GetComponent<Resist_MH>())
                       .Select(value => value.R)
                       .Sum();

        V_Total = GameObject.FindGameObjectsWithTag("Power")
                       .Select(nm => nm.GetComponent<NodeManager_MH>())
                       .Where(ready => ready.isPartReady)
                       .Select(po => po.GetComponent<Power_MH>())
                       .Select(volt => volt.powerVoltage)
                       .Sum();
        I_Total = V_Total / R_Total;
    }

    void ValueReset()
    {
        R_Total = 0;
        V_Total = 0;
        I_Total = 0;
    }

    Switch_MH switchComp;

    public void TouchInteraction()
    {
        // 모바일 터치
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                bool laycast = Physics.Raycast(ray, out hitinfo);

                if (laycast)
                {
                    #region Toggle식 스위치
                    //if (hitinfo.transform.gameObject.layer == LayerMask.NameToLayer("Switch"))
                    //{
                    //    switchId = touch.fingerId;
                    //    switchComp = hitinfo.transform.GetComponentInParent<Switch_MH>();
                    //    switchComp.isOn = true;
                    //    switchComp.isStateChange = true;
                    //}
                    #endregion

                    #region On/Off식 스위치
                    if (hitinfo.transform.gameObject.layer == LayerMask.NameToLayer("Switch"))
                    {
                        switchId = touch.fingerId;
                        switchComp = hitinfo.transform.GetComponentInParent<Switch_MH>();
                        switchComp.isOn = !switchComp.isOn;
                        switchComp.isStateChange = true;
                    }
                    #endregion
                }

                if (laycast)
                {
                    if (hitinfo.transform.gameObject.layer == LayerMask.NameToLayer("Potentiometer"))
                    {
                        potentiometerId = touch.fingerId;
                        interactionObject[potentiometerId] = hitinfo.transform.gameObject;
                    }
                }

                if (laycast)
                {
                    if (hitinfo.transform.gameObject.layer == LayerMask.NameToLayer("RemoteController"))
                    {
                        RemoteControllerId = touch.fingerId;
                        interactionObject[RemoteControllerId] = hitinfo.transform.gameObject;
                        interactionObject[RemoteControllerId].GetComponent<RC_Button_MH>().rc.ResetButtonPositions();
                        interactionObject[RemoteControllerId].GetComponent<RC_Button_MH>().rc.isStateChange = true;
                        interactionObject[RemoteControllerId].GetComponent<RC_Button_MH>().isOn = true;
                        interactionObject[RemoteControllerId].GetComponent<RC_Button_MH>().isPushing = true;
                    }
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (touch.fingerId == potentiometerId)
                {
                    interactionObject[potentiometerId].transform.localPosition += new Vector3(0, touch.deltaPosition.y * 0.0000125f, 0);
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (touch.fingerId == switchId)
                {
                    #region Toggle식 스위치
                    //switchComp.isOn = false;
                    //switchComp = null;
                    //FingerIdResetTo99(ref switchId);
                    #endregion

                    #region On/Off식 스위치
                    switchComp = null;
                    FingerIdResetTo99(ref switchId);
                    #endregion
                }

                if (touch.fingerId == potentiometerId)
                {
                    interactionObject[potentiometerId] = null;
                    FingerIdResetTo99(ref potentiometerId);
                }

                if (touch.fingerId == RemoteControllerId)
                {
                    interactionObject[RemoteControllerId].GetComponent<RC_Button_MH>().isPushing = false;
                    interactionObject[RemoteControllerId] = null;
                    FingerIdResetTo99(ref RemoteControllerId);
                }
            }
        }
    }

    public void NodeInitialize()
    {
        Node_MH[] nodes = GameObject.FindGameObjectsWithTag("Node").Select(obj => obj.GetComponent<Node_MH>()).ToArray();
        Node_MH[] plusNodes = GameObject.FindGameObjectsWithTag("PlusNode").Select(obj => obj.GetComponent<Node_MH>()).ToArray();
        Node_MH[] minusNodes = GameObject.FindGameObjectsWithTag("MinusNode").Select(obj => obj.GetComponent<Node_MH>()).ToArray();

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

    public Node_MH[] ContactedOppositeNode(Node_MH node)
    {
        if (node.ContactNodes != null && node.ContactNodes.Length > 0)
        {
            Node_MH[] contactOppositeNodes = node.ContactNodes.Select(opp => opp.oppositeNode).ToArray();
            return contactOppositeNodes;
        }
        return null;
    }
    int FingerIdResetTo99(ref int value)
    {
        value = 99;
        return value;
    }
}
