using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnOffGameObject : MonoBehaviour
{
    public GameObject affectThisGameObject;
    [SerializeField] bool switchOn = true;

    bool switchedFlag = false;

    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
   
    void Update()
    {
        CheckDist();
    }
    void CheckDist()
    {
        float dist = Vector3.Distance(PlayerScript.instance.gameObject.transform.position, this.gameObject.transform.position);
      //  print(dist);
        if(dist < 5 && switchedFlag == false)
        {
            DoSwitch();
            switchedFlag = true;
        }
    }
    void DoSwitch()
    {
     
        affectThisGameObject.SetActive(switchOn);
    }
}
