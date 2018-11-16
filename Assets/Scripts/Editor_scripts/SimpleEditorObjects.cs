using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SimpleEditorObjects : MonoBehaviour {

	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
	}

	void OnMouseDrag()
	{
		if (!EventSystem.current.IsPointerOverGameObject ()) {
			gameObject.transform.position = new Vector3 (0, Camera.main.ScreenToWorldPoint (Input.mousePosition).y,
				Camera.main.ScreenToWorldPoint (Input.mousePosition).z);
		}
	}
	// Update is called once per frame
	void Update (){
		if (rb.velocity.magnitude != 0) {
			rb.velocity = new Vector3 (0, rb.velocity.y * 0.5f, rb.velocity.y * 0.5f);
		}
		if (this.transform.position.y >= 7 || this.transform.position.y <= -7) {
			Destroy (this.gameObject);
		}
	}
}
