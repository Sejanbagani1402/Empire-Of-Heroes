using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor actor; // Single actor

    public void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().openDialogue(messages, actor);
    }
}

[System.Serializable]
public class Message
{
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}
