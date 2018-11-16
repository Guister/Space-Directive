using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class drag : MonoBehaviour {

	public float scrollSpeed;
	public float pageSpeed;
	public float dragSpeed;
	private Vector3 dragOrigin;
	private float direction;
	private Ray ray;
	private RaycastHit hit;
	public Canvas canvas;
	private UIcontroller script;
	private bool dragbool;

	void Start() 
	{
		dragbool = false;
		script = canvas.GetComponent<UIcontroller> ();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject () && !Physics.Raycast(ray, out hit) && script.toggled_object == 0)
		{
			dragOrigin = Input.mousePosition;
			dragbool = true;
			return;
		}

		if (Input.GetMouseButtonUp (0)) 
		{
			dragbool = false;
		}

		if (!Input.GetMouseButton(0)) return;

		if (dragbool) {
			Vector3 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition - dragOrigin);
			Vector3 move = new Vector3 (0, 0, pos.x * dragSpeed);

			transform.Translate (move, Space.World); 
			transform.position = new Vector3( 10, 0, Mathf.Clamp(transform.position.z, 0, 600) );

		}
	}
}
