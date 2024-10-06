using System.Collections;
using UnityEngine;

public class CommandTesting : MonoBehaviour
{
    void Start()
    {
        // StartCoroutine(Running());
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            CommandManager.instance.Execute("moveCharDemo", "left");
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            CommandManager.instance.Execute("moveCharDemo", "right");
    }
    IEnumerator Running()
    {
        yield return CommandManager.instance.Execute("print");
        yield return CommandManager.instance.Execute("print_1p", "hello world");
        yield return CommandManager.instance.Execute("print_mp", "line", "line2", "line3");
        
        yield return CommandManager.instance.Execute("lambda");
        yield return CommandManager.instance.Execute("lambda_1p", "hello lambda");
        yield return CommandManager.instance.Execute("lambda_mp", "lambda1", "lambda2", "lambda3");
        
        yield return CommandManager.instance.Execute("process");
        yield return CommandManager.instance.Execute("process_1p", "hello Process 3");
        yield return CommandManager.instance.Execute("process_mp", "Process line 1", "Process line 2", "Process line 3");
    }
}
