using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTrackedImg : MonoBehaviour
{
    public float _timer;

    public ARTrackedImageManager trackedImageManager;

    public List<GameObject> _objectList = new List<GameObject>();
    private Dictionary<string, GameObject> _prefabDic = new Dictionary<string, GameObject>();
    //시간이 되면 사라지게 하기 위한 변수 선언
    private List<ARTrackedImage> _trackedImg = new List<ARTrackedImage>();
    private List<float> _trackedTimer = new List<float>();

    private void Awake()
    {
        foreach(GameObject obj in _objectList)
        {
            string tName = obj.name;
            _prefabDic.Add(tName, obj);
        }
    }

    private void Update()
    {
        if(_trackedImg.Count > 0)
        {
            List<ARTrackedImage> tNumList = new List<ARTrackedImage>();

            for (int i = 0; i < _trackedImg.Count; i++)
            {
                if(_trackedImg[i].trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
                {
                    if(_trackedTimer[i] > _timer)
                    {
                        //Remove
                        string name = _trackedImg[i].referenceImage.name;
                        GameObject tObj = _prefabDic[name];
                        tObj.SetActive(false);
                        tNumList.Add(_trackedImg[i]);
                    }
                    else
                    {
                        _trackedTimer[i] += Time.deltaTime;
                    }
                }
            }

            if(tNumList.Count > 0)
            {
                for (var i = 0; i < tNumList.Count; i++)
                {
                    //리스트에서 선택된 리스트 값을 삭제
                    int num = _trackedImg.IndexOf(tNumList[i]);
                    _trackedImg.Remove(_trackedImg[num]);
                    _trackedTimer.Remove(_trackedTimer[num]);
                }
            }
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
    void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        //카메라에 이미지가 추가 되었을 때
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            if (!_trackedImg.Contains(trackedImage))
            {
                _trackedImg.Add(trackedImage);
                _trackedTimer.Add(0);
            }
        }
        //이미지가 업데이트 되었을 때
        foreach(ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (!_trackedImg.Contains(trackedImage))
            {
                _trackedImg.Add(trackedImage);
                _trackedTimer.Add(0);
            }
            else
            {
                int num = _trackedImg.IndexOf(trackedImage);
                _trackedTimer[num] = 0;
            }
            UpdateImage(trackedImage);
        }
    }

    void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        GameObject tObj = _prefabDic[name];
        tObj.transform.position = trackedImage.transform.position;
        tObj.transform.rotation = trackedImage.transform.rotation;
        tObj.SetActive(true);
    }
}
