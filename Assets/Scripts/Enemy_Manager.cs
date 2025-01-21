using UnityEngine;
using UnityEngine.AI;


public class Enemy_Manager : MonoBehaviour
{
    public int EnemyHealth = 200;
    public NavMeshAgent enemyAgent;
    public Transform Player;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public Vector3 walkPoint;
    public float walkPointRange;
    public bool walkPointSet;
    public float sightRange, attackRange;
    public bool EnemySightRange, EnemyAttackRange;
    public float attackDelay;
    public bool isAttacking;


    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        EnemySightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        EnemyAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if(!EnemySightRange && !EnemyAttackRange)
        {
            Patrolling();
        }
        if(EnemySightRange && !EnemyAttackRange)
        {
            DetectPlayer();
        }
        else if(EnemySightRange && EnemyAttackRange)
        {
            AttackPlayer();
        }
    }
    void Patrolling()
    {
        if(walkPointSet == false)
        {
            float randomZPos = Random.Range(-walkPointRange, walkPointRange);
            float randomXPos = Random.Range(-walkPointRange, walkPointRange);

            walkPoint =new Vector3(transform.position.x + randomXPos, transform.position.y, transform.position.z +randomZPos);
            if(Physics.Raycast(walkPoint, -transform.up,2f, groundLayer))
            {
               walkPointSet = true;
            }
            
        }
        if(walkPointSet == true)
            {
                enemyAgent.SetDestination(walkPoint);
            }
            Vector3 distanceToWalkPoint = transform.position - walkPoint;
            if(distanceToWalkPoint.magnitude < 1f)
            {
                walkPointSet =false;
            }
    }
    void DetectPlayer()
    {
        enemyAgent.SetDestination(Player.position);
        transform.LookAt(Player);
    }
    void AttackPlayer()
    {
        enemyAgent.SetDestination(transform.position);
        transform.LookAt(Player);


        if(isAttacking == false )
        {
            //Atack Türü
            isAttacking = true;
            Invoke("ResetAttack", attackDelay);
        }

    }
    void ResetAttack()
    {
        isAttacking =false;

    }
    public void EnemyTakeDamage(int DamageAmount) 
    {
        EnemyHealth -= DamageAmount;
        if (EnemyHealth <= 0 )
        {
            EnemyDeath();
        }
    }
    void EnemyDeath()
    {
        Destroy(gameObject);
        
    }
}
