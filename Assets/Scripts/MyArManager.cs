using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class MyArManager : MonoBehaviour
{
    public ARRaycastManager aRRaycastManager;
    public Transform indicator;
    public GameObject factory;
    Vector2 center;

    public GameObject floor;

    // Start is called before the first frame update
    void Start()
    {

#if UNITY_EDITOR
        floor.SetActive(true);

#else
        floor.SetActive(false);
#endif
        center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }


    void Update()
    {
        UpdateForPhone();
    }

    void UpdateForUnityEditor()
    {
        //ī�޶�(ȭ���߾�)�� ���� ���� �ٴ��� �ִٸ�
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.transform.name.Equals("Floor") == true)
            {
                indicator.gameObject.SetActive(true);
                indicator.parent.position = hitInfo.point + hitInfo.normal * 0.01f;
            }
            else if (false == hitInfo.transform.name.Equals("Indicator"))
                {
                    indicator.gameObject.SetActive(false);
                }
            }
        

        //�װ��� �ε������Ͱ� ��������.
        if (indicator.gameObject.activeSelf == true)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                //�ε������Ͱ� ������ ��, �ε������͸� Ŭ��(��ġ)�ϸ�
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo2;
                //�� ��ġ�� ��ü�� ��ġ�ϰ� �ʹ�.
                if (Physics.Raycast(ray2, out hitInfo2))
                {
                    if (hitInfo2.transform.name.Equals("Indicator"))
                    {
                        GameObject obj = Instantiate(factory);
                        obj.transform.position = hitInfo2.point;
                    }
                }
            }
        }
    }




    // Update is called once per frame
    void UpdateForPhone()
    {
        print(1);
        List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
        //ī�޶�(ȭ���߾�)�� ���� ���� �ٴ��� �ִٸ�
        if (aRRaycastManager.Raycast(center, hitResults))
        {
            ARRaycastHit hitInfo = hitResults[0];

            indicator.gameObject.SetActive(true);
            indicator.transform.position = hitInfo.pose.position;
        }
        else
        {
                indicator.gameObject.SetActive(false);
        }



        //�װ��� �ε������Ͱ� ��������.
        if (true == indicator.gameObject.activeSelf)
        {
            //�ε������͸� ��ġ�ϸ�
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                {
                    //�ε������Ͱ� ������ ��, �ε������͸� Ŭ��(��ġ)�ϸ�
                    Ray ray2 = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hitInfo2;
                    //�� ��ġ�� ��ü�� ��ġ�ϰ� �ʹ�.
                    if (Physics.Raycast(ray2, out hitInfo2))
                    {
                        if (hitInfo2.transform.name.Equals("Indicator"))
                        {
                            GameObject obj = Instantiate(factory);
                            obj.transform.position = hitInfo2.point;
                        }
                    }
                }
            }
        }
    }
}
