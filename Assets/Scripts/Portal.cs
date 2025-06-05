using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    GameObject[] twoPortals = new GameObject[2]; // store begin and end point of teleporter


    GameObject thePlayer;

    GameObject teleportLastObj = null;

    void Start()
    {
        thePlayer = PlayerScript.instance.gameObject;
        int i = 0;
        foreach (Transform child in transform)
        {
    
            if (child.name.Contains("Port"))
            {
                twoPortals[i] = child.gameObject; // store start and end point

                i++;
            }
        }
    }

    void Update()
    {
 
         CheckForTouch();
        CheckForTurnBackOn();
    }
    void CheckForTouch()
    {
        if (teleportLastObj == null && thePlayer) // the first index of twoPortals is the start point, which is what is sensative to player touch.  
        {
           
            for (int i = 0; i < twoPortals.Length; i++)
            {

                float dist = Vector3.Distance(twoPortals[i].transform.position, thePlayer.transform.position);
               
                if (dist < 2.5)
                {
                    DoPort(i);
                    break;
                }
            }
        }
    }
    void CheckForTurnBackOn()
    {
        if (teleportLastObj != null) // has a gameObject referenced
        {
            float dist = Vector3.Distance(teleportLastObj.transform.position, thePlayer.transform.position);
         
            if (dist > 4)
            {
                teleportLastObj = null; //clear
            }
        }
    }
    void DoPort(int _startIndex)
    {
          GetComponent<AudioSource>().Play();

        int targetIndex = 0;
        if (_startIndex == 0)
            targetIndex = 1;

        thePlayer.transform.position = twoPortals[targetIndex].transform.position;
        teleportLastObj = twoPortals[targetIndex]; // reference to end of portal where we ended up.  So we can check when far enough away to reenable

        StartCoroutine(ToggleCharacterController(0, false));
        StartCoroutine(ToggleCharacterController(0.5f, true));


        thePlayer.GetComponent<FOVSlide>().BoostFOVstep();
    }
    public IEnumerator ToggleCharacterController(float delay, bool isEnable)
    {
        yield return new WaitForSeconds(delay);
        thePlayer.GetComponent<CharacterController>().enabled = isEnable;
    }
}

