using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    GameObject thePlayer;

    
    void Start()
    {
        thePlayer = PlayerScript.instance.gameObject;
        
    }

    
    void Update()
    {
        transform.LookAt(thePlayer.transform );
            
    }
}
