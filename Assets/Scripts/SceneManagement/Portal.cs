using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace Zelda.SceneManagement
{
  public class Portal : MonoBehaviour
  {
    enum DestinationIdentifier
    {
      A, B, C, D, E
    }

    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] DestinationIdentifier destination;
    [SerializeField] float fadeIn = 1f;
    [SerializeField] float fadeOut = 3f;
    [SerializeField] float fadeWait = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
      if (other.tag == "Player")
      {
        StartCoroutine(Transition());
      }
    }

    private IEnumerator Transition()
    {
      if (sceneToLoad < 0)
      {
        Debug.LogError("Scene to load not set");
        yield break;
      }

      DontDestroyOnLoad(gameObject);

      Fader fader = FindObjectOfType<Fader>();

      yield return fader.FadeOut(fadeOut);

      yield return SceneManager.LoadSceneAsync(sceneToLoad);

      Portal otherPortal = GetOtherPortal();
      UpdatePlayer(otherPortal);

      yield return new WaitForSeconds(fadeWait);
      yield return fader.FadeIn(fadeIn);

      Destroy(gameObject);
    }

    private void UpdatePlayer(Portal otherPortal)
    {
      GameObject player = GameObject.FindWithTag("Player");
      player.transform.position = otherPortal.spawnPoint.position;
    }

    private Portal GetOtherPortal()
    {
      foreach (Portal portal in FindObjectsOfType<Portal>())
      {
        if (portal == this) continue;
        if (portal.destination != destination) continue;
        return portal;
      }
      return null;
    }
  }
}