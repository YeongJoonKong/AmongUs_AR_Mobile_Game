using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEffect2 : MonoBehaviour
{

    public ParticleSystem particle;


    AudioSource audioSource;
    public AudioClip soundBomb;

    public float radius = 0.8f;


    private void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            return;
        }

        audioSource.clip = soundBomb;
        audioSource.Play();

        Instantiate(particle, transform.position, transform.rotation);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Transform child = gameObject.transform.GetChild(0);
        child.gameObject.GetComponent<MeshRenderer>().enabled = false;
        print(child.gameObject.GetComponent<MeshRenderer>());
        //Destroy(gameObject, 0.5f);

        Collider[] colls2;

        colls2 = Physics.OverlapSphere(transform.position, 10f);

        foreach (Collider col in colls2)
        {
            if (col.gameObject.tag == "Spider")
            {
                Destroy(col.gameObject);
                ScoreManager2.instance.Kill++;
            }
        }


    }
}
