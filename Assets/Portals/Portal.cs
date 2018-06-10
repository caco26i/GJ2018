using UnityEngine;

public class Portal : MonoBehaviour
{

	public Vector3 offset, rotationVelocity;
	public float recycleOffset, spawnChance;

	public enum Colors { Red, Green, Blue };
	public Material[] materials;
	public Colors color;
	public Material material;

	public enum Shapes { Circle, Square, Triangle };
	public Shapes shape;

	public Renderer renderer;


	void Start()
	{
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(false);
		RandomColor();
	}

	void Update()
	{
		if (transform.localPosition.x + recycleOffset < Runner.distanceTraveled)
		{
			gameObject.SetActive(false);
			return;
		}
		transform.Rotate(rotationVelocity * Time.deltaTime);

		UpdateMaterial();
	}

	public void UpdateMaterial()
	{
		Material material = ColorManager.GetMaterial((int)color);
		Color colortemp = ColorManager.GetColor((int)color);

		foreach (Transform child in transform)
		{
			//child.GetComponent<Renderer>().material = material;
			child.GetComponent<Renderer>().material.SetColor("_Color", colortemp);
			foreach (Transform child2 in child.GetComponent<Transform>())
				child2.GetComponent<Renderer>().material.SetColor("_Color", colortemp);
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		//int shape = (int)Cat.shape+0;
		//int color = (int)Cat.color+0;
		bool fallo = false;
		if (GUIManager.getRuleNumber() == 0)
		{
			if ((int)Cat.color != (int)this.color)
			{
				GameObject.FindWithTag("Player").BroadcastMessage("AnimateSad");
				Cat.LifeLess();
				fallo = true;
			}
		}
		else if (GUIManager.getRuleNumber() == 1)
		{
			if ((int)Cat.shape != (int)this.shape)
			{
				GameObject.FindWithTag("Player").BroadcastMessage("AnimateSad");
				Cat.LifeLess();
				fallo = true;

			}
		}
		else if (GUIManager.getRuleNumber() == 2)
		{
			if ((int)Cat.color == (int)this.color)
			{

				GameObject.FindWithTag("Player").BroadcastMessage("AnimateSad");
				Cat.LifeLess();
				fallo = true;

			}
		}
		else if (GUIManager.getRuleNumber() == 3)
		{
			if ((int)Cat.shape == (int)this.shape)
			{
				GameObject.FindWithTag("Player").BroadcastMessage("AnimateSad");
				Cat.LifeLess();
				fallo = true;
			}
		}

		if (fallo)
		{
			GameObject.FindWithTag("SadSong").GetComponent<AudioSource>().Play();
		}
		else {
			GameObject.FindWithTag("CatSong").GetComponent<AudioSource>().Play();
			Runner.bonuses += 100;
		}

		fallo = false;

		//Cat.shape = (Cat.Shapes) shape;
		//Cat.color = (Cat.Colors) color;
		//print(Cat.shape);
		//print(this.shape);

		//print((int)Cat.color);
		//print(this.color);
	}

	public void SpawnIfAvailable(Vector3 position)
	{
		position.z = offset.z;
		if (gameObject.activeSelf || spawnChance <= Random.Range(0f, 100f))
		{
			return;
		}
		transform.localPosition = position + offset + Vector3.right * recycleOffset;
		gameObject.SetActive(true);
		RandomColor();
	}

	private void GameOver()
	{
		gameObject.SetActive(false);
	}

	public void RandomColor()
	{
		color = (Colors)(int)Random.Range(0, 3);
	}
}