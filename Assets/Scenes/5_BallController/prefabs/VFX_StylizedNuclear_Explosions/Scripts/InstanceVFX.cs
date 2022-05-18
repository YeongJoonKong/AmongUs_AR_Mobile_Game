using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceVFX : MonoBehaviour
{
    public GameObject[] Prefabs;
    public Transform[] InstanceTransform;
    public int Index = 0;
    public Text targetText;

    // Start is called before the first frame update
    void Start()
    {

        targetText.text = "";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject PrefabCur;

            PrefabCur = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], InstanceTransform[Random.Range(0, InstanceTransform.Length)].position, Quaternion.identity) as GameObject;

            targetText.text = PrefabCur.name;



        }

        
    }
}
