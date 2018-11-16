 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class EditorController : MonoBehaviour {

	public static EditorController control;
	public GameObject asteroid;
	public GameObject hazards;
	public GameObject ship;
	public float dragSpeed = 2;
	public Toggle asteroidToggle;
	public Toggle shipToggle;
	public Toggle noneToggle;

	private Vector3 difference;
	private Vector3 origin;
	public bool objectSelected;
	private GameObject myCurrentObject;
	private GameObject selectedObject;
	private GameObject loadedObject;
	private Ray ray;
	private RaycastHit hit;

	void Start() 
	{
		// Sets timescale back to one (coming from menu or other scenes)
		Time.timeScale = 1;
	}

	void Update()
	{
		//Instantiate ();
	}
	// Gets all the hazards in game and creates a new object of the Hazard class with its type, xPosition and zPosition, that Hazard object is then added
	// to a HazardContainer object list
	// The list then gets ordered by -zPosition and finally Serialized into a Xml file. 
	/*public void Save(string mapName) 
	{
		char[] delimiterChar = {' ', '('};
		HazardContainer Level = new HazardContainer ();
		foreach (GameObject hazard in GameObject.FindGameObjectsWithTag("hazard")) 
		{
			// The type of the ship is the first part of its name (before the (clone)).
			String[] ship = hazard.name.Split(delimiterChar);
			String typeship = ship[0];
			Level.Hazards.Add (new Hazard() {Name = hazard.name, 
				type = typeship, xPos = hazard.transform.position.x, zPos = hazard.transform.position.z});
		}

		Level.Hazards = Level.Hazards.OrderBy(Hazard=> - Hazard.zPos).ToList();

		var serializer = new XmlSerializer(typeof(HazardContainer));
		var stream = new FileStream(Application.persistentDataPath + "/" + mapName + ".xml", FileMode.Create);
		serializer.Serialize(stream, Level);
		stream.Close();
	}
	// Clears the level,
	// Gets the Serialized Xml file and Deserializes it,
	// Then, for each Hazard object in the list, it instantiates on gameObject of the same type and Coordinates.
	public void Load(string mapName)
	{
		Clear ();

		var serializer = new XmlSerializer(typeof(HazardContainer));
		var stream = new FileStream(Application.persistentDataPath + "/" + mapName + ".xml", FileMode.Open);
		var Level = serializer.Deserialize(stream) as HazardContainer;
		stream.Close();
		foreach (Hazard haz in Level.Hazards) 
		{
			switch (haz.type)
			{
			case "Asteroid_editor":
				loadedObject = asteroid;
				break;
			case "Ship_editor":
				loadedObject = ship;
				break;
			}
			GameObject Asteroid_editor = Instantiate
				(loadedObject, new Vector3 (haz.xPos, 1, haz.zPos),new Quaternion (0, 180, 0, 0)) as GameObject;
			Asteroid_editor.transform.parent = hazards.transform;

		}
	}

	// Simply clears the entire level of hazards, to be used with load level and load new ones
	public void Clear() 
	{
		GameObject[] Hazards = GameObject.FindGameObjectsWithTag("hazard");;
		
		for (int i = 0; i < Hazards.Length; i++)
		{
			Destroy(Hazards[i]);
		}
	}

	public void TestMap() 
	{
		Save ("testmap");
		LoadController.mapName = "testmap";
		Application.LoadLevel (2);
	}
	
	// instantiate one object of the objectType from the CreateAsteroid() function on mouse click
	void Instantiate()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (!objectSelected) 
		{
		} 
		else
		{
			// That hazard is now "myCurrentObject" and is placed inside the hazards parent gameObject
			if(Input.GetMouseButtonDown(0) && Camera.main.ScreenToWorldPoint (Input.mousePosition).x <= 3 && !Physics.Raycast(ray, out hit))
			{
				myCurrentObject = Instantiate(selectedObject, 
				new Vector3 (Mathf.Clamp(Camera.main.ScreenToWorldPoint (Input.mousePosition).x, -12, 0), 1
				, Camera.main.ScreenToWorldPoint (Input.mousePosition).z ), new Quaternion (0, 180, 0, 0)) as GameObject;
				myCurrentObject.transform.parent = hazards.transform;
			}
			// If we keep pressing on the mouse after instantiating we can drag "myCurrentObject"
			if(Input.GetMouseButton(0) && myCurrentObject)
			{
				myCurrentObject.transform.position =  new Vector3 
					(Mathf.Clamp(Camera.main.ScreenToWorldPoint (Input.mousePosition).x, -12, 0), 1
				    , Camera.main.ScreenToWorldPoint (Input.mousePosition).z );
			}
			// if we let go of the mouse, we lose myCurrentObject, so we can start all over again
			if(Input.GetMouseButtonUp(0) && myCurrentObject)
			{
				myCurrentObject = null;
			}
		}
	}
	*/
	public void Toggle ()
	{
		if (asteroidToggle.isOn) 
		{
			objectSelected = true;
			selectedObject = asteroid;

		} 
		else if (shipToggle.isOn) 
		{
			objectSelected = true;
			selectedObject = ship;
		}
		else if (noneToggle.isOn) 
		{
			objectSelected = false;
		}
	}
}

