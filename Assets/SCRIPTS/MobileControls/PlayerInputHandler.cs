using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PLayerInputHandler : MonoBehaviour
{
    [SerializeField] private CustomHorizontalController driveControler;
    [SerializeField] private CustomSwipeController handleControler;

    [SerializeField] private RawImage leftImage;
    [SerializeField] private RawImage rightImage;


    [SerializeField] public float controlFadeTime;
    private float currentFadeTime;

    void Start()
    {
        TouchInputManager.TouchInputs input = new TouchInputManager.TouchInputs();
        input.driveControl = driveControler;
        input.handleControl = handleControler;

        TouchInputManager.Get().RegisterInput(input,this.name);

        Color imageColor = handleControler.GetComponent<RawImage>().color;
        if (Application.isEditor)
        {
            imageColor.a = 0.3f;
        }
        else
        {
            imageColor.a = 0;
        }

        handleControler.GetComponent<RawImage>().color = imageColor;

        currentFadeTime = 2 * controlFadeTime;
    }

    private void Update()
    {
        if (currentFadeTime == 0) return;

        Color leftColor = leftImage.color;
        leftColor.a = currentFadeTime / 4.0f;
        leftImage.color = leftColor;

        Color rightColor = rightImage.color;
        rightColor.a = currentFadeTime / 4.0f;
        rightImage.color = rightColor;

        currentFadeTime -= Time.deltaTime;

        if (currentFadeTime < 0) currentFadeTime = 0;
    }
}
