using UnityEngine;
using System.Collections;

public class BossManeuver : MonoBehaviour
{
	public float zLine;
	private Rigidbody rb;

	void Start () 
	{
		rb = GetComponent <Rigidbody> ();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		if (rb.position.z < zLine)
		{
			rb.position = new Vector3 (rb.position.x, rb.position.y, zLine);
		}
	}
}