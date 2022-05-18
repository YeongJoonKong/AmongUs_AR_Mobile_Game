using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Asteroid : MonoBehaviour
{
    int randomIndex;
    // Start is called before the first frame update
    void Start()
    {
        System.Random random = new System.Random();
        char[] str = { 'x', 'y', 'z'};
        randomIndex = random.Next(str.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount != 0)
        {
            SoundManager.instance.WeaponShootPlay();
            Destroy(gameObject);
        }

        if (randomIndex == 0)
            gameObject.transform.position += new Vector3(0.01f, 0.01f, 0f);
        else if (randomIndex == 1)
            gameObject.transform.position += new Vector3(0f, 0.01f, 0.01f);
        else if (randomIndex == 2)
            gameObject.transform.position += new Vector3(0.01f, 0f, 0.01f);
    }

}
