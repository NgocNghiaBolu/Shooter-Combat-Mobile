using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    [Header("PlayerAI Health anh Damage")]
    public float PlayerAIHealth = 110f;
    public float presentPlayerAI;
    public float giveDamage = 7f;
    public float speedPlayerAI;

    [Header("PlayerAI things")]
    public NavMeshAgent PlayerAIAgent;
    public Transform lookPoint;
    public GameObject ShootingRaycastArea;
    public Transform enemyBody;
    public LayerMask layerEnemy;
    public Transform Spawn;
    public Transform PlayerAICharacter;

    [Header("PlayerAI Animation and Effect")]
    public Animator anim;
    public ParticleSystem muzzleEffect;

    [Header("Sound Shoot")]
    public AudioSource audi;
    public AudioClip shootingSound;

    [Header("PlayerAI Shooting Var")]
    public float timeBtwShoot;
    bool previouslyShoot;   //ktra da tung ban chua

    [Header("PlayerAI States")]
    public float chaseRadius;
    public float shootingRadius;
    public bool enemyInChaseRadius;
    public bool enemyInshootRadius;

    public ScoreManager scoreManager;

    private void Awake()
    {
        PlayerAIAgent = GetComponent<NavMeshAgent>();
        presentPlayerAI = PlayerAIHealth;
    }

    private void Update()
    {
        enemyInChaseRadius = Physics.CheckSphere(transform.position, chaseRadius, layerEnemy);//ktra ng choi cho trong ban kinh san duoi khong
        enemyInshootRadius = Physics.CheckSphere(transform.position, shootingRadius, layerEnemy);// ktra player trong ban kinh de shoot chua

        if (enemyInChaseRadius && !enemyInshootRadius) PursueEnemy();
        if (enemyInChaseRadius && enemyInshootRadius) ShootEnemy();
    }

    private void PursueEnemy()//trong ban kinh thi san duoi play
    {
        if (PlayerAIAgent.SetDestination(enemyBody.position))
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

    private void ShootEnemy()
    {
        PlayerAIAgent.SetDestination(transform.position);

        transform.LookAt(lookPoint);

        if (!previouslyShoot)
        {
            audi.PlayOneShot(shootingSound);
            muzzleEffect.Play();

            RaycastHit hit;

            if (Physics.Raycast(ShootingRaycastArea.transform.position, ShootingRaycastArea.transform.forward, out hit, shootingRadius))
            {
                Debug.Log("PlayerAI hit " + hit.transform.name);
                Enemy enemy = hit.transform.GetComponent<Enemy>();//tham chieu toi Enemyhealth
                if  (enemy != null)
                {
                    enemy.EnemyHitDam(giveDamage);
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

    public void PlayerAIHitDam(float damage)
    {
        presentPlayerAI -= damage;

        if (presentPlayerAI <= 0)
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()//chet roi nen cho stop het
    {
        PlayerAIAgent.SetDestination(transform.position);
        speedPlayerAI = 0f;
        shootingRadius = 0f;
        chaseRadius = 0f;
        enemyInChaseRadius = false;
        enemyInshootRadius = false;

        anim.SetBool("Die", true);
        anim.SetBool("Run", false);
        anim.SetBool("Shoot", false);
        Debug.Log("dead");
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        scoreManager.enemyKills += 1;

        yield return new WaitForSeconds(5f);

        Debug.Log("Respawn");
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        presentPlayerAI = 110f;
        speedPlayerAI = 2.5f;
        chaseRadius = 100f;
        shootingRadius = 8f;
        enemyInChaseRadius = true;
        enemyInshootRadius = false;
        anim.SetBool("Die", false);
        anim.SetBool("Run", true);

        //enemy spawn
        PlayerAICharacter.transform.position = Spawn.transform.position;
        PursueEnemy();
    }
}
