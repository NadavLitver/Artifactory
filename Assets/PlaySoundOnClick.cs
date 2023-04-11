using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySoundOnClick : MonoBehaviour
{
    [SerializeField] Button m_button;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;

    private void Awake()
    {
        if (ReferenceEquals(m_button, null)){
            m_button = GetComponent<Button>();
        }
    }
    private void OnEnable()
    {
        m_button.onClick.AddListener(PlayClipOnce);
    }
    private void OnDisable()
    {
        m_button.onClick.RemoveListener(PlayClipOnce);
    }
    void PlayClipOnce() => audioSource.PlayOneShot(clip);
}
