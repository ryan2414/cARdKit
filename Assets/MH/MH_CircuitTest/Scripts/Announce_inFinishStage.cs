using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Announce_inFinishStage : MonoBehaviour
{
    public GameObject Manager;

    public void CallAnnounce()
    {
        Manager.GetComponent<StageManager_WorldMap>().AnnounceThankyou();
    }
}
