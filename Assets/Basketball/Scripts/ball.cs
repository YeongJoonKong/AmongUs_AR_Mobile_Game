using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public Player player;
    Collider other;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("ScoreTrigger"))
        {
            ScoreManager.instance.Score++;

            other.enabled = false;
            this.other = other;

            Invoke("BallReset", 0.7f);
        }
    }

    public void BallReset()
    {
        other.enabled = true;
        player.OnclickReset();
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
