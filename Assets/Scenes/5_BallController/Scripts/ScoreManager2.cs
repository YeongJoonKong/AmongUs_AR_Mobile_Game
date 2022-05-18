using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager2 : MonoBehaviour
{
    public static ScoreManager2 instance;
    int kill;

    private void Awake()
    {
        ScoreManager2.instance = this;

        kill = 0;
    }

    public Text TextKillingCount;
    public GameObject TextVictory;

    public int Kill
    {
        get
        {
            return kill;
        }

        set
        {
            kill = value;
            TextKillingCount.text = " " + kill;
        }
    }








    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(kill >= 50)
        {
            TextVictory.SetActive(true);


            Invoke("SceneChange", 2);

        }



    }

    void SceneChange()
    {
        SceneManager.LoadScene("AmongUs");

    }


}
