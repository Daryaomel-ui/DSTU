
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioClip fireSound;
    public AudioClip iceSound;
    public AudioClip playerHitSound;
    public AudioClip golemDeathSound;
    public AudioClip explosionSound;
    public AudioClip buttonClickSound;

    private AudioSource audioSource;


    public AudioClip victorySound;
    public AudioClip defeatSound;

    public AudioSource musicSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFire()
    {
        if (fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }

    public void PlayIce()
    {
        if (iceSound != null)
        {
            audioSource.PlayOneShot(iceSound);
        }
    }

    public void PlayPlayerHit()
    {
        PlaySound(playerHitSound);
    }

    public void PlayGolemDeath()
    {
        PlaySound(golemDeathSound);
    }

    public void PlayExplosion()
    {
        PlaySound(explosionSound);
    }

    public void PlayButtonClick()
    {
        PlaySound(buttonClickSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void PlayVictory()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }

        PlaySound(victorySound);
    }

    public void PlayDefeat()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }

        PlaySound(defeatSound);
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
