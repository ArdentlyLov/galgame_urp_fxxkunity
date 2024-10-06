
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
        // add lambda with no parameters
        database.AddCommand("lambda", new Action(() => { Debug.Log("printing a default message to console from lambda command."); }));
        database.AddCommand("lambda_1p", new Action<string>((arg) => { Debug.Log($"log user lambda message: '{arg}'");}));
        database.AddCommand("lambda_mp", new Action<string[]>((args) => { Debug.Log(string.Join(", ", args)); } ));
        //add coroutine with no parameters添加不带参数的协程
        database.AddCommand("process", new Func<IEnumerator>(SimpleProcess));
        database.AddCommand("process_1p", new Func<string, IEnumerator>(LineProcess));
        database.AddCommand("process_mp", new Func<string[], IEnumerator>(MultiLineProcess));
        
        //special example
        database.AddCommand("moveCharDemo", new Func<string, IEnumerator>(MoveCharacter));
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
    
    private static IEnumerator LineProcess(string data)
    {
        if (int.TryParse(data, out int num))
        {
            for (int i = 0; i <= num; i++)
            {
                Debug.Log($"process running……[{i}]");
                yield return new WaitForSeconds(1);
            }
        }
    }

    private static IEnumerator MultiLineProcess(string[] data)
    {
        foreach (var line in data)
        {
            Debug.Log($"Process Message: '{line}'");
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    private static IEnumerator MoveCharacter(string direction)
    {
        bool left = direction.ToLower() == "left";

        Transform character = GameObject.Find("Image").transform;
        float moveSpeed = 15;

        float targetX = left ? -8 : 8;

        float currentX = character.position.x;
        
        while (Mathf.Abs(targetX - currentX) > 0.1f)
        {
            Debug.Log($"Moving character to {(left ? "left" : "right")} [{currentX}/{targetX}]");
            currentX = Mathf.MoveTowards(currentX, targetX, moveSpeed * Time.deltaTime);
            character.position = new Vector3(currentX, character.position.y, character.position.z);
            yield return null;
        }
    }
    
}
