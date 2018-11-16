using UnityEngine;
using System.Collections;

public class EvasiveManeuvers : MonoBehaviour {
	
	public float dodge;						//Max amount of time the ship is evading
	public float smoothing;					//Speed to control the ship movement
	public float tilt;						//Speed to create the rotation effect
	public float angle;						//Angle that the ship is facing
	public Vector2 startWait;				//Time range before it starts evading
	public Vector2 maneuverTime;			//Time range while evading
	public Vector2 maneuverWait;			//Time range required to evade again
	public Boundary boundary;				//Limits of the scene
	
	private float targetManeuver;
	private float currentSpeed;
	private Rigidbody rb;

	void Start () 
	{
		rb = GetComponent <Rigidbody> ();
		currentSpeed = rb.velocity.z;
		StartCoroutine (Evade ());
	}
	//Coroutine that creates a loop after a random amount of time
	IEnumerator Evade()
	{
		while (true)
		{	
			//Performs the maneuver, moving in the x axis either left or right by using the Mathf.Sign function
			//With Mathf.Sign the ship will always dodge towards the center by returning the value of the ship's position
			//(The left side of the scene is considered negative and the right side is considered positive)
			targetManeuver = dodge * -Mathf.Sign (transform.position.y);
			//Random time it takes to perform the evasion maneuver
			yield return new WaitForSeconds (0.3f);
			//Resseting the maneuver
			targetManeuver = 0;
			//Random time it takes to restart the loop
		}
	}
	void FixedUpdate () 
	{
		//Moving the ship towards the maneuver we set
		float newManeuver = Mathf.MoveTowards (rb.velocity.y, targetManeuver, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (0.0f, newManeuver, currentSpeed);
		//Prevents the ship from leaving the scene
		rb.position = new Vector3
			(
				0.0f,
				Mathf.Clamp (rb.position.y, boundary.yMin, boundary.yMax),rb.position.z
				);
		//Creates a rotation effect when the ship is turning
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, (rb.velocity.y * tilt) - angle);
	}
}
