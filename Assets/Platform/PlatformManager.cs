using UnityEngine;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour {

    public Transform prefab;
    public Transform[] prefabs;
    public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;
	public Vector3 size, minGap, maxGap;
    public float minY, maxY;
    public Material[] materials;
    public PhysicMaterial[] physicMaterials;
    public string[] botones;

	public Booster booster;
    public int materialIndex;

    public Portal portalSquare;
	public Portal portalCircle;
	public Portal portalTriangle;

	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;
	private Queue<Transform> objectQueue2;
	private Queue<Transform> objectQueue3;

	public static float depth;

    void Start () {
		depth = 5f;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		objectQueue = new Queue<Transform>(numberOfObjects);
		for (int i = 0; i < numberOfObjects; i++)
		{
			objectQueue.Enqueue((Transform)Instantiate(
                prefabs[Random.Range(0, 3)], new Vector3(0f, 1000f, 0f), Quaternion.identity));
		}

		enabled = false;
	}

	void Update () {
		if (objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled)
		{
			Recycle();
		}
	}

	private void Recycle () {
		Vector3 scale = new Vector3(size.x, depth, depth);

        Vector3 position = nextPosition;
        Vector3 positionBooster = nextPosition;
        positionBooster.z = scale.z;
		booster.SpawnIfAvailable(positionBooster);

		//portalCircle.SpawnIfAvailable(position);
		//portalTriangle.SpawnIfAvailable(position);

		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		o.GetComponent<Renderer>().material = materials[materialIndex];
        o.GetComponent<Collider>().material = physicMaterials[materialIndex];
        materialIndex = Random.Range(0, materials.Length);

        objectQueue.Enqueue(o);

		nextPosition += new Vector3(scale.x + Random.Range(minGap.x, maxGap.x), Random.Range(minGap.y, maxGap.y), 0);
	}
	
	private void GameStart () {
		nextPosition = startPosition;
		for(int i = 0; i < numberOfObjects; i++){
			Recycle();
		}
		enabled = true;
	}

	private void GameOver () {
		enabled = false;
	}
}