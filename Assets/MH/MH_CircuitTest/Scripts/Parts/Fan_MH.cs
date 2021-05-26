using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan_MH : MonoBehaviour
{
    public float resistanceValue;
    NodeManager_MH nodeManager;
    Resist_MH resist;

    Node_MH node1;
    Node_MH node2;

    public GameObject turnningShaft;

    // record_rotation: 0 -> current, 1 -> pre, 2 -> prepre
    float[] record_rotation = new float[3];
    float diff;
    float diff_ratio;
    float speedUp;
    Vector3 speed;

    void Start()
    {
        nodeManager = GetComponent<NodeManager_MH>();
        resist = GetComponent<Resist_MH>();
        resist.R = resistanceValue;
        node1 = nodeManager.nodes[0];
        node2 = nodeManager.nodes[1];

    }

    void Update()
    {
        ConnectCheck();
        NodeInteraction();
        ContactNodeInteraction();

        TurnOnFan();
    }

    private void OnDisable()
    {
        CircuitManager_MH.instance.NodeInitialize();
        diff = 0;
        speedUp = 0;
    }

    void ConnectCheck()
    {
        if (node1.CompareTag("PlusNode") && node2.CompareTag("MinusNode"))
        {
            nodeManager.isPartReady = true;
        }
        else if (node1.CompareTag("MinusNode") && node2.CompareTag("PlusNode"))
        {
            nodeManager.isPartReady = true;
        }
        else
        {
            nodeManager.isPartReady = false;
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

        if (node2.isPowerSupplied)
        {
            node1.isPowerSupplied = node2.isPowerSupplied;
        }
        if (node2.isGrounded)
        {
            node1.isGrounded = node2.isGrounded;
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



    void TurnOnFan()
    {
        if (CircuitManager_MH.instance.isCircuitActivated && nodeManager.isPartReady)
        {
            resist.SetFixedVoltage();

            float spd = resist.FanSpeedUp();

            // 갑자기 무한대로 튀는 현상을 방지 하기위한 코드
            if (resist.FanSpeedUp() > 1000000)
            {
                spd = 1;
            }

            speedUp = Mathf.Lerp(speedUp, spd, 0.006f);

            speed = new Vector3(0, speedUp, 0);
            turnningShaft.transform.Rotate(speed);

            if (resist.V_fixed > 4.8f)
            {
                CircuitManager_MH.instance.isStageClearSatisfied = true;
            }
            else
            {
                CircuitManager_MH.instance.isStageClearSatisfied = false;
            }

            #region #Rotation Check For When Stop
            record_rotation[0] = turnningShaft.transform.localEulerAngles.y;
            // diff가 - 350정도가 나오는 경우가 있음
            // => 이때, 회전이 정지될 경우 빠르게 역회전을 함으로 이 값을 버려주기위해 차이값 이중체크 함
            diff = record_rotation[0] - record_rotation[1];
            if (diff < 0)
            {
                diff = record_rotation[1] - record_rotation[2];
            }
            record_rotation[2] = record_rotation[1];
            record_rotation[1] = record_rotation[0];
            diff_ratio = diff * 0.0008f;
            #endregion
        }
        else
        {
            resist.SetFixedVoltage();
            //turnningShaft.transform.Rotate((resist.FanSpeedDown(diff * 1.6f)));
            turnningShaft.transform.Rotate(Vector3.zero);
            turnningShaft.GetComponent<Rigidbody>().angularVelocity = turnningShaft.GetComponent<Rigidbody>().angularVelocity;
            //diff -= diff_ratio;
            //if (diff <= 0)
            //{
            //    diff = 0;
            //}
            speedUp = 0;
        }
    }


}
