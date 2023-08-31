using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [Header("Rifle")]
    public Camera cam;
    public float damage = 10f;
    public float rangeShoot = 100f;
    public float fireCharge = 10f;
    public PlayerMoverment player;
    public Animator animator;

    [Header("Refle Ammunition and Shoot")]
    private float nextTimeShooting = 0f;
    private int MaxAmmo = 20;
    private int presentAmmo;
    private int mag = 15;
    public float reloadingTime = 1.3f;
    private bool setReload = false;

    [Header("Refle Effects")]
    public ParticleSystem muzzleEffects;
    public GameObject hitEffects;
    public GameObject HitEffectEnemy;

    [Header("Sound Shoot")]
    public AudioSource audi;
    public AudioClip shootingSound;
    public AudioClip reloadingSound;

    Objects OBject;

    private void Awake()
    {
        presentAmmo = MaxAmmo ;
    }

    void Update()
    {
        if (setReload) return;//neu setReload Sai thif cu nhu bth

        if(presentAmmo <= 0)//Ammo hien tai be hon k thi nap vo
        {
            StartCoroutine(Reload());
            return;
        }

        if (player.mobileJoystick == true)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeShooting)
            {
                animator.SetBool("Shoot", true);
                animator.SetBool("Idle", false);
                nextTimeShooting = Time.time + 1f / fireCharge;//1/15 = 0.06s, thoi gian ban dan ra
                Shoot();
            }
            else if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetBool("Idle", false);
                animator.SetBool("ShootWalk", true);
            }
            else if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
            {
                animator.SetBool("Idle", false);
                animator.SetBool("IdleAim", true);
                animator.SetBool("ShootWalk", true);
                animator.SetBool("Walk", true);
                animator.SetBool("Reload", false);
            }
            else
            {
                animator.SetBool("Shoot", false);
                animator.SetBool("Idle", true);
                animator.SetBool("ShootWalk", false);
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeShooting)
            {
                animator.SetBool("Shoot", true);
                animator.SetBool("Idle", false);
                nextTimeShooting = Time.time + 1f / fireCharge;//1/15 = 0.06s, thoi gian ban dan ra
                Shoot();
            }
            else if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetBool("Idle", false);
                animator.SetBool("ShootWalk", true);
            }
            else if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
            {
                animator.SetBool("Idle", false);
                animator.SetBool("IdleAim", true);
                animator.SetBool("ShootWalk", true);
                animator.SetBool("Walk", true);
                animator.SetBool("Reload", false);
            }
            else
            {
                animator.SetBool("Shoot", false);
                animator.SetBool("Idle", true);
                animator.SetBool("ShootWalk", false);
            }
        }
    }

    void Shoot()
    {
        if(mag == 0)
        {
            //show Ammo out text
        }

        presentAmmo--;

        if(presentAmmo == 0)//moi lan nap dan lai thi mag(o dan) tru di 1
        {
            mag--;
        }

        //Update UI
        AmmoCount.occurrence.UpdateAmmoText(presentAmmo);
        AmmoCount.occurrence.UpdateMagText(mag);

        audi.PlayOneShot(shootingSound);
        muzzleEffects.Play();
        RaycastHit hitInfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, rangeShoot))//huong di cua Ammo
        {
            Debug.Log(hitInfo.transform.name);
            OBject = hitInfo.transform.GetComponent<Objects>();

            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if(OBject != null)
            {
                OBject.TakeDamage(damage);
                GameObject impactEffect = Instantiate(hitEffects, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactEffect, 0.5f);
            }

            else if(enemy != null)
            {
                enemy.EnemyHitDam(damage);
                GameObject dieEff = Instantiate(HitEffectEnemy, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(dieEff, 0.5f);
            }
        }
    }

    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerRun = 0f;
        setReload = true;//dung lai het ms cho nap dan
        Debug.Log("Reloading...");
        audi.PlayOneShot(reloadingSound);
        animator.SetBool("Reload", true);

        yield return new WaitForSeconds(reloadingTime);
        animator.SetBool("Reload", false);  
        presentAmmo = MaxAmmo;
        AmmoCount.occurrence.UpdateAmmoText(presentAmmo);
        player.playerSpeed = 2f;
        player.playerRun = 3f;
        setReload = false;

    }
}
