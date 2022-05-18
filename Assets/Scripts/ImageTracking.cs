using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTracking : MonoBehaviour
{
    public Text passWord;
    public GameObject gameObject3;
    public void CheckingPassWord()
    {
        if (passWord.text == "123")
        {
            GameObject gameObject2 = Instantiate(gameObject3, transform.position, transform.rotation);
        }


    }




}
