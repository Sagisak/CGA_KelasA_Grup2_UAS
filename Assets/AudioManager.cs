using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-----Audio Source-----")]
   [SerializeField] AudioSource musicSource;
   [SerializeField] AudioSource SFxSource;

[Header("-----Audio Source-----")]
   public AudioClip background;
   public AudioClip button;

   private void Start()
   {
       musicSource.clip = background;
       musicSource.loop = true;
       musicSource.Play();
   }

   public void PlaySFX(AudioClip clip)
   {
       SFxSource.clip = clip;
       SFxSource.Play();
   }
}
