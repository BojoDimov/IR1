using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ObstacleDescription
{
	// References
	public GameObject Obstacle;

	// Position and Rotation
	public Vector3 SpawnArea = new Vector3(41, 0, 18);
	public Vector3 SpawnOffset = new Vector3(0, 20, 0);
	public Vector3 RepositionOffset = new Vector3(0, -2, 0);
	public Quaternion Rotation = Quaternion.identity;
	public bool ShouldPositionOnSpawn = true;

	// Spawn Parameters
	public float LocalSpawnProbability;
}

//[CustomEditor(typeof(ObstaclesController)), CanEditMultipleObjects]
//public class ObstacleDescriptionEditor : Editor
//{
//	public override void OnInspectorGUI()
//	{
//		base.OnInspectorGUI();
//		var test = (ObstaclesController)target;

//		if (GUILayout.Button("Add Obstacle"))
//			test.AddObstacle();

//		if (GUILayout.Button("Remove Obstacle"))
//			test.RemoveObstacle();
//	}
//}

public class ObstaclesController : MonoBehaviour
{
	public float GlobalSpawnProbability = .25f;
	public List<ObstacleDescription> ObstacleDescriptions;

	public void AddObstacle() => ObstacleDescriptions.Add(new ObstacleDescription());

	public void RemoveObstacle() => ObstacleDescriptions.Remove(ObstacleDescriptions.Last());

	public List<GameObject> GenerateObstacles(GameObject parent)
	{
		List<GameObject> result = new List<GameObject>();

		// Initialize generator with random number. Actually using determined seed is better IMHO
		var generator = new System.Random((int)parent.transform.position.x);

		ObstacleDescriptions.ForEach(desc => {
			float a = -desc.SpawnArea.z / 2;
			float b = desc.SpawnArea.z / 2;

			for (float i = -desc.SpawnArea.x / 2; i < desc.SpawnArea.x / 2; i += 3)
			{
				// Determine if we have obstacle on this row
				if (!generator.SampleWithProbability(desc.LocalSpawnProbability * GlobalSpawnProbability))
					continue;

				// Generate noise in the range (-SpawnArea.z/2, SpawnArea.z/2)
				var noise = generator.Next((int)a, (int)b);

				var obstacle = CreateObstacle(parent, desc, i, noise);
				if (obstacle != null)
					result.Add(obstacle);
			}
		});

		return result;
	}

	private GameObject CreateObstacle(GameObject parent, ObstacleDescription obstacleDescription, float x, float noise)
	{
		var obstacle = Instantiate(
				 obstacleDescription.Obstacle,
				 new Vector3(parent.transform.position.x + x, parent.transform.position.y, parent.transform.position.z + noise) + obstacleDescription.SpawnOffset,
				 obstacleDescription.Rotation
			 );
		obstacle.name = "Obstacle";
		obstacle.transform.parent = parent.transform;

		//obstacle.AddComponent<ObstacleCollision>();

		if (!obstacleDescription.ShouldPositionOnSpawn)
			return obstacle;

		if (Physics.Raycast(obstacle.transform.position, Vector3.down, out var hit, obstacleDescription.SpawnOffset.y * 2, Constants.NotInvisibleTrigger))
		{
			obstacle.transform.rotation = Quaternion.FromToRotation(obstacle.transform.up, hit.normal) * obstacle.transform.rotation;
			obstacle.transform.position = obstacleDescription.RepositionOffset + new Vector3(obstacle.transform.position.x, obstacle.transform.position.y - hit.distance, obstacle.transform.position.z);
			return obstacle;
		}
		else
		{
			Destroy(obstacle);
			return null;
		}
	}
}