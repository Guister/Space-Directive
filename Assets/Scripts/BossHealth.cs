using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

	public float maxHealth;
	public float currentHealth;
	public float damage;
	public GameObject lifeBar;


	// Use this for initialization
	void Start () 
	{
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy"))
		{
			return;
		}
		currentHealth -= damage;
		float calcHealth = currentHealth / maxHealth;
		SetHealthBar (calcHealth);
	}

	public void SetHealthBar(float myHealth)
	{
		lifeBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth,0f ,1f), lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);	
	}
}
