using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Login_Controller : MonoBehaviour {
	
	public InputField formname; // username
	public InputField formPassword; // password
	public GameObject login_panel;
	public Text username_txt;
	public Text points_txt;
	public Text error_text;
	private string formText = ""; //this field is where the messages sent by PHP script will be in

	private string formNick = "";
	private string formPass = "";
	private int points = 0;

	private string URL_login = "http://www.spacedirective.net16.net/login.php"; //Server URL for logins
	private string URL_register = "http://www.spacedirective.net16.net/register.php"; //Server URL for resgistering

	IEnumerator Login (string url){

		// Create a form object for sending login data to the server
		WWWForm form = new WWWForm ();

		formNick = formname.text;
		formPass = formPassword.text;

		form.AddField ("username", formNick);
		form.AddField ("password", formPass);

		//create download object
		WWW download = new WWW (url, form);
		yield return download;

		if (download.text == "failed") 
		{
			error_text.gameObject.SetActive(true);

			if (url == "http://www.spacedirective.net16.net/login.php") {
				error_text.text = "Wrong Username or Password";
			} else 
			{
				error_text.text = "Username already being used";
			}
		} 
		else 
		{
			points = int.Parse(download.text);
			Static_Gamemaster.score = points;
			Static_Gamemaster.username = formNick;
			login_panel.SetActive (false);
			username_txt.gameObject.SetActive (true);
			points_txt.gameObject.SetActive (true);
			username_txt.text = Static_Gamemaster.username;
			points_txt.text =  "Points: " + Static_Gamemaster.score.ToString ();
		}
		formNick = ""; //just clean our variables
		formPass = "";

	}
	public void Wrapper_login(){
		StartCoroutine(Login(URL_login));
	}

	public void Wrapper_register(){
		StartCoroutine(Login(URL_register));
	}

	void Start() 
	{
		if (Static_Gamemaster.username != "") 
		{
			login_panel.SetActive (false);
			username_txt.gameObject.SetActive (true);
			points_txt.gameObject.SetActive (true);
			username_txt.text = Static_Gamemaster.username;
			points_txt.text =  "Points: " + Static_Gamemaster.score.ToString ();

		}
	}
}
