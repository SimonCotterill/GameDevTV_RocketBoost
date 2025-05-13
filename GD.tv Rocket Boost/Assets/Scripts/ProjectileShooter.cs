using UnityEngine;

//Followed: https://www.youtube.com/watch?v=wZ2UUOC17AY 

public class ProjectileShooter : MonoBehaviour
{

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] LeftLaunchPad leftLaunchPad;

    [SerializeField] float shootForce = 100f;
    [SerializeField] float timeBetweenShots = 2f;
    [SerializeField] Transform targetPoint;
    [SerializeField] Transform attackPoint;

    [SerializeField] AudioClip rockShootAudio;

    AudioSource audioSource;

    bool readyToShoot = true;
    bool allowInvoke = true;






    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leftLaunchPad != null && leftLaunchPad.boolLeftLaunchPad)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void Shoot()
    {
        if (readyToShoot)
        {
            UnityEngine.Debug.Log("bullet shot");

            Vector3 bulletDirection = targetPoint.position - attackPoint.position;

            GameObject currentBullet = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);
            currentBullet.transform.forward = bulletDirection.normalized;

            currentBullet.GetComponent<Rigidbody>().AddForce(bulletDirection.normalized * shootForce, ForceMode.Impulse);

            audioSource.PlayOneShot(rockShootAudio);
        }

        readyToShoot = false;

        
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShots);
            allowInvoke = false;
            UnityEngine.Debug.Log("allowing invoke");
        }
        
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
        UnityEngine.Debug.Log("Reseting shot");
    }

}
