using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static int vsync = 0;

    [SerializeField] private TMPro.TextMeshProUGUI difficultyText;
    [SerializeField] private TMPro.TextMeshProUGUI fullscreenText;
    [SerializeField] private TMPro.TextMeshProUGUI vsyncText;

    public enum Difficulty { Easy,Medium,Hard }

    public static  Difficulty currentDifficulty;

    public static bool singleplayer;

    void Start()
    {
        vsync = QualitySettings.vSyncCount;

        difficultyText.text = currentDifficulty.ToString();
        fullscreenText.text = (Screen.fullScreen ? "Fullscreen: On" : "Fullscreen: Off");
        vsyncText.text = (vsync == 0 ? "Vsync: Off" : "Vsync: On");

        if (Application.isMobilePlatform)
        {
            fullscreenText.gameObject.SetActive(false);
            vsyncText.gameObject.SetActive(false);
        }
    }

    public void OnDifficulty()
    {
        if (currentDifficulty != Difficulty.Hard)
        {
            currentDifficulty++;
        }
        else
        {
            currentDifficulty = Difficulty.Easy;
        }
        difficultyText.text = currentDifficulty.ToString();
    }
    public void OnFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void OnVsync()
    {
        QualitySettings.vSyncCount = (vsync == 1 ? 0 : 1);
        vsync = QualitySettings.vSyncCount;

        vsyncText.text = (vsync == 0 ? "Vsync: Off" : "Vsync: On");
    }
}
