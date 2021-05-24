using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTrackedImg : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;

    //게임 오브젝트를 받아와서 이름과 오브젝트를 저장 하기 위한 변수
    public List<GameObject> _objectList = new List<GameObject>();
    public Dictionary<string, GameObject> _prefabDic = new Dictionary<string, GameObject>();

    private List<ARTrackedImage> _trackedImg = new List<ARTrackedImage>();
    //private List<float> _trackedTimer = new List<float>();

    #region TrackingSH_MH
    public GameObject fader;
    bool isFaderOut;

    public Image image_SH;
    public Image image_SH_Front;
    [SerializeField]
    bool isStageRecognize = false;
    float timer_Recognize = 0f;
    public float time_fill;
    #endregion

    private void Awake()
    {
        foreach (GameObject obj in _objectList)
        {
            string tName = obj.name;
            _prefabDic.Add(tName, obj);
        }
        image_SH_Front.fillAmount = 0;
        image_SH.gameObject.SetActive(false);
    }

    private void Update()
    {
        //승형이 인식 마커 관련 함수//
        if (!fader.activeInHierarchy && !isFaderOut)
        {
            image_SH.gameObject.SetActive(true);
            isFaderOut = true;
        }

        if (isStageRecognize)
        {
            RecognizeSH_MH();
        }
        else
        {
            image_SH_Front.fillAmount = 0f;
            timer_Recognize = 0;
        }
        // //

        if (_trackedImg.Count > 0)
        {
            List<ARTrackedImage> tNumList = new List<ARTrackedImage>();

            //trackedImage를 검색해서
            for (int i = 0; i < _trackedImg.Count; i++)
            {
                //TrackingState.Limited > Some tracking information is available, but it is limited or of poor quality.
                //trackedImage의 상태가 Limited가 된다면
                if (_trackedImg[i].trackingState == TrackingState.Limited)
                {
                    //limied된 게임 오브젝트를 setactive(false)로 해준다.
                    string name = _trackedImg[i].referenceImage.name;
                    _prefabDic[name].SetActive(false);

                    
                    _trackedImg.Remove(_trackedImg[i]);

                    //setactive(false)된 게임오브젝트를 _trackedImg, _trackedTimer리스트에서 제거하기 위한 리스트에 저장
                    //tNumList.Add(_trackedImg[i]);
                }
            }

            ////_trackedImg, _trackedTimer리스트에서 오브젝트 제거하기
            //if (tNumList.Count > 0)
            //{
            //    for (var i = 0; i < tNumList.Count; i++)
            //    {
            //        //리스트에서 선택된 리스트 값을 삭제
            //       // int num = _trackedImg.IndexOf(tNumList[i]);
            //       // _trackedImg.Remove(_trackedImg[num]);
            //        // _trackedTimer.Remove(_trackedTimer[num]);
            //    }
            //}
        }

        
    }
    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }
    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    //이미지가 보이는지 안보이는지를 확인하는 함수
    public void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        //카메라에 이미지가 추가 되었을 때
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            if (!_trackedImg.Contains(trackedImage))
            {
                _trackedImg.Add(trackedImage);
                //_trackedTimer.Add(0);
            }
        }

        //이미지가 업데이트 되었을 때
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (!_trackedImg.Contains(trackedImage))
            {
                _trackedImg.Add(trackedImage);
                // _trackedTimer.Add(0);
            }

            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                UpdateImage(trackedImage);
            }
            else
            {
                isStageRecognize = false;
            }


        }

    }

    //게임 오브젝트가 Circuit라면 한번만 출력을 하고 싶다.
    //출력된 회로가 마커가 있는 위치에 나오도록 하고 싶다.
    bool creatOnce;
    GameObject stageCrct;

    void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        if (trackedImage.referenceImage.name.Contains("Stage"))
        {
            // SH 마크가 차오르는 상태On
            isStageRecognize = true;
            //게임 오브젝트가 Circuit라면 한번만 출력을 하고 싶다.
            //출력된 회로가 마커가 있는 위치에 나오도록 하고 싶다.
            if (!creatOnce && isFaderOut)
            {
                if (image_SH_Front.fillAmount == 1)
                {
                    image_SH.gameObject.SetActive(false);
                    stageCrct = Instantiate(_prefabDic[name]);
                    creatOnce = true;
                }
            }

            stageCrct.transform.position = trackedImage.transform.position;
            stageCrct.transform.rotation = trackedImage.transform.rotation;
            stageCrct.SetActive(true);
        }
        //circuit가 아닌 소자는 마커가 인식 되면
        //그자리에 놓고 싶다. 
        else if (!trackedImage.referenceImage.name.Contains("Stage"))
        {
            //이미지 위치에 위치시키고 싶다.
            _prefabDic[name].transform.position = trackedImage.transform.position;
            _prefabDic[name].transform.rotation = trackedImage.transform.rotation;
            _prefabDic[name].SetActive(true);
        }

    }

    void RecognizeSH_MH()
    {
        timer_Recognize += Time.deltaTime;
        image_SH_Front.fillAmount = timer_Recognize / time_fill;
    }

}