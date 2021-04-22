using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    public static FlagManager instance;
    private void Awake()
    {
        instance = this;
        
    }

    public List<bool> clearFlags = new List<bool>();
    

    private void Start()
    {
        //int count = StageManager_WorldMap.instance.btnStage.Count;
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i < 5; i++)
        {
            clearFlags.Add(false);
        }
    }
}
