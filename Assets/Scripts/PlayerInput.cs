using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Button _jumpButton;

    public event Action OnJumped;

    public event Action<float> OnInputedHorizontal;

    public float CurrentDirectionX;
    
    public float MoveX { get; private set; }

    private void OnEnable()
    {
        _jumpButton.onClick.AddListener(OnJump);
    }

    private void OnDisable()
    {
        _jumpButton.onClick.RemoveListener(OnJump);
    }
    
#if UNITY_EDITOR
    private void Update()
    {
        InputX(Input.GetAxisRaw("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump();
        }
    }
#endif 

    
    private void OnJump()
    {
        OnJumped?.Invoke();
    }
    
    public void InputX(float input)
    {
        MoveX = input;
        CurrentDirectionX = input;
        OnInputedHorizontal?.Invoke(input);
    }

    public void ResetInput()
    {
        MoveX = 0;
    }
}
