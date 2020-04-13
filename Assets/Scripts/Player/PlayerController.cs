using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	CharacterController characterController;
	PlayerInputHandler inputHandler;

	// Parameters
	public float speed = 10f;
	public float movementSharpness = 15;
	public float maxVelocity = 100f;
	public float minVelocity = 8f;
	public float accelerationConstant = 1f;
	public float sidewaysDescreteStep = .5f;
	public float sidewaysMovementDebounce = 1f;
	public Vector3 MovementBounds = new Vector3(Mathf.Infinity, Mathf.Infinity, 40.8f);

	// Runtime Variables
	Vector3 velocity = Vector3.zero;
	float lastSidewaysMovement = 0f;

	void Start()
	{
		characterController = GetComponent<CharacterController>();
		inputHandler = GetComponent<PlayerInputHandler>();

		velocity = new Vector3(speed, 0, 0);
	}

	void Update()
	{
		characterController.Move(CalculateVelocity() * Time.deltaTime + CalculateSidewaysMovement());
		changeSize();
	}

	void FixedUpdate()
	{
		changeOrientation();
	}

	public void changeSize()
	{
		
	}

	public Vector3 CalculateVelocity()
	{
		var acceleration = inputHandler.GetAcceleration();
		if (acceleration == 0)
			// Decelerate
			velocity = Vector3.Lerp(velocity, new Vector3(speed, 0, 0), movementSharpness * Time.deltaTime);
		else
			// Accelerate
			velocity += accelerationConstant * acceleration * new Vector3(1, 0, 0);

		// Cap the character acceleration and deceleration
		velocity.x = Math.Min(velocity.x, maxVelocity);
		velocity.x = Math.Max(velocity.x, minVelocity);
		return velocity;
	}

	public Vector3 CalculateSidewaysMovement()
	{
		// Apply debounce to sideways movement
		if (Time.time < lastSidewaysMovement + sidewaysMovementDebounce)
			return Vector3.zero;

		var sidewaysMovementAmount = inputHandler.GetSidewaysMovement();
		if (sidewaysMovementAmount == 0)
			return Vector3.zero;

		var result = sidewaysMovementAmount * sidewaysDescreteStep * new Vector3(0, 0, 1);

		// Limit z-axis movement to the bounds of the platform
		var futureZ = Math.Min(MovementBounds.z / 2, transform.position.z + result.z);
		futureZ = Math.Max(-MovementBounds.z / 2, futureZ);
		result.z = futureZ - transform.position.z;

		if (result != Vector3.zero)
			lastSidewaysMovement = Time.time;

		// TransformVector is needed in order to move downhill when we are on the side platforms
		return transform.TransformVector(result);
	}

	void changeOrientation()
	{
		var ray = transform.TransformDirection(Vector3.down);
		Quaternion rotation;
		Vector3 position;

		if (Physics.Raycast(transform.position, ray, out RaycastHit hit, 10))
		{
			Debug.DrawRay(transform.position, 10 * ray, Color.red);

			position = new Vector3(transform.position.x, hit.point.y + 2, transform.position.z);
			rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

			//transform.position = Vector3.Slerp(transform.position, position, 0.25f);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.25f );
		}
	}
}