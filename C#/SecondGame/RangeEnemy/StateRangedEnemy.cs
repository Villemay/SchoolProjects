using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRangedEnemy : MonoBehaviour
{

    private IRangedEnemyState currentState;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private IdleState idleState;
    private ChaseState chaseState;
    private ShootState shootState;
    private DeathState deathState;
    [SerializeField] private Transform chaseTarget;
    [SerializeField] private float agroRange = 5f;
    [SerializeField] private float shootingRange = 2f;
    [SerializeField] private float loseAgroRange = 20f;

    [SerializeField] private GameObject plasmaBall;
    [SerializeField] private GameObject plasmaBallSpawnPoint;
    [SerializeField] private GameObject projectileDirection;
    [SerializeField] public int projectileDmg = 10;
    [SerializeField] private float plasmaBallSpeed = 300f;
    [SerializeField] private float rangedShootRate = 1f;
    [SerializeField] private float nextShootTime = 1f;
    [SerializeField] private float ProjectileFlyTime;

    [SerializeField]
    [FMODUnity.EventRef]
    private string aAggro;

    private Animator animator;
    private bool wait=false;

    private void Awake()
    {
        idleState = new IdleState(this);
        chaseState = new ChaseState(this);
        shootState = new ShootState(this);
        deathState = new DeathState(this);

        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }



    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState;
        animator = GetComponent<Animator>();
        chaseTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    public void setStateToChase()
    {
        currentState = chaseState;
    }
    public void setStateToIdle()
    {
        currentState = idleState;
    }
    public void setStateToShoot()
    {
        currentState = shootState;
    }
    public void setStateToDeath()
    {
        currentState = deathState;
    }

    public void setNavMeshToMove()
    {
        if (chaseTarget != null)
        {
            navMeshAgent.speed = 3.5f;
            navMeshAgent.destination = chaseTarget.position;
            navMeshAgent.isStopped = false;
        }

    }
    public float getTargetDistance()
    {
        if (chaseTarget != null)
        {
            return (Vector3.Distance(chaseTarget.position, this.transform.position));
        }
        else
        {
            return 0;
        }
    }
    public float getShootingRange()
    {
        return shootingRange;
    }
    public void stopEnemyMovement()
    {
        navMeshAgent.speed = 0f;
        navMeshAgent.isStopped = true;
    }
    public void ResetEnemyMovementSpeed()
    {
        navMeshAgent.speed = 3.5f;
    }
    public float getLoseAgroDistance()
    {
        return loseAgroRange;
    }
    public float getTargetPositionX()
    {
        if (chaseTarget != null)
        {
            return chaseTarget.position.x;
        }
        else
        {
            return 0;
        }
    }
    public float getEnemyPositionY()
    {
        return this.transform.position.y;
    }
    public float getTargetPositionZ()
    {
        if (chaseTarget != null)
        {
            return chaseTarget.position.z;
        }
        else
        {
            return 0;
        }
    }
    public void lookAtTarget(Vector3 target)
    {
        transform.LookAt(target);
    }
    public float getNextShootTime()
    {
        return nextShootTime;
    }
    public float getAgroRange()
    {
        return agroRange;
    }
    public Animator getAnimator()
    {
        return animator;
    }
    public void agroIsready()
    {
        animator.SetBool("isMoving", true);
        setStateToChase();

    }
    public void attackTheMiddle()
    {
        shootAtPlayer();
    }

    public void shootAtPlayer()
    {
        if(chaseTarget != null)
        {
            StartCoroutine(WaitAfterShooting());
            Vector3 relativePos = projectileDirection.transform.position - transform.position;
            GameObject plasma = Instantiate(plasmaBall, plasmaBallSpawnPoint.transform.position, Quaternion.LookRotation(relativePos)) as GameObject;
            plasma.GetComponent<EnemyProjectile>().damageAmount = projectileDmg;
            plasma.GetComponent<Rigidbody>().AddForce(transform.forward * plasmaBallSpeed);

            Destroy(plasma, ProjectileFlyTime);
        }

        nextShootTime = Time.time + rangedShootRate;
    }

    public void SetTarget(GameObject player)
    {
        chaseTarget = player.gameObject.transform;
    }

    public bool getWait()
    {
        return wait;
    }

    public void aggroSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(aAggro);
    }

    private IEnumerator WaitAfterShooting()
    {
        navMeshAgent.speed = 0f;
        navMeshAgent.isStopped = true;
        wait = true;
        yield return new WaitForSeconds(1f);
        wait = false;
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = 3.5f;
    }

}
