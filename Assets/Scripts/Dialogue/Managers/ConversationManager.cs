using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace DIALOGUE
{
    
    public class ConversationManager
    {
        private DialogueSystem dialogueSystem => DialogueSystem.instance;
        //当访问这个属性时，它不会返回一个字段或变量的值，而是执行 DialogueSystem.instance 表达式，并返回其结果
        private Coroutine process = null;
        public bool isRunning => process != null;
        //如果 process 不是 null（即协程已经被启动），则 isRunning 返回 true，
        //表示协程正在运行；如果 process 是 null，则返回 false，表示协程没有运行
        
        private TextArchitect architect = null;
        private bool userPrompt = false;
        
        public ConversationManager(TextArchitect architect)
        {
            this.architect = architect;
            dialogueSystem.onUserPrompt_Next += OnUserPrompt_Next;
        }

        private void OnUserPrompt_Next() 
        {
            userPrompt = true;
        }
        
        public void StartConversation(List<string> converation)
        {
            StopConversation();
            process = dialogueSystem.StartCoroutine(RunningConversations(converation));
        }

        public void StopConversation()
        {
            if (!isRunning)
                return;
            
            dialogueSystem.StopCoroutine(process);
            process = null;
        }

        IEnumerator RunningConversations(List<string> conversation)
        {
            for (int i = 0; i < conversation.Count; i++)
            {
                //dont show any blank lines or try to run any logic on them
                if (string.IsNullOrWhiteSpace(conversation[i]))
                    continue;
                
                DIALOGUE_LINE line = DialogueParser.Parse(conversation[i]);
                
                //show dialogue
                if (line.hasDialogue)
                    yield return Line_RunDialogue(line);
                //run any commands
                if (line.hasCommands)
                    yield return Line_RunCommands(line);
                
                //每段话延迟1秒
                // yield return new WaitForSeconds(1);
            }
        }
        IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
        {
            //show or hide the speaker name if there is one present
            if (line.hasspeaker)
                dialogueSystem.ShowSpeakerName(line.speaker);
            
            //build dialogue
            yield return BuildLineSegment(line.dialogue);
            
            //wait for user input
            yield return WaitForUserInput();

        }        
        IEnumerator Line_RunCommands(DIALOGUE_LINE line)
        {
            Debug.Log(line.commands);
            yield return null;
        }

        IEnumerator BuildLineSegment(DL_DIALOGUE_DATA line)
        {
            //构建线段
            for (int i = 0; i < line.segments.Count; i++)
            {
                DL_DIALOGUE_DATA.DIALOGUE_SEGMENT segment = line.segments[i];
                yield return WaitForDialogueSegmentSignalToBeTriggered(segment);

                yield return BuildDialogue(segment.dialogue, segment.appendText);
            }
        }
        IEnumerator WaitForDialogueSegmentSignalToBeTriggered(DL_DIALOGUE_DATA.DIALOGUE_SEGMENT segment)
        {
            switch (segment.startSignal)
            {
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.C:
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.A:
                    yield return WaitForUserInput();
                    break;
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.WC:
                case DL_DIALOGUE_DATA.DIALOGUE_SEGMENT.StartSignal.WA:
                    yield return new WaitForSeconds(segment.signalDelay);
                    break;
                default:
                    break;
            }
        }
        
        IEnumerator BuildDialogue(string dialogue, bool append = false)
        {
            //build the dialogue
            if (!append)
                architect.Build(dialogue);
            else
                architect.Append(dialogue);
            //wait for the dialogue to complete
            while (architect.isBuilding)
            {
                if (userPrompt)
                {
                    //如果不是!architect.hurryUp就加快
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();
                    
                    userPrompt = false;
                }
                yield return null;
            }
        }

        IEnumerator WaitForUserInput()
        {
            while (!userPrompt)
                yield return null;

            userPrompt = false;
        }
    }
}

