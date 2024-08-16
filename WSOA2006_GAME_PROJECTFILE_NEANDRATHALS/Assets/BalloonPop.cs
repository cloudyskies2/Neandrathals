using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPop : MonoBehaviour
{
private GameObject balloon;

 void OnCollisionEnter(Collision collision)
    {

    if(collision.gameObject.CompareTag("Balloon"))
    {
      balloon = collision.gameObject;
      Destroy(balloon);
    }
    }
}
