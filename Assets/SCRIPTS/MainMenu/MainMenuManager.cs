using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    [SerializeField] private GameObject loadingScreen;

    [SerializeField] private string gameplaySceneName;

    void Start()
    {
        ToMainMenu();
    }

    void Update()
    {

    }
    public void ToMainMenu()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);

        loadingScreen.SetActive(false);
    }

    public void OnSingleplayer()
    {

        StartGame();
    }

    public void OnMultiplayer()
    {

        StartGame();
    }

    public void StartGame()
    {
        loadingScreen.SetActive(true);


        SceneManager.LoadSceneAsync(gameplaySceneName);
    }

    public void OnSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }
    public void OnCredits()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }


    public void OnExit()
    {
        Application.Quit();
    }
}
