using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;


public class TestDialogueFiles : MonoBehaviour
{
    void Start()
    {
        StartConversation();

    }

    void StartConversation()
    {
        List<string> lines = FileManager.ReadTextAsset("TextDialogueSegametation");

        //对话分割日志
        // foreach (var line in lines)
        // {
        //     if(string.IsNullOrEmpty(line))
        //         continue;
        //     
        //     Debug.Log($"Segment line '{line}'");
        //     DIALOGUE_LINE dlLine = DialogueParser.Parse(line);
        //
        //     int i = 0;
        //     foreach (DL_DIALOGUE_DATA.DIALOGUE_SEGMENT segment in dlLine.dialogue.segments)
        //     {
        //         Debug.Log($"Segment [{i++}] = '{segment.dialogue}' [signal = {segment.startSignal.ToString()}{(segment.signalDelay > 0 ? $" {segment.signalDelay}" : $"")}]");
        //     }
        // }
        
        DialogueSystem.instance.Say(lines);
    }
    
}