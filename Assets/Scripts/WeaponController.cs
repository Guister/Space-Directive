using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
	
	public GameObject shot;					//Object we are going to instantiate
	public Transform shotSpawn;				//Position from where the shot comes from
	public float fireRate;					//Time in between each shot
	public float delay;						//Time the enemy takes to start shooting, after appearing
	
	private AudioSource audioSource;		//Sound it plays when shooting

	void Start () 
	{
		//Calls the method "Fire", repeating it every "fireRate"
		audioSource = GetComponent<AudioSource> ();
		InvokeRepeating ("Fire", delay, fireRate); 
	}
	//creates enemie's shoots
	void Fire()
	{
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		audioSource.Play ();
	}
	
}
