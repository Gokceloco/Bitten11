using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float _remainingTime;
    private float _levelTime;

    public GameDirector gameDirector;

    public void RestartTimeManager(float levelTime)
    {
        _levelTime = levelTime;
        _remainingTime = levelTime;
    }

    private void Update()
    {
        if (gameDirector.gameState == GameState.GamePlay)
        {
            _remainingTime -= Time.deltaTime;
            print(_remainingTime);
        }
        
        if (_remainingTime < 0 && gameDirector.gameState == GameState.GamePlay) 
        {
            gameDirector.LevelFailed();
        }

        gameDirector.uiManager.UpdateTimerUI(_levelTime, _remainingTime);
    }
}
