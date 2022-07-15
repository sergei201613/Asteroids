using TeaGames.Asteroids;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text _recordText;
    [SerializeField] private Text _coinsText;
    [SerializeField] private PlayerData _playerData;

    private void OnEnable()
    {
        UpdateView();
    }

    private void UpdateView()
    {
        if (PlayerPrefs.HasKey("Record"))
            _recordText.text = "Рекорд: " + PlayerPrefs.GetInt("Record").ToString();
        else
            _recordText.text = "";

        _coinsText.text = _playerData.Coins.ToString();
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