using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zelda.Control
{
  public class AIController : MonoBehaviour
  {
    [SerializeField] float chaseDistance = 7f;
    [SerializeField] float suspicionTime = 2f;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float patrolSpeed = 10f;
    [SerializeField] float waypointTolerance = 1f;
    [SerializeField] float waypointDwellTime = 5f;
    [Range(0f, 100f)]
    [SerializeField] float hearSensitivity = 1f;

    [SerializeField] GameObject DefeatCanvasUI;

    Animator animator;
    GameObject player;

    Vector3 guardPosition;
    float timeSinceLastHearedPlayer = Mathf.Infinity;
    float timeSinceArrivedAtWaypoint = Mathf.Infinity;
    int currentWaypointIndex = 0;

    private void Start()
    {
      player = GameObject.FindWithTag("Player");

      animator = GetComponent<Animator>();

      guardPosition = transform.position;
    }
    private void Update()
    {
      if (InAtackRange() && CanSeePlayer(player))
      {
        AttackBehaviour();
      }
      else if (timeSinceLastHearedPlayer < suspicionTime)
      {
        SuspciousBehaviour();
      }
      else
      {
        PatrolBehaviour();
      }

      if (InAtackRange() && CanHearPlayer())
      {
        timeSinceLastHearedPlayer = 0;
      }

      UpdateTimers();
    }

    private bool CanHearPlayer()
    {
      return Random.Range(0f, 100f) <= hearSensitivity * Time.deltaTime;
    }

    private void AttackBehaviour()
    {
      animator.SetBool("isWalking", false);
      DefeatCanvasUI.SetActive(true);
      if (Input.GetMouseButtonDown(0))
      {
        DefeatCanvasUI.SetActive(false);
        SceneManager.LoadScene(3);
      }
    }

    private bool CanSeePlayer(GameObject player)
    {
      Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
      float dot = Vector3.Dot(transform.forward, directionToPlayer);
      if (dot > 0.8) return true;
      return false;
    }

    private bool InAtackRange()
    {
      return Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
    }

    private void UpdateTimers()
    {
      timeSinceLastHearedPlayer += Time.deltaTime;
      timeSinceArrivedAtWaypoint += Time.deltaTime;
    }

    private void PatrolBehaviour()
    {
      animator.SetBool("isWalking", false);
      Vector3 nextPosition = guardPosition;

      if (patrolPath != null)
      {
        if (AtWaypoint())
        {
          timeSinceArrivedAtWaypoint = 0;
          CycleWaypoint();
        }
        nextPosition = GetCurrentWaypoint();
      }

      if (timeSinceArrivedAtWaypoint > waypointDwellTime)
      {
        animator.SetBool("isWalking", true);
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, patrolSpeed * Time.deltaTime);
        transform.LookAt(nextPosition);
      }
    }

    private bool AtWaypoint()
    {
      float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
      return distanceToWaypoint < waypointTolerance;
    }


    private void CycleWaypoint()
    {
      currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private Vector3 GetCurrentWaypoint()
    {
      return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    IEnumerator SuspciousBehaviour()
    {
      animator.SetBool("isWalking", false);
      transform.Rotate(Vector3.up, 180);
      suspicionTime = Random.Range(0f, 10f);
      yield return new WaitForSeconds(suspicionTime);
      transform.Rotate(Vector3.up, 180);
    }

    private void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
  }
}