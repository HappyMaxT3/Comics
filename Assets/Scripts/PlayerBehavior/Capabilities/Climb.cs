using UnityEngine;

namespace Main
{
    public class Climb : MonoBehaviour
    {
        [SerializeField] public float climbSpeed = 5f;
        [SerializeField] public float raycastLength = 0.5f;
        [SerializeField] public float climbHeight = 1f;
        [SerializeField] public float climbLength = 1f;
        [SerializeField] public float headOffset = 1f;
        [SerializeField] public float footOffset = 1f;

        private Rigidbody2D rb;
        private Animator animator;
        private bool isClimbing;
        private bool isTouchingLeftEdge;
        private bool isTouchingRightEdge;
        private Ground ground;
        private Vector2 dotY;
        private Vector2 dotX;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            ground = GetComponent<Ground>();
        }

        private void Update()
        {
            CheckForEdge();

            if (CanClimb() && !isClimbing)
            {
                StartClimbing();
            }

            if (isClimbing)
            {
                ClimbUp();
            }
        }

        private void CheckForEdge()
        {
            RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, raycastLength, LayerMask.GetMask("Default"));
            RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, raycastLength, LayerMask.GetMask("Default"));
            RaycastHit2D headLeftHit = Physics2D.Raycast(transform.position + Vector3.up * headOffset, Vector2.left, raycastLength, LayerMask.GetMask("Default"));
            RaycastHit2D headRightHit = Physics2D.Raycast(transform.position + Vector3.up * headOffset, Vector2.right, raycastLength, LayerMask.GetMask("Default"));
            RaycastHit2D footLeftHit = Physics2D.Raycast(transform.position + Vector3.down * footOffset, Vector2.left, raycastLength, LayerMask.GetMask("Default"));
            RaycastHit2D footRightHit = Physics2D.Raycast(transform.position + Vector3.down * footOffset, Vector2.right, raycastLength, LayerMask.GetMask("Default"));

            isTouchingLeftEdge = (leftHit.collider || footLeftHit) && !headLeftHit.collider;
            isTouchingRightEdge = (rightHit.collider || footRightHit) && !headRightHit.collider;
        }

        private bool CanClimb()
        {
            return isTouchingLeftEdge || isTouchingRightEdge;
        }

        private void StartClimbing()
        {
            isClimbing = true;
            rb.velocity = Vector2.zero;
            animator.SetBool("isClimbing", true);

            dotY = new Vector2(transform.position.x, transform.position.y + climbHeight);
            dotX = isTouchingLeftEdge ? new Vector2(transform.position.x - climbLength, dotY.y) : new Vector2(transform.position.x + climbLength, dotY.y);
        }

        private void ClimbUp()
        {
            if (transform.position.y < dotY.y)
            {
                float climbStep = climbSpeed * Time.deltaTime;
                transform.Translate(Vector2.up * climbStep);
            }
            else
            {
                float climbStep = climbSpeed * Time.deltaTime;
                Vector2 direction = isTouchingLeftEdge ? Vector2.left : Vector2.right;
                transform.Translate(direction * climbStep);

                if ((isTouchingLeftEdge && transform.position.x <= dotX.x) || (!isTouchingLeftEdge && transform.position.x >= dotX.x))
                {
                    StopClimbing();
                }
            }

            if (ground.OnGround)
            {
                StopClimbing();
            }
        }

        private void StopClimbing()
        {
            isClimbing = false;
            animator.SetBool("isClimbing", false);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, raycastLength, LayerMask.GetMask("Default"));
            RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, raycastLength, LayerMask.GetMask("Default"));
            RaycastHit2D headLeftHit = Physics2D.Raycast(transform.position + Vector3.up * headOffset, Vector2.left, raycastLength, LayerMask.GetMask("Default"));
            RaycastHit2D headRightHit = Physics2D.Raycast(transform.position + Vector3.up * headOffset, Vector2.right, raycastLength, LayerMask.GetMask("Default"));
            RaycastHit2D footLeftHit = Physics2D.Raycast(transform.position - Vector3.down * footOffset, Vector2.left, raycastLength, LayerMask.GetMask("Default"));
            RaycastHit2D footRightHit = Physics2D.Raycast(transform.position - Vector3.down * footOffset, Vector2.right, raycastLength, LayerMask.GetMask("Default"));

            Gizmos.color = leftHit ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.left * raycastLength);

            Gizmos.color = rightHit ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * raycastLength);

            Gizmos.color = headLeftHit ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position + Vector3.up * headOffset, transform.position + Vector3.up * headOffset + Vector3.left * raycastLength);

            Gizmos.color = headRightHit ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position + Vector3.up * headOffset, transform.position + Vector3.up * headOffset + Vector3.right * raycastLength);

            Gizmos.color = footLeftHit ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position + Vector3.down * footOffset, transform.position + Vector3.down * footOffset + Vector3.left * raycastLength);

            Gizmos.color = footRightHit ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position + Vector3.down * footOffset, transform.position + Vector3.down * footOffset + Vector3.right * raycastLength);
        }
    }
}
