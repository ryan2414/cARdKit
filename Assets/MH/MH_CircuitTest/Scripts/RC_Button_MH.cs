using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RC_Button_MH : MonoBehaviour
{
    public bool isOn;
    public bool isPushing;
    public RemoteController_MH rc;
    private void Awake()
    {
        rc = GetComponentInParent<RemoteController_MH>();
    }
}
