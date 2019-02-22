using UnityEngine;
using System.Collections;

public class mp5_fire : base_fire
{
	public override void Start()
	{
		player = GameObject.Find(name);
		anim = GameObject.Find(name).GetComponent<Animator>();
		playerHand = GameObject.Find(hand);
		shots = 45;
		fireRate = 0.05f;
		base.Start();
	}
}
