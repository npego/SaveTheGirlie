using UnityEngine;

public class Player : Entity
{

    private float xInput;
    [SerializeField] private float jumpForce = 8;
    private bool canJump=true;

    protected override void HandleMovement(){
         if(canMove)
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

     private void TryToJump()
    {
        if(isGrounded && canJump)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    protected override void Update(){
        base.Update();
        HandleInput();
    }

    private void HandleInput()
{
    // 1) Leer del móvil
    xInput = MobileButtons.MoveAxis;

    // 2) Si no hay input móvil (0), usar teclado para testear en PC
    if (Mathf.Approximately(xInput, 0f))
        xInput = Input.GetAxisRaw("Horizontal");

    // Teclado (solo para pruebas)
    if (Input.GetKeyDown(KeyCode.Space))
        TryToJump();

   
}

      public override void EnableMovementAndJump (bool enable){
        base.EnableMovementAndJump(enable);
        canJump=enable;
    }

    public void ButtonJump()
{
    TryToJump();
}

public void ButtonAttack()
{
    HandleAttack();
}

    protected override void Die(){
        base.Die();
        UI.instance.EnableGameOverUI();
    }
    
}
