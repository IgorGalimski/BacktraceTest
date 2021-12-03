using System;
using System.Collections.Generic;
using Backtrace.Unity;
using Backtrace.Unity.Model;
using UnityEngine;

public class BacktraceInitializer : MonoBehaviour
{
    // Backtrace client instance
    private BacktraceClient _backtraceClient;
    void Start()
    {
        var serverUrl = "https://submit.backtrace.io/testfm/0c2c0601488087986eb51b3ebb3d73ab130efc12d9dbf33adea93bd92dd214e2/json";
        var gameObjectName = "Backtrace";
        var databasePath =  "${Application.persistentDataPath}/sample/backtrace/path";
        var attributes = new Dictionary<string, string>() { {"my-super-cool-attribute-name", "attribute-value"} };

        // use game object to initialize Backtrace integration
        _backtraceClient = GameObject.Find(gameObjectName).GetComponent<BacktraceClient>();
        //Read from manager BacktraceClient instance
        var database = GameObject.Find(gameObjectName).GetComponent<BacktraceDatabase>();

        // or initialize Backtrace integration directly in your source code
        _backtraceClient = BacktraceClient.Initialize(
            serverUrl,
            databasePath ,
            gameObjectName: gameObjectName,
            attributes: attributes);
    }

    public void ThrowException()
    {
        try
        {
            throw new NullReferenceException();
        }
        catch (Exception exception)
        {
            var report = new BacktraceReport(
                exception: exception,
                attributes: new Dictionary<string, string> { { "key", "value" } },
                attachmentPaths: new List<string> { @"file_path_1", @"file_path_2" }
            );
            _backtraceClient.Send(report);
        }
    }
}
