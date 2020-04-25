using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleCollision : MonoBehaviour
{
  PlayerController Player;
  private void Start()
  {
    Player = GameObject.Find("Player").GetComponent<PlayerController>();
  }

  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.name == "Player")
    {
      Player.health--;
      if (Player.health <= 0)
      {
        Debug.Log("Player died");
        SceneManager.LoadScene("Menu");
      }
    }
  }
}
