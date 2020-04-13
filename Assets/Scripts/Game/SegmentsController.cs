using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SegmentsController : MonoBehaviour
{
	// References
	public GameObject PlatformSegmentReference;
	public List<GameObject> Segments = new List<GameObject>();

	// Parameters
	public float segmentLength = 41f;
	public int segmentsCount = 5;
	public int cleanupSize = 2;
	public bool enableCleanup = true;
	public Quaternion nextSegmentRotation = new Quaternion(0,0,0,0);

	// Runtime variables
	int cleanupTreshold;

	void Start()
	{
		cleanupTreshold = segmentsCount + cleanupSize * 2 + 1;

		GameObject firstSegment = Instantiate(
			PlatformSegmentReference,
			new Vector3(0, 0, 0),
			PlatformSegmentReference.transform.rotation
		);
		//firstSegment.hideFlags = HideFlags.HideInHierarchy;
		firstSegment.name = "PlatformSegment";
		firstSegment.transform.parent = transform;

		Segments.Add(firstSegment);

		for (int i = 1; i < segmentsCount; ++i)
			AddSegment();
	}

	public void AddSegment()
	{
		GameObject lastSegment = Segments.Last();
		GameObject nextSegment = Instantiate(
			PlatformSegmentReference,
			new Vector3(lastSegment.transform.position.x + segmentLength, lastSegment.transform.position.y, lastSegment.transform.position.z),
			nextSegmentRotation * lastSegment.transform.rotation
		);
		nextSegment.hideFlags = HideFlags.HideInHierarchy;
		nextSegment.name = "PlatformSegment";
		nextSegment.transform.parent = transform;

		Segments.Add(nextSegment);

		if (enableCleanup && Segments.Count >= cleanupTreshold)
			this.Cleanup();
	}

	private void Cleanup()
	{
		for (int i = 0; i < cleanupSize; ++i)
		{
			ObstaclesController obstaclesController = Segments[i].GetComponent<ObstaclesController>();
			if (obstaclesController != null)
				obstaclesController.Cleanup();
			Destroy(Segments[i]);
		}
			
		Segments.RemoveRange(0, cleanupSize);
	}
}
