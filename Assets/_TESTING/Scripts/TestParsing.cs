using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;

namespace TESTING
{
    public class TestParsing : MonoBehaviour
    {
        void Start()
        {
            SendFiletoParse();
            
            // string line = "Speaker \"Dialogue \\\"Goes In\\\" Here!\" Commands(arguments here)";
            // DialogueParser.Parse(line);
        }
        void SendFiletoParse()
        {
            List<string> lines = FileManager.ReadTextAsset("textFile");
            foreach (string line in lines)
            {
                if (line == string.Empty)
                    continue;
                
                DIALOGUE_LINE dl = DialogueParser.Parse(line);
            }
        }
    }
}
