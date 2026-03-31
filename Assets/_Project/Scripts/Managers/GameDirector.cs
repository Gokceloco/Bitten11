using UnityEngine;
using UnityEngine.InputSystem;

public class GameDirector : MonoBehaviour
{
    public UIManager uiManager;
    public LevelManager levelManager;
    public Player player;
    public TimeManager timeManager;

    public GameState gameState;

    private void Start()
    {
        uiManager.ShowMainMenu();
    }

    private void Update()
    {
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            RestartLevel();
        }
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            LoadPreviousLevel();
        }
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
    }
    public void RestartLevel()
    {
        gameState = GameState.GamePlay;
        levelManager.RestartLevelManager();
        timeManager.RestartTimeManager(levelManager.GetCurrentLevel().levelTime);
        player.RestartPlayer();
    }
    public void LevelCompleted()
    {
        PlayerPrefs.SetInt("LastLevelReached", levelManager.levelNo + 1);
        //Invoke(nameof(LoadNextLevel), 1);
        uiManager.ShowVictoryUI();
        gameState = GameState.Menu;
        //uIManager.ShowVictoryUI();
    }

    public void LoadNextLevel()
    {
        levelManager.levelNo++;
        RestartLevel();
    }

    public void LoadPreviousLevel()
    {
        levelManager.levelNo--;
        RestartLevel();
    }

    public void LevelFailed()
    {
        //Invoke(nameof(RestartLevel), 1f);
        uiManager.ShowFailUI();
        gameState = GameState.Menu;
        //uIManager.ShowFailUI();
    }
}

public enum GameState
{
    Menu,
    GamePlay,
}