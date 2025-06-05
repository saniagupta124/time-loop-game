using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation3D : MonoBehaviour
{
    private Animator anim;
    GameObject thePlayer;
    [SerializeField] float theDist;

    [SerializeField] string boolForPlay;
    bool playIsHappening = false;

    AudioSource mySound;

    void Start()
    {
        anim = GetComponent<Animator>();
        thePlayer = PlayerScript.instance.gameObject;
        mySound = GetComponent<AudioSource>();
    }

    void CheckForPlayer()
    {
        if (thePlayer)
        {
            float dist = Vector3.Distance(transform.position, thePlayer.transform.position);
            if (dist < theDist)
            {
                if (playIsHappening == false) // a flag so we don't perpetually 'SetBool' when within distance
                {
                    anim.SetBool(boolForPlay, true);
                    playIsHappening = true;
                }
            }
            else
            {
                if (playIsHappening ) // a flag so we don't perpetually 'SetBool' when OUT of distance
                {
                    anim.SetBool(boolForPlay, false);
                    playIsHappening = false;
                }

            }

        }   
    }
    void CheckForSound()
    {
        if (playIsHappening )
        {
            if (!mySound.isPlaying)
                mySound.Play();
           
        }
        else
        {
            if (mySound.isPlaying)
                mySound.Stop();
        }
    }
    void Update()
    {
        CheckForPlayer();
        CheckForSound();

    }
}
