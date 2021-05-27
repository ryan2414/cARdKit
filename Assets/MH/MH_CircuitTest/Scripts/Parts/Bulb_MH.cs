using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulb_MH : MonoBehaviour
{
    public float resistanceValue;
    NodeManager_MH nodeManager;
    Resist_MH resist;

    Node_MH node1;
    Node_MH node2;

    public GameObject bulbLight;
    public ParticleSystem bulbLightParticle;
    ParticleSystem.MinMaxCurve particleStartSize;

    void Start()
    {
        nodeManager = GetComponent<NodeManager_MH>();
        resist = GetComponent<Resist_MH>();
        resist.R = resistanceValue;
        node1 = nodeManager.nodes[0];
        node2 = nodeManager.nodes[1];

        bulbLight.SetActive(false);
    }

    void Update()
    {
        ConnectCheck();
        NodeInteraction();
        ContactNodeInteraction();

        TurnOnLight();
    }

    private void OnEnable()
    {
        bulbLight.GetComponent<Light>().intensity = 0;
    }

    private void OnDisable()
    {
        CircuitManager_MH.instance.NodeInitialize();
        bulbLight.GetComponent<Light>().intensity = 0;
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

    void TurnOnLight()
    {
        if (CircuitManager_MH.instance.isCircuitActivated && nodeManager.isPartReady)
        {
            bulbLight.SetActive(true);
            resist.SetFixedVoltage();

            bulbLight.GetComponent<Light>().intensity = Mathf.Lerp(bulbLight.GetComponent<Light>().intensity, resist.LightIntensity(), 0.018f);

            ParticleSystem.MainModule main = bulbLightParticle.main;
            particleStartSize = resist.ParticleStartSize();
            main.startSize = particleStartSize;
            if (bulbLight.GetComponent<Light>().intensity > 1.6f)
            {
                CircuitManager_MH.instance.isStageClearSatisfied = true;
            }
            else
            {
                CircuitManager_MH.instance.isStageClearSatisfied = false;
            }
        }
        else
        {
            resist.SetFixedVoltage();
            bulbLight.GetComponent<Light>().intensity = Mathf.Lerp(bulbLight.GetComponent<Light>().intensity, 0, 0.018f);

            ParticleSystem.MainModule main = bulbLightParticle.main;
            main.startSize = 0;

            if (bulbLight.GetComponent<Light>().intensity < 0.2f)
            {
                bulbLight.GetComponent<Light>().intensity = 0;
                bulbLight.SetActive(false);
            }
        }
    }

}