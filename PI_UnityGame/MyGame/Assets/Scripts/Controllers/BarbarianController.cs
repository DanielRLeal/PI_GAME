using UnityEngine;
using UnityEngine.UI;



public class BarbarianController : MonoBehaviour
{
    private bool hasCollided = false;
    public LayerMask movementMask;
    static Animator anim;
    public float speed = 10.0F;
    public float rotationSpeed = 100.0F;
    public Collider other;
    public Slider healthBar;
    public WinePiilBottle wine;
    public int score = 0;
    float translation;
    float rotation;
    public int TakingDamage = 20;
    public DeathMenu DeathMenu;
    private float TimeLeftToDeathMenu = 2f;
    bool onGround = true;
    private Rigidbody rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
      

    // Update is called once per frame
    void Update()
    {
        StopDeadBody();
        Die();
    }

    void BarbarianControllers()
    {
        //apanhar caixas
        if (Input.GetKeyDown(KeyCode.E) && hasCollided == true)
        {
            //MagicBoxSurprise();     
            Destroy(other.gameObject);
        }

        //diferentes ataques
        if (Input.GetButtonDown("Fire3"))
        {
            int r = UnityEngine.Random.Range(0, 2);
            if (r.Equals(0))
            {
                anim.SetTrigger("isRoundKicking");
                anim.SetBool("isIdle", false);

            }
            if (r.Equals(1))
            {
                anim.SetTrigger("isLeftUpper");
                anim.SetBool("isIdle", false);
            }
            if (r.Equals(2))
            {
                anim.SetTrigger("isRightHook");
                anim.SetBool("isIdle", false);
            }
        }

        //andar
        if (translation != 0)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", true);
        }

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("isJumping", true);
        }
    }

    void BarbarianMovement()
    {
        translation = Input.GetAxis("Vertical") * speed;
        rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        this.other = other;
        if (other.gameObject.CompareTag("Item"))
        {
            hasCollided = true;
            Debug.Log("Collide");
        }

        if (other.gameObject.CompareTag("Inimigos"))
        {
            healthBar.value -= TakingDamage;
        }

        if (other.gameObject.CompareTag("HealthItem") && healthBar.value<100) { 

            healthBar.value += 20;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Money"))
        {
            score += 5;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Terreno"))
        {
            onGround = true;

        }

    }

    void OnTriggerExit(Collider other)
    {
        hasCollided = false;
    }

    void Die()
    {
        if (healthBar.value <= 0)
        {
            anim.SetTrigger("isDead");
            TimeLeftToDeathMenu -= Time.deltaTime;
            if (TimeLeftToDeathMenu < 0)
            {
                DeathMenu.ToggleEndMenu(score);
            }
        }

    }

    void StopDeadBody()
    {
        if (healthBar.value <= 0)
        {
            return;
        }
        else
        {
            BarbarianControllers();
            BarbarianMovement();
        }
    }

    public void JumpIt()
    {
        if (onGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector3(0f, 5f, 0f);
                onGround = false;
            }
        }
    }

    //public void MagicBoxSurprise()
    //{
    //    ////que probabilidades adicionar?
    //    int r = UnityEngine.Random.Range(0, 2);
    //    if (r.Equals(0))
    //        Enemy.healthBar.value = 0;
    //    Debug.Log("calhou 0");
    //    if (r.Equals(1))
    //        Debug.Log("calhou 1");

    //    if (r.Equals(2))
    //        Debug.Log("calhou 2");
    //}

}

