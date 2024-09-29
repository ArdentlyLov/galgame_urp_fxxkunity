
using System;
using System.Collections;
using UnityEngine;

public class CMD_Extensions_Examples : CMD_DatabaseExtension
{
    new public static void Extend(CommandDatabase database)
    {
        //add command with no parameters
        database.AddCommand("print", new Action(PrintDefaultMessage));
        database.AddCommand("print_1p", new Action<string>(PrintUsermessage));
        database.AddCommand("print_mp", new Action<string[]>(PrintLines));
        // add lambda with no parpameters
        database.AddCommand("lambda", new Action(() => { Debug.Log("printing a default message to console from lambda command."); }));
        database.AddCommand("lambda_1p", new Action<string>((arg) => { Debug.Log($"log user lambda message: '{arg}'");}));
        database.AddCommand("lambda_mp", new Action<string[]>((args) => { Debug.Log(string.Join(", ", args)); } ));
        //add coroutine with no parameters添加不带参数的协程
        database.AddCommand("process", new Func<IEnumerator>(SimpleProcess));
    }

    private static void PrintDefaultMessage()
    {
        Debug.Log("printing a default message to console.");
    }

    private static void PrintUsermessage(string message)
    {
        Debug.Log($"user message: '{message}'");
    }

    private static void PrintLines(string[] lines)
    {
        int i = 1;
        foreach (var line in lines)
        {
            Debug.Log($"{i++}. '{line}'");
        }
    }

    private static IEnumerator SimpleProcess()
    {
        for (int i = 0; i <= 5; i++)
        {
            Debug.Log($"process running……[{i}]");
            yield return new WaitForSeconds(1);
        }
    }
    
}
