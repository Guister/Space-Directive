using UnityEngine;
using System.Collections;

public class AttackPattern : MonoBehaviour {

	public GameObject shot;					//Object we are going to instantiate
	public GameObject superShot;
	public Transform shotSpawn1;				//Position from where the shot comes from
	public Transform shotSpawn2;
	public Transform shotSpawn3;
	public Transform shotSpawn4;
	public Transform shotSpawn5;
	public Transform shotSpawn6;
	public float fireRate;					//Time in between each shot
	public float rocketRate;
	public float delay;						//Time the enemy takes to start shooting, after appearing
	public float rocketDelay;
	public float secondaryfireRate;			//Time in between each shot
	public float secondaryfireDelay;

	private AudioSource audioSource;		//Sound it plays when shooting

	void Start () 
	{
		//Calls the method "Fire", repeating it every "fireRate"
		audioSource = GetComponent<AudioSource> ();
		InvokeRepeating ("Fire", delay, fireRate); 
		InvokeRepeating ("Rocket", rocketDelay, rocketRate);
		InvokeRepeating ("SecondaryFire", secondaryfireDelay, secondaryfireRate);
	}
	//creates enemie's shoots
	void Fire()
	{
		Instantiate (superShot, new Vector3 (0, shotSpawn1.position.y, shotSpawn1.position.z ), shotSpawn1.rotation);
		Instantiate (superShot, new Vector3 (0, shotSpawn2.position.y, shotSpawn2.position.z ), shotSpawn2.rotation);
		audioSource.Play ();
	}
	void Rocket()
	{
		Instantiate (shot, new Vector3 (0, shotSpawn3.position.y, shotSpawn3.position.z ), shotSpawn3.rotation);
		Instantiate (shot, new Vector3 (0, shotSpawn4.position.y, shotSpawn4.position.z ), shotSpawn4.rotation);
		audioSource.Play ();
	}
	void SecondaryFire()
	{
		Instantiate (shot, new Vector3 (0, shotSpawn5.position.y, shotSpawn5.position.z ), shotSpawn5.rotation);
		Instantiate (shot, new Vector3 (0, shotSpawn6.position.y, shotSpawn6.position.z ), shotSpawn6.rotation);
		audioSource.Play ();
	}
}
