using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    public ParticleSystem ps;
    public ParticleSystem.MinMaxCurve mmcurve;
    float fl;
    // Start is called before the first frame update
    void Start()
    {
        fl = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ParticleSystem.MainModule main = ps.main;
            fl+=0.2f;
            mmcurve = fl;
            main.startSize = mmcurve;
            
        }
        else if (Input.GetMouseButtonDown(1))
        {
            ParticleSystem.MainModule main = ps.main;
            fl-=0.2f;
            mmcurve = fl;
            main.startSize = mmcurve;
        }
    }
}
