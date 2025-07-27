using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumDisplayController : MonoBehaviour
{
    [Header("Album Display Slot")]
    public AlbumDisplay[] albumDisplaySlotArr;

    [Header("Album Pages")]
    [SerializeField] private GameObject[] leftAlbumArr;
    [SerializeField] private GameObject[] rightAlbumArr;
    [SerializeField] private GameObject fullImageCanvas;
    [SerializeField] private GameObject fullImageBG;
    [SerializeField] private Image fullImageSprite;
    private int currentPage = 0;

    [Header("Album Button")]
    [SerializeField] private GameObject albumNextPageButton;
    [SerializeField] private GameObject albumPreviousPageButton;
    [SerializeField] private GameObject albumHideUIButton;
    [SerializeField] private GameObject albumBackToAlbumButton;
    [SerializeField] private Button albumFullImage;

    [Header("Album Animation")]
    [SerializeField] private Animation albumFullImageAnimation;

    private bool isUIHidden = false;
    private void Start()
    {
        AlbumData albumData = AlbumDataHandler.instance.LoadAlbumData();
        if (albumData != null)
        {
            for (int i = 0; i < albumDisplaySlotArr.Length; i++)
            {
                albumDisplaySlotArr[i].albumImageSO = albumData.albumImagesSO[i];
                if (albumData.albumImagesSO[i].unlockStatus == true)
                {
                    albumDisplaySlotArr[i].image.sprite = albumData.albumImagesSO[i].image;
                    albumDisplaySlotArr[i].imageText.text = albumData.albumImagesSO[i].unlockImageText;
                    albumDisplaySlotArr[i].fullImageButton.gameObject.SetActive(true);
                }
                else
                {
                    albumDisplaySlotArr[i].fullImageButton.gameObject.SetActive(false);
                    albumDisplaySlotArr[i].imageText.text = albumData.albumImagesSO[i].lockImageText;
                }
            }
        }
        albumPreviousPageButton.SetActive(false);
        albumNextPageButton.SetActive(true);
        leftAlbumArr[0].SetActive(true);
        rightAlbumArr[0].SetActive(true);
    }
    public void NextAlbumPage()
    {
        if (currentPage < 2)
        {
            currentPage++;
            leftAlbumArr[currentPage - 1].SetActive(false);
            rightAlbumArr[currentPage - 1].SetActive(false);
            leftAlbumArr[currentPage].SetActive(true);
            rightAlbumArr[currentPage].SetActive(true);
            albumPreviousPageButton.SetActive(true);
        }
        if(currentPage == 2)
        {
            albumNextPageButton.SetActive(false);
        }
    }
    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            leftAlbumArr[currentPage + 1].SetActive(false);
            rightAlbumArr[currentPage + 1].SetActive(false);
            leftAlbumArr[currentPage].SetActive(true);
            rightAlbumArr[currentPage].SetActive(true);
            albumNextPageButton.SetActive(true);
        }
        if (currentPage == 0)
        {
            albumPreviousPageButton.SetActive(false);
        }
    }
    public void ShowFullImage(AlbumSO image)
    {
        fullImageSprite.sprite = image.image;
        fullImageCanvas.SetActive(true);
        albumFullImageAnimation.Play("Album_ShowImage");
    }
    public void CloseFullImage()
    {
        StartCoroutine(StartCloseFullImageAnimation());
    }
    public void HideUI()
    {
        if(isUIHidden == false)
        {
            isUIHidden = true;
            StartCoroutine(StartHideUIAnimation());
        }
        else
        {
            isUIHidden = false;
            fullImageBG.SetActive(true);
            albumFullImageAnimation.Play("Album_ShowUI");
            albumFullImage.enabled = false;
            albumHideUIButton.SetActive(true);
            albumBackToAlbumButton.SetActive(true);
        }
    }
    private IEnumerator StartHideUIAnimation()
    {
        albumFullImageAnimation.Play("Album_HideUI");
        yield return new WaitUntil(() => albumFullImageAnimation.isPlaying == false);
        albumHideUIButton.SetActive(false);
        albumBackToAlbumButton.SetActive(false);
        fullImageBG.SetActive(false);
        albumFullImage.enabled = true;
    }
    private IEnumerator StartCloseFullImageAnimation()
    {
        albumFullImageAnimation.Play("Album_CloseImage");
        yield return new WaitUntil(() => albumFullImageAnimation.isPlaying == false);
        fullImageCanvas.SetActive(false);
    }
}
