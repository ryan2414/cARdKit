using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager_WorldMap : MonoBehaviour
{
   public void OnClickNextStage()
    {
        SceneManager.LoadScene("3SJ_Story1-1");
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("1SJ_StartScene");
    }
}
