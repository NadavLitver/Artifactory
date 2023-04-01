using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialExit : MonoBehaviour
{
    [SerializeField] TutorialRoom m_room;
    [SerializeField] AudioSource m_audioSource;
    private bool triggered;
    private void Start()
    {
        triggered = false;
        m_audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(onPlayerEnter(collision.transform));
        }
    }
    IEnumerator onPlayerEnter(Transform player)
    {
        if (triggered)
            yield break;

        triggered = true;
        GameManager.Instance.assets.blackFade.FadeToBlack();
        SoundManager.Play(SoundManager.Sound.RoomPortalSound, m_audioSource);
        yield return new WaitForSeconds(2);
        if (m_room.isLastTutorialRoom)
        {
            GameManager.Instance.assets.blackFade.FadeToBlack();
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            //do last thing and break;
            yield break;
        }
        m_room.NextRoom.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        player.position = m_room.NextRoom.StartingPosition.position;
        //Debug.Break();
        yield return new WaitForSeconds(1);
        m_room.gameObject.SetActive(false);
        GameManager.Instance.assets.blackFade.FadeFromBlack();
        GameManager.Instance.assets.PlayerController.canMove = true;
        player.gameObject.SetActive(true);



    }
}
