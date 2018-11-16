using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class FormationScript : MonoBehaviour {

	public Transform[] positions;
	public GameObject grunt;
	public GameObject vector;
	private GameObject myObject;
	private Quaternion rotation;
	public Camera camera;
	public Canvas canvas;
	public RectTransform selected_panel;
	public RectTransform stats_fleet;
	private RectTransform canvas_transform;
	public bool selected;

	private Renderer first_mesh;
	private Renderer last_mesh;
	private StatsFleet statsScript;
	private float xPosition;
	private float yPosition;

	public int speed;
	public int movement;
	public int special;
	public string special_name = "Shot Power";
	public int ship;

	private Rigidbody rb;
	private float timer;
	private Ray ray;
	private RaycastHit hit;

	private Vector3 posStart, posEnd;
	// Use this for initialization
	void Start () {
		rotation = Quaternion.Euler (0, 180, 30);

		timer = 0.0f;
		ship = 1;
		rb = gameObject.GetComponent<Rigidbody> ();

		foreach (Transform t in positions) 
		{
			myObject = Instantiate(grunt, 
				t.position, rotation) as GameObject;
			myObject.transform.parent = this.transform;
		}

		selected = false;
	}

	void OnMouseDrag()
	{
		if (!EventSystem.current.IsPointerOverGameObject () && timer >= 0.2f) {
			if (this.gameObject.name.ToString () == "Block_Formation(Clone)") {
				this.transform.position = new Vector3 (0, Mathf.Clamp (Camera.main.ScreenToWorldPoint (Input.mousePosition).y, -5, 5),
					Camera.main.ScreenToWorldPoint (Input.mousePosition).z);
			} else if (this.gameObject.name.ToString () == "Triangle_Formation(Clone)") {
				this.transform.position = new Vector3 (0, Mathf.Clamp (Camera.main.ScreenToWorldPoint (Input.mousePosition).y, -4.5f, 4.5f),
					Camera.main.ScreenToWorldPoint (Input.mousePosition).z);
			} else {
				this.transform.position = new Vector3 (0, Mathf.Clamp (Camera.main.ScreenToWorldPoint (Input.mousePosition).y, -1, 1),
					Camera.main.ScreenToWorldPoint (Input.mousePosition).z);
			}
			selected_panel.gameObject.SetActive (false);
			stats_fleet.gameObject.SetActive (false);
		}
	}

	void OnMouseDown() 
	{
		timer = 0.0f;
		stats_fleet.gameObject.SetActive (false);
	}

		
	void OnMouseUp()
	{
		if (timer <= 0.2f) 
		{
			first_mesh = this.transform.GetChild(6).GetComponent<Renderer> ();
			last_mesh = this.transform.GetChild(11).GetComponent<Renderer> ();
			posStart = camera.WorldToScreenPoint(new Vector3(first_mesh.bounds.min.x, first_mesh.bounds.min.y, first_mesh.bounds.min.z));
			posEnd = camera.WorldToScreenPoint(new Vector3(last_mesh.bounds.max.x, last_mesh.bounds.max.y, last_mesh.bounds.max.z));
			statsScript = stats_fleet.gameObject.GetComponent<StatsFleet> ();
			statsScript.fleet = this;
			int widthX = (int)(posEnd.x - posStart.x);
			int widthY = (int)(posEnd.y - posStart.y);

			int centerX = (int)(posStart.x + 0.5f * widthX);
			int centerY = (int)(posStart.y + 0.5f * widthY);

			selected_panel.position = new Vector2(centerX, centerY);
			selected_panel.gameObject.SetActive (true);
			selected_panel.sizeDelta = new Vector2( 1.2f * widthX, 1.2f * widthY);

			canvas_transform = canvas.GetComponent<RectTransform> ();

			stats_fleet.sizeDelta = new Vector2 (0.2f * canvas_transform.rect.width, 0.5f * canvas_transform.rect.height);
			stats_fleet.gameObject.SetActive (true);

			if (this.transform.position.y >= 3) {
				yPosition = centerY - stats_fleet.rect.height * 0.5f;
			} else if (this.transform.position.y <= -3) {
				yPosition = centerY + stats_fleet.rect.height * 0.5f;
			} else {
			yPosition = centerY;
			}

			if (this.transform.position.z >= 13) {
				xPosition = centerX - widthX - stats_fleet.rect.width * 0.5f;
			} else {
				xPosition = centerX + widthX + stats_fleet.rect.width * 0.5f;
			}
			stats_fleet.position = new Vector2 (xPosition, yPosition);
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (rb.velocity.magnitude != 0) {
			rb.velocity = new Vector3 (0, rb.velocity.y * 0.5f, rb.velocity.y * 0.5f);
			if (this.transform.position.y >= 7 || this.transform.position.y <= -7) {
				Destroy (this.gameObject);
			}
		}
	}

}
