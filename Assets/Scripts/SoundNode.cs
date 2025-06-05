using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundNode : MonoBehaviour
{
    GameObject thePlayer;
    void Start
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        ()
    {
        thePlayer = PlayerScript.instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, thePlayer.transform.position);
        if(dist < 4 && !GetComponent<AudioSource>().isPlaying)
        {

            transform.parent.GetComponent<SoundTriggerParent>().PlayNewSound(this.gameObject);
        }
    }
}
