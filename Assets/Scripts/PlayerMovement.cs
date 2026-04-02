using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static readonly int HashMove = Animator.StringToHash("Move");

    public float moveSpeed = 5f;

    private Animator playerAnimator;
    private Rigidbody playerRigidbody;
    private PlayerInput playerInput;


    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        playerAnimator.SetFloat(HashMove, 0f);
    }



    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y;

            transform.LookAt(targetPosition);
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, 0f, v);
        direction = Vector3.ClampMagnitude(direction, 1f);
        playerRigidbody.linearVelocity = direction * moveSpeed;

        playerAnimator.SetFloat(HashMove, (direction * moveSpeed).magnitude);


    }

}


