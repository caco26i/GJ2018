using UnityEngine;
using System.Collections;



public class CompleteCameraController : MonoBehaviour
{

	// The target we are following
	public Transform target;
	// The distance in the x-z plane to the target
	public float distance = 10.0f;
	// the Depth we want the camera to be above the target
	public float Depth = 5.0f;
	// How much we 
	public float DepthDamping = 2.0f;

	private Vector3 offset;         //Private variable to store the offset distance between the player and camera


	// Place the script in the Camera-Control group in the component menu
	[AddComponentMenu("Camera-Control/Smooth Follow")]

	// Use this for initialization
	void Start()
	{
		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		offset = transform.position - target.transform.position;
	}

	void LateUpdate()
	{
		// Early out if we don't have a target
		if (!target) return;

		Vector3 newPos = target.transform.position + offset;
		float wantedDepth = newPos.z*0.8f - 10;
		float currentDepth = transform.position.z;

		// Damp the Depth
		currentDepth = Mathf.Lerp(currentDepth, wantedDepth, DepthDamping * Time.deltaTime);

		// Set the Depth of the camera
		transform.position = new Vector3(newPos.x, newPos.y, currentDepth);

	}
}