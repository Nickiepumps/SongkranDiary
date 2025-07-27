using System.Collections.Generic;

[System.Serializable]
public class StageClearData
{
    public List<LevelDataSO> levelData = new List<LevelDataSO>();
    public string mapName;
    public bool Side_L1_Run;
    public bool Side_L1_Boss;
    public bool ISO_L1;
    public bool Side_L2_Run;
    public bool Side_L2_Boss;
    public bool ISO_L2;
    public bool Side_L3_Run;
    public bool Side_L3_Boss;
    public bool ISO_L3;
}
