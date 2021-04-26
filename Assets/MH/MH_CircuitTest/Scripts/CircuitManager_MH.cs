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

    public float V_Total;
    public float R_Total;
    public float I_Total;

    RaycastHit hitinfo;

    GameObject[] interactionObject;
    int switchId;
    int potentiometerId;

    private void Start()
    {
        interactionObject = new GameObject[10];
        FingerIdResetTo99(ref switchId);
        FingerIdResetTo99(ref potentiometerId);
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

    [SerializeField]
    Switch_MH switchComp;

    Potentiometer_MH potentiometerComp;

    public void TouchInteraction()
    {
        // PC 터치
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //int layer = 1 << LayerMask.NameToLayer("Switch");
        //bool laycast = Physics.Raycast(ray, out hitinfo, float.MaxValue, layer);

        //if (laycast)
        //{
        //    switchComp = hitinfo.transform.GetComponentInParent<Switch_MH>();
        //}
        //if (!laycast || Input.GetMouseButtonUp(0))
        //{
        //    if (switchComp != null)
        //    {
        //        switchComp.isOn = false;
        //        switchComp = null;
        //    }
        //}
        //if (Input.GetMouseButton(0) && switchComp != null)
        //{
        //    switchComp.isOn = true;
        //    switchComp.isStateChange = true;
        //}

        // 모바일 터치
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                bool laycast = Physics.Raycast(ray, out hitinfo);

                if (laycast)
                {
                    if (hitinfo.transform.gameObject.layer == LayerMask.NameToLayer("Switch"))
                    {
                        switchId = touch.fingerId;
                        switchComp = hitinfo.transform.GetComponentInParent<Switch_MH>();
                        switchComp.isOn = true;
                        switchComp.isStateChange = true;
                    }
                }

                if (laycast)
                {
                    if (hitinfo.transform.gameObject.layer == LayerMask.NameToLayer("Potentiometer"))
                    {
                        potentiometerId = touch.fingerId;
                        interactionObject[potentiometerId] = hitinfo.transform.gameObject;
                    }
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (touch.fingerId == potentiometerId)
                {
                    interactionObject[potentiometerId].transform.localPosition += new Vector3(-touch.deltaPosition.y * 0.0025f, 0, 0);
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (touch.fingerId == switchId)
                {
                    switchComp.isOn = false;
                    switchComp = null;
                    FingerIdResetTo99(ref switchId);
                }

                if (touch.fingerId == potentiometerId)
                {
                    interactionObject[potentiometerId] = null;
                    FingerIdResetTo99(ref potentiometerId);
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
