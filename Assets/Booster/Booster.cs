using UnityEngine;

public class Booster : MonoBehaviour {

    public Cat Player;
    public Vector3 offset, rotationVelocity;
    public float recycleOffset, spawnChance;
	public static bool respawn;

	void Start () {
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(false);
	}
	
	void Update () {
		if(transform.localPosition.x + recycleOffset < Runner.distanceTraveled){
			gameObject.SetActive(false);
			return;
		}
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}
	
	void OnTriggerEnter (Collider other) {
		Runner.AddBoost();
        //Player.SetShape(next.shape);
        GameObject.FindWithTag("Player").BroadcastMessage("RandomShape");

        GUIManager.SetRandomRule();
	}

	public void SpawnIfAvailable (Vector3 position) {
		if (gameObject.activeSelf || false)
		{
			return;
		}
		else if (true)
		{
			transform.localPosition = position + offset;
			gameObject.SetActive(true);
			respawn = false;
		}

		transform.localPosition = position + offset;
		gameObject.SetActive(true);
	}

    private void GameOver () {
		gameObject.SetActive(false);
	}
}