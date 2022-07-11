using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text _recordText;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Record"))
            _recordText.text = "Рекорд: " + PlayerPrefs.GetInt("Record").ToString();
        else
            _recordText.text = "";
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}