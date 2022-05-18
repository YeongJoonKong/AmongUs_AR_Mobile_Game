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
                //�ε������Ͱ� ������ ��, �ε������͸� Ŭ��(��ġ)�ϸ�
                Ray ray2 = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hitInfo2;

                //�� ��ġ�� ��ü�� ��ġ�ϰ� �ʹ�.
                if (Physics.Raycast(ray2, out hitInfo2))
                {
                    GameObject obj = Instantiate(factory);
                    obj.transform.position = hitInfo2.point;
                }
            }
        }

    }
}
