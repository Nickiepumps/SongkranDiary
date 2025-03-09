using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NPCDialogController : MonoBehaviour, INPCObserver, IPlayerObserver, IGameObserver
{
    [Header("Dialog Type")]
    [SerializeField] private NPCDialogType dialogType;

    [Header("Observer References")]
    [SerializeField] private NPCSubject npcSubject; // NPC
    [SerializeField] private PlayerSubject playerSubject; // Player
    [SerializeField] private GameSubject isometricGameSubject; // Isometric Game subject To Do: find gameSubject object in the scene and reference it when turned into prefab

    [Header("Dialog box and Notif References")]
    [SerializeField] private GameObject interactNotif; // Interact notification popup
    [SerializeField] private GameObject dialogBox; // NPC dialog box
    [SerializeField] private TextMeshProUGUI dialogText; // Dialog text
    [SerializeField] private GameObject nextDialogNotif;
    private int dialogSeqNumber = 0;
    private GameObject player; // Player to NPC status

    [Header("Dialog and Choices")]
    [SerializeField] private List<string> dialogLists = new List<string>(); // NPC's normal dialog lists
    [SerializeField] private List<string> correctAnswerDialogLists = new List<string>();
    [SerializeField] private List<string> wrongAnswerDialogLists = new List<string>();
    [SerializeField] private List<string> choiceLists = new List<string>(); // Player's choice iists
    [SerializeField] private string outroDialog;
    [SerializeField] private GameObject choicesGroup; // Player's choices group gameObject
    [SerializeField] private GameObject correctChoice; // Player's correct choice
    [SerializeField] private List<GameObject> choiceButtonLists = new List<GameObject>(); // Player's choices list

    [Header("Player Answered Status")]
    [SerializeField] private bool isPlayerAnswered = false;

    private bool finishedDialog = false; // Check status of dialog typewriter effect
    private GameObject currentChoiceToConfirm; // Player's current choices before submit (For keyboard choosing)
    private GameObject confirmedChoice; // Player's confirmed choice
    private bool startChoosing = false; // Player's choosing dialogs
    private void OnEnable()
    {
        if(dialogType == NPCDialogType.Choices)
        {
            int choiceTextOrder = 0;
            foreach(GameObject choice in choiceButtonLists)
            {
                choice.GetComponentInChildren<TMP_Text>().text = choiceLists[choiceTextOrder];
                choiceTextOrder++;
            }
        }
        npcSubject.AddNPCObserver(this);
        playerSubject.AddPlayerObserver(this);
        isometricGameSubject.AddGameObserver(this);
    }
    private void OnDisable()
    {
        npcSubject.RemoveNPCObserver(this);
        playerSubject.RemovePlayerObserver(this);
        isometricGameSubject?.RemoveGameObserver(this);
    }
    public void OnNPCNotify(NPCAction npcAction)
    {
        
    }
    public void OnPlayerNotify(PlayerAction playerAction)
    {
        switch (playerAction)
        {
            case (PlayerAction.Idle):
                if (player != null)
                {
                    interactNotif.SetActive(true);
                }
                else
                {
                    interactNotif.SetActive(false);
                }
                return;
            case (PlayerAction.Talk):
                interactNotif.SetActive(false);
                dialogBox.SetActive(true);
                if (player != null && dialogType == NPCDialogType.Normal)
                {   
                    StartCoroutine(NormalDialogSequence());
                }
                else if(player != null && dialogType == NPCDialogType.Choices)
                {
                    StartCoroutine(ChoiceDialogSequence());
                }
                return;
            case(PlayerAction.Walk):
                if (player != null)
                {
                    interactNotif.SetActive(true);
                }
                else
                {
                    interactNotif.SetActive(false);
                }
                return;
        }
    }
    public void OnGameNotify(IsometricGameState isoGameState)
    {
        switch (isoGameState)
        {
            case (IsometricGameState.Play): // Not working right now, Fix this later!
                if(isPlayerAnswered == true)
                {
                    dialogText.text = "";
                    dialogText.text = outroDialog; // To Do: Replace with the typewriter effect
                }
                return;
        }
    }
    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState)
    {

    }
    public void OnSideScrollGameNotify(IsometricGameState isoGameState)
    {
    }
    private void Update()
    {
        if(dialogType == NPCDialogType.Choices && startChoosing == true)
        {
            ChoosingDialog();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player = null;
    }
    private IEnumerator NormalDialogSequence()
    {
        nextDialogNotif.SetActive(false); // Disable interactive ui popup
        // Play dialog sequence
        if(dialogSeqNumber < dialogLists.Count)
        {
            StartCoroutine(TypeWriterAnimation(dialogLists[dialogSeqNumber].ToString()));
            yield return new WaitUntil(() => finishedDialog == true);
            nextDialogNotif.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            dialogSeqNumber++;
            StartCoroutine(NormalDialogSequence());
        }
        // stop dialog sequence when npc ran through all dialogs in the list
        else
        {
            isometricGameSubject.NotifyGameObserver(IsometricGameState.Play);
            dialogSeqNumber = 0;
            dialogBox.SetActive(false);
            interactNotif.SetActive(true);
            yield return null;
        }
    }
    private IEnumerator ChoiceDialogSequence()
    {
        nextDialogNotif.SetActive(false); // Disable interactive ui popup
        // Play dialog sequence
        if(dialogSeqNumber == dialogLists.Count - 1)
        {
            StartCoroutine(TypeWriterAnimation(dialogLists[dialogSeqNumber].ToString()));
            dialogSeqNumber = 0;
            yield return new WaitUntil(() => finishedDialog == true);
            startChoosing = true;
            choicesGroup.SetActive(true); // Show choices
            yield return null;
        }
        else if (dialogSeqNumber < dialogLists.Count)
        {
            StartCoroutine(TypeWriterAnimation(dialogLists[dialogSeqNumber].ToString()));
            yield return new WaitUntil(() => finishedDialog == true);
            nextDialogNotif.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            dialogSeqNumber++;
            StartCoroutine(ChoiceDialogSequence());
        }
    }
    private IEnumerator CorrectResultDialog()
    {
        nextDialogNotif.SetActive(false); // Disable interactive ui popup
        choicesGroup.SetActive(false);
        // Play dialog sequence
        if (dialogSeqNumber < correctAnswerDialogLists.Count)
        {
            StartCoroutine(TypeWriterAnimation(correctAnswerDialogLists[dialogSeqNumber].ToString()));
            yield return new WaitUntil(() => finishedDialog == true);
            nextDialogNotif.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            dialogSeqNumber++;
            StartCoroutine(CorrectResultDialog());
        }
        // stop dialog sequence when npc ran through all dialogs in the list
        else
        {
            isometricGameSubject.NotifyGameObserver(IsometricGameState.Upgrade);
            dialogSeqNumber = 0;
            choicesGroup.SetActive(false);
            dialogBox.SetActive(false);
            yield return null;
        }
    }
    private IEnumerator WrongResultDialog()
    {
        nextDialogNotif.SetActive(false); // Disable interactive ui popup
        choicesGroup.SetActive(false);
        // Play dialog sequence
        if (dialogSeqNumber < wrongAnswerDialogLists.Count)
        {
            StartCoroutine(TypeWriterAnimation(wrongAnswerDialogLists[dialogSeqNumber].ToString()));
            yield return new WaitUntil(() => finishedDialog == true);
            nextDialogNotif.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            dialogSeqNumber++;
            StartCoroutine(WrongResultDialog());
        }
        // stop dialog sequence when npc ran through all dialogs in the list
        else
        {
            isometricGameSubject.NotifyGameObserver(IsometricGameState.Play); // Notify gamestate observer to change the state to play state
            dialogSeqNumber = 0;
            dialogBox.SetActive(false);
            interactNotif.SetActive(true);
            yield return null;
        }
    }
    private void ChoosingDialog()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentChoiceToConfirm = choiceButtonLists[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentChoiceToConfirm = choiceButtonLists[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentChoiceToConfirm = choiceButtonLists[2];
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            confirmedChoice = currentChoiceToConfirm;
            startChoosing = false;
            if(CheckAnswer(confirmedChoice) == true)
            {
                Debug.Log("Correct!");
                StartCoroutine(CorrectResultDialog());
            }
            else
            {
                Debug.Log("Wrong!");
                StartCoroutine(WrongResultDialog());
            }
        }
    }
    private bool CheckAnswer(GameObject choice)
    {
        if(choice == correctChoice)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private IEnumerator TypeWriterAnimation(string dialog)
    {
        finishedDialog = false;
        float timeBtwChar = 0.05f; // Time between character
        dialogText.text = ""; // Clear all previous text
        foreach(char line in dialog.ToCharArray())
        {
            dialogText.text += line; // Add a character
            yield return new WaitForSeconds(timeBtwChar);
        }
        finishedDialog = true;
    }
}
