using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;
    public Button nextButton; // Reference to the button

    Message[] currentMessages;
    Actor currentActor; // Single actor
    int activeMessage = 0;
    public static bool isActive = false;

    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
        nextButton.onClick.AddListener(OnNextButtonClicked); // Add listener for the button click
        nextButton.gameObject.SetActive(false); // Hide the button initially
    }

    public void openDialogue(Message[] messages, Actor actor)
    {
        currentMessages = messages;
        currentActor = actor;
        activeMessage = 0;
        isActive = true;
        Debug.Log("Started conversation! Loaded Messages: " + messages.Length);
        DisplayMessage();
        backgroundBox.LeanScale(Vector3.one, 0.5f).setOnComplete(() =>
        {
            Debug.Log("Background box animation completed.");
        });
        nextButton.gameObject.SetActive(true); // Show the button when the dialogue starts
    }

    void DisplayMessage()
    {
        if (activeMessage >= currentMessages.Length)
        {
            Debug.LogError("activeMessage index out of bounds: " + activeMessage);
            return;
        }

        Message messageToDisplay = currentMessages[activeMessage];
        Debug.Log("Displaying message " + activeMessage + ": " + messageToDisplay.message);

        actorName.text = currentActor.name;
        actorImage.sprite = currentActor.sprite;
        messageText.text = messageToDisplay.message;

        AnimateTextColor();
    }

    void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Convo ended!!!");
            isActive = false;
            nextButton.gameObject.SetActive(false); // Hide the button when the conversation ends
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo(); // Scale down the background box
        }
    }

    void AnimateTextColor()
    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f).setOnComplete(() =>
        {
            Debug.Log("Text color animation completed.");
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive)
        {
            Debug.Log("Space key pressed.");
            NextMessage();
        }
    }

    void OnNextButtonClicked()
    {
        if (isActive)
        {
            Debug.Log("Next button clicked.");
            NextMessage();
        }
    }
}
