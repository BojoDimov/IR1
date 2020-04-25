using UnityEngine;

public class DifficultyController : MonoBehaviour
{
  // References
  public PlayerController player;
  public ObstaclesController obstacles;

  // Parameters
  public float IncreaseInterval = 100f;
  public float ObstaclesSpawnIncrease = 0.05f;
  public float PlayerSpeedIncrease = 10f;

  // Runtime
  private float lastUpdate = 0f;

  void Start()
  {
    player = GameObject.Find("Player").GetComponent<PlayerController>();
    obstacles = GetComponent<ObstaclesController>();
  }

  void LateUpdate()
  {
    if (player == null || obstacles == null)
      return;

    if(player.transform.position.x > lastUpdate + IncreaseInterval)
    {
      lastUpdate = player.transform.position.x;
      player.speed = Mathf.Max(player.speed + PlayerSpeedIncrease, player.maxVelocity);
      player.minVelocity += PlayerSpeedIncrease;
      obstacles.GlobalSpawnProbability += ObstaclesSpawnIncrease;
    }
  }
}
