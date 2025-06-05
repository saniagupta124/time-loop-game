using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyPicture : MonoBehaviour
{
     Sprite firstsprite;
    [SerializeField] Sprite secondsprite;
    bool checkForExit = false;

    private void Start()
    {
        firstsprite = GetComponent<SpriteRenderer>().sprite;
    }
    void Update()
    {
        if (checkForExit == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                float dist = Vector3.Distance(PlayerScript.instance.gameObject.transform.position, transform.position);
                if (dist < 8)
                {
                    GetComponent<SpriteRenderer>().sprite = secondsprite;
                    checkForExit = true;
                }

            }
        }
        else
        {

            float dist = Vector3.Distance(PlayerScript.instance.gameObject.transform.position, transform.position);
            if (dist > 10 || Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<SpriteRenderer>().sprite = firstsprite;
                checkForExit = false;
            }
        }
    }
}
