using UnityEngine;

public static class UnusedMethods
{
	//void _orientate()
	//{
	//	Vector3 size = new Vector3(2, 2, 2);

	//	var rays = new Vector3[4] {
	//			transform.TransformDirection(Vector3.down),
	//			transform.TransformDirection(Vector3.down),
	//			transform.TransformDirection(Vector3.down),
	//			transform.TransformDirection(Vector3.down)
	//		};
	//	//var rays = new Vector3[4] {
	//	//		Vector3.down,
	//	//		Vector3.down,
	//	//		Vector3.down,
	//	//		Vector3.down
	//	//	};

	//	var positions = new Vector3[4]
	//	{
	//		transform.position + transform.rotation * new Vector3(-size.x/2, -size.y/2, -size.z/2),	// left back
	//		transform.position + transform.rotation * new Vector3(size.x/2, -size.y/2, -size.z/2),			// left front
	//		transform.position + transform.rotation * new Vector3(-size.x/2, -size.y/2, size.z/2),			// right back
	//		transform.position + transform.rotation * new Vector3(size.x/2, -size.y/2, size.z/2)			// right front
	//	};

	//	for(int i=0; i < 4; ++i)
	//	{
	//		Physics.Raycast(positions[i], rays[i], out var hit, 10f);
	//		Debug.DrawRay(positions[i], rays[i] * hit.distance, new Color(0, i%2, (i + 1) % 2, 1));
	//	}
	//}

	//bool _canMoveLeft()
	//{
	//	var ray = transform.TransformDirection(Quaternion.AngleAxis(30, Vector3.left) * Vector3.down);
	//	return Physics.Raycast(transform.position, ray, 100f);
	//}

	//bool _canMoveRight()
	//{
	//	var ray = transform.TransformDirection(Quaternion.AngleAxis(-30, Vector3.left) * Vector3.down);
	//	return Physics.Raycast(transform.position, ray, 100f);
	//}

	// It is almost working
	//void _tripleRaycast()
	//{
	//	var leftRotation = Quaternion.AngleAxis(30, Vector3.left);
	//	var rightRotation = Quaternion.AngleAxis(-30, Vector3.left);
	//	var forwardRotation = Quaternion.AngleAxis(15, Vector3.up);
	//	var backwardRotation = Quaternion.AngleAxis(-15, Vector3.up);

	//	var centralRayRot = Quaternion.AngleAxis(-15, Vector3.forward);

	//	var rays = new Vector3[4] {
	//		transform.TransformDirection(centralRayRot * Vector3.down),
	//		transform.TransformDirection(forwardRotation * leftRotation * Vector3.down),
	//		transform.TransformDirection(backwardRotation * rightRotation * Vector3.down),
	//		transform.TransformDirection(Vector3.down)
	//	};

	//	var hits = new RaycastHit[4];

	//	Quaternion rotation;
	//	Vector3 position;

	//	if (Physics.Raycast(transform.position, rays[0], out hits[0], Mathf.Infinity)
	//		&& Physics.Raycast(transform.position, rays[1], out hits[1], Mathf.Infinity)
	//		&& Physics.Raycast(transform.position, rays[2], out hits[2], Mathf.Infinity)
	//		&& Physics.Raycast(transform.position, rays[3], out hits[3], Mathf.Infinity)
	//	)
	//	{
	//		var plane = new Plane(hits[1].point, hits[2].point, hits[0].point);

	//		Debug.DrawRay(transform.position, rays[0] * hits[0].distance, Color.red);
	//		Debug.DrawRay(transform.position, rays[1] * hits[1].distance, Color.red);
	//		Debug.DrawRay(transform.position, rays[2] * hits[2].distance, Color.red);

	//		//Debug.DrawRay(hits[0].point, axisA, Color.green);
	//		//Debug.DrawRay(hits[0].point, axisB, Color.green);
	//		Debug.DrawRay(hits[0].point, plane.normal, Color.yellow);

	//		position = new Vector3(transform.position.x, hits[3].point.y, transform.position.z) + new Vector3(0, 1, 0);
	//		rotation = Quaternion.FromToRotation(transform.up, new Vector3(transform.up.x, plane.normal.y, plane.normal.z)) * transform.rotation;

	//		transform.position = position;
	//		transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.125f);
	//	}
	//}

	//public int GetSidewaysMovement()
	//{
	//	if (Input.GetAxisRaw("Horizontal") < 0 && _canMoveLeft())
	//		return 1;
	//	else if (Input.GetAxisRaw("Horizontal") > 0 && _canMoveRight())
	//		return -1;
	//	else
	//		return 0;
	//}

	//https://en.wikipedia.org/wiki/Logistic_function
	public static float logistic(float x)
	{
		float L = 2;
		float k = 1;
		float x0 = 0;

		// Get logistic function value in the interval (0,L)
		var value = L / (1 + Mathf.Exp(-k * (x - x0)));

		// Translate to interval (-L/2, L/2);
		return value - L / 2;
	}
}
