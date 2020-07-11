using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Seek))]
public class FollowPlayerWhileInRange : MonoBehaviour
{
	GameObject player;
	Seek seekAgent;

	[SerializeField] float awarenessRange;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		seekAgent = GetComponent<Seek>();
	}

	void Update()
	{
		if ((player.transform.position - transform.position).sqrMagnitude < awarenessRange * awarenessRange)
			seekAgent.SetTarget(player.transform.position);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
	}
}
