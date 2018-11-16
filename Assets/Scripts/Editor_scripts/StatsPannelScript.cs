using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsPannelScript : MonoBehaviour {

	public EditorObjects ship;
	public Slider speed, movement, special;
	public GameObject select_pannel;
	public Text special_txt;


	void OnEnable () {
		speed.value = ship.speed;
		movement.value = ship.movement;
		special.value = ship.special;
		special_txt.text = ship.special_name;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void changeSpeed()
	{
		ship.speed = (int)speed.value;
	}

	public void changeMovement()
	{
		ship.movement = (int)movement.value;
	}

	public void changeSpecial()
	{
		ship.special = (int)special.value;
	}

	public void DeleteShip() 
	{
		Destroy (ship.gameObject);
		this.gameObject.SetActive (false);
		select_pannel.SetActive (false);
	}
}
