using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BoxController2 : MonoBehaviour
{
    private const float CameraDistance2 = 7.5f;
    public float positionY2 = 0.4f;
    public GameObject[] prefab2;

    protected Camera mainCamera2;
    protected GameObject HoldingObject2;
    protected Vector3 InputPosition2;


    private void Reset2()
    {
        var pos2 = mainCamera2.ViewportToWorldPoint(new Vector3(x: 0.5f, y: positionY2, z: mainCamera2.nearClipPlane * CameraDistance2));

        //Camera.main.ScreenToViewportPoint(Input.mousePosition));  
        // Screen 좌표계로 표시된 마우스 커서 위치 좌표를 Viewport 좌표계, 즉 0 ~ 1 사이의 비율 좌표계로 변환한다.

        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        ////게임 화면에 마우스를 클릭하면 그 위치를 월드 좌표계로 변환하기
        //롤이나 스타크래프트처럼 게임 화면 상에서 특정 오브젝트 혹은 위치를 마우스로 클릭하면 반응하게 하고 싶을 때
        //마우스 커서의 Screen 좌표 👉 World 좌표로 변환
        //카메라 현재 위치로부터, 위에서 변환했던 클릭한 마우스 위치의 World 좌표로 향하는 방향을 구한다.
        //카메라로부터 해당 방향으로 Raycast를 쏘면 마우스로 클릭했던 그 오브젝트가 충돌 될 것이다!
        //ScreenToWorldPoint 함수 👉 화면 좌표계를 월드 좌표계로 변환하고 리턴함
        //ScreenToWorldPoint 에다가 마우스를 클릭한 화면상의 좌표(X, Y)와 (0, 0) ~(Screen.width, Screen.height) 범위를 가진다.

        //mainCamera.nearClipPlane, near clipping plane 거리를 나타냅니다.
        //인수로 넘겨준 z 값, 즉 고려할 카메라와의 거리를 렌더링 시작 위치까지의 거리인 Camera.main.nearClipPlane로 넘겨준 이유는!
        //카메라 현재 위치로부터, 위에서 변환했던 클릭한 마우스 위치의 World 좌표로 향하는 방향을 구해서
        //그 방향으로 Raycast를 쏠 것이기 때문에 어차피 방향이 중요하지 z 값은 크게 중요하지 않아서
        //렌더링 시작 거리인 Camera.main.nearClipPlane로 선택하신 듯 하다
        //마치 Z 축은 무시하고 X, y 좌표만 고려한 한 장의 사진으로 찍듯이 카메라는
        //3 D 의 실제 게임 월드를 2 D 인 게임 화면으로 투영 하게 된다.
        //마치 단면을 잘라 버린 듯이. 그래도 실제 게임 월드 위치 상에서의 비율이 지켜지는 것이 핵심 포인트다

        var index = Random.Range(0, prefab2.Length);

        var obj2 = Instantiate(prefab2[index], pos2, Quaternion.identity, mainCamera2.transform);
        //생성할 오브젝트는 prefab의 0번째 오브젝트를 생성하고, 생성되는 위치는 방금 작성한 pos의 값을 가지게된다.
        //생성시의 회전 값은 Quaternion.identity 가지게 되고
        //parent는 메인카메라의 포지션값을 주어서 카메라의 child로 계속해서 생성되게 된다.

        //생성된 오브젝트는 기본적으로 Rigidbody가 생성되게 된다.
        var rigidbody = obj2.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        //생성된 후 중력값은 사용하지 않게 설정
        rigidbody.velocity = Vector3.zero;
        //물리적인 움직임 값은 0으로 설정
        rigidbody.angularVelocity = Vector3.zero;
        //회전의 값도 0으로 설정
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera2 = Camera.main;
        Reset2();

    }

    // Update is called once per frame
    void Update()
    {
#if !UNITY_EDITOR
        if(Input.touchCount == 0)
        {
            return;
        }
#endif
        InputPosition2 = TouchHelper.TouchPosition;
        //InputPosition은 다른 스크립트에서 구현해두었던 함수를 가져와서 쓴다.

        if (TouchHelper.Touch2)
        {
            Reset2();
            return;
        }

        if (HoldingObject2)
        {
            if (TouchHelper.IsUp)
            {
                OnPut(InputPosition2);
                HoldingObject2 = null;
                return;
            }
            Move(InputPosition2);
            return;
        }

        if (!TouchHelper.IsDown)
        {
            return;
        }

        if (Physics.Raycast(mainCamera2.ScreenPointToRay(InputPosition2), out var hits, mainCamera2.farClipPlane))
        //Ray의 결과 값은 out var hits에 받아오게 된다.
        {
            if (hits.transform.gameObject.tag.Equals("Player"))
            {
                HoldingObject2 = hits.transform.gameObject;
                OnHold();
            }
        }
    }

    protected virtual void OnPut(Vector3 pos2)
    {
        HoldingObject2.GetComponent<Rigidbody>().useGravity = true;
        HoldingObject2.transform.SetParent(null);

    }

    private void Move(Vector3 pos2)
    {
        pos2.z = mainCamera2.nearClipPlane * CameraDistance2;
        HoldingObject2.transform.position = Vector3.Lerp(HoldingObject2.transform.position, mainCamera2.ScreenToWorldPoint(pos2), Time.deltaTime * 7f);
    }

    protected virtual void OnHold()
    {
        HoldingObject2.GetComponent<Rigidbody>().useGravity = false;

        HoldingObject2.transform.SetParent(mainCamera2.transform);
        //HoldingObject의 transform의 parent는 카메라쪽으로 이동시켜준다.
        HoldingObject2.transform.rotation = Quaternion.identity;
        HoldingObject2.transform.position = mainCamera2.ViewportToWorldPoint(new Vector3(0.5f, positionY2, mainCamera2.nearClipPlane * CameraDistance2));

    }
}
