using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISO_GameController : MonoBehaviour
{
    [SerializeField] PlayerCameraController playerISOCameraController;
    private void Start()
    {
        StageClearData stageData = SideScroll_StageClearDataHandler.instance.LoadSideScrollStageClear();
        if(stageData != null)
        {
            for(int i = 0; i < stageData.levelData.Count; i++)
            {
                if(stageData.levelData[i].isClear == true && stageData.levelData[i].isClearFirstTime == true)
                {
                    StartCoroutine(StartRemoveBoundaryAnim(stageData.levelData[i].isoLevelBoundary, stageData.levelData[i].nextLevel));
                    stageData.levelData[i].isClearFirstTime = false;
                    break;
                }
            }
        }
    }
    private IEnumerator StartRemoveBoundaryAnim(GameObject[] boundaryTarget, Transform nextLevelTarget)
    {
        playerISOCameraController.isFocusNextLevel = true;
        if(boundaryTarget != null)
        {
            for (int i = 0; i < boundaryTarget.Length; i++)
            {
                playerISOCameraController.currentFocusTarget = boundaryTarget[i].transform;
                yield return new WaitForSeconds(0.3f);
                yield return new WaitUntil(() => playerISOCameraController.isReachFocusTarget == true);
                yield return new WaitForSeconds(1);
                GameObject.Find(boundaryTarget[i].name).SetActive(false);
                yield return new WaitForSeconds(0.3f);
            }
        }
        playerISOCameraController.currentFocusTarget = nextLevelTarget;
        yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(() => playerISOCameraController.isReachFocusTarget == true);
        yield return new WaitForSeconds(1f);
        playerISOCameraController.isReachFocusTarget = false;
        playerISOCameraController.isFocusNextLevel = false;
        yield return null;
    }
}
