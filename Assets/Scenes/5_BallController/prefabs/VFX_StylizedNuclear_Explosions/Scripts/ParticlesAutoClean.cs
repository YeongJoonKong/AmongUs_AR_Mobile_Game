using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesAutoClean : MonoBehaviour
{
    // THIS SCRIPT DESTROY PARTICLES AFTER LIFETIME =)
    private ParticleSystem SelfParticle;
    public bool isPlayNow = true;

    // Start is called before the first frame update
    void Start()
    {
        isPlayNow = true;
           SelfParticle = gameObject.GetComponent<ParticleSystem>();


        
    }

    // Update is called once per frame
    void Update()
    {
        isPlayNow = SelfParticle.IsAlive(true);

        if(isPlayNow == false)
        {
           
            Destroy(gameObject);

        }
       
        
    }
}
