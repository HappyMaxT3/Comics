using UnityEngine;

namespace Main
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _body;
        private Ground _ground;
        private Controller _controller;
        private float _inputX;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _body = GetComponent<Rigidbody2D>();
            _ground = GetComponent<Ground>();
            _controller = GetComponent<Controller>();
        }

        private void Update()
        {
            _inputX = _controller.input.RetrieveMoveInput();
            if (_inputX != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(_inputX), transform.localScale.y, transform.localScale.z);
            }

            _animator.SetFloat("VelocityX", Mathf.Abs(_body.velocity.x));
            _animator.SetFloat("VelocityY", _body.velocity.y);
            _animator.SetBool("IsJumping", !_ground.OnGround);
 
        }

    }
}
