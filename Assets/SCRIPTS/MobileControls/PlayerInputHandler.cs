using UnityEngine;
using UnityEngine.InputSystem;

public class PLayerInputHandler : MonoBehaviour
{
    [SerializeField] private CustomHorizontalController driveControler;
    [SerializeField] private CustomSwipeController handleControler;

    void Start()
    {
        TouchInputManager.TouchInputs input = new TouchInputManager.TouchInputs();
        input.driveControl = driveControler;
        input.handleControl = handleControler;

        TouchInputManager.Get().RegisterInput(input);
    }
}
