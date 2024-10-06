using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandDatabase
{
    private Dictionary<string, Delegate> database = new Dictionary<string, Delegate>();
    public bool Hascommand(string commandName) => database.ContainsKey(commandName);

    public void AddCommand(string commandName, Delegate command)
    {
        if (!database.ContainsKey(commandName))
        {
            database.Add(commandName, command);
        }
        else
        {
            Debug.LogError($"command already exists in the database'{commandName}'");
        }
    }

    public Delegate GetCommand(string commandName)
    {
        if (!database.ContainsKey(commandName))
        {
            Debug.LogError($"command '{commandName}' does not exist in the databse");
            return null;
        }

        return database[commandName];
    }
}
