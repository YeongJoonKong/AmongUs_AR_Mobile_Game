using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    int triggerPoint = 0;
    

    public static Card instance = null;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            transform.Translate(Input.GetTouch(0).deltaPosition * Time.deltaTime * 0.02f);
        }
        catch
        {

        }

        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("TriggerRead"))
        {
            if (triggerPoint < 2)
            {
                ++triggerPoint;
            } 
            else
            {
                TextMeshProUGUI missionText = GameObject.Find("MissionText_Admin").transform.GetComponent<TextMeshProUGUI>();
                missionText.color = Color.green;

                SoundManager.instance.CardPlay();
                Destroy(gameObject);
                Destroy(GameObject.Find("Admin(Clone)").gameObject);
            }
        }
    }

}
