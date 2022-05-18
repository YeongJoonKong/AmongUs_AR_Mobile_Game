using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioPlayer;
    public AudioClip[] audioClip;

    public static SoundManager instance = null;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audioPlayer = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Play(string name)
    {
        for (int i = 0; i < audioClip.Length; i++)
        {
            if (audioClip[i].name.Contains(name))
            {
                audioPlayer.clip = audioClip[i];
                audioPlayer.Play();
                break;
            }
        }
    }

    public void CardPlay()
    {
        Play("Complete");
    }

    public void WeaponShootPlay()
    {
        Play("weaponfire");
    }

    public void EmergencyPlay()
    {
        Play("emergencymeeting");
    }

    public void ImposterKillPlay()
    {
        Play("kill");
    }
    public void VictoryCrewPlay()
    {
        Play("crew");
    }
}
