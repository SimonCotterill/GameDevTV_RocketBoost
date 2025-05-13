using UnityEngine;

public class ProjectileExplosion : MonoBehaviour
{

    [SerializeField] ParticleSystem explodeParticles;
    [SerializeField] AudioClip rockExplosionAudio;

    AudioSource audioSource;

    bool soundAllowed = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (soundAllowed)
        {
            Invoke("AllowSoundAndParticles", 3f);
            audioSource.PlayOneShot(rockExplosionAudio);
            explodeParticles.Play();
            soundAllowed = false;
        }
        Invoke("ByeBye", 1f);
    }

    void ByeBye()
    {
        Destroy(gameObject);
    }

    void AllowSoundAndParticles()
    {
        soundAllowed = true;
    }
}
