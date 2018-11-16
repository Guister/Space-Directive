using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EditorObjects : MonoBehaviour {

	public int speed;
	public int movement;
	public int special;
	public string special_name = "Shot Power";

	public Camera camera;
	public Canvas canvas;
	public RectTransform selected_panel;
	private Vector3 posStart;
	private Vector3 posEnd;
	private Collider object_colider;
	private RectTransform canvas_transform;
	private RectTransform rt;
	private int widthX;
	private int widthY;
	private float xposition;
	private float yposition;
	private StatsPannelScript statsScript;
	private Rigidbody rb;

	public RectTransform stats_pannel;
	private float timer;

	void Start() 
	{
		timer = 0.0f;
		speed = 1;
		movement = 1;
		special = 1;
		object_colider = this.GetComponent<Collider> ();
		rb = gameObject.GetComponent<Rigidbody> ();
	}
	
	void Update () {
		timer += Time.deltaTime;
		if (rb.velocity.magnitude != 0) {
			rb.velocity = new Vector3 (0, rb.velocity.y * 0.5f, rb.velocity.y * 0.5f);
			if (this.transform.position.y >= 7 || this.transform.position.y <= -7) {
				Destroy (this.gameObject);
			}
		}
	}
	// drag objects within a certain boundary
	void OnMouseDrag()
	{
		if (timer >= 0.2f && !EventSystem.current.IsPointerOverGameObject ()) 
		{
			gameObject.transform.position = new Vector3 (0, Mathf.Clamp (Camera.main.ScreenToWorldPoint (Input.mousePosition).y, -7, 7),
				Camera.main.ScreenToWorldPoint (Input.mousePosition).z);
			selected_panel.gameObject.SetActive (false);
			stats_pannel.gameObject.SetActive (false);
		}
	}

	void OnMouseDown() 
	{
		timer = 0.0f;
		stats_pannel.gameObject.SetActive (false);
	}

	void OnMouseUp()
	{
		if (timer <= 0.2f) 
		{
			posStart = camera.WorldToScreenPoint(new Vector3(object_colider.bounds.min.x, object_colider.bounds.min.y, object_colider.bounds.min.z));
			posEnd = camera.WorldToScreenPoint(new Vector3(object_colider.bounds.max.x, object_colider.bounds.max.y, object_colider.bounds.max.z));
			statsScript = stats_pannel.gameObject.GetComponent<StatsPannelScript> ();
			statsScript.ship = this;
			int widthX = (int)(posEnd.x - posStart.x);
			int widthY = (int)(posEnd.y - posStart.y);

			int centerX = (int)(posStart.x + 0.5f * widthX);
			int centerY = (int)(posStart.y + 0.5f * widthY);

			selected_panel.position = new Vector2(centerX, centerY);
			selected_panel.gameObject.SetActive (true);
			selected_panel.sizeDelta = new Vector2( 1.2f * widthX, 1.2f * widthY);

			canvas_transform = canvas.GetComponent<RectTransform> ();

			stats_pannel.sizeDelta = new Vector2 (0.2f * canvas_transform.rect.width, 0.5f * canvas_transform.rect.height);
			stats_pannel.gameObject.SetActive (true);

			if (this.transform.position.y >= 3) {
				yposition = centerY - stats_pannel.rect.height * 0.5f;
			} else if (this.transform.position.y <= -3) {
				yposition = centerY + stats_pannel.rect.height * 0.5f;
			} else {
				yposition = centerY;
			}

			if (this.transform.position.z >= 16) {
				xposition = centerX - widthX - stats_pannel.rect.width * 0.5f;
			} else {
				xposition = centerX + widthX + stats_pannel.rect.width * 0.5f;
			}
			stats_pannel.position = new Vector2 (xposition, yposition);
		}
	}
}
