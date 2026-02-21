using System.Collections;
using UnityEngine;


public class Entity : MonoBehaviour
{
   
    protected Animator animator;
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer sr;

    [Header("Health")]
    [SerializeField] private int maxHealth=1;
    [SerializeField] private int currentHealth;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private float damageFeedbackDuration=.1f;
    private Coroutine damageFeedbackCoroutine;

   
    [Header("Attack details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask whatIsTarget;


    [Header("Movement details")]
    [SerializeField] protected float moveSpeed = 3.5f;
    protected bool facingRight=true;
    protected int facingDir=1;
    protected bool canMove=true;

    [Header("Collision details")]
    [SerializeField] private float groundCheckDistance;
    protected bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;



    protected virtual void Awake()
{
    
    rb = GetComponent<Rigidbody2D>();
    col=GetComponent<Collider2D>();
    animator = GetComponentInChildren<Animator>();
    sr=GetComponentInChildren<SpriteRenderer>();


    currentHealth = maxHealth;
}

    protected virtual void Update()
    {
        HandleCollision();
        HandleMovement();
        HandleAnimations();
        HandleFlip();
    }

    public void DamageTargets(){

    Collider2D[] enemyColliders =
        Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsTarget);

    foreach (Collider2D enemy in enemyColliders)
    {
        Entity entityTarget = enemy.GetComponent<Entity>();
        entityTarget.TakeDamage();
    }
}



    private void TakeDamage()
{
    currentHealth--;
    PlayDamageFeedback();

    if (currentHealth <= 0)
        Die();
}


    private void PlayDamageFeedback()
{
    if (damageFeedbackCoroutine != null)
        StopCoroutine(damageFeedbackCoroutine);

    damageFeedbackCoroutine = StartCoroutine(DamageFeedbackCo());
}


    private IEnumerator DamageFeedbackCo(){

        Material originalm=sr.material;

        sr.material=damageMaterial;

        yield return new WaitForSeconds(damageFeedbackDuration);

        sr.material=originalm;

        damageFeedbackCoroutine = null;
    }

    protected virtual void Die(){
        animator.enabled=false;
        col.enabled=false;

        rb.gravityScale=12;
        rb.linearVelocity=new Vector2(rb.linearVelocity.x,15);

        Destroy(gameObject,3);
    }


    public virtual void EnableMovementAndJump (bool enable){
        canMove=enable;
        //canJump=enable;
    }

    protected virtual void HandleAnimations()
    { 
        animator.SetFloat("xVelocity",rb.linearVelocity.x);
        animator.SetFloat("yVelocity",rb.linearVelocity.y);
        animator.SetBool("isGrounded",isGrounded); 
    }
 

    protected virtual void HandleAttack()
    {
        if (isGrounded){
            animator.SetTrigger("attack");
        }
    }

    protected virtual void HandleMovement(){
    }


    protected virtual void HandleCollision(){
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance,whatIsGround);
    }

    protected virtual void HandleFlip(){
        if (rb.linearVelocity.x>0 && !facingRight)
            Flip();
        else if (rb.linearVelocity.x<0 && facingRight)
            Flip();
    }
    public virtual void Flip(){
    facingRight = !facingRight;
    facingDir *= -1;

    // En vez de rotar el transform:
    transform.Rotate(0,180,0);
}

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);

    }
}
