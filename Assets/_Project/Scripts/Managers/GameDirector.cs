using UnityEngine;
using UnityEngine.InputSystem;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;
    public Player player;

    public GameState gameState;

    private void Start()
    {
        RestartLevel();
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartLevel();
        }
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            LoadPreviousLevel();
        }
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
    }
    public void RestartLevel()
    {
        gameState = GameState.GamePlay;
        levelManager.RestartLevelManager();
        player.RestartPlayer();
    }
    public void LevelCompleted()
    {
        PlayerPrefs.SetInt("LastLevelReached", levelManager.levelNo + 1);
        Invoke(nameof(LoadNextLevel), 1);
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
        Invoke(nameof(RestartLevel), 1f);
        gameState = GameState.Menu;
        //uIManager.ShowFailUI();
    }
}

public enum GameState
{
    Menu,
    GamePlay,
}