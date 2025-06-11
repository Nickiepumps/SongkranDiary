using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletWarningDisplay : MonoBehaviour
{
    [SerializeField] private Image[] warningSignArr;
    public void ShowBulletWarningSign(Transform bulletSpawner, int spawnerIndex)
    {
        Vector2 spawnerViewportPosToScreenPos = Camera.main.WorldToScreenPoint(bulletSpawner.transform.position);
        Debug.Log(spawnerViewportPosToScreenPos);
        if(spawnerViewportPosToScreenPos.y >= 1080)
        {
            spawnerViewportPosToScreenPos = new Vector2(spawnerViewportPosToScreenPos.x, 1000);
        }
        else if(spawnerViewportPosToScreenPos.x < 0)
        {
            spawnerViewportPosToScreenPos = new Vector2(80, spawnerViewportPosToScreenPos.y);
        }
        else if(spawnerViewportPosToScreenPos.x > 0)
        {
            spawnerViewportPosToScreenPos = new Vector2(1840, spawnerViewportPosToScreenPos.y);
        }
        warningSignArr[spawnerIndex].transform.position = spawnerViewportPosToScreenPos;
        warningSignArr[spawnerIndex].gameObject.SetActive(true);
    }
    public void DisableWarningSign()
    {
        for (int i = 0; i < warningSignArr.Length; i++)
        {
            warningSignArr[i].gameObject.SetActive(false);
        }
    }
}
