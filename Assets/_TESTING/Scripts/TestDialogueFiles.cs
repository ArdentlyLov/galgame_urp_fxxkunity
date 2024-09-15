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
        
        //测试演讲者对话投射测试
        for (int i = 0; i < lines.Count; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue;
            Debug.Log($"Processing line: {line}");
            DIALOGUE_LINE dl = DialogueParser.Parse(line);
            
            Debug.Log($"{dl.speaker.name} as [{(dl.speaker.castName != string.Empty ? dl.speaker.castName : dl.speaker.name)}]at {dl.speaker.castPosition}");

            List<(int l, string ex)> expr = dl.speaker.CastExpressions;
            for (int c = 0; c < expr.Count; c++)
            {
                Debug.Log($"[Layer[{expr[c].l}] = '{expr[c].ex}']");
            }
        }
        
        // DialogueSystem.instance.Say(lines);
    }
    
}