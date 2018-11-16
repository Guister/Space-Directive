using UnityEngine;
using System.Collections;

public class FormationScriptArcade : MonoBehaviour {


	public Transform[] positions;
	public GameObject grunt;
	public GameObject vector;
	private GameObject myObject;
	private GameObject instanced;
	private Quaternion spawnRotation;
	private ShipBehaviour sb;
	private float timer;

	public int speed;
	public int movement;
	public int special;
	public int ship;
	// Use this for initialization
	void Start () {
		foreach (Transform t in positions) 
		{
			if (ship == 1) {
				instanced = grunt;
				spawnRotation = Quaternion.Euler (0, 0, 330);
			} else if (ship == 2) 
			{
				instanced = vector;
				spawnRotation = Quaternion.Euler (0, 0, 330);
			}
			myObject = Instantiate(instanced, 
				t.position, spawnRotation) as GameObject;
			sb = myObject.GetComponent<ShipBehaviour> ();
			sb.speed = speed;
			sb.movement = movement;
			sb.special = special;
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= 1) 
		{
			Destroy (this.gameObject);
		}
	}
}
