using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[ExecuteInEditMode]
public class Popup_Common : MonoBehaviour
{

    public string title;
    public string info;
    public string noText;
    public string okText;

    [SerializeField]
    private Button.ButtonClickedEvent m_OnClickConfirm = new Button.ButtonClickedEvent();

    [SerializeField]
    private Button.ButtonClickedEvent m_OnClickCancel= new Button.ButtonClickedEvent();

    private Animator animator;

    public TMP_Text textTitle;
    public TMP_Text textInfo;
    public TMP_Text textBtnNo;
    public TMP_Text textBtnYes;
    public Button btnConfirm;
    public Button btnCancel;
    private void Awake() {
        animator = GetComponent<Animator>();
        btnCancel.onClick.AddListener(() => OnClickCancel());
        btnConfirm.onClick.AddListener(() => OnClickConfirm());
    }

    private void OnEnable() {
        animator.Play("open");
    }

    private void LateUpdate() {
        UpdateUI();
    }

    private void UpdateUI() {
        textTitle.text = title;
        textInfo.text = info;
        textBtnNo.text = noText;
        textBtnYes.text = okText;
    }

    public void OnClickCancel() {
        if (m_OnClickCancel != null) m_OnClickCancel.Invoke();
        ClosePopup();
    }

    public void OnClickConfirm() {
        if (m_OnClickConfirm != null) m_OnClickConfirm.Invoke();
        ClosePopup();
    }

    private void ClosePopup() {
        StartCoroutine(IeClosePopup());
    }

    private IEnumerator IeClosePopup() {
        animator.Play("close");
        yield return new WaitForSeconds(0.58f);
        gameObject.SetActive(false);
    }
}
