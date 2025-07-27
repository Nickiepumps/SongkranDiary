using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlbumImage", menuName = "Album")]
public class AlbumSO : ScriptableObject
{
    public string unlockImageText;
    public string lockImageText;
    public Sprite image;
    public bool unlockStatus;
}
