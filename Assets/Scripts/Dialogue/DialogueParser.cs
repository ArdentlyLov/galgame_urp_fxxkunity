using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DIALOGUE
{
    public class DialogueParser
    {
        //commands正则表达式字符串
        private const string commandsRegexPattern = "\\w*[^\\s]\\(";
        public static DIALOGUE_LINE Parse(string rawLine)
        {
            //解析原始对话行
            Debug.Log($"Parsing line - '{rawLine}'");
            (string speaker, string dialogue, string commands) = RipContent(rawLine);
            //RipContent方法应该返回一个包含三个元素的元组，每个元素都是一个字符串
            
            Debug.Log($"Speaker = '{speaker}'\nDialogue = '{dialogue}'\nCommands = '{commands}'");
            
            return new DIALOGUE_LINE(speaker, dialogue, commands);
        }
        //三元组，三个字符，就能得到三个值
        private static (string, string, string) RipContent(string rawLine)
        {
            string speaker = "", dialogue = "", commands = "";
            //初始化dialogueStart和dialogueEnd，用来标记对话的起始和结束位置
            int dialogueStart = -1;
            int dialogueEnd = -1;
            //初始化isEscaped为false，用来追踪当前是否处于转义字符序列中
            bool isEscaped = false;

            for (int i = 0; i < rawLine.Length; i++)
            {
                char current = rawLine[i];
                //将当前索引i处的字符赋值给变量current

                //检查当前字符是否是反斜杠\
                if (current == '\\')
                {
                    isEscaped = !isEscaped;
                    //切换isEscaped的布尔值。如果之前不是转义序列，现在进入转义序列；如果之前是转义序列，现在退出
                    //如果当前字符是双引号"且当前不在转义序列中
                }
                else if (current == '"' && !isEscaped)
                {
                    if (dialogueStart == -1)
                        //如果dialogueStart仍然是初始值-1，这意味着找到了对话内容的开始位置
                        //将dialogueStart设置为i，表示对话内容从索引i开始
                        dialogueStart = i;
                    else if (dialogueEnd == -1)
                        //如果dialogueEnd仍然是初始值-1，这意味着找到了对话内容的结束位置。
                        //将dialogueEnd设置为当前索引i，表示对话内容结束于此。
                        dialogueEnd = i;
                }
                else
                {
                    //如果当前字符既不是反斜杠也不是双引号，或者当前处于转义序列中
                    //重置isEscaped为false，表示当前不在转义序列中
                    isEscaped = false;
                }
            }
            //调试dialogue
            // Debug.Log(rawLine.Substring(dialogueStart + 1, (dialogueEnd - dialogueStart) - 1));
            
            //Identifl Commands Pattern
            Regex commandsRegex = new Regex(commandsRegexPattern);
            Match match = commandsRegex.Match(rawLine);
            //使用Regex对象的Match方法来在rawLine字符串中查找与正则表达式模式匹配的部分。match对象将包含匹配的结果。

            int commandsStart = -1;
            if (match.Success)
            {
                commandsStart = match.Index;

                if (dialogueStart == -1 && dialogueEnd == -1)
                    return ("", "", rawLine.Trim());
            }
            
            //if we are here then we either have dialogue or a multi word argument in a command. figure out if this is dialogue
            if (dialogueStart != -1 && dialogueEnd != -1 && (commandsStart == -1 || commandsStart > dialogueEnd))
            {
                //we know that we have valid dialogue
                speaker = rawLine.Substring(0, dialogueStart).Trim();
                dialogue = rawLine.Substring(dialogueStart + 1, dialogueEnd - dialogueStart -1).Replace("\\\"", "\"");
                if (commandsStart != -1)
                    commands = rawLine.Substring(commandsStart).Trim();
            }else if (commandsStart != -1 && dialogueStart > commandsStart)
            {
                commands = rawLine;
            }
            else
                speaker = rawLine;
            return (speaker, dialogue, commands);
        }
    }
}
