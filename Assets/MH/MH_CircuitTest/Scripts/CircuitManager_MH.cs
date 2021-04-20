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

    public float V_Voltage;
    public float R_Resistance;
    public float I_Current;

    Ray ray;
    RaycastHit hitinfo;

    private void Update()
    {
        SwitchTouchInteraction();

        if (isCircuitActivated)
        {
            R_Resistance = GameObject.FindGameObjectsWithTag("Resistance")
                           .Select(nm => nm.GetComponent<NodeManager_MH>())
                           .Where(ready => ready.isPartReady)
                           .Select(resist => resist.gameObject.GetComponent<Resist_MH>())
                           .Select(value => value.resistance)
                           .Sum();
            V_Voltage = GameObject.FindGameObjectsWithTag("Power")
                           .Select(nm => nm.GetComponent<NodeManager_MH>())
                           .Where(ready => ready.isPartReady)
                           .Select(po => po.GetComponent<Power_MH>())
                           .Select(volt => volt.powerVoltage)
                           .Sum();
            I_Current = V_Voltage / R_Resistance;
        }
        else
        {
            R_Resistance = 0;
            V_Voltage = 0;
            I_Current = 0;
        }
    }

    [SerializeField]
    Switch_MH switchComp;
    public void SwitchTouchInteraction()
    {
        // PC 터치
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layer = 1 << LayerMask.NameToLayer("Switch");
        bool laycast = Physics.Raycast(ray, out hitinfo, float.MaxValue, layer);
        if (laycast)
        {
            switchComp = hitinfo.transform.GetComponentInParent<Switch_MH>();
        }
        if (!laycast || Input.GetMouseButtonUp(0))
        {
            if (switchComp != null)
            {
                switchComp.isOn = false;
                switchComp = null;
            }
        }
        if (Input.GetMouseButton(0) && switchComp != null)
        {
            switchComp.isOn = true;
            switchComp.isStateChange = true;
        }

        /* // 모바일 터치
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Canceled)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                int layer = 1 << LayerMask.NameToLayer("Switch");
                bool laycast = Physics.Raycast(ray, out hitinfo, float.MaxValue, layer);
                switchComp = hitinfo.transform.GetComponentInParent<Switch_MH>();
                if (laycast)
                {
                    switchComp.isOn = true;
                    switchComp.isStateChange = true;
                }
                else if (!laycast)
                {
                    switchComp.isOn = false;
                }

            }
            if (touch.phase == TouchPhase.Canceled)
            {
                if (switchComp != null)
                {
                    switchComp.isOn = false;
                    switchComp = null;
                }
            }
        }
        */
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
}
