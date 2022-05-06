using UnityEngine;

public class PushBox : MonoBehaviour
{

  public float _pushForce;

  private void OnCollisionStay(Collision hit) {    
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
