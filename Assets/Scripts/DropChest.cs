using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropChest : Actor
{
    [SerializeField] RelicDrop myDrop;
    [SerializeField] private Animator anim;
    public AudioSource m_audioSource;
    private int OpenHash;

    public Animator Anim { get => anim;}

    private void Start()
    {
        OpenHash = Animator.StringToHash("Open");
    }
    public void Open()
    {
        anim.Play(OpenHash);
        SoundManager.Play(SoundManager.Sound.NebulaFlowerPopped, m_audioSource);

    }

    public void DoneOpening()
    {
        myDrop = Instantiate(GameManager.Instance.assets.relicDropPrefab, transform.position, transform.rotation);
        myDrop.CacheRelic(GameManager.Instance.RelicManager.GetFreeRelic());
    }
}
