using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggerParent : MonoBehaviour
{
    [SerializeField] GameObject startWithThisSound;
    void Start()
    {
        if (startWithThisSound)
            startWithThisSound.GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        
    }
    public void PlayNewSound(GameObject _sound)
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<AudioSource>())
            {
                child.GetComponent<AudioSource>().Stop();

            }
        }
            _sound.GetComponent<AudioSource>().Play();
    }
}
