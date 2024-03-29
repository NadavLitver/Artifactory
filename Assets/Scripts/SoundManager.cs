using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public List<SoundAudioClip> clips;
    public float DeactivateAudioObjectsTime;

    public ObjectPool m_pool;
    public enum Sound
    {
        PlayerJump,
        PlayerLand,
        PlayerRun1,
        PlayerRun2,
        PlayerRun3,
        PlayerRun4,
        PlayerGainResources,
        BasicGunShoot,
        BasicGunShoot2,
        BasicGunCharged,
        BasicGunExplosion,
        BasicGunMobility,
        BasicPickaxeHit1,BasicPickaxeHit2,BasicPickaxeHit3,
        BasicPickaxeAirAttack,BasicPickaxeClimb,
        BasicPickaxeSwing,BasicPickaxeSwing2,
        BasicSwordDash,
        BasicSwordHit1,BasicSwordHit2,BasicSwordHit3,
        BasicSwordSwing1, BasicSwordSwing2, BasicSwordSwing3,
        MushroomEnemyRevive,MushroomEnemyBlocked, MushroomEnemyDead,MushroomEnemyCapHitPlayer, MushroomEnemyCapHitGround, MushroomEnemyRamHit, 
        NebulaFlowerPopped,BellFlowerBelling,NearNebulaFlower,
        OnDandilion,OnJumpPlatform,
        SpikedFromJump,SpikedFrogDeath,
        GlimmeringWoodsAmbiance

    }
    [ContextMenu("Reset List")]
    private void ResetClipListBasedOnExistingSoundEnum()
    {
        clips.Clear();
        foreach (Sound item in Enum.GetValues(typeof(Sound)))
        {
            clips.Add(new SoundAudioClip(item));
        }
    }

    public static void Play(Sound sound)
    {

        GameObject soundGO = GameManager.Instance.soundManager.m_pool.GetPooledObject();
        soundGO.SetActive(true);
        AudioSource audioSource = soundGO.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
        GameManager.Instance.StartCoroutine(GameManager.Instance.soundManager.DestroyAudioObjects(soundGO));

    }
    public static void Play(Sound sound, Vector3 worldPos)
    {
        if (ReferenceEquals(GameManager.Instance.soundManager.m_pool, null))
        {
            return;
        }
        GameObject soundGO = GameManager.Instance.soundManager.m_pool.GetPooledObject();
        soundGO.SetActive(true);
        soundGO.transform.position = worldPos;
        AudioSource audioSource = soundGO.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
        GameManager.Instance.soundManager.StartCoroutine(GameManager.Instance.soundManager.DestroyAudioObjects(soundGO));


    }
    public static void Play(Sound sound, Vector3 worldPos, float Volume)
    {
        GameObject soundGO = GameManager.Instance.soundManager.m_pool.GetPooledObject();
        soundGO.SetActive(true);
        soundGO.transform.position = worldPos;
        AudioSource audioSource = soundGO.GetComponent<AudioSource>();
        audioSource.volume = Mathf.Clamp01(Volume);
        audioSource.PlayOneShot(GetAudioClip(sound));
        GameManager.Instance.StartCoroutine(GameManager.Instance.soundManager.DestroyAudioObjects(soundGO));


    }
    public static void Play(Sound sound, Vector3 worldPos, float Volume, float pitch)
    {
        GameObject soundGO = GameManager.Instance.soundManager.m_pool.GetPooledObject();
        soundGO.SetActive(true);
        soundGO.transform.position = worldPos;
        AudioSource audioSource = soundGO.GetComponent<AudioSource>();
        audioSource.volume = Mathf.Clamp01(Volume);
        audioSource.pitch = Mathf.Clamp(pitch, -3, 3);
        audioSource.PlayOneShot(GetAudioClip(sound));
        GameManager.Instance.StartCoroutine(GameManager.Instance.soundManager.DestroyAudioObjects(soundGO));


    }
    public static void Play(Sound sound, Vector3 worldPos, float Volume, float pitch, float reverb)
    {

        GameObject soundGO = GameManager.Instance.soundManager.m_pool.GetPooledObject();
        soundGO.SetActive(true);
        soundGO.transform.position = worldPos;
        AudioSource audioSource = soundGO.GetComponent<AudioSource>();
        audioSource.reverbZoneMix = Mathf.Clamp(reverb, 0, 1.1f);
        audioSource.volume = Mathf.Clamp01(Volume);
        audioSource.pitch = Mathf.Clamp(pitch, -3, 3);
        audioSource.PlayOneShot(GetAudioClip(sound));
        GameManager.Instance.StartCoroutine(GameManager.Instance.soundManager.DestroyAudioObjects(soundGO));

    }
    public static void Play(Sound sound, Vector3 worldPos, float Volume, float pitch, float reverb,float _spatialBlend)
    {

        GameObject soundGO = GameManager.Instance.soundManager.m_pool.GetPooledObject();
        soundGO.SetActive(true);
        soundGO.transform.position = worldPos;
        AudioSource audioSource = soundGO.GetComponent<AudioSource>();
        audioSource.reverbZoneMix = Mathf.Clamp(reverb, 0, 1.1f);
        audioSource.volume = Mathf.Clamp01(Volume);
        audioSource.pitch = Mathf.Clamp(pitch, -3, 3);
        audioSource.spatialBlend = _spatialBlend;
        audioSource.PlayOneShot(GetAudioClip(sound));
        GameManager.Instance.StartCoroutine(GameManager.Instance.soundManager.DestroyAudioObjects(soundGO));

    }
    public static void Play(Sound sound, AudioSource source)
    {
        source.volume = Mathf.Clamp01(GetVolumeOfClip(sound));
        if (source.volume == 0)
            return;
        source.PlayOneShot(GetAudioClip(sound));

    }
    public static void Play(Sound sound, AudioSource source, float Volume)
    {

        source.volume = Mathf.Clamp01(Volume);
        if (source.volume == 0)
            return;
        source.PlayOneShot(GetAudioClip(sound));

    }
    public static void Play(Sound sound, AudioSource source, float Volume, float pitch)
    {
        source.pitch = Mathf.Clamp(pitch, -3, 3);
        source.volume = Mathf.Clamp01(Volume);
        if (source.volume == 0)
            return;
        source.PlayOneShot(GetAudioClip(sound));

    }
    public static void Play(Sound sound, AudioSource source, float Volume, float pitch, float reverb)
    {
        source.reverbZoneMix = Mathf.Clamp(reverb, 0, 1.1f);
        source.pitch = Mathf.Clamp(pitch, -3, 3);
        source.volume = Mathf.Clamp01(Volume);
        if (source.volume == 0)
            return;
        source.PlayOneShot(GetAudioClip(sound));

    }
    public static void Play(Sound sound, AudioSource source, float Volume, float pitch, float reverb, float _SpatialBlend)
    {
        source.spatialBlend = Mathf.Clamp(_SpatialBlend, -1, 1);
        source.reverbZoneMix = Mathf.Clamp(reverb, 0, 1.1f);
        source.pitch = Mathf.Clamp(pitch, -3, 3);
        source.volume = Mathf.Clamp01(Volume);
        if (source.volume == 0)
            return;
        source.PlayOneShot(GetAudioClip(sound));

    }
    /// <summary>
    /// Use this method when you want a random pitch
    /// </summary>
    /// <param name="pitchRange">Set the X of the vector to the minimum of the pitch range (can be negative) And the Y to the Max Range.</param>
    public static void Play(Sound sound, AudioSource source, float Volume, float reverb, Vector3 pitchRange)
    {

        source.reverbZoneMix = Mathf.Clamp(reverb, 0, 1.1f);
        source.pitch = UnityEngine.Random.Range(Mathf.Clamp(pitchRange.x, -3, 3), Mathf.Clamp(pitchRange.y, -3, 3));
        source.volume = Mathf.Clamp01(Volume);
        if (source.volume == 0)
            return;
        source.PlayOneShot(GetAudioClip(sound));

    }

    public static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAudioClip clip in GameManager.Instance.soundManager.clips)
        {
            if (clip.m_Sound == sound)
            {
                return clip.m_AudioClip;
            }
        }
        return null;
    }
    public static float GetVolumeOfClip(Sound sound)
    {
        foreach (SoundAudioClip clip in GameManager.Instance.soundManager.clips)
        {
            if (clip.m_Sound == sound)
            {
                return clip.m_Volume;
            }
        }
        return 1;
    }
    private IEnumerator DestroyAudioObjects(GameObject go)
    {
        yield return new WaitForSeconds(DeactivateAudioObjectsTime);
        go.SetActive(false);

    }

}
[Serializable]
public class SoundAudioClip
{
    public SoundAudioClip()
    {

    }
   public SoundAudioClip(SoundManager.Sound _sound)
    {
        m_Sound = _sound;
    }
    public SoundManager.Sound m_Sound;
    public AudioClip m_AudioClip;
    [Range(0, 1)] public float m_Volume = 1;
}