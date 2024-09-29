using UnityEditor.Animations;
    
namespace DIALOGUE
{
    public class DIALOGUE_LINE
    {
        public DL_SPEAKER_DATA speakerData;
        public DL_DIALOGUE_DATA dialogueData;
        public DL_COMMAND_DATA commandsData;

        public bool hasSpeaker => speakerData != null;//speaker != string.Empty;
        public bool hasDialogue => dialogueData != null;
        public bool hasCommands => commandsData != null;
        // hasDialogue 属性的逻辑是检查 dialogue 变量是否不为空。如果 dialogue 不是一个空字符串，那么 hasDialogue 将返回 true，
        // 表示存在对话；如果 dialogue 是空字符串，那么 hasDialogue 将返回 false，表示没有对话
        
        public DIALOGUE_LINE(string speaker, string dialogue, string commands)
        {
            this.speakerData = (string.IsNullOrWhiteSpace(speaker) ? null : new DL_SPEAKER_DATA(speaker));
            this.dialogueData = (string.IsNullOrWhiteSpace(dialogue) ? null : new DL_DIALOGUE_DATA(dialogue));
            this.commandsData = (string.IsNullOrWhiteSpace(commands) ? null : new DL_COMMAND_DATA(commands));
        }
    }
}

