using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class DialogueSystem : MonoBehaviour
    {
        public DialogueContainer dialogueContainer = new DialogueContainer();
        private ConversationManager conversationManager;
        private TextArchitect architect;
        
        public static DialogueSystem instance;

        public delegate void dialogueSystemEvent();
        public event dialogueSystemEvent onUserPrompt_Next;
        
        public bool isRunningConversation => conversationManager.isRunning;
        //指向对话管理器
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Initialize();
            }
            else
                DestroyImmediate(gameObject);
        }

        //名称构建师
        bool _initialized = false;
        private void Initialize()
        {
            if (_initialized)
                return;

            architect = new TextArchitect(dialogueContainer.dialogueText);
            conversationManager = new ConversationManager(architect);
        }

        public void OnUserPrompt_Next()
        {
            //检查用户是否点击屏幕
            onUserPrompt_Next?.Invoke();
            
        }
        public void ShowSpeakerName(string speakerName = "")
        {
            if (speakerName != null && dialogueContainer.nameContainer != null)
            {
                //隐藏旁白
                if (speakerName.ToLower() != "narrator")
                    dialogueContainer.nameContainer.Show(speakerName);
                else
                    HideSpeakerName();
                
                Debug.Log("speakerName是：" + speakerName + "dialogueContainer.nameContainrt是：" + dialogueContainer.nameContainer);

            }
            else
            {
                Debug.Log("speakerName或者dialogueContainer.nameContainrt未初始化");
                Debug.Log("speakerName是：" + speakerName + "dialogueContainer.nameContainrt是：" + dialogueContainer.nameContainer);
            }
        } 
        public void HideSpeakerName() => dialogueContainer.nameContainer.Hide();
        
        public void Say(string speaker, string dialogue)
        {
            List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
            Say(conversation);
        }
        public void Say(List<string> conversation)
        {
            conversationManager.StartConversation(conversation);
        }
    }
}
