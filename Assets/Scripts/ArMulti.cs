using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//Ar Tracked Image Manager�� ��Ŀ������ ������ʹ�.
//���� ������ ��Ŀ�� �ִٸ�, �� ��Ŀ�� �ش��ϴ� ���ӿ�����Ʈ�� ��Ŀ ���뿡 ��ġ�ϰ�,���̰� �ϰ�ʹ�.
public class ArMulti : MonoBehaviour
{
    //�׸� ���� ������� üũ�Ͽ� 1�̵� ��������� ��Ÿ����

    ARTrackedImageManager aRTrackedImageManager;

    [System.Serializable]
    //UniryEditor�� ����ǰ� �ϴ� �Լ�

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
        //Start���� ���� ������ �ȴ�.
        //���� ��, �����ϴ� �Լ�
        aRTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
        //+= ���� ������ �ִ� ��ɿ��� ���� ������ ����� �߰��� �� �ִ�.
    }

    private void OnDisable()
    {
        //�Ⱥ��� ��. �����ϴ� �Լ�
        aRTrackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
        //+= ���� ������ �ִ� ��ɿ��� ���� ������ ����� �߰��� �� �ִ�.
        //trackedImagesChanged�� ���� �Ǹ鼭 ���������� ������ �ϸ� ������ �ϰ� �ȴ�.
    }

    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        //���
        List<ARTrackedImage> list = obj.updated;
        //���ŵ� ��ϰ� ���� �˰��ִ� ����� ���ؼ� ���ٸ�,
        //�� �̹����� �ش��ϴ� ���� ������Ʈ�� ��ġ�ϰ� �ʹ�.

        for (int i = 0; i < list.Count; i++)
        {
            for (int k = 0; k < info.Length; k++)
            {
                if (list[i].referenceImage.name == info[k].name)
                {
                    //���� ���Ÿ���� ��Ŀ�� �������̶�� ��ġ�ϰ�ʹ�
                    if (list[i].trackingState == TrackingState.Tracking)
                    //using UnityEngine.XR.ARSubsystems; �߰� �Ǿ�� �Ѵ�.
                    {
                        info[k].obj2.SetActive(true);
                        info[k].obj2.transform.position = list[i].transform.position;
                        //info[k].obj2.transform.forward = list[i].transform.forward;
                    }
                    else
                    {
                    //�׷��� �ʴٸ�, �������ʰ� �ϰ� �ʹ�.
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
