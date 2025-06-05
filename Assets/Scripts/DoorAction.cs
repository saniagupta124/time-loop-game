using UnityEngine;
using System.Collections;

public class DoorAction : MonoBehaviour
{

	Vector3 initialDoorPosition;
	Vector3 initialSwitchLocalPosition;
	GameObject mySwitch;
	GameObject myCenter;
	 GameObject player;
	const int WAITLOW = 0, RISEUP = 1, WAITHIGH = 2, MOVEDOWN = 3;
	int state = WAITLOW;

	public float distFromSwitch = 0.8f;
	public float ySpeed = 0.2f;
	float upHighCounter = 0;
	public int upHighWaitTime = 250;
	public float doorRisesThisHigh = 3;

	void Start ()
	{
		initialDoorPosition = transform.position; // remember where door is
		mySwitch = transform.Find ("mySwitch").gameObject; // find the switch that is my child



		initialSwitchLocalPosition = mySwitch.transform.localPosition; // remember where switch is
		myCenter = transform.Find ("center").gameObject;  // for drawing the line

        player = PlayerScript.instance.gameObject;
	}

	void Update ()
	{
		CheckSwitch ();
	}

	void CheckSwitch ()
	{

		switch (state) {
		case WAITLOW:
			float dist = Vector3.Distance (player.transform.position, mySwitch.transform.position);
			if (dist < distFromSwitch) { // if player gets close to switch then....
				mySwitch.transform.Translate (-99999, 99999, -99999); // move switch darn far away.  The effect is that it disappeared. 
				GetComponent<AudioSource> ().timeSamples = 0; // start sound effect at beginning
				GetComponent<AudioSource> ().pitch = 1; // normal pitch
				GetComponent<AudioSource> ().Play (); // play it
				state = RISEUP; // change state
			}
		//	DrawLine (myCenter.transform.position, mySwitch.transform.position, new Color (1, 0, 0.1f, 0.5f)); // draw a red line from the switch to where the door waits
			break;

		case RISEUP:
			float yMove = 60 * Time.deltaTime * ySpeed; // move door upwards this amount per 'frame'
			transform.Translate (0, yMove, 0);
			if (transform.position.y > initialDoorPosition.y + doorRisesThisHigh) { // did it reach our goal? 
				state = WAITHIGH;
			}
			break;
		case WAITHIGH:
			upHighCounter += 60 * Time.deltaTime; // count in order to wait.  
			if (upHighCounter > upHighWaitTime) { // waited long enough? 
				upHighCounter = 0; // reset for next use
				GetComponent<AudioSource> ().timeSamples = GetComponent<AudioSource> ().clip.samples - GetComponent<AudioSource> ().clip.samples / 10; // cue sound 10% from it's end point
				GetComponent<AudioSource> ().pitch = -1.5f; // will play backwards a little faster than normal
				GetComponent<AudioSource> ().Play (); // play it
				state = MOVEDOWN; // change state
			}
			break;

		case MOVEDOWN:
			float yMove2 = 60 * Time.deltaTime * ySpeed; // how much to movedownward per frame
			transform.Translate (0, -yMove2, 0);
			if (transform.position.y - initialDoorPosition.y <= 0.1) { // is door darn close to the initial location? 
				transform.position = initialDoorPosition; // force door to be exactly where it was now that we established it's very close
				mySwitch.transform.localPosition = initialSwitchLocalPosition; // place switch back where it was 
				state = WAITLOW; // change state
			}
			break;
		}

	}



	//void DrawLine (Vector3 start, Vector3 end, Color color)
	//{
		
	//	GameObject myLine = new GameObject ();
	//	myLine.transform.position = start;
	//	myLine.AddComponent<LineRenderer> ();
	//	LineRenderer lr = myLine.GetComponent<LineRenderer> ();
	//	lr.material = new Material (Shader.Find ("Particles/Alpha Blended Premultiply"));
	//	lr.SetColors (color, color);
	//	lr.SetWidth (0.05f, 0.05f);
	//	lr.SetPosition (0, start);
	//	lr.SetPosition (1, end);
	//	GameObject.Destroy (myLine, 0.01f);
	//}


}
