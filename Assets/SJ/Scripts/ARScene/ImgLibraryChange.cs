using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class ImgLibraryChange : MonoBehaviour
{
    ARTrackedImageManager manager;
    private void Start()
    {
        int stgNUm = FlagManager.instance.stageNum;

        manager = gameObject.GetComponent<ARTrackedImageManager>();

        if (stgNUm == 1)
        {
            manager.referenceLibrary = Resources.Load("ImageLibrary/Stage1-1") as XRReferenceImageLibrary;
        }
        else if (stgNUm == 2)
        {
            manager.referenceLibrary = Resources.Load("ImageLibrary/Stage1-2") as XRReferenceImageLibrary;
        }
        else if (stgNUm == 3)
        {
            manager.referenceLibrary = Resources.Load("ImageLibrary/Stage1-3") as XRReferenceImageLibrary;
        }
        else if (stgNUm == 5)
        {
            manager.referenceLibrary = Resources.Load("ImageLibrary/Stage2-1") as XRReferenceImageLibrary;
        }
        else if (stgNUm == 6)
        {
            manager.referenceLibrary = Resources.Load("ImageLibrary/Stage2-2") as XRReferenceImageLibrary;
        }

    }
}
