using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowIt : MonoBehaviour
{
    public GameObject basketBallPrefab;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            
        {
          GameObject basketball =  Instantiate(basketBallPrefab, transform.position + Vector3.up, Quaternion.identity);
            basketball.GetComponent<Rigidbody>().velocity = transform.forward * 40;
        }
    }
}
