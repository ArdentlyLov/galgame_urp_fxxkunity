using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class DIALOGUE_LINE
    {
        //有点像java里面的接口,,,构造函数
        public string speaker;
        public string dialogue;
        public string commands;

        public bool hasspeaker => speaker != string.Empty;
        public bool hasDialogue => dialogue != string.Empty;
        // hasDialogue 属性的逻辑是检查 dialogue 变量是否不为空。如果 dialogue 不是一个空字符串，那么 hasDialogue 将返回 true，
        // 表示存在对话；如果 dialogue 是空字符串，那么 hasDialogue 将返回 false，表示没有对话
        public bool hasCommands => commands != string.Empty;
        
        public DIALOGUE_LINE(string speaker, string dialogue, string commands)
        {
            this.speaker = speaker;
            this.dialogue = dialogue;
            this.commands = commands;
        }
    }
}

