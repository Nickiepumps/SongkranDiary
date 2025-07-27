using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "LevelData")]
public class LevelDataSO : ScriptableObject
{
    public string LevelName;
    public string UnitySceneName;
    public Sprite LevelImagePostcard;
    public GameObject[] isoLevelBoundary;
    public Transform nextLevel;
    public bool isClear = false;
    public bool isClearFirstTime = true;
}
