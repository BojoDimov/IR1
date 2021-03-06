﻿using UnityEngine;

public class AddSegmentTrigger : MonoBehaviour
{
	// References
	GameObject Game;
	SegmentsController SegmentsController;

	// Runtime variables
	bool isColliding = false;

	void Start()
	{
		Game = GameObject.Find("Game");

		if (Game != null)
			SegmentsController = Game.GetComponent<SegmentsController>();
	}

	void OnTriggerEnter(Collider collision)
	{
		if (isColliding)
			return;

		isColliding = true;

		if (SegmentsController != null)
			SegmentsController.AddSegment();
	}

	void OnTriggerExit(Collider collision)
	{
		isColliding = false;
	}
}
