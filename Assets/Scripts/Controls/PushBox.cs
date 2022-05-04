using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour
{

  private float _pushForce;

  public float PushForce { get { return _pushForce; } set { _pushForce = value; } }


  private void OnControllerColliderHit(ControllerColliderHit hit)
  {
    Rigidbody rigidbody = hit.collider.attachedRigidbody;

    if (rigidbody != null)
    {
      Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
      forceDirection.y = 0;
      forceDirection.Normalize();

      rigidbody.AddForceAtPosition(forceDirection * _pushForce, transform.position, ForceMode.Impulse);
    }
  }
}
