using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
  public Vector3 ObstaclesOffset = new Vector3(0, 2, 0);
  public Vector3 ObstaclesArea = new Vector3(41, 0, 18);

  public GameObject ObstacleReference;
  public List<GameObject> Obstacles;

  void Start()
  {
    noiseMethod3();
  }

  public void Cleanup()
  {
    foreach (var obstacle in Obstacles)
      Destroy(obstacle);
  }

  private void noiseMethod1()
  {
    for (float i = -ObstaclesArea.z / 2; i < ObstaclesArea.z / 2; ++i)
    {
      for (float j = -ObstaclesArea.x / 2; j < ObstaclesArea.x / 2; ++j)
      {
        var noise = Mathf.PerlinNoise(i + transform.position.z, j + transform.position.x);
        if (noise < 0.802)
          continue;

        Debug.Log("Perlin noise is working?");

        var obstacle = Instantiate(
          ObstacleReference,
          new Vector3(transform.position.x + j, transform.position.y, transform.position.z + i) + ObstaclesOffset,
          Quaternion.identity
        );
        obstacle.name = "Obstacle";
        obstacle.transform.parent = transform;
        Obstacles.Add(obstacle);
      }
    }
  }

  private void noiseMethod2()
  {
    for (float i = -ObstaclesArea.x / 2; i < ObstaclesArea.x / 2; i += 3)
    {
      // Generate noise in the range (-ObstacleArea.z/2, ObstacleArea.z/2)
      var noise = (Mathf.PerlinNoise(transform.position.z * 100, i + transform.position.x * 100) - .5f) * ObstaclesArea.z / 2;

      var obstacle = Instantiate(
          ObstacleReference,
          new Vector3(transform.position.x + i, transform.position.y, transform.position.z + noise) + ObstaclesOffset,
          Quaternion.identity
        );
      obstacle.name = "Obstacle";
      obstacle.transform.parent = transform;
      Obstacles.Add(obstacle);
    }
  }

  private void noiseMethod3()
  {
    float a = -ObstaclesArea.z / 2;
    float b = ObstaclesArea.z / 2;
    float probability = 15/100f;

    var generator = new System.Random((int)transform.position.x);

    for (float i = -ObstaclesArea.x / 2; i < ObstaclesArea.x / 2; i += 3)
    {
      // Determine if we have obstacle on this row
      if (generator.Next(0, 100) >= probability * 100)
        continue;

      // Generate noise in the range (-ObstacleArea.z/2, ObstacleArea.z/2)
      var noise = generator.Next((int)a, (int)b);

      placeObstacle(i, noise);
    }
  }

  private void placeObstacle(float x, float noise)
  {
    var obstacle = Instantiate(
         ObstacleReference,
         new Vector3(transform.position.x + x, transform.position.y, transform.position.z + noise) + ObstaclesOffset,
         Quaternion.identity
       );
    obstacle.name = "Obstacle";
    obstacle.transform.parent = transform;

    int layerMask = ~(1 << 9);

    if (Physics.Raycast(obstacle.transform.position, Vector3.down, out var hit, ObstaclesOffset.y * 2, layerMask))
    {
      obstacle.transform.rotation = Quaternion.FromToRotation(obstacle.transform.up, hit.normal) * obstacle.transform.rotation;
      obstacle.transform.position = new Vector3(obstacle.transform.position.x, obstacle.transform.position.y - hit.distance - 2, obstacle.transform.position.z);
      Obstacles.Add(obstacle);
    }
    else
    {
      Debug.Log("Couldn't project obstacle onto terrain");
      Destroy(obstacle);
    }
  }

  private void drawObstacleRay(GameObject obstacle)
  {
    if(Physics.Raycast(obstacle.transform.position, Vector3.down, out var hit, ObstaclesOffset.y * 2))
    {
      Debug.DrawRay(obstacle.transform.position, 20 * Vector3.down, Color.red);
    }
  }

  public void Update()
  {
    foreach (var obstacle in Obstacles)
      drawObstacleRay(obstacle);
  }
}
