using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryScene : MonoBehaviour
{
    public GameObject text;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    public GameObject text5;
    public GameObject text6;

    float currentTime = 0;
    float creatTime = 4f;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("textScene0", 4f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void textScene0()
    {
        text.SetActive(false);
        text1.SetActive(true);
        Invoke("textScene", 4f);
    }


    void textScene()
    {
        text1.SetActive(false);
        text2.SetActive(true);
        Invoke("textScene2", 4f);

    }

    void textScene2()
    {
        text2.SetActive(false);
        text3.SetActive(true);
        Invoke("textScene3", 4f);

    }

    void textScene3()
    {
        text3.SetActive(false);
        text4.SetActive(true);
        Invoke("textScene4", 4f);

    }

    void textScene4()
    {
        text4.SetActive(false);
        text5.SetActive(true);
        Invoke("textScene5", 4f);
    }

    void textScene5()
    {
        text5.SetActive(false);
        text6.SetActive(true);
        Invoke("textScene6", 4f);
    }

    void textScene6()
    {
        SceneManager.LoadScene("NewMissionScene");
    }
}
