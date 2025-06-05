using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleWalkParent : MonoBehaviour
{
    int lastNode = 0;

    [SerializeField] List<GameObject> correctOrder;

    [SerializeField] List<GameObject> removeThese;

    [SerializeField] Vector3 removeVelocity;

    [SerializeField] GameObject particlePrefab;
    [SerializeField] Vector3 particleOffset;
    [SerializeField] Material nodeCompleteMaterial;

    public void ReceiveNode(GameObject _nextNode)
    {
        if (correctOrder.Count > 0)
        {
            if (_nextNode == correctOrder[0])
            {
                print("found " + _nextNode.name);
                if (particlePrefab != null)
                {
                    GameObject particle = Instantiate(particlePrefab);
                    particle.transform.position = _nextNode.transform.position + particleOffset;
                }
                if (nodeCompleteMaterial != null)
                {
                 //   print("change mat");
                    _nextNode.GetComponent<MeshRenderer>().material = nodeCompleteMaterial;
                }

                correctOrder.RemoveAt(0);
            }
        }
        else
        {
            print("all found");
            for (int i = 0; i < removeThese.Count; i++)
            {


                removeThese[i].AddComponent<PuzzleResultBehavior>();
                removeThese[i].GetComponent<PuzzleResultBehavior>().myVelocity = removeVelocity;
                Destroy(removeThese[i], 20);
            }

            GameObject successSoundObj = transform.Find("SuccessSound").gameObject;
            successSoundObj.transform.parent = null;
            successSoundObj.GetComponent<AudioSource>().Play();
            Destroy(successSoundObj, 7);



            Destroy(this.gameObject);
        }



    }


}
