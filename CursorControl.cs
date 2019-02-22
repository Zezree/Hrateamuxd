using UnityEngine;

public class CursorControl : MonoBehaviour
{
	private GameObject mouseCursor;
	private GameObject cam;
	private GameObject player;

	// private float yMin = -5.0f;
	// private float yMax = 5.5f;
	// private float xMin = -5.0f;
	// private float xMax = 5.5f;

	private float mouseSensitivity = 0.5f;

	private float bonusX;
	private float bonusY;

	// Use this for initialization
	void Awake()
	{
		mouseCursor = GameObject.Find("Cursor");
		player = GameObject.Find("Player");
		cam = GameObject.Find("Camera");
	}

	// Update is called once per frame
	void Update()
	{
		var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
		var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
		// var moveX = Input.GetAxis("Horizontal");
		// var moveY = Input.GetAxis("Vertical");

		Vector2 offset = new Vector2(0f, 0f);
		Vector2 offset2 = new Vector2(0f, 0f);

		if (Input.GetKey(KeyCode.LeftShift))
		{
			Vector3 tempVec = transform.up.normalized;
			offset = new Vector2(tempVec.x, tempVec.y) * 3f;
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			Vector3 tempVec = transform.up.normalized;
			offset2 = new Vector2(tempVec.x, tempVec.y) * 3f;

		}
		else if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			if (Vector2.Distance(player.transform.position, mouseCursor.transform.position) > 4f)
			{
				Vector3 tempVec = transform.up.normalized;
				offset2 = new Vector2(tempVec.x, tempVec.y) * 3f * -1f;
			}
		}

		// Vector3 moveMouse = new Vector3(mouseX, mouseY, 0f).normalized;

		Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var camPoint = cam.transform.position;

		var xDist = camPoint.x - transform.position.x;
		var yDist = camPoint.y - transform.position.y;

		mPosition.x += transform.position.x - camPoint.x;
		mPosition.y += transform.position.y - camPoint.y;

		// float clampMin = 0f;
		// float clampMax = 0f;

		// if (moveY != 0)
		// {
			// clampMin = Mathf.Clamp(moveY * 5, -1.4f, -1f);
			// clampMax = Mathf.Clamp(moveY * 5, 1f, 1.4f);
		// }

		float xMin = Mathf.Clamp(-12f + xDist + offset.x, -50f, 0f);
		float xMax = Mathf.Clamp(12f + xDist + offset.x, 0f, 50f);
		float yMin = Mathf.Clamp(-6f + yDist + offset.y, -50f, 0f);
		float yMax = Mathf.Clamp(6f + yDist + offset.y, 0f, 50f);

		bonusX = Mathf.Clamp(bonusX + mouseX + offset2.x, xMin, xMax);
		bonusY = Mathf.Clamp(bonusY + mouseY + offset2.y, yMin, yMax);

		var pos = transform.position + new Vector3(bonusX, bonusY, 0f);
		pos.z = transform.position.z;

		mouseCursor.transform.position = pos;
		Quaternion rot = Quaternion.LookRotation(transform.position - pos, Vector3.forward);
		rot.x = 0;
		rot.y = 0;

		transform.rotation = rot;
	}

}
