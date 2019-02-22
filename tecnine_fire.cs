using UnityEngine;

public class tecnine_fire : base_fire
{
	public override void Start()
	{
		if (name == null)
			name = "Enemy";

		if (hand == null)
			hand = "enemySub";

		player = GameObject.Find(name);
		anim = GameObject.Find(name).GetComponent<Animator>();
		playerHand = GameObject.Find(hand);
		shots = 30;
		fireRate = 0.15f;
		base.Start();
	}
}
