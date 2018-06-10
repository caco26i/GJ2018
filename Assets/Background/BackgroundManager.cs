using UnityEngine;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour
{

	public Transform prefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;
	public Vector3 minSize, maxSize;
	public float offsetXNext;

	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;

	SpriteRenderer sp;
	RectTransform rt;
	void Start()
	{
		sp = prefab.GetComponent<SpriteRenderer>();
		rt = prefab.GetComponent<RectTransform>();

		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		objectQueue = new Queue<Transform>(numberOfObjects);
		for (int i = 0; i < numberOfObjects; i++)
		{
			objectQueue.Enqueue((Transform)Instantiate(
				prefab, new Vector3(0f, 0f, 0f), Quaternion.identity));
		}
		enabled = false;
	}

	void Update()
	{
		if (objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled)
		{
			Recycle();
		}
	}

	private void Recycle()
	{
		Vector3 scale = new Vector3(
			Random.Range(minSize.x, maxSize.x),
			Random.Range(minSize.y, maxSize.y),
			Random.Range(minSize.z, maxSize.z));

		Vector3 position = nextPosition;
		position.x += scale.x;
		position.y += scale.y;

		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;

		nextPosition.x += offsetXNext;
		objectQueue.Enqueue(o);
	}

	private void GameStart()
	{
		nextPosition = startPosition;
		for (int i = 0; i < numberOfObjects; i++)
		{
			Recycle();
		}
		enabled = true;
	}

	private void GameOver()
	{
		enabled = false;
	}
}