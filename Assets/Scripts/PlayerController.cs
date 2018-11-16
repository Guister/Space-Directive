using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]
public class Boundary
{
	public float yMin, yMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	void Update ()
	{
		// Checks to see if the player can shoot and if the pressed the shoot button
		if (Input.GetButton ("Fire1") && Time.time > nextFire) 
		{
			// Instantiates the shot and increases the nextFire variable so he can only shoot after a while (fireRate)
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation); // as GameObject;
			GetComponent<AudioSource>().Play ();
		}
	}

	// Moves the player with the Horizontal and Vertical Axis within the game boundary 
	//and turns the ship slightly when moving horizontaly
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal") + CrossPlatformInputManager.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical") + CrossPlatformInputManager.GetAxis("Vertical");
		Vector3 movement = new Vector3 (0.0f, moveVertical, moveHorizontal);
		GetComponent<Rigidbody>().velocity = movement * speed;

		GetComponent<Rigidbody>().position = new Vector3 
			(
				0.0f,
				Mathf.Clamp (GetComponent<Rigidbody>().position.y, boundary.yMin, boundary.yMax),
				Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
			);

		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, (GetComponent<Rigidbody>().velocity.y * -tilt) - 30);
	}

}