using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyForText : MonoBehaviour
{

    [SerializeField] string textResult;
    StudyByPlayer resultTextComponent;
    bool checkForExit = false;

    private void Start()
    {
        resultTextComponent = PlayerScript.instance.GetComponent<StudyByPlayer>();
    }
    void Update()
    {
        if (checkForExit == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                float dist = Vector3.Distance(PlayerScript.instance.gameObject.transform.position, transform.position);
                if (dist < 12)
                {
                    resultTextComponent.RecieveText(textResult);
                    checkForExit = true;
                }

            }
        }
        else
        {

            float dist = Vector3.Distance(PlayerScript.instance.gameObject.transform.position, transform.position);
            if (dist > 15 || Input.GetKeyDown(KeyCode.E))
            {
                resultTextComponent.RecieveText("");
                checkForExit = false;
            }
        }
    }
}
