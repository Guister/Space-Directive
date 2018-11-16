using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using System.Xml;

public class GameController : MonoBehaviour 
{
	public GameObject[] Ships;
	public GameObject[] Asteroids;
	public GameObject[] Fleets;
	public GameObject[] Backgrounds;
	public AudioClip[] clips;
	public GameObject boss1;
	public GameObject boss2;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public Text scoreText;
	
	private bool gameOver;
	private int score;
	private bool bossEnabled;
	private HazardContainer Level;
	private Ship firstShip;
	private Asteroid firstAsteroid;
	private Fleet firstFleet;
	private float timeToSpawn;
	private float time;
	private int firstObjectType;
	private Quaternion spawnRotation;
	private Vector3 spawnPosition;
	private bool remove;
	private float timeToSpawnShip;
	private float timeToSpawnAsteroid;
	private float timeToSpawnFleet;
	private AudioSource audio;
	private GameObject instancedObject;

	private ShipBehaviour shipScript;
	private FormationScriptArcade formationScript;

	private GameObject loadedObject;
	private GameObject myBoss;
	public float timer;
	public Vector3 spawnBossValues;
	
	void Start ()
	{
		gameOver = false;
		bossEnabled = false;
		remove = false;
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
		Time.timeScale = 1;
		Load ();
	}
	
	void Update ()
	{
		time = Time.timeSinceLevelLoad;
	}

	void Load () 
	{
		if (Static_Gamemaster.mapname == "Test") {
			var serializer = new XmlSerializer (typeof(HazardContainer));
			var stream = new FileStream (Application.persistentDataPath + "/Test.xml", FileMode.Open);
			Level = serializer.Deserialize (stream) as HazardContainer;
			stream.Close ();
		} else if (Static_Gamemaster.mapname == "") {
			XmlReader reader = XmlReader.Create (new StringReader (Static_Gamemaster.lvl1));
			reader.MoveToContent ();
			Level = new XmlSerializer (typeof(HazardContainer)).Deserialize (reader) as HazardContainer;
		} else if (Static_Gamemaster.mapname == "Map") {
			var serializer = new XmlSerializer (typeof(HazardContainer));
			var stream = new FileStream (Application.persistentDataPath + "/Map.xml", FileMode.Open);
			Level = serializer.Deserialize (stream) as HazardContainer;
			stream.Close ();
		}
			
		if (Level.Background == "Purple Haze") {
			spawnRotation = Quaternion.Euler (0, -90, 0);
			spawnPosition = new Vector3 (-10, 0, -30);
			Instantiate (Backgrounds [0], spawnPosition, spawnRotation);
		} else if (Level.Background == "Green Nebula") {
			spawnRotation = Quaternion.Euler (0, -90, 0);
			spawnPosition = new Vector3 (-10, 0, -30);
			Instantiate (Backgrounds [1], spawnPosition, spawnRotation);
		}

		audio = GetComponent<AudioSource>();

		if (Level.Music == "Citadel") 
		audio.clip = clips [0];

		else if (Level.Music == "Eclipse")
		audio.clip = clips [1];

		else if (Level.Music == "Nebula")
		audio.clip = clips [2];
		audio.Play ();


	}
	// Coroutine to randomly spawn the waves
	IEnumerator SpawnWaves()
	{
		
		yield return new WaitForSeconds (0.5f);

		while (true) {
			if (Level.Ships.Count > 0) 
			{
				firstShip = Level.Ships [0];
				timeToSpawnShip = firstShip.zPos / 4;

			} 
			else 
			{
				timeToSpawnShip = 1000;
			}
			if (Level.Asteroids.Count > 0) 
			{
				firstAsteroid = Level.Asteroids [0];
				timeToSpawnAsteroid = firstAsteroid.zPos /4;

			} else {
				timeToSpawnAsteroid = 1000;
			}
			if (Level.Fleets.Count > 0) {
				firstFleet = Level.Fleets [0];
				timeToSpawnFleet = firstFleet.zPos / 4;
			} else 
			{
				timeToSpawnFleet = 1000;
			}

			if (timeToSpawnShip <= timeToSpawnAsteroid && timeToSpawnShip <= timeToSpawnFleet) 
			{
				timeToSpawn = timeToSpawnShip;
				firstObjectType = 0;
			} 
			else if (timeToSpawnAsteroid <= timeToSpawnShip && timeToSpawnAsteroid <= timeToSpawnFleet) 
			{
				timeToSpawn = timeToSpawnAsteroid;
				firstObjectType = 1;
			} 
			else if (timeToSpawnFleet <= timeToSpawnAsteroid && timeToSpawnFleet <= timeToSpawnShip) 
			{
				timeToSpawn = timeToSpawnFleet;
				firstObjectType = 2;
			} 
			if (time < timeToSpawn) {
				yield return new WaitForSeconds (timeToSpawn - time);
			}
			// Instantiates the hazard at a set z value for position and the corresponding x value from the editor 
			// In the editor the x goes from 0 to -12 and in the Load Level and arcade it goes from -6 to 6, hence the +6 on the x position
			// After it instantiates the hazard, it flags it to be removed from the list
			else if (time >= timeToSpawn) {
				if (firstObjectType == 0) {
					switch (firstShip.type) {
					case "Grunt":
						loadedObject = Ships [0];
						spawnRotation = Quaternion.Euler (0, 0, 330);
						break;
					case "Vector":
						loadedObject = Ships [1];
						spawnRotation = Quaternion.Euler (0, 0, 330);
						break;
					}
					spawnPosition = new Vector3 (0, firstShip.yPos, 30);
				} else if (firstObjectType == 1) {
					switch (firstAsteroid.type) {
					case "Asteroid_small":
						loadedObject = Asteroids [0];
						break;
					case "Asteroid_Medium":
						loadedObject = Asteroids [1];
						break;
					case "Asteroid_Big":
						loadedObject = Asteroids [2];
						break;
					}
					spawnRotation = Quaternion.identity;
					spawnPosition = new Vector3 (0, firstAsteroid.yPos, 30);
				} else {
					switch (firstFleet.type) 
					{
					case "Block_Formation":
						loadedObject = Fleets [0];
						break;
					case "Triangle_Formation":
						loadedObject = Fleets [1];
						break;
					case "Row_Formation":
						loadedObject = Fleets [2];
						break;
					}
					spawnRotation = Quaternion.identity;
					spawnPosition = new Vector3 (0, firstFleet.yPos, 30);
				}
				instancedObject = Instantiate (loadedObject, spawnPosition, spawnRotation) as GameObject;

				if (firstObjectType == 0) {
					shipScript = instancedObject.GetComponent<ShipBehaviour> ();
					shipScript.speed = firstShip.speed;
					shipScript.movement = firstShip.movement;
					shipScript.special = firstShip.special;
				} else if (firstObjectType == 2) 
				{
					formationScript = instancedObject.GetComponent<FormationScriptArcade> ();
					formationScript.speed = firstFleet.speed;
					formationScript.movement = firstFleet.movement;
					formationScript.special = firstFleet.special;
					formationScript.ship = firstFleet.ship_type;
				}

				remove = true;



			}
			// Removes the instantiated hazard 
			if (remove) {
				if (firstObjectType == 0 && Level.Ships.Count > 0) {
					Level.Ships.RemoveAt (0);
					remove = false;
				} else if (firstObjectType == 1 && Level.Asteroids.Count > 0) {
					Level.Asteroids.RemoveAt (0);
					remove = false;
				} else if (firstObjectType == 2 && Level.Fleets.Count > 0) 
				{
					Level.Fleets.RemoveAt (0);
					remove = false;
				}
			}
			// If there are no more Hazards in the list, then instantiate boss
			if (Level.Ships.Count == 0 && Level.Asteroids.Count == 0 && Level.Fleets.Count == 0) {
				yield return new WaitForSeconds (4);
				Vector3 spawnBoss = new Vector3 (spawnBossValues.x, spawnBossValues.y, spawnBossValues.z);
				if (Level.Boss == "Goliath") {
					myBoss = boss1;
				}
				if (Level.Boss == "Red Flash") {
					myBoss = boss2;
				}
				Instantiate (myBoss, spawnBoss, Quaternion.identity);
				break;
			}
			// If the player is Destroyed, stops the Coroutine
			if (gameOver) {
				break;
			}
		}
	}
	//is used outside, in the DestroyByContact class for each hazard, 
	//it uses it's value and adds to the player's score when destroyed
	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}
	
	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}
	// used in the DestroyByContact class, if the player is destroyed, its game over
	public void GameOver (string text)
	{
		gameOver = true;
	}
}

