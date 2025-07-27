using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumDataHandler : MonoBehaviour
{
    public static AlbumDataHandler instance;
    
    [Header("Album Display Controller")]
    [SerializeField] private AlbumDisplayController albumDisplayController;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SaveAlbumData()
    {
        if (Directory.Exists(Application.dataPath) == false)
        {
            Directory.CreateDirectory(Application.dataPath);
        }
        AlbumData albumData = new AlbumData();
        for(int i = 0; i < albumData.albumImagesSO.Count; i++)
        {
            albumData.albumImagesSO[i] = albumDisplayController.albumDisplaySlotArr[i].albumImageSO;
        }
        string albumDataToJson = JsonUtility.ToJson(albumData);
        File.WriteAllText(Application.dataPath + "/albumData.json", albumDataToJson);
    }
    public void SaveAlbumData(SideScroll_AlbumController sidescrollAlbumController)
    {
        if (Directory.Exists(Application.dataPath) == false)
        {
            Directory.CreateDirectory(Application.dataPath);
        }
        AlbumData albumData = new AlbumData();
        for (int i = 0; i < sidescrollAlbumController.albumImageArr.Length; i++)
        {
            albumData.albumImagesSO.Add(sidescrollAlbumController.albumImageArr[i]);
        }
        string albumDataToJson = JsonUtility.ToJson(albumData);
        File.WriteAllText(Application.dataPath + "/albumData.json", albumDataToJson);
    }
    public AlbumData LoadAlbumData()
    {
        if(File.Exists(Application.dataPath + "/albumData.json") == false)
        {
            return null;
        }
        string loadedAlbumDataJson = File.ReadAllText(Application.dataPath + "/albumData.json");
        AlbumData loadedAlbumData = JsonUtility.FromJson<AlbumData>(loadedAlbumDataJson);
        return loadedAlbumData;
    }
}
