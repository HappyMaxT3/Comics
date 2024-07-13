using UnityEngine;

namespace Main
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class WallSlide : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] private float _slideSpeed = 2f;
        [SerializeField, Range(0f, 1f)] private float _raycastLength = 0.2f;

        private Rigidbody2D _body;
        private Animator _animator;
        private bool _isSliding;
        private bool _onWall;
        private Ground ground;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            ground = GetComponent<Ground>();
        }

        private void Update()
        {
            CheckForWall();
            HandleWallSlide();
        }

        private void CheckForWall()
        {
            RaycastHit2D leftWallHit = Physics2D.Raycast(transform.position, Vector2.left, _raycastLength, LayerMask.GetMask("Default"));
            RaycastHit2D rightWallHit = Physics2D.Raycast(transform.position, Vector2.right, _raycastLength, LayerMask.GetMask("Default"));

            _onWall = leftWallHit.collider != null || rightWallHit.collider != null;
        }

        private void HandleWallSlide()
        {
            if (_onWall && !ground.OnGround)
            {
                _isSliding = true;
                _animator.SetBool("isSliding", true);
                _body.velocity = new Vector2(_body.velocity.y, -_slideSpeed);
            }
            else
            {
                _isSliding = false;
                _animator.SetBool("isSliding", false);
            }
        }
    }
}
