using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AsteroidsManager : MonoBehaviour
{
    TextMeshProUGUI Score;
    TextMeshProUGUI Gummy;

    // Start is called before the first frame update
    void Start()
    {
        Score = GameObject.Find("MissionText_Weapons").transform.GetComponent<TextMeshProUGUI>();
        Gummy = GameObject.Find("MissionText_KillBug").transform.GetComponent<TextMeshProUGUI>();
        Gummy.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        if (MultipleImageTracker.clearAsteroid == 5)
        {
            Score.color = Color.green;
        }

        Score.text = "公扁绊 - 家青己 颇鲍(" + MultipleImageTracker.clearAsteroid + "/5)";
    }

}
