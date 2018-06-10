using UnityEngine;

public class Runner : MonoBehaviour
{

	public static float distanceTraveled;
	public static float score;
	private static int boosts;
	public static int bonuses;

	public float acceleration;

	public float maxSpeed;

	public Vector3 boostVelocity, jumpVelocity;
	public float gameOverY;
    private int speedPerSec;
    public float speed;
    private Vector3 oldPosition;
    private bool touchingPlatform;
	private Vector3 startPosition;

	void Start()
	{
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		GetComponent<Renderer>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
        touchingPlatform = true;
        enabled = false;
	}

	void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			if (touchingPlatform)
			{
				GetComponent<Rigidbody>().AddForce(jumpVelocity, ForceMode.VelocityChange);
				touchingPlatform = false;
			}
			else if (boosts > 0)
			{
				GetComponent<Rigidbody>().AddForce(boostVelocity, ForceMode.VelocityChange);
				boosts -= 1;
				GUIManager.SetBoosts(boosts);
			}
		}
		distanceTraveled = transform.localPosition.x;
		score = distanceTraveled + bonuses;
		GUIManager.SetDistance(score);

		if (transform.localPosition.y < gameOverY)
		{
			GameEventManager.TriggerGameOver();
		}

        bool test = Input.GetKeyDown(KeyCode.JoystickButton0);
        if (test)
        {
            Debug.Log("Get Key Working");
        }
    }

	void FixedUpdate()
	{
		/*if (c <= maxSpeed)
		{*/
			GetComponent<Rigidbody>().AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);

        if (GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
        }
        /*}*/

    }


	private void GameStart()
	{
		boosts = 0;
		GUIManager.SetBoosts(boosts);
		distanceTraveled = 0f;
		score = 0f;
		GUIManager.SetDistance(distanceTraveled);
		transform.localPosition = startPosition;
		GetComponent<Renderer>().enabled = true;
		GetComponent<Rigidbody>().isKinematic = false;
		enabled = true;
		//GetComponent<Rigidbody>().AddForce(acceleration*2f, 0f, 0f, ForceMode.Impulse);
		//GetComponent<Rigidbody>().velocity.magnitude
	}

	private void GameOver()
	{
		//GetComponent<Renderer>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		LeaderBoardSample.gs = LeaderBoardSample.gameState.enterscore;
		//enabled = false;
	}

	public static void AddBoost()
	{
		boosts += 1;
		GUIManager.SetBoosts(boosts);
	}

    void OnCollisionEnter()
    {
        touchingPlatform = true;
    }
}
