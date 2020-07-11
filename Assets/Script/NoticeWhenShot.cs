using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Seek))]
public class NoticeWhenShot : MonoBehaviour
{
	GameObject player;
	Seek seekAgent;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		seekAgent = GetComponent<Seek>();
	}

	public void Detection() {
		seekAgent.SetTarget(player.transform.position);
	}
}
