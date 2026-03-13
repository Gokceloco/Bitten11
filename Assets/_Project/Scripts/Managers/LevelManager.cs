using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameDirector gameDirector;

    private Level _curLevel;

    public List<Level> levelPrefabs;

    public int levelNo;

    public void RestartLevelManager()
    {
        levelNo = Mathf.Clamp(levelNo, 1, levelPrefabs.Count);
        DeleteOldLevel();
        CreateNewLevel();
    }

    private void DeleteOldLevel()
    {
        if (_curLevel != null)
        {
            Destroy(_curLevel.gameObject);
        }
    }
    private void CreateNewLevel()
    {
        _curLevel = Instantiate(levelPrefabs[levelNo-1]);
    }    
}
