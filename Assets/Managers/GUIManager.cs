using UnityEngine;

public class GUIManager : MonoBehaviour {

	private static GUIManager instance;
	public GameObject gameOver;

	public GUIText ruleText, boostsText, distanceText, gameOverText, instructionsText;
	public string[] rules = new string[] { "Shape", "F*ck da Shape" };
	public int ruleNumber;


	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameOverText.enabled = false;
	}

	void Update () {
		if(Input.GetButtonDown("Jump")){
			GameEventManager.TriggerGameStart();
		}
	}
	
	private void GameStart () {
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		enabled = false;
		GetComponent<AudioSource>().Play();
		gameOver.SetActive(false);
	}

	private void GameOver () {
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		enabled = true;
		GetComponent<AudioSource>().Stop();
		gameOver.SetActive(true);
	}

	public static void SetBoosts(int boosts){
		instance.boostsText.text = boosts.ToString();
	}

	public static void SetDistance(float distance){
		instance.distanceText.text = distance.ToString("f0");
	}

	public static void SetRandomRule()
	{
		instance.ruleNumber = (int)Random.Range(0, instance.rules.Length);
		instance.ruleText.text = instance.rules[instance.ruleNumber];
	}

	public static int getRuleNumber()
	{
		return instance.ruleNumber;
	}

	public static string GetScore() {
		return instance.distanceText.text;

	}
}