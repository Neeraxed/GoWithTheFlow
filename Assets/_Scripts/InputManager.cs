using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;    
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;

    private TouchControls touchControls;

    private void Awake()
    {
        touchControls = new TouchControls(); 
    }

    private void OnEnable()
    {
        Debug.Log("touch controls enabled");
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);        
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log($"Touch started {touchControls.Touch.TouchPosition.ReadValue<Vector2>()}");

        if (OnStartTouch != null)
            OnStartTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch ended");
        if (OnEndTouch != null)
            OnEndTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
    }
}
