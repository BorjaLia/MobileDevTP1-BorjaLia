using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PLayerInputHandler : MonoBehaviour
{
    [SerializeField] private CustomHorizontalController driveControler;
    [SerializeField] private CustomSwipeController handleControler;

    [SerializeField] private RawImage leftImage;
    [SerializeField] private Image leftBackgorund;
    [SerializeField] private RawImage rightImage;
    [SerializeField] private Image rightBackgorund;


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

        ResetFade();
    }

    private void Update()
    {
        if (currentFadeTime == 0) return;

        Color leftColor = leftImage.color;
        leftColor.a = currentFadeTime / 4.0f;
        leftImage.color = leftColor;

        Color leftBgColor = leftBackgorund.color;
        leftBgColor.a = currentFadeTime / 4.0f;
        leftBackgorund.color = leftBgColor;

        Color rightColor = rightImage.color;
        rightColor.a = currentFadeTime / 4.0f;
        rightImage.color = rightColor;

        Color rightBgColor = rightBackgorund.color;
        rightBgColor.a = currentFadeTime / 4.0f;
        rightBackgorund.color = rightBgColor;

        currentFadeTime -= Time.deltaTime;

        if (currentFadeTime < 0) currentFadeTime = 0;
    }

    public void ResetFade()
    {
        currentFadeTime = 2 * controlFadeTime;
    }
}
