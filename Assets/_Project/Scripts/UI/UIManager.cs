using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameDirector gameDirector;

    public MainMenu mainMenu;
    public VictoryUI victoryUI;
    public FailUI failUI;
    public ProgressionUI progressionUI;

    private bool _isGamePaused;

    public void ShowMainMenu()
    {
        mainMenu.Show();
        victoryUI.Hide();
        failUI.Hide();
        progressionUI.Hide();
    }

    public void ShowInGameUI()
    {
        progressionUI.Show(0);
    }

    public void SetLevelTMP(int levelNo)
    {
        progressionUI.SetLevelNo(levelNo);
    }
    public void ShowVictoryUI()
    {
        victoryUI.Show(1.5f);
    }
    public void ShowFailUI()
    {
        failUI.Show(1f);
    }

    public void StartGameButtonPressed()
    {
        mainMenu.Hide();
        gameDirector.RestartLevel();
    }
    public void LoadNextLevelButtonPressed()
    {
        victoryUI.Hide();
        gameDirector.LoadNextLevel();
    }    
    public void RetryButtonPressed()
    {
        failUI.Hide();
        gameDirector.RestartLevel();
    }

    public void PauseButtonPressed()
    {
        if (_isGamePaused)
        {
            Time.timeScale = 1;
            _isGamePaused = false;
        }
        else
        {
            Time.timeScale = 0;
            _isGamePaused = true;
        }
    }
}
