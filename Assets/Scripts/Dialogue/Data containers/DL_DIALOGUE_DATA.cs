using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class DL_DIALOGUE_DATA : MonoBehaviour
{
    public List<DIALOGUE_SEGMENT> segments;
    private const string segmentIdentifierPattern = @"\{[ca]\}|\{w[ca]\s\d*\.?\d*\}";
    
    //分段标识符模式
    public DL_DIALOGUE_DATA(string rawDialogue)
    {
        segments = RipSegments(rawDialogue);
        //把处理好饿对话给segments
    }

    public List<DIALOGUE_SEGMENT> RipSegments(string rawDialogue)
    {
        //撕取一个对话片段
        List<DIALOGUE_SEGMENT> segments = new List<DIALOGUE_SEGMENT>();
        MatchCollection matches = Regex.Matches(rawDialogue, segmentIdentifierPattern);

        int lastIndex = 0;
        //find the first or only segment in the file
        DIALOGUE_SEGMENT segment = new DIALOGUE_SEGMENT();
        segment.dialogue = (matches.Count == 0 ? rawDialogue : rawDialogue.Substring(0, matches[0].Index));
        segment.startSignal = DIALOGUE_SEGMENT.StartSignal.NOME;
        segment.signalDelay = 0;
        segments.Add(segment);

        if (matches.Count == 0)
            return segments;
        else
            lastIndex = matches[0].Index;

        for (int i = 0; i < matches.Count; i++)
        {
            Match match = matches[i];
            segment = new DIALOGUE_SEGMENT();
            //get the start signal for the segemnt
            string signalMatch = match.Value;
            signalMatch = signalMatch.Substring(1, match.Length - 2);
            string[] signalSplit = signalMatch.Split(' ');

            segment.startSignal = (DIALOGUE_SEGMENT.StartSignal)Enum.Parse(typeof(DIALOGUE_SEGMENT.StartSignal), signalSplit[0].ToUpper());
            
            //get the dialogue for the segment
            if (signalSplit.Length > 1)
                float.TryParse(signalSplit[1], out segment.signalDelay);
            
            //get the dialogue for the segament
            int nextIndex = i + 1 < matches.Count ? matches[i + 1].Index : rawDialogue.Length;
            segment.dialogue = rawDialogue.Substring(lastIndex + match.Length, nextIndex - (lastIndex + match.Length));
            lastIndex = nextIndex;
            
            segments.Add(segment);
        }
        return segments;
    }

    public struct DIALOGUE_SEGMENT
    {
        public string dialogue;
        public StartSignal startSignal;
        public float signalDelay;
        
        public enum StartSignal
        {
            NOME,   //sign信号
            C,      //clear
            A,      // append
            WA,     //wait append
            WC,     //wait clear
        }

        public bool appendText => (startSignal == StartSignal.A || startSignal == StartSignal.WA);
        
    }
}
