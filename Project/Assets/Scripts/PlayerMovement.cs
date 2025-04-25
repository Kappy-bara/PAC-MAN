using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveDirection = Vector2.right; 
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        HandleInput();
        HandleTeleport();
    }
    void FixedUpdate()
    {
        MoveCharacter();
    }
    private void HandleTeleport()
    {
        if (transform.position.x < 0f) transform.position = new Vector3(165f,transform.position.y,transform.position.z);
        else if(transform.position.x > 170f) transform.position = new Vector3(5f,transform.position.y, transform.position.z);
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            moveDirection = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            moveDirection = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            moveDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            moveDirection = Vector2.right;
        }
    }
    private void MoveCharacter()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}
