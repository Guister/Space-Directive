using UnityEngine;
using System.Collections;

public class Camera_rotator : MonoBehaviour {

	public float rotate_speed;
	private float rotateX;
	private float rotateY;
	private float rotateZ;
	private Vector3 rotation;


	// Use this for initialization
	void Start () {
		rotateX = Random.Range (1, 10);
		rotateY = Random.Range (1, 10);
		rotateZ = Random.Range (1, 10);
		rotation = new Vector3 (rotateX, rotateY, rotateZ);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (rotation * rotate_speed * Time.deltaTime);
	}
}
