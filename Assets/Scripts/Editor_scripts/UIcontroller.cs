using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using System.Xml.Serialization;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using System.Xml;

public class UIcontroller : MonoBehaviour {

	public GameObject General_panel;
	public GameObject Ships_panel;
	public GameObject Asteroids_panel;
	public GameObject Fleets_panel;
	public GameObject Bosses_panel;
	public GameObject Music_panel;
	public GameObject Background_panel;
	public GameObject Selected_panel;
	public GameObject Stats_panel;
	public GameObject Stats_fleet;
	public GameObject hazards;
	public GameObject Error;
	public GameObject background1;
	public GameObject background2;
	public Canvas canvas;

	public GameObject[] editor_objects;

	public ToggleGroup Ships_toggle;
	public ToggleGroup Asteroids_toggle;
	public ToggleGroup Fleets_toggle;
	public ToggleGroup Bosses_toggle;
	public ToggleGroup Music_toggle;
	public ToggleGroup Background_toggle;
	public AudioClip[] clips;

	public Toggle default_background;
	public Toggle default_music;
	public Toggle default_boss;

	public InputField map_name;
	public Camera camera;

	private GameObject myObject;
	private GameObject[] Panels;
	private ToggleGroup[] Toggles;
	private ToggleGroup toggle;
	private GameObject panel;
	private GameObject selected_object;
	private int open_panel;
	public int toggled_object;
	private Vector3 select_size;
	private Quaternion rotation;
	private GameObject myBackground;

	private EditorObjects shipScript;
	private FormationScript formationScript;

	public RectTransform selected_panel;
	public RectTransform stats_panel;
	public RectTransform stats_fleet;

	private Toggle toggled_background;
	private Toggle toggled_music;
	private Toggle toggled_boss;

	private AudioSource audio;

	private float timer;


	private Ray ray;
	private RaycastHit hit;

	// Use this for initialization
	void Start () 
	{
		timer = 0.0f;
		audio = GetComponent<AudioSource>();
		Panels = new GameObject[6] {Ships_panel, Asteroids_panel, Fleets_panel, Bosses_panel, Music_panel, Background_panel};
		Toggles = new ToggleGroup[6] {Ships_toggle,Asteroids_toggle,Fleets_toggle,Bosses_toggle,Music_toggle,Background_toggle};
		open_panel = 0;
		toggled_object = 0;
		toggled_background = default_background;
		toggled_boss = default_boss;
		toggled_music = default_music;

	}

	void Update() 
	{
		timer += Time.deltaTime;
		if (Background_toggle.IsActive ()) 
		{
			toggled_background = Background_toggle.ActiveToggles ().FirstOrDefault ();
		}
		if (Music_toggle.IsActive ()) 
		{
			toggled_music = Music_toggle.ActiveToggles ().FirstOrDefault ();
		}
		if (Bosses_toggle.IsActive ()) 
		{
			toggled_boss = Bosses_toggle.ActiveToggles ().FirstOrDefault ();
		}

		if (timer >= 5.0f) 
		{
			audio.Stop ();
		}

		UnFocusObject ();
		Instantiate ();
	}

	//Code to open the Specific Panels
	public void MoveIn(int button_number) 
	{
		panel = Panels [button_number];
		panel.SetActive (true);
		General_panel.SetActive (false);
		open_panel = button_number;
	}
	//Code to close the Open Panel.
	public void MoveOut()
	{
		if (open_panel == 0 || open_panel == 1 || open_panel == 2) 
		{
			toggle = Toggles [open_panel];
			toggle.SetAllTogglesOff ();
			toggled_object = 0;
		}

		General_panel.SetActive (true);
		panel = Panels [open_panel];
		panel.SetActive (false);
	}
	//Toggled Object
	public void SelectObject (int objectNumber)
	{
		if (toggled_object == objectNumber) {
			toggled_object = 0;
		} else {
			toggled_object = objectNumber;
		}
	}

	private void UnFocusObject ()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (!Physics.Raycast (ray, out hit) && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject ()) 
		{
			Selected_panel.SetActive (false);
			Stats_fleet.SetActive (false);
			Stats_panel.SetActive (false);
		}
	}

	public void ChangeBackground(int number)
	{
		GameObject[]activeBackground = GameObject.FindGameObjectsWithTag ("Background");

		foreach (GameObject b in activeBackground) {
			Destroy (b);
		}

		if (number == 1) {
			myBackground = background1;
		} else if (number == 2) {
			myBackground = background2;
		}

		GameObject newBackground = Instantiate(myBackground, 
			new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
	}

	public void ChangeMusic(int number)
	{
		audio.clip = clips [number];
		audio.Play ();
		timer = 0.0f;

	}

	void Instantiate()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (toggled_object == 0) 
		{
		} 
		else
		{
			// That hazard is now "myCurrentObject" and is placed inside the hazards parent gameObject
			if(Input.GetMouseButtonDown(0) && Camera.main.ScreenToWorldPoint (Input.mousePosition).y <= 7 && !Physics.Raycast(ray, out hit) && Camera.main.ScreenToWorldPoint (Input.mousePosition).y >= -7 && !EventSystem.current.IsPointerOverGameObject ())
			{
				if (toggled_object == 1) {
					rotation = Quaternion.Euler (0, 180, 30);
				} else if (toggled_object == 2) {
					rotation = Quaternion.Euler (0, 0, 330);
				} else if (toggled_object == 3 || toggled_object == 4 || toggled_object == 5) {
					rotation = UnityEngine.Random.rotation;
				} else if (toggled_object == 6 || toggled_object == 7 || toggled_object == 8) {
					rotation = Quaternion.identity;
				}

				myObject = Instantiate(editor_objects[toggled_object - 1], 
					new Vector3 (0, Camera.main.ScreenToWorldPoint (Input.mousePosition).y
						, Camera.main.ScreenToWorldPoint (Input.mousePosition).z ), rotation) as GameObject;
				myObject.transform.parent = hazards.transform;

				if (toggled_object == 1 || toggled_object == 2) {
					EditorObjects myObjectScript = myObject.GetComponent<EditorObjects> ();

					myObjectScript.camera = camera;
					myObjectScript.canvas = canvas;
					myObjectScript.selected_panel = selected_panel;
					myObjectScript.stats_pannel = stats_panel;
				} else if (toggled_object == 6 || toggled_object == 7 || toggled_object == 8) {
					FormationScript myObjectScript = myObject.GetComponent<FormationScript> ();

					myObjectScript.camera = camera;
					myObjectScript.canvas = canvas;
					myObjectScript.selected_panel = selected_panel;
					myObjectScript.stats_fleet = stats_fleet;

				}

			}
		}
	}

	public void CloseError() 
	{
		Error.SetActive (false);
	}

	public void SaveMap() 
	{
		if (map_name.text == "") 
		{
			Error.SetActive (true);
			return;
		} 
		HazardContainer Level = CreateLevel ();
		var serializer = new XmlSerializer(typeof(HazardContainer));
		var stream = new FileStream(Application.persistentDataPath + "/Map.xml", FileMode.Create);
		serializer.Serialize(stream, Level);
		stream.Close();





	}

	public void exit() 
	{
		SceneManager.LoadScene ("MainMenu");
	}

	public void TestMap()
	{
		HazardContainer Level = CreateLevel ();
		var serializer = new XmlSerializer(typeof(HazardContainer));
		var stream = new FileStream(Application.persistentDataPath + "/Test.xml", FileMode.Create);
		serializer.Serialize(stream, Level);
		stream.Close();
		Static_Gamemaster.mapname = "Test";
		SceneManager.LoadScene ("Arcade");
	}

	public HazardContainer CreateLevel() 
	{ 
		char[] delimiterChar = {' ', '('};
		HazardContainer Level = new HazardContainer ();

		foreach (GameObject Ship in GameObject.FindGameObjectsWithTag("Ship")) 
		{
			shipScript = Ship.GetComponent<EditorObjects> ();

			// The type of the ship is the first part of its name (before the (clone)).
			String[] ship = Ship.name.Split(delimiterChar);
			String typeship = ship[0];
			Level.Ships.Add (new Ship() {Name = Ship.name, 
				type = typeship, yPos = Ship.transform.position.y, zPos = Ship.transform.position.z, speed = shipScript.speed, movement = shipScript.movement, special = shipScript.special});
		}

		foreach (GameObject Fleet in GameObject.FindGameObjectsWithTag("Formation")) 
		{
			formationScript = Fleet.GetComponent<FormationScript> ();

			// The type of the ship is the first part of its name (before the (clone)).
			String[] fleet = Fleet.name.Split(delimiterChar);
			String typefleet = fleet[0];
			Level.Fleets.Add (new Fleet() {Name = Fleet.name, 
				type = typefleet, yPos = Fleet.transform.position.y, zPos = Fleet.transform.position.z, speed = formationScript.speed, movement = formationScript.movement, special = formationScript.special, ship_type = formationScript.ship});
		}

		foreach (GameObject Asteroid in GameObject.FindGameObjectsWithTag("Asteroid")) 
		{
			// The type of the ship is the first part of its name (before the (clone)).

			String[] asteroid = Asteroid.name.Split(delimiterChar);
			String typeasteroid = asteroid[0];
			Level.Asteroids.Add (new Asteroid() {Name = Asteroid.name, 
				type = typeasteroid, yPos = Asteroid.transform.position.y, zPos = Asteroid.transform.position.z});
		}

		Level.Background = toggled_background.name;
		Level.Music = toggled_music.name;
		Level.Boss = toggled_boss.name;

		Level.Ships = Level.Ships.OrderBy(Ship=> Ship.zPos).ToList();
		Level.Fleets = Level.Fleets.OrderBy(Fleet=> Fleet.zPos).ToList();
		Level.Asteroids = Level.Asteroids.OrderBy(Asteroid=> Asteroid.zPos).ToList();

		return Level;
	}
}



public class Ship
{
	[XmlAttribute("name")]
	public string Name;
	public string type;
	public float yPos;
	public float zPos;
	public int speed;
	public int movement;
	public int special;
}

public class Asteroid
{
	[XmlAttribute("name")]
	public string Name;
	public string type;
	public float yPos;
	public float zPos;
}

public class Fleet 
{
	[XmlAttribute("name")]
	public string Name;
	public string type;
	public float yPos;
	public float zPos;
	public int speed;
	public int movement;
	public int special;
	public int ship_type;
}
// Class to be used in the Load and Save functions
[XmlRoot("Level")]
public class HazardContainer
{
	[XmlArray("Ships"),XmlArrayItem("Ship")]
	public List<Ship> Ships = new List<Ship>();

	[XmlArray("Asteroids"),XmlArrayItem("Asteroid")]
	public List<Asteroid> Asteroids = new List<Asteroid>();

	[XmlArray("Fleets"),XmlArrayItem("Fleet")]
	public List<Fleet> Fleets = new List<Fleet>();

	public string Background;
	public string Boss;
	public string Music;
}
