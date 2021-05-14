using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resist_MH : MonoBehaviour
{
    public float delta;
    public float maxFanSpeed;

    public bool isParallel;

    public float maxIntensity;
    public float maxLightSize;
    public float R;
    public float fixedResistance;

    public float V_fixed;

    NodeManager_MH nodeManager;
    Node_MH node1;
    Node_MH node2;

    void Start()
    {
        nodeManager = GetComponent<NodeManager_MH>();
        node1 = nodeManager.nodes[0];
        node2 = nodeManager.nodes[1];
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Node_MH node in nodeManager.nodes)
        {
            if (node.ContactNodes != null)
            {

            }
        }
        if (node1.ContactNodes != null)
        {
            NodeManager_MH[] temp_serchedNM1 = node1.ContactNodes.Select(nm => nm.nm_part).Where(comptag => comptag.CompareTag("Resistance")).ToArray();
            if (temp_serchedNM1 != null && temp_serchedNM1.Length > 0)
            {
                if (node2.ContactNodes != null)
                {
                    NodeManager_MH[] temp_serchedNM2 = node1.ContactNodes.Select(nm => nm.nm_part).Where(comptag => comptag.CompareTag("Resistance")).ToArray();
                    if (temp_serchedNM2 != null && temp_serchedNM2.Length > 0)
                    {

                    }
                }
            }
        }
    }

    public void SetFixedVoltage()
    {
        if (CircuitManager_MH.instance.I_Total != 0)
        {
            V_fixed = CircuitManager_MH.instance.I_Total * R;
        }
        else
        {
            V_fixed = 0;
        }
    }

    public float LightIntensity()
    {
        // Intensity  = (MaxIntensity x V_fixed) / (maxVoltage)
        float intensity = (maxIntensity * V_fixed) / (5);
        if (intensity >= maxIntensity)
        {
            intensity = maxIntensity;
        }
        return intensity;
    }

    public float ParticleStartSize()
    {
        // Intensity  = (maxLightSize x V_fixed) / (maxVoltage)
        float lightSize = (maxLightSize * V_fixed) / (5);
        if (lightSize >= maxLightSize)
        {
            lightSize = maxLightSize;
        }
        return lightSize;
    }

    public float FanSpeedUp()
    {
        // speed  = (maxFanSpeed x V_fixed) / (maxVoltage)
        float speed = (maxFanSpeed * V_fixed * Time.deltaTime * delta) / (5);

        return speed;
    }

    public Vector3 FanSpeedDown(float diff)
    {
        // speed  = (maxFanSpeed x V_fixed) / (maxVoltage)
        Vector3 speed = new Vector3(0, diff * Time.deltaTime * delta, 0);

        return speed;
    }
}
