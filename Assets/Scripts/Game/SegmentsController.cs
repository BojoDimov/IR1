using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct SegmentData
{
	public GameObject PlatformRef;
	public List<GameObject> ObstacleRefs;
}

public class SegmentsController : MonoBehaviour
{
	// References
	public GameObject PlatformSegmentReference;
	private ObstaclesController ObstaclesController;
	private List<SegmentData> Segments = new List<SegmentData>();

	// Parameters
	public float segmentLength = 41f;
	public int segmentsCount = 20;
	public int segmentsWithoutObstacles = 5;
	public int cleanupSize = 2;
	public bool enableCleanup = true;
	public Quaternion nextSegmentRotation = new Quaternion(0,0,0,0);

	// Runtime variables
	int cleanupTreshold;
	int currentSegment;

	void Start()
	{
		ObstaclesController = GetComponent<ObstaclesController>();

		cleanupTreshold = segmentsCount + cleanupSize * 2 + 1;

		AddFirstSegment();
		for (int i = 1; i < segmentsCount; ++i)
			AddSegment();
	}

	public void AddFirstSegment()
	{
		var segmentData = new SegmentData {
			PlatformRef = Instantiate(
				PlatformSegmentReference,
				new Vector3(0, 0, 0),
				PlatformSegmentReference.transform.rotation
			),
			ObstacleRefs = new List<GameObject>()
		};

		segmentData.PlatformRef.name = "PlatformSegment";
		segmentData.PlatformRef.transform.parent = transform;

		// Do not spawn obstacles for the first segment
		//if (ObstaclesController != null)
		//	segmentData.ObstacleRefs = ObstaclesController.GenerateObstacles(segmentData.PlatformRef);

		++currentSegment;
		Segments.Add(segmentData);
	}

	public void AddSegment()
	{
		GameObject lastSegment = Segments.Last().PlatformRef;

		var nextSegmentData = new SegmentData {
			PlatformRef = Instantiate(
				PlatformSegmentReference,
				new Vector3(lastSegment.transform.position.x + segmentLength, lastSegment.transform.position.y, lastSegment.transform.position.z),
				nextSegmentRotation * lastSegment.transform.rotation
			),
			ObstacleRefs = new List<GameObject>()
		};

		//GameObject nextSegment = Instantiate(
		//	PlatformSegmentReference,
		//	new Vector3(lastSegment.transform.position.x + segmentLength, lastSegment.transform.position.y, lastSegment.transform.position.z),
		//	nextSegmentRotation * lastSegment.transform.rotation
		//);
		//nextSegment.hideFlags = HideFlags.HideInHierarchy;
		nextSegmentData.PlatformRef.name = "PlatformSegment";
		nextSegmentData.PlatformRef.transform.parent = transform;

		if (ObstaclesController != null && currentSegment > segmentsWithoutObstacles)
			nextSegmentData.ObstacleRefs = ObstaclesController.GenerateObstacles(nextSegmentData.PlatformRef);

		++currentSegment;
		Segments.Add(nextSegmentData);

		if (enableCleanup && Segments.Count >= cleanupTreshold)
			Cleanup();
	}

	private void Cleanup()
	{
		for (int i = 0; i < cleanupSize; ++i)
		{
			foreach (var obstacle in Segments[i].ObstacleRefs)
				Destroy(obstacle);
			Destroy(Segments[i].PlatformRef);
		}
			
		Segments.RemoveRange(0, cleanupSize);
	}
}
