using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyArPlaneManager : MonoBehaviour
{
    public GameObject factory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                //인디케이터가 보여질 때, 인디케이터를 클릭(터치)하면
                Ray ray2 = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hitInfo2;

                //그 위치에 물체를 배치하고 싶다.
                if (Physics.Raycast(ray2, out hitInfo2))
                {
                    GameObject obj = Instantiate(factory);
                    obj.transform.position = hitInfo2.point;
                }
            }
        }

    }
}
