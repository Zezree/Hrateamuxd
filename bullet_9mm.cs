using UnityEngine;

public class bullet_9mm : MonoBehaviour
{
	private float bulletSpeed = 37f;
	private float bulletLife = 5f;
	public LayerMask objects;


	// Use this for initialization
	void Start()
	{
		GameObject tempReference = GameObject.Find("rightSub");
		transform.Rotate(0f, 0f, Random.Range(-0f, 0f));
		Vector3 bulletVelocity = transform.up * bulletSpeed;
		RaycastHit2D hit = Physics2D.Raycast(tempReference.transform.position, transform.up, 40f, objects);
		// float bulletOverflow = 5f;



		if (hit.collider != null)
		{
			float getDist = Vector2.Distance(transform.position, hit.point);
			bulletLife = Mathf.Abs(getDist / bulletVelocity.magnitude);
			// bulletOverflow = bulletLife + 0.1f / bulletVelocity.magnitude;
		}

		GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		Debug.Log(coll.collider.name);
		Destroy(gameObject);
	}
}
