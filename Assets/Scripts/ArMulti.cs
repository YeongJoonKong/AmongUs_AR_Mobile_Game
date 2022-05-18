using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//Ar Tracked Image Manager에 마커정보를 얻어오고싶다.
//만약 추적된 마커가 있다면, 그 마커에 해당하는 게임오브젝트를 마커 위취에 배치하고,보이게 하고싶다.
public class ArMulti : MonoBehaviour
{
    //그림 파일 사이즈는 체크하여 1이든 설정해줘야 나타난다

    ARTrackedImageManager aRTrackedImageManager;

    [System.Serializable]
    //UniryEditor에 저장되게 하는 함수

    public class MyARTtrackedImageInfo
    {
        public string name;
        public GameObject obj2;
    }

    public MyARTtrackedImageInfo[] info;






    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        //Start보다 먼저 실행이 된다.
        //보일 때, 실행하는 함수
        aRTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
        //+= 으로 기존에 있던 기능에서 내가 구현한 기능을 추가할 수 있다.
    }

    private void OnDisable()
    {
        //안보일 떼. 실행하는 함수
        aRTrackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
        //+= 으로 기존에 있던 기능에서 내가 구현한 기능을 추가할 수 있다.
        //trackedImagesChanged는 실행 되면서 지속적으로 추적을 하며 실행을 하게 된다.
    }

    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        //목록
        List<ARTrackedImage> list = obj.updated;
        //갱신된 목록과 내가 알고있는 목록을 비교해서 같다며,
        //그 이미지에 해당하는 게임 오브젝트를 배치하고 싶다.

        for (int i = 0; i < list.Count; i++)
        {
            for (int k = 0; k < info.Length; k++)
            {
                if (list[i].referenceImage.name == info[k].name)
                {
                    //만약 갱신목록의 마커가 추적중이라면 배치하고싶다
                    if (list[i].trackingState == TrackingState.Tracking)
                    //using UnityEngine.XR.ARSubsystems; 추가 되어야 한다.
                    {
                        info[k].obj2.SetActive(true);
                        info[k].obj2.transform.position = list[i].transform.position;
                        //info[k].obj2.transform.forward = list[i].transform.forward;
                    }
                    else
                    {
                    //그렇지 않다면, 보이지않게 하고 싶다.
                        info[k].obj2.SetActive(false);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
