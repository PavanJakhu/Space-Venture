using UnityEngine;
using System.Collections;

enum Resolution
{
    R_640_720,
    R_480_720,
    R_720_576,
    R_800_600,
    R_1024_768,
    R_1152_864,
    R_1280_720,
    R_1280_768,
    R_1280_960,
    R_1280_1024,
    R_1600_900,
    R_1680_1050,
    R_1920_1080
}

public class StartScreen : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas optionsMenu;

    private bool fullscreen;

    void Start()
    {
        fullscreen = Screen.fullScreen;
    }

    public void PlayButtonClick()
    {
        Application.LoadLevel("GamePlay");
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }

    public void OptionsButtonClick()
    {
        mainMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void BackButtonClick()
    {
        mainMenu.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
    }

    public void ResolutionClick(int value)
    {
        switch ((Resolution)value)
        {
            case Resolution.R_640_720:
                Screen.SetResolution(640, 720, fullscreen);
                break;
            case Resolution.R_480_720:
                Screen.SetResolution(480, 720, fullscreen);
                break;
            case Resolution.R_720_576:
                Screen.SetResolution(720, 576, fullscreen);
                break;
            case Resolution.R_800_600:
                Screen.SetResolution(800, 600, fullscreen);
                break;
            case Resolution.R_1024_768:
                Screen.SetResolution(1024, 768, fullscreen);
                break;
            case Resolution.R_1152_864:
                Screen.SetResolution(1152, 864, fullscreen);
                break;
            case Resolution.R_1280_720:
                Screen.SetResolution(1280, 720, fullscreen);
                break;
            case Resolution.R_1280_768:
                Screen.SetResolution(1280, 768, fullscreen);
                break;
            case Resolution.R_1280_960:
                Screen.SetResolution(1280, 960, fullscreen);
                break;
            case Resolution.R_1280_1024:
                Screen.SetResolution(1280, 1024, fullscreen);
                break;
            case Resolution.R_1600_900:
                Screen.SetResolution(1600, 900, fullscreen);
                break;
            case Resolution.R_1680_1050:
                Screen.SetResolution(1680, 1050, fullscreen);
                break;
            case Resolution.R_1920_1080:
                Screen.SetResolution(1920, 1080, fullscreen);
                break;
            default:
                break;
        }
    }
}
