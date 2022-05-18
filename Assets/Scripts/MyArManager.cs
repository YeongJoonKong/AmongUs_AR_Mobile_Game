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
        //카메라(화면중앙)가 향한 곳에 바닥이 있다면
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
        

        //그곳에 인디케이터가 보여질때.
        if (indicator.gameObject.activeSelf == true)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                //인디케이터가 보여질 때, 인디케이터를 클릭(터치)하면
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo2;
                //그 위치에 물체를 배치하고 싶다.
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
        //카메라(화면중앙)가 향한 곳에 바닥이 있다면
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



        //그곳에 인디케이터가 보여질때.
        if (true == indicator.gameObject.activeSelf)
        {
            //인디케이터를 터치하면
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                {
                    //인디케이터가 보여질 때, 인디케이터를 클릭(터치)하면
                    Ray ray2 = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hitInfo2;
                    //그 위치에 물체를 배치하고 싶다.
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
