using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour 
{
	//This script is used to destroy the explosion and effects objects a while after they are instantiated (since they never leave the boundary)
	public float lifetime;

	void Start ()
	{
		Destroy (gameObject, lifetime);
	}
}