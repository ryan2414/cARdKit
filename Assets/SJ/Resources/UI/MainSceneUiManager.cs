using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneUiManager : MonoBehaviour
{
    public float uiShowingDuration = 3;
    public float animDuration = 0.7f;
    public Image[] uiObjects;
    private Coroutine hideTimer;
    private bool isShowing = false;

    private void Start() {
        SetImagesAlpha(uiObjects, 0);
    }

    public void OnClickScreen() {
        if (!isShowing) {
            isShowing = true;
            StartCoroutine(IeUiFadeAnim(0, 1));
        }

        if (hideTimer != null) {
            StopCoroutine(hideTimer);
        }
        hideTimer = StartCoroutine(IeStartHideTimer(uiShowingDuration));
    }

    public void GoToStartScene() {
        SceneManager.LoadScene(0);
    }

    public void GoToArScene() {
        SceneManager.LoadScene(1);
    }

    public void QuitApplication() {
        Application.Quit();
    }

    private IEnumerator IeStartHideTimer(float delay) {
        yield return new WaitForSeconds(delay);
        yield return IeUiFadeAnim(1, 0);
        isShowing = false;
    }

    private IEnumerator IeUiFadeAnim(float from, float to) {
        float time = 0;
        float alphaValue;

        while (time < animDuration) {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();

            alphaValue = Mathf.Lerp(from, to, time / animDuration);

            SetImagesAlpha(uiObjects, alphaValue);
        }

        SetImagesAlpha(uiObjects, to);
    }

    private void SetImagesAlpha(Image[] images, float alpha) {
        foreach(var image in images) {
            SetImageAlpha(image, alpha);
        }
    }

    private void SetImageAlpha(Image image, float alpha) {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
