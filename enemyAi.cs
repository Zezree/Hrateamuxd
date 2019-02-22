using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAi : MonoBehaviour
{
	public bool patrol = true, guard = false, clockwise = false;
	public bool moving = true;
	public bool pursuingPlayer = false;
	public int neededHitsToDie = 3;

	public bool mp5;
	public bool tecnine;

	private GameObject player;
	private Vector3 target;
	private Rigidbody2D rid;
	public Vector3 playerLastPos;
	private RaycastHit2D hit;
	private float speed = 3.5f;
	private int layerMask = 1 << 8;
	public Animator anim;
	public Animator animLegs;

	public base_fire weampon;

	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		animLegs = GameObject.Find("EnemyLegs").GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		playerLastPos = this.transform.position;
		rid = this.GetComponent<Rigidbody2D>();
		layerMask = ~layerMask;

		if(mp5)
		{
			weampon = gameObject.AddComponent(typeof(mp5_fire)) as mp5_fire;
			anim.SetBool("MP5", true);
		}
		else if(tecnine)
		{
			weampon = gameObject.AddComponent(typeof(tecnine_fire)) as tecnine_fire;
			anim.SetBool("tec9", true);
		}
		//if (mp5)
		//{
		//	createGun("MP5", new mp5_fire());
		//}
		//else if (tecnine)
		//{
		//	createGun("tec9", new tecnine_fire());
		//}
	}

	// Update is called once per frame
	void Update()
	{
		// InvokeRepeating("shoot", 0.00f, 0.001f);
		movement();
		playerDetect();
	}

	void shoot()
	{
		weampon.ShootGun();
	}

	void movement()
	{
		float dist = Vector3.Distance(player.transform.position, this.transform.position);
		Vector3 dir = player.transform.position - this.transform.position;
		hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(dir.x, dir.y), dist, layerMask);

		Vector3 fwt = this.transform.TransformDirection(Vector3.up);
		RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(fwt.x, fwt.y), 1.2f, layerMask);

		if (moving)
		{
			anim.SetFloat("move", 1f);
			animLegs.SetFloat("move", 1f);
			transform.Translate(Vector3.up * speed * Time.deltaTime);
		}
		else
		{
			anim.SetFloat("move", 0f);
			animLegs.SetFloat("move", 0f);
		}

		if (patrol)
		{
			speed = 3.5f;

			if (hit2.collider != null)
			{
				if (hit2.collider.gameObject.tag == "obstacle")
				{
					Quaternion rot = this.transform.rotation;

					if (!clockwise)
					{
						transform.Rotate(0, 0, 90);
					}
					else
					{
						transform.Rotate(0, 0, -90);
					}
				}
			}
		}

		if (pursuingPlayer)
		{
			speed = 6.0f;
			rid.transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan2((playerLastPos.x - transform.position.x), (playerLastPos.y - transform.position.y)) * Mathf.Rad2Deg);
			shoot();

			if (hit.collider.gameObject.tag == "Player")
			{
				playerLastPos = player.transform.position;
			}
		}
	}

	void playerDetect()
	{
		Vector3 pos = this.transform.InverseTransformPoint(player.transform.position);

		if (hit.collider != null)
		{
			if (hit.collider.gameObject.tag == "Player" && pos.y > 1.2f && Vector3.Distance(this.transform.position, player.transform.position) < 9)
			{
				patrol = false;
				pursuingPlayer = true;
			}
			else
			{
				patrol = true;
				pursuingPlayer = false;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "bullet")
		{
			this.neededHitsToDie--;

			if(this.neededHitsToDie == 0)
			{
				Destroy(this.gameObject);
			}
		}
	}
}
