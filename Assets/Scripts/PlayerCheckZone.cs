using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerCheckZone : MonoBehaviour
{
    [SerializeField] AudioMixer zoneMixer;
    [SerializeField] AudioSource[] audioTracks;

    AudioSource currentTrack;

    float chorusRate = 0;


    void Start()
    {
        //  print(ZoneType.ORANGE.GetHashCode());
        
       
    }

    void Update()
    {
        // print(zone);

        ChangeTrack();

        ChorusControl();
      

    }
    void ChangeTrack()
    {
        int zone = PointAtGround();

        if (zone >= 0 && zone < audioTracks.Length)
        {
            AudioSource theTrack = audioTracks[zone];
            if (currentTrack != theTrack)
            {
                theTrack.Play();

                if (currentTrack)
                    currentTrack.Pause();

                currentTrack = theTrack;
                //  print("play " + theTrack.name);

                chorusRate = 9.4f;
                GetComponent<FOVSlide>().BoostFOVstep();
            }
        }
    }
 
    void ChorusControl()
    {
        if (chorusRate > 0)
        {
            chorusRate -= Time.deltaTime * 8;
            zoneMixer.SetFloat("RateOfChorus", chorusRate);
        }
    }

    int PointAtGround()
    {
        Vector3 lookDownPoint = transform.position;

        int foundZone = 10;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(lookDownPoint, Vector3.down, 40);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            foundZone = hit.transform.gameObject.layer.GetHashCode() - 10;

        }

        return foundZone;
    }
}
