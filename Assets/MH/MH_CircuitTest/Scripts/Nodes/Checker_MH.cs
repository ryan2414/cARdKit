using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker_MH : MonoBehaviour
{
    public GameObject checker;

    private void OnEnable()
    {
        checker = GetComponent<BoxCollider>().gameObject;
    }
}
