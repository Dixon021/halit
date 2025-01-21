using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public CharacterController characterController;
    public float Speed = 10f;
    public float gravity = -15f;
    public int PlayerHealth = 100;
    private Vector3 gravityVector;
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.35f;
    public LayerMask groundLayer;
    public bool isGrounded = false; 
    public float jumpSpeed = 6f;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
       MovePlayer();
       GroundCheck();
       JumpAndGravity();
        
    }
    void MovePlayer()
    {
        Vector3 moveVector = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        characterController.Move(moveVector * Speed * Time.deltaTime);
    }
    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheckPoint.position,groundCheckRadius,groundLayer);
    }
    void JumpAndGravity()
    {
        gravityVector.y += gravity * Time.deltaTime;
        characterController.Move(gravityVector * Time.deltaTime);
       
        if (isGrounded && gravityVector.y < 0)
        {
            gravityVector.y = -3f;
        }
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            gravityVector.y = jumpSpeed;
        }   
    }
    void PlayerTakeDamage(int DamageAmount)
    {
        PlayerHealth -= DamageAmount;
        if (PlayerHealth <= 0)
        {
            PlayerDeath();
        }
    }
    void PlayerDeath()
    {

    }
}
