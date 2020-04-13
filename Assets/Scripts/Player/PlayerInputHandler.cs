using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
  public float GetAcceleration()
  {
    float acceleration = 0f;
    if (Input.GetKey("w"))
      acceleration += 1f;

    if (Input.GetKey("s"))
      acceleration += -1f;

    return acceleration;
  }

  public float GetSidewaysMovement()
  {
    var axisMovement = Input.GetAxisRaw("Horizontal");
    return axisMovement == 0 ? 0 : -1 * Mathf.Sign(axisMovement);
  }
}
