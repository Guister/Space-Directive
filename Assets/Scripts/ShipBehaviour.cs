using UnityEngine;
using System.Collections;

public class ShipBehaviour : MonoBehaviour {

	public int movement;
	public int speed;
	public int special;
	public int special_type;
	public GameObject shot;					//Object we are going to instantiate
	public Transform shotSpawn;				//Position from where the shot comes from
	public float fireRate;					//Time in between each shot
	public float delay;	
	private AudioSource audioSource;
	private EvasiveManeuvers ev;
	private int movespeed;
	// Use this for initialization
	void Start () {

		ev = this.gameObject.GetComponent<EvasiveManeuvers>();
		audioSource = GetComponent<AudioSource> ();
		if (special_type == 1) {
			InvokeRepeating ("FireWeek", delay, fireRate); 

			if (special > 1) {
				InvokeRepeating ("FireMedium", delay, fireRate); 
			}

			if (special > 2) {
				InvokeRepeating ("FireHard", delay, fireRate); 
			}
		} else if (special_type == 2) 
		{
			InvokeRepeating ("FireMissileBack", delay, fireRate); 
			if (special > 1) 
			{
				InvokeRepeating ("FireMissileRandom", delay + 0.4f, fireRate); 
			}
			if (special > 2) 
			{
				InvokeRepeating ("FireMissileRandom", delay + 0.7f, fireRate);
			}
		}

		if (speed == 1) {
			movespeed = 4;
		} else if (speed == 2) {
			movespeed = 6;
		} else if (speed == 3) {
			movespeed = 8;
		}

		if (movement == 1) {
			GetComponent<Rigidbody> ().velocity = transform.forward * - movespeed;
		} else if (movement == 2) {
			GetComponent<Rigidbody> ().velocity = new Vector3(0,  - movespeed * 0.7f,  - movespeed * 0.7f);
		}
		else if (movement == 3) {
			GetComponent<Rigidbody> ().velocity = new Vector3(0,  - movespeed * - 0.7f,  - movespeed * 0.7f);
		}
		else if (movement == 4) {
			GetComponent<Rigidbody> ().velocity = transform.forward *  - movespeed;
			ev.enabled = true;
		}

	}

	void FireWeek()
	{
		Instantiate (shot, shotSpawn.position, Quaternion.Euler(0, 0, 90));
		audioSource.Play ();
	}

	void FireMedium() 
	{

		Instantiate (shot, shotSpawn.position, Quaternion.Euler(-10, 0, 90));
		Instantiate (shot, shotSpawn.position, Quaternion.Euler(10, 0, 90));
	}

	void FireHard() 
	{
		Instantiate (shot, shotSpawn.position, Quaternion.Euler(-3, 0, 90));
		Instantiate (shot, shotSpawn.position, Quaternion.Euler(3, 0, 90));
		Instantiate (shot, shotSpawn.position, Quaternion.Euler(-6, 0, 90));
		Instantiate (shot, shotSpawn.position, Quaternion.Euler(6, 0, 90));
	}

	void FireMissileBack () 
	{
		Instantiate (shot, shotSpawn.position, Quaternion.Euler(0, 0, 0));
	}

	void FireMissileRandom () 
	{
		Instantiate (shot, shotSpawn.position, Quaternion.Euler(Random.Range(-50, 50), 0, 0));
	}



	// Update is called once per frame
	void Update () {
	
	}
}
