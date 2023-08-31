using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Health anh Damage")]
    public float enemyHealth = 110f;
    public float presentEnemy;
    public float giveDamage = 5f;
    public float speedEnemy;

    [Header("Enemy things")]
    public NavMeshAgent enemyAgent;
    public Transform lookPoint;
    public GameObject ShootingRaycastArea;
    public Transform playerBody;
    public LayerMask layerPlayer;
    public Transform Spawn;
    public Transform enemyCharacter;

    [Header("Enemy Animation and Effect")]
    public Animator anim;
    public ParticleSystem muzzlEffect;

    [Header("Sound Shoot")]
    public AudioSource audi;
    public AudioClip shootingSound;

    [Header("Enemy Shooting Var")]
    public float timeBtwShoot;
    bool previouslyShoot;   //ktra da tung ban chua

    [Header("Enemy States")]
    public float chaseRadius;
    public float shootingRadius;
    public bool playerInChaseRadius;
    public bool playerInshootRadius;
    public bool isPlayer = false;

    public ScoreManager scoreManager;

    private void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        presentEnemy = enemyHealth;
    }

    private void Update()
    {
        playerInChaseRadius = Physics.CheckSphere(transform.position, chaseRadius, layerPlayer);//ktra ng choi cho trong ban kinh san duoi khong
        playerInshootRadius = Physics.CheckSphere(transform.position, shootingRadius, layerPlayer);// ktra player trong ban kinh de shoot chua

        if (playerInChaseRadius && !playerInshootRadius) PursuePlayer();
        if (playerInChaseRadius && playerInshootRadius) ShootPlayer();
    }

    private void PursuePlayer()//trong ban kinh thi san duoi play
    {
        if (enemyAgent.SetDestination(playerBody.position)) 
        {
            anim.SetBool("Run", true);
            anim.SetBool("Shoot", false);
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Shoot", false);
        }
    }

    private void ShootPlayer()
    {
        enemyAgent.SetDestination(transform.position);

        transform.LookAt(lookPoint);

        if (!previouslyShoot)
        {
            audi.PlayOneShot(shootingSound);
            muzzlEffect.Play();

            RaycastHit hit;

            if (Physics.Raycast(ShootingRaycastArea.transform.position, ShootingRaycastArea.transform.forward, out hit, shootingRadius))
            {
                Debug.Log("Enemy hit " + hit.transform.name);

                if (isPlayer == true )//neu ban trung ng choi real thi xay ra
                {
                    PlayerHealth playerBody = hit.transform.GetComponent<PlayerHealth>();//tham chieu toi playerhealth
                    if (playerBody != null)
                    {
                        playerBody.PlayerHitDam(giveDamage);
                    }
                }
                else 
                {
                    PlayerAI playerBD = hit.transform.GetComponent<PlayerAI>();
                    if (playerBD != null)
                    {
                        playerBD.PlayerAIHitDam(giveDamage);
                    }
                }
                
            }
            //ani
            anim.SetBool("Run", false);
            anim.SetBool("Shoot", true);

            previouslyShoot = true;//neu da shoot thi coi nhu da shoot
            Invoke(nameof(ActiveShoot), timeBtwShoot);//the nen viec Shoot k con nhu truoc do, va se co khoag cach moi lan enemy ban ra
        }
    }

    private void ActiveShoot()
    {
        previouslyShoot = false;
    }

    public void EnemyHitDam(float damage)
    {
        presentEnemy -= damage;

        if(presentEnemy <= 0)
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()//chet roi nen cho stop het
    {
        enemyAgent.SetDestination(transform.position);
        speedEnemy = 0f;
        shootingRadius = 0f;
        chaseRadius = 0f;   
        playerInChaseRadius = false;
        playerInshootRadius = false;
        anim.SetBool("Die", true);
        anim.SetBool("Run", false);
        anim.SetBool("Shoot", false);

        Debug.Log("dead");
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        scoreManager.playerKills += 1;

        yield return new WaitForSeconds(5f);

        Debug.Log("Respawn");
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        presentEnemy = 100f;
        speedEnemy = 2.5f;
        chaseRadius = 100f;
        shootingRadius = 8f;
        playerInChaseRadius = true;
        playerInshootRadius = false;
        anim.SetBool("Die", false);
        anim.SetBool("Run", true);

        //enemy spawn
        enemyCharacter.transform.position = Spawn.transform.position;
        PursuePlayer();
    }
}
