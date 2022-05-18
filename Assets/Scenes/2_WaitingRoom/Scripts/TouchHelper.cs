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
    //��ġ�� �ι� �Ǿ��� �� bool���� Ʈ�簡 �Ǿ� �̺�Ʈ�� �����´�.
    public static bool Touch2 => Input.touchCount == 2 && (Input.GetTouch(index: 1).phase == TouchPhase.Began);
    //��ġ�Ͽ��� ���� ���� �������� �Լ�
    public static bool IsDown => Input.GetTouch(index: 0).phase == TouchPhase.Began;
    //��ġ�� ���� ���� �������� �Լ�
    public static bool IsUp => Input.GetTouch(index: 0).phase == TouchPhase.Ended;
    //��ġ ������ ���� �������� �Լ�
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
