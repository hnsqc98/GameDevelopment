using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAnimation : MonoBehaviour {

	public float speed=1f;
	public float strength=6f;

	private float randomOffset;

	// Use this for initialization
	void Start () {
		randomOffset = Random.Range(0f, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.z = Mathf.Sin(Time.time * speed + randomOffset) * strength-16f;
		transform.position = pos;
	}
}
