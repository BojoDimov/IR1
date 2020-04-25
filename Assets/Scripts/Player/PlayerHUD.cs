using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
  // References
  public PlayerController Player;
  public TextMeshProUGUI Health;
  public TextMeshProUGUI Speed;
  public TextMeshProUGUI Distance;

  // Parameters
  public float UpdateRate = 0.1f;

  private float lastUpdateTime = 0;

  public void Start()
  {
    Player = GetComponent<PlayerController>();
    Health = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
    Speed = GameObject.Find("SpeedText").GetComponent<TextMeshProUGUI>();
    Distance = GameObject.Find("DistanceText").GetComponent<TextMeshProUGUI>();
    lastUpdateTime = Time.time;
  }

  public void LateUpdate()
  {
    if (Player == null)
      return;

    if (Time.time <= lastUpdateTime + UpdateRate)
      return;

    lastUpdateTime = Time.time;

    Health.SetText(GetHealth().ToString());
    Speed.SetText(GetSpeed().ToString());
    Distance.SetText(GetDistance().ToString() + " m");
  }

  private int GetHealth()
  {
    return Player.health;
  }

  private float GetDistance()
  {
    //return (int)(Player.transform.position.x / 1000);
    return (float)Math.Round(Player.transform.position.x);
  }

  private float GetSpeed()
  {
    return (float)Math.Round(Player.velocity.x);
  }
}
