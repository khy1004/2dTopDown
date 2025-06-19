using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI 요소 - lnstector에서 연결")]
    public GameObject DialoguePanel;
    public Image characterlmage;
    public TextMeshProUGUI characternameText;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    [Header("기본 설정")]
    public Sprite defaultCharacterlmage;

    [Header("타이핑 효과 설정")]
    public float typingSpeed = 0.05f;
    public bool skipTypingOnClick = true;

    private DialogueDataSO currentDialogue;
    private int currentLinelndex = 0;
    private bool isDialogueActive = false;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    IEnumerator TypeText(string textToType)
    {
        isTyping = true;
        dialogueText.text = "";

        for (int i = 0; i < textToType.Length; i++)
        {
            dialogueText.text += textToType[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
    private void CompleteTyping()
    {
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        isTyping = false;

        if(currentDialogue != null && currentLinelndex < currentDialogue.diaiogueLines.Count)
        {
            dialogueText.text = currentDialogue.diaiogueLines[currentLinelndex];
        }
    }

    void ShowCurrentLine()
    {
        if(currentDialogue != null && currentLinelndex < currentDialogue.diaiogueLines.Count)
        {

            if(typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            string currentText = currentDialogue.diaiogueLines[currentLinelndex];
            typingCoroutine = StartCoroutine(TypeText(currentText));
        }
    }
    // Start is called before the first frame update
    void StopCoroutine()
    {
        if (currentDialogue != null && currentLinelndex < currentDialogue.diaiogueLines.Count)

            if(typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }


        string currentText = currentDialogue.diaiogueLines[currentLinelndex];
        typingCoroutine = StartCoroutine(TypeText(currentText));



    }
    public void ShowNextLine()
    {
        currentLinelndex++;

        if(currentLinelndex >= currentDialogue.diaiogueLines.Count)
        {
            EndDialogue();
        }
        else
        {
            ShowCurrentLine();
        }

    }

    void EndDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        isDialogueActive = false;
        isTyping = false;
        DialoguePanel.SetActive(false);
        currentLinelndex = 0;
    }

    public void HandleNextlnput()
    {
        if(isTyping && skipTypingOnClick)
        {
            CompleteTyping();
        }
        else if(!isTyping)
        {
            ShowNextLine();
        }
    }

    public void SkipDialogue()
    {
        Debug.Log("P 2");
        EndDialogue();
    }

    public bool lsDialogueActive()
    {
        return isDialogueActive;
    }

    public void StartDialogue(DialogueDataSO dialogue)
    {
        if (dialogue == null || dialogue.diaiogueLines.Count == 0) return;
        currentDialogue = dialogue;
        currentLinelndex = 0;
        isDialogueActive = true;

        DialoguePanel.SetActive(true);
        characternameText.text = dialogue.characterName;

        if (characterlmage != null)
        {
            if (dialogue.characterlmage != null)
            {
                characterlmage.sprite = dialogue.characterlmage;
            }
            else
            {
                characterlmage.sprite = defaultCharacterlmage;
            }
            ShowNextLine();

        }
    }
    // Update is called once per frame
    void Start()
    {
        DialoguePanel.SetActive(false);
        nextButton.onClick.AddListener(HandleNextlnput);
    }

    void Update()
    {
        if(isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            HandleNextlnput();
        }
    }
}
