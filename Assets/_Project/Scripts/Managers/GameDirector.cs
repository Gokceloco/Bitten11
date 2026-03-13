using UnityEngine;
using UnityEngine.InputSystem;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;
    public Player player;

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
        levelManager.RestartLevelManager();
        player.RestartPlayer();
    }
    public void LevelCompleted()
    {
        PlayerPrefs.SetInt("LastLevelReached", levelManager.levelNo + 1);
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
        //uIManager.ShowFailUI();
    }
}
