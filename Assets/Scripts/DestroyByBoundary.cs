using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour 
{
	//When other game objects leave the boundary, destroy them
	void OnTriggerExit(Collider other)
	{
		Destroy (other.gameObject);
	}
}