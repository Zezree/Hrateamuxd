using UnityEngine;

public class SmoothFollow : MonoBehaviour
{

	private float dampX = 0.2f;
	private float dampY = 0.2f;
	public float velocityX = 0f;
	public float velocityY = 0f;
	public Transform target;

	private GameObject cursor;

	// Use this for initialization
	void Start()
	{
		cursor = GameObject.Find("Cursor");
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		Vector2 mouseDir = (cursor.transform.position - target.position).normalized * 0.8f;
		float offset = 0f;
		if (Input.GetKey(KeyCode.LeftShift))
		{
			offset = 7f;
		}

		Vector2 targetOffset = target.position + target.up * 0.5f * offset;
		float posX = Mathf.SmoothDamp(transform.position.x, targetOffset.x + mouseDir.x, ref velocityX, dampX);
		float posY = Mathf.SmoothDamp(transform.position.y, targetOffset.y + mouseDir.y, ref velocityY, dampY);
		transform.position = new Vector3(posX, posY, transform.position.z);
	}
}
