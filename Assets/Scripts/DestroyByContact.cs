using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	public int hitPoints;
	private GameController gameController; 
	
	void Start()
	{
		// We need to get a reference of the Game Controller so that we can use two of its functions later, the AddScore and the GameOver
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}

	}
	
	void OnTriggerEnter(Collider other) 
	{
		// Enemies dont explode if they bump with eachother or the Boundary (although in the boundary object they get destroyed withouth the explosion effect)
		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy") || other.CompareTag ("Boss"))
		{
			return;
		}
		hitPoints = hitPoints - 1;
		if (explosion != null && hitPoints <= 0)
		{
			Instantiate (explosion, transform.position, transform.rotation);
		}
		//When the player gets hit its GameOver
		if (other.tag == "Player")
		{
			
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ("Game Over");
		}
		if (this.tag == "Boss" && hitPoints <= 0) 
		{
			gameController.GameOver ("Congratulations!");
		}
		//Adding score when an game Object other than the player is hit
		if (other.tag != "Player" && hitPoints <= 0)
		{
			gameController.AddScore(scoreValue);
		}

		Destroy(other.gameObject);
		if (hitPoints <= 0)
		{
			Destroy (gameObject);
		}
	}
}
