using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        public Animator animator;
        public PlayerController playerController;
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int Attack = Animator.StringToHash("attack");
        private int _direction = 1;
        private Camera mainCamera;

        void Start()
        {
            mainCamera = Camera.main;
            playerController.OnAttackStart += OnAttackStart;
        }

        void OnAttackStart()
        {
            animator.SetTrigger(Attack);
        }

        void Update()
        {
            animator.SetBool(IsRunning, playerController.Velocity.magnitude > 0.1f);

            var mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var mouseDirectionX = mousePos.x - transform.position.x;
            var charDir = mouseDirectionX < 0 ? 1 : -1;
            if (_direction != charDir)
            {
                _direction = charDir;
                var scale = transform.localScale;
                // ReSharper disable once Unity.InefficientPropertyAccess
                transform.localScale = new Vector3(charDir, scale.y, scale.z);
            }
        }
    }
}