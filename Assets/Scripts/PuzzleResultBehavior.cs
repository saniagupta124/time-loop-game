using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleResultBehavior : MonoBehaviour
{
    public Vector3 myVelocity;
    void Start()
    {
       
    }

  
    void Update()
    {
        transform.Translate(myVelocity * Time.deltaTime);
    }
}
