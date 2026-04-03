using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameDirector gameDirector;

    public MainMenu mainMenu;
    public VictoryUI victoryUI;
    public FailUI failUI;
    public ProgressionUI progressionUI;
    public TimerUI timerUI;

    private bool _isGamePaused;

    public void ShowMainMenu()
    {
        mainMenu.Show();
        victoryUI.Hide();
        failUI.Hide();
        HideInGameUI();
    }

    public void ShowInGameUI()
    {
        progressionUI.Show(0);
        timerUI.Show(0);
    }

    public void HideInGameUI()
    {
        progressionUI.Hide();
        timerUI.Hide();
    }

    public void SetLevelTMP(int levelNo)
    {
        progressionUI.SetLevelNo(levelNo);
    }
    public void ShowVictoryUI()
    {
        victoryUI.Show(1.5f);
        HideInGameUI();
    }
    public void UpdateTimerUI(float levelTime, float remainingTime)
    {
        timerUI.SetRemainingTime(levelTime, remainingTime);
    }
    public void ShowFailUI()
    {
        failUI.Show(1f);
        HideInGameUI();
    }

    public void StartGameButtonPressed()
    {
        mainMenu.Hide();
        gameDirector.RestartLevel();
        SetLevelTMP(gameDirector.levelManager.levelNo);
        ShowInGameUI();
    }
    public void LoadNextLevelButtonPressed()
    {
        victoryUI.Hide();
        gameDirector.LoadNextLevel();
        ShowInGameUI();
    }    
    public void RetryButtonPressed()
    {
        failUI.Hide();
        gameDirector.RestartLevel();
        ShowInGameUI();
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
