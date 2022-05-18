using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using System;

public class myfacemanagerar : MonoBehaviour
{
    public GameObject[] cubes;
    public ARFaceManager aRFaceManager;


    private void Awake()
    {
        aRFaceManager = GetComponent<ARFaceManager>();
    }

    private void OnEnable()
    {
        aRFaceManager.facesChanged += OnFaceChanged;
    }

    private void OnDisable()
    {
        aRFaceManager.facesChanged -= OnFaceChanged;

    }

    private void OnFaceChanged(ARFacesChangedEventArgs args)
    {
        if ( args.updated.Count > 0 )
        {
            ARCoreFaceSubsystem SUB = aRFaceManager.subsystem as ARCoreFaceSubsystem;
            NativeArray<ARCoreFaceRegionData> data = new NativeArray<ARCoreFaceRegionData>();
            //부모의 속성을 자신의 속성으로 상속받아서 사용
            SUB.GetRegionPoses(args.updated[0].trackableId, Allocator.Persistent, ref data);

            for ( int i = 0; i < data.Length; i++)
            {
                cubes[i].transform.position = data[i].pose.position;
                cubes[i].transform.rotation = data[i].pose.rotation;
            }
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
