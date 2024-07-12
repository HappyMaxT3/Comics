using UnityEngine;

namespace Main
{
    public class Climb : MonoBehaviour
    {
        [SerializeField] public float climbSpeed = 5f; 
        [SerializeField] public float raycastLength = 0.5f; // Длина луча для определения касания грани
        [SerializeField] public float climbHeight = 1f; 
        private Rigidbody2D rb;
        private Animator animator;
        private bool isClimbing;
        private bool isTouchingLeftEdge;
        private bool isTouchingRightEdge;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if(CanClimb())
            {
                StartClimbing();
            }

            if (isClimbing)
            {
                ClimbUp();
            }
        }

        public void CheckForEdge()
        {
            RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, raycastLength, LayerMask.GetMask("Default"));
            isTouchingLeftEdge = leftHit.collider != null;

            RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, raycastLength, LayerMask.GetMask("Default"));
            isTouchingRightEdge = rightHit.collider != null;
        }

        public bool CanClimb()
        {
            return isTouchingLeftEdge || isTouchingRightEdge;
        }

        public void StartClimbing()
        {
            isClimbing = true;
            rb.velocity = Vector2.zero;
            animator.SetBool("isClimbing", true);
        }

        void ClimbUp()
        {
            float climbStep = climbSpeed * Time.deltaTime;
            transform.Translate(Vector2.up * climbStep);

            if (IsAbovePlatform())
            {
                Vector2 newPosition = transform.position;
                newPosition.y = Mathf.Floor(newPosition.y) + climbHeight;
                transform.position = newPosition;

                StopClimbing();
            }
        }

        bool IsAbovePlatform()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, LayerMask.GetMask("Default"));
            return hit.collider != null;
        }

        void StopClimbing()
        {
            isClimbing = false;
            animator.SetBool("isClimbing", false);
        }
    }

}