using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 10f;
    private CharacterCombat combat;
    static Animator anim;
    private Transform target;
    public BarbarianController Barbarian;
    private NavMeshAgent agent;
    public Slider healthBar;
    private float timeLeft = 4.0f;
    private bool Dead = false;
    public Collider other;
    public int TakingDamage = 20;

    // Use this for initialization
    void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        combat = GetComponent<CharacterCombat>();
    }
	
	// Update is called once per frame
	void Update () {
        //depois de morto nao mexe mais
        EnemyMovementSink();
        Die();
    }

    //É colidido pelo Player e perde vida
    public void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.CompareTag("Jogador")) { 
                anim.SetTrigger("isTakingDamage");
            anim.SetBool("isRunning", false);
            healthBar.value -= 20;
        }
    
}

    void EnemyMovementSink()
    {
        if (healthBar.value <= 0) {
            return;
        }
        else { 
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            agent.stoppingDistance = 1.75F;
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);

            if (distance < agent.stoppingDistance)
            {
                anim.SetTrigger("isAttacking");
                anim.SetBool("isRunning", false);
                FaceTarget();
            }
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
        }
    }
    }
    //fica de frente para o Barbarian
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void Die()
    {
        if (healthBar.value <= 0)
        {
            anim.SetTrigger("isDead");
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                Barbarian.score += 20 ;
                Destroy(gameObject);
            }
        }
       
    }

}
