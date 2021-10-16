using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : Singleton<InputManager>
{
    private PlayerControls playerControls;

    void Awake(){
        playerControls = new PlayerControls();
    }

    void OnEnable(){
        playerControls.Enable();
    }
    void OnDisable(){
        playerControls.Disable();
    }

    void Start(){
        playerControls.Mouse.TouchInput.started += ctx => StartTouch(ctx);
        playerControls.Mouse.TouchInput.canceled -= ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext context){
        Debug.Log("Touch Started" + context.ReadValue<float>());
    }

    private void EndTouch(InputAction.CallbackContext context){
        Debug.Log("Touch Ended" + context.ReadValue<float>());
    }

}
