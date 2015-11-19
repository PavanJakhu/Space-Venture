using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TryAgain : MonoBehaviour
{
    public Button tryAgainButton;

    public void TryAgainOnClick()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void SetTryAgainButtonActive(bool value)
    {
        tryAgainButton.gameObject.SetActive(value);
    }
}
