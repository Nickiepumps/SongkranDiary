using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "LevelData")]
public class LevelDataSO : ScriptableObject
{
    public string levelName;
    public string unitySceneName;
    public Sprite levelImagePostcard;
    public GameObject[] isoLevelBoundary;
    public Transform nextLevel;
    public bool isClear = false;
    public bool isClearFirstTime = true;
}
