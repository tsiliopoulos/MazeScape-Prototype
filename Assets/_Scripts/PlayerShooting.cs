using UnityEngine;
using System.Collections;

public class PlayerShooting : Gun
{

    // PUBLIC VARIABLES FOR TESTING
    public Transform FlashPoint;
    public GameObject MuzzleFlash;
    public GameObject Explosion;
    public GameObject BulletImpact;
    public AudioSource RifleShotSound;
    public Transform PlayerCam;

    // PRIVATE VARIABLES

    // Use this for initialization
   /* void Start()
    {
        while (Input.GetButtonDown("Fire1"))
        {
            Fire();
            // show the MuzzleFlash at the FlashPoint without any rotation
            Instantiate(this.MuzzleFlash, this.FlashPoint.position, Quaternion.identity);

            // need a variable to hold the location of our Raycast Hit
            RaycastHit hit;

            // if raycast hits an object then do something...
            if (Physics.Raycast(this.PlayerCam.position, this.PlayerCam.forward, out hit))
            {
                // COMMENTED OUT FOR NOW 
                //if (hit.transform.gameObject.CompareTag("Barrels"))
                //{
                //    Instantiate(this.Explosion, hit.point, Quaternion.identity);
                //    Destroy(hit.transform.gameObject);
                //}
                //else
                //{
                Instantiate(this.BulletImpact, hit.point, Quaternion.identity);
                //}


            }

            // Play Rifle Sound
            this.RifleShotSound.Play();
        }
    }*/

    void Start()
    {

        //    //InvokeRepeating("Fire", 1, 1);
           //StartCoroutine(Fire());
    }

    void Update()
    {

    }
    // Update is called once per frame (for Physics)
    void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            //InvokeRepeating("Fire", 1, 1);
            StartCoroutine(Fire());
        }

    }

    IEnumerator Fire()
    {
        FireGun();
        yield return new WaitForSeconds(0.1f);
        this.RifleShotSound.Play();
        Instantiate(this.MuzzleFlash, this.FlashPoint.position, Quaternion.identity);
        RaycastHit hit;
        if (Physics.Raycast(this.PlayerCam.position, this.PlayerCam.forward, out hit))
        {
            Instantiate(this.BulletImpact, hit.point, Quaternion.identity);

            
        }
        yield return new WaitForEndOfFrame();  
    }
    
}
