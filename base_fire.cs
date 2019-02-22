using UnityEngine;
using System.Collections;

public abstract class base_fire : MonoBehaviour
{
	public bool lockFire = false;
	public string name;
	public string hand;

	protected int shots = 10;
	protected float fireRate = 1f;
	// private bool isRender = true;

	protected Animator anim;
	protected GameObject playerHand;
	protected GameObject player;
	// private Renderer thisRender;
	// private Rigidbody2D rb;
	private CircleCollider2D thisCollider;

	private Color32[] shotColors = { new Color32(255, 247, 23, 255), new Color32(255, 255, 255, 255), new Color32(255, 222, 36, 255), new Color32(255, 253, 125, 255), new Color32(255, 229, 31, 255) };
	private Sprite[] shotModels;

	// Use this for initialization
	public virtual void Start()
	{
		shotModels = Resources.LoadAll<Sprite>("bullets");

		thisCollider = GetComponent<CircleCollider2D>();
		Physics2D.IgnoreCollision(thisCollider, player.GetComponent<CircleCollider2D>());
		Physics2D.IgnoreCollision(thisCollider, thisCollider);
	}

	// Update is called once per frame
	void Update()
	{
		if (lockFire)
			return;

		// Used only by player, Ai calls directecly method below.
		if (transform.parent != null)
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				if (shots > 0)
				{
					InvokeRepeating("ShootGun", 0.00f, fireRate);
					anim.SetBool("shooting", true);
				}
			}
			else if (Input.GetKeyUp(KeyCode.Mouse0))
			{
				CancelInvoke("ShootGun");
				anim.SetBool("shooting", false);
			}
		}
		else
		{
			CancelInvoke("ShootGun");
			anim.SetBool("shooting", false);
		}
	}

	public void ShootGun()
	{
		shots--;
		if (shots > 0)
		{
			GameObject bullInst = (GameObject)Instantiate(Resources.Load("9mmbullet"), playerHand.transform.position, player.transform.rotation);
			bullInst.GetComponent<SpriteRenderer>().sprite = shotModels[Random.Range(0, 4)];
			bullInst.GetComponent<SpriteRenderer>().color = shotColors[Random.Range(0, 4)];
		}
	}

	public float getFireRate()
	{
		return this.fireRate;
	}
}
