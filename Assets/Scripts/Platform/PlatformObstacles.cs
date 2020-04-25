using System.Collections.Generic;
using UnityEngine;

public class PlatformObstacles : MonoBehaviour
{
	public List<GameObject> ObstaclesContainer;

	public void Cleanup()
	{
		foreach (var obstacle in ObstaclesContainer)
			Destroy(obstacle);
	}
}
