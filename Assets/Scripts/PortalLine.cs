using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLine : MonoBehaviour
{
    // just the between the two teleporter points

    GameObject[] twoPortals = new GameObject[2]; // store begin and end point of teleporter
    [SerializeField] Material theMateral;

    LineRenderer line;
    Vector3 offset = new Vector3(0, 5, 0);
     int offsetDir = 1;
    void Start()
    {
        line = GetComponent<LineRenderer>();

        int i = 0;
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Port"))
            {
                twoPortals[i] = child.gameObject; // store start and end point

                i++;
            }
        }

        if (twoPortals[0].transform.position.y < twoPortals[1].transform.position.y)
            offsetDir = -1;
    }
    void Update()
    {
        if (twoPortals[0] != null && twoPortals[1] != null)
        {
            

            // Update position of the two vertex of the Line Renderer
            line.SetPosition(0, twoPortals[0].transform.position + offset * -offsetDir * 0.2f);
            line.SetPosition(1, twoPortals[1].transform.position + offset * offsetDir);
        }
    }
}
