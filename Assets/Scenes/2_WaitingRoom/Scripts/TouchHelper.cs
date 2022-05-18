using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHelper : MonoBehaviour
{
#if UNITY_EDITOR
    public static bool Touch2 => Input.GetMouseButtonDown(1);
    public static bool IsDown => Input.GetMouseButtonDown(0);
    public static bool IsUp => Input.GetMouseButtonUp(0);
    public static Vector2 TouchPosition => Input.mousePosition;

#else
    //터치가 두번 되었을 때 bool값이 트루가 되어 이벤트를 가져온다.
    public static bool Touch2 => Input.touchCount == 2 && (Input.GetTouch(index: 1).phase == TouchPhase.Began);
    //터치하였을 때의 값을 가져오는 함수
    public static bool IsDown => Input.GetTouch(index: 0).phase == TouchPhase.Began;
    //터치한 후의 값을 가져오는 함수
    public static bool IsUp => Input.GetTouch(index: 0).phase == TouchPhase.Ended;
    //터치 포지션 값을 가져오는 함수
    public static Vector2 TouchPosition => Input.GetTouch(index: 0).position;
#endif

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }




}
