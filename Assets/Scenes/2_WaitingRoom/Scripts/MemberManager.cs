using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MemberManager : MonoBehaviour
{
    public static MemberManager instance;
    int head;

    private void Awake()
    {
        MemberManager.instance = this;

        head = 10;

    }

    public Text TextNumberCount;
    public GameObject TextGameStart;
    public GameObject TextDisappear;

    public int Head
    {
        get
        {
            return head;
        }

        set
        {
            head = value;
            TextNumberCount.text = " " + head;
        }


    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (head <= 0)
        {
            TextGameStart.SetActive(true);
            TextDisappear.SetActive(false);

            Invoke("NextScene", 2f);
        }



    }

    void NextScene()
    {
        SceneManager.LoadScene("StoryScene");
    }
}
