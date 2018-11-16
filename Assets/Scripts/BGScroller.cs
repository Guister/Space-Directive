using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour {
	
	public float scrollSpeed;					//speed used to move the background
	public float tileSizedZ;					//length of the tile along the z axis
	
	private Vector3 startPosition;
	
	// Use this for initialization
	void Start () 
	{
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizedZ);
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}
