using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;



public class TestDialogueFiles : MonoBehaviour
{
    void Start()
    {
        StartConversation();

    }

    void StartConversation()
    {
        List<string> lines = FileManager.ReadTextAsset("textFile");
        DialogueSystem.instance.Say(lines);
    }
}