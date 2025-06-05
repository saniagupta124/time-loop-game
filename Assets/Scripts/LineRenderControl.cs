using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderControl : MonoBehaviour
{
    // just the between the two teleporter points

   [SerializeField] GameObject door;// store begin and end point of teleporter
   [SerializeField] GameObject mySwitch;// store begin and end point of teleporter

    [SerializeField] Material theMateral;

    LineRenderer line;

    int offsetDir = 1;
    void Start()
    {
        line = GetComponent<LineRenderer>();

       

     
    }
    void Update()
    {
        if (door != null && mySwitch != null)
        {


            // Update position of the two vertex of the Line Renderer
            line.SetPosition(0, door.transform.position );
            line.SetPosition(1, mySwitch.transform.position );
        }
    }
}
