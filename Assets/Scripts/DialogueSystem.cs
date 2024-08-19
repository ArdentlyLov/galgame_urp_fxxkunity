using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class DialogueSystem : MonoBehaviour
    {
        public DialogueContainer dialogueContainer = new DialogueContainer();
        public static DialogueSystem instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
                DestroyImmediate(gameObject);
        }
    }
}
