using UnityEngine;

public class CommandTesting : MonoBehaviour
{
    void Start()
    {
        CommandManager.instance.Execute("print");
        CommandManager.instance.Execute("print_1p", "hello world");
        CommandManager.instance.Execute("print_mp", "line", "line2", "line3");
        
        CommandManager.instance.Execute("lambda");
        CommandManager.instance.Execute("lambda_1p", "hello lambda");
        CommandManager.instance.Execute("lambda_mp", "lambda1", "lambda2", "lambda3");
    }
}
