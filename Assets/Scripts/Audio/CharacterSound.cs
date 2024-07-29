using Main;
using System;
using UnityEngine;

namespace BHSCamp
{
    [RequireComponent(typeof(AudioSource))]
    public class CharacterSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _jumpSound;
        [SerializeField] private AudioClip _attackSound;
        [SerializeField] private AudioClip _hurtSound;
        [SerializeField] private AudioClip _runSound;
        [SerializeField] private AudioClip _deathSound;
        [SerializeField] private AudioClip _rollSound;
        [SerializeField] private AudioClip _climbSound;
        [SerializeField] private AudioClip _slideSound;
        private AudioSource _audioSource;
        private Ground _ground;
        private float _inputX;
        private bool _isRunngin;

        private void Awake()
        {
            _ground = GetComponent<Ground>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            ////just started running
            //if (false == _isRunngin && _ground.OnGround && 0 != _inputX)
            //{
            //    _isRunngin = true;
            //    _audioSource.clip = _runSound;
            //    _audioSource.Play(0);
            //}
            ////finish running or jump
            //if (_isRunngin && (false == _ground.OnGround || 0 == _inputX))
            //{
            //    _isRunngin = false;
            //    _audioSource.Stop();
            //}
        }

        public void PlayJumpSound()
        {
            if (_jumpSound != null)
                _audioSource.PlayOneShot(_jumpSound);
        }

        public void PlayAttackSound()
        {
            if (_attackSound != null)
                _audioSource.PlayOneShot(_attackSound);
        }

        public void PlayHurtSound()
        {
            if (_hurtSound != null)
                _audioSource.PlayOneShot(_hurtSound);
        }

        public void PlayDeathSound()
        {
            if (_deathSound != null)
                _audioSource.PlayOneShot(_deathSound);
        }

        public void PlayRollSound()
        {
            if(_rollSound != null)
            {
               _audioSource.PlayOneShot(_rollSound);
            }
        }

        public void PlayClimbSound()
        {
            if (_deathSound != null)
                _audioSource.PlayOneShot(_climbSound);
        }

        public void PlaySlideSound()
        {
            if (_rollSound != null)
            {
                _audioSource.PlayOneShot(_slideSound);
            }
        }

        public void PlayRunSound()
        {
            _audioSource.PlayOneShot(_runSound);
        }
    }
}