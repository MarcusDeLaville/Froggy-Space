using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PhysicsMovement _physicsMovement;
    
    private void Start()
    {
        _physicsMovement.FirstJump += FirstJump;
        _physicsMovement.SecondJump += SecondJump;
    }

    private void Update()
    {
        // bool moveX = _physicsMovement.Velocity.x > 0.1f;
        
        _animator.SetFloat(AnimationStates.WalkX, _physicsMovement.Velocity.x);
        _animator.SetBool(AnimationStates.IsGrounded, _physicsMovement.IsGrounded);
    }

    private void FirstJump()
    {
        PlayAnimationState(AnimationStates.Jump);
    }
    
    private void SecondJump()
    {
        PlayAnimationState(AnimationStates.SecondJump);
    }

    private void PlayAnimationState(string stateName)
    {
        _animator.StopPlayback();
        _animator.Play(stateName);
    }
    
    private struct AnimationStates
    {
        public const string Jump = nameof(Jump);
        public const string SecondJump = nameof(SecondJump);
        public const string WalkX = nameof(WalkX);
        public const string IsGrounded = nameof(IsGrounded);
    }
}
