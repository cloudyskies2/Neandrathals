using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BalloonPop : MonoBehaviour
{
private GameObject balloon;
private bool balloonPopped = false; // Flag to check if a balloon has been popped


void OnCollisionEnter(Collision collision)
    {

     if (!balloonPopped && collision.gameObject.CompareTag("Balloon"))
      {
      balloon = collision.gameObject;
      Destroy(balloon);
      balloonPopped = true;
      }
    }
}
