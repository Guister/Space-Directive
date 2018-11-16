using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsFleet : MonoBehaviour {

	public FormationScript fleet;
	public Slider speed, movement, special;
	public GameObject select_pannel;
	public Text special_txt;
	public ToggleGroup selected;

	public GameObject grunt;
	public GameObject vector;

	private GameObject myShip;
	private GameObject myObject;
	private Quaternion rotation;

	void OnEnable () {
		speed.value = fleet.speed;
		movement.value = fleet.movement;
		special.value = fleet.special;
		special_txt.text = fleet.special_name;
	}

	// Update is called once per frame
	void Update () {
	}

	public void changeSpeed()
	{
		fleet.speed = (int)speed.value;
	}

	public void changeMovement()
	{
		fleet.movement = (int)movement.value;
	}

	public void changeSpecial()
	{
		fleet.special = (int)special.value;
	}

	public void DeleteShip() 
	{
		Destroy (fleet.gameObject);
		this.gameObject.SetActive (false);
		select_pannel.SetActive (false);
	}

	public void changeShip(int ship) 
	{
		int i = 0;
		foreach (Transform s in fleet.transform) 
		{
			if (i > 5) {
				Destroy (s.gameObject);
			}
			i++;
		}

		if (ship == 1) {
			rotation = Quaternion.Euler (0, 180, 30);
			myShip = grunt;
		} else if (ship == 2) {
			rotation = Quaternion.Euler (0, 0, 330);
			myShip = vector;
		}

		foreach (Transform t in fleet.positions) 
		{
			myObject = Instantiate(myShip, 
				t.position, rotation) as GameObject;
			myObject.transform.parent = fleet.transform;
		}

		fleet.ship = ship;
			
	}
}
