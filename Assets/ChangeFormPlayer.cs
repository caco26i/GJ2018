using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFormPlayer : MonoBehaviour {

    public int shape;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        GameObject.FindWithTag("Player").BroadcastMessage("SetShape", (Cat.Shapes)shape);
    }
}
