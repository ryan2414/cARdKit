using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class CameraPermission_MH : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0;
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
    }

    private void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Application.Quit();
        }
        else
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("0SJ_Intro");
        }
    }
}
