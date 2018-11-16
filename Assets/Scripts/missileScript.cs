using UnityEngine;
using System.Collections;

public class missileScript : MonoBehaviour {

	private GameObject player;
	private Quaternion aimPlayer;
	private Quaternion currentAim;
	public GameObject explosion;
	private Rigidbody rb;
	private float timer;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		rb = GetComponent<Rigidbody>();
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

		if (player == null) 
		{
			Destroy (this.gameObject);
		}
		timer += Time.deltaTime;

		if (timer >= 4.5) 
		{
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (this.gameObject);
		}

	}

	void FixedUpdate() 
	{
		if (player == null) {
			
		} else {
			currentAim = transform.rotation;
			transform.LookAt ( player.transform );
			aimPlayer = transform.rotation ;
			transform.rotation = Quaternion.Lerp ( currentAim , aimPlayer, 0.04f ) ;
			//transform.rotation = Quaternion.Lerp (this.transform.rotation, aimPlayer, 0.04f);
			rb.velocity = transform.forward * 10;


		}

	}
}
