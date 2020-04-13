using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset = new Vector3(-10, 3.5f, 0);

	void LateUpdate()
	{
		if (target == null)
			return;

		transform.position = target.position + offset;
		transform.LookAt(target);
	}
}
