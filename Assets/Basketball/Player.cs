using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum State
    {
        Hoop,
        Play,
    }
    State state;

    public GameObject hoop;


    public Transform ball;
    Rigidbody ballRigidbody;
    bool isPressed;
    Vector3 firstPosition;



    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = Vector3.up * -9.81f * 2;


        ballRigidbody = ball.GetComponent<Rigidbody>();
        isPressed = false;
    }

    private void Update()
    {
        switch(state)
        {
            case State.Hoop: UpdateHoop(); break;
            case State.Play: UpdatePlay(); break;
        }    
    }

    private void UpdateHoop()
    {
        if(Input.GetButtonDown("Fire1"))
        {



            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo))
            {
                state = State.Play;
                target.gameObject.SetActive(true);

                //첫 번 째 방법
                hoop.transform.position = hitinfo.point;
                hoop.transform.LookAt(Camera.main.transform);
                Vector3 angle = hoop.transform.eulerAngles;
                angle.x = 0;
                angle.z = 0;

                hoop.transform.eulerAngles = angle;
                hoop.SetActive(true);
            }

        }
    }


    public void OnclickHoop()
    {
        state = State.Hoop;
    }


    // Update is called once per frame
    void UpdatePlay()
    {
        //화면을 눌렀다가 드래그 후 떼면, Y방향의 방향으로 던지고
        //그 힘을 볼에게 넘겨 주고 싶다.

        if(isPressed)
        {

        }

        if(Input.GetButtonDown("Fire1"))
        {
            isPressed = true;
            firstPosition = Input.mousePosition;
            

        }
        else if (Input.GetButtonUp("Fire1"))
        {
            isPressed = false;
            float y = Input.mousePosition.y - firstPosition.y;
            if(y > 0)
            {
                ballRigidbody.isKinematic = false;
                ball.parent = null;

                Vector3 dir = Camera.main.transform.forward + Vector3.up;
                dir.Normalize();
                ballRigidbody.AddForce(dir * 40 * (y / Screen.height), ForceMode.Impulse);
                //(y / Screen.height) 화면에서 시작점에서 얼마나 드래그 하냐로 힘을 조절 할 수 있는데
                //폰마다 화면의 크기가 다르므로, 동일한 화면에서 드래그 힘을 주기 위해(y / Screen.height) 로 나눠서 곱해준다

                ballRigidbody.AddTorque(Vector3.right * 40);

            }
        }

        


    }

    public Transform target;

    public void  OnclickReset()
    {
        ballRigidbody.isKinematic = true;
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;

        ball.transform.position = target.position;
        ball.transform.rotation = Quaternion.identity;
        ball.parent = target;
        
    }
}
