using System;
using Godot;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public partial class BeatTimeReader : Control
{
    const string PYTHON_PATH = @"audio_importer\pythonEnvLib\Scripts\python.exe";
    const string SCRIPT_PATH = @"audio_importer\createTimestamps.py";

    const string AUDIO_FOLDER_PATH_EDITOR = @"audio";

    const string AUDIO_FOLDER_PATH_RELEASE = @"";

    private List<string> songs = new();

    Button openFileExplorerButton;
    Button confirmButton;


    public override void _Ready()
    {


        openFileExplorerButton = GetNode<Button>("box/OpenFileExplorerButton");
        openFileExplorerButton.Pressed += CheckForNewSongs;


    }

    private void CheckForNewSongs()
    {
        // Check if the audio folder exists

        //check for new songs in the audio folder

        //if new songs are found, add them to the list of songs

        //call python script for each new song

        ExecutePythonScript("audio\\8-bit-circus.wav");
    }

    private void OnFileSelected(string path)
    {
        GD.Print("Selected file: " + path);
        // Here, add the logic to handle the selected file, like loading the song
    }


    public static void ExecutePythonScript(string audioFilePath)
    {
        //  GD.Print("Current directory: " + Directory.GetCurrentDirectory());
        GD.Print("Executing python script");
        ProcessStartInfo start = new ProcessStartInfo
        {
            FileName = PYTHON_PATH,
            Arguments = $"\"{SCRIPT_PATH}\" \"{audioFilePath}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        using (Process process = Process.Start(start))
        {
            // Wait for the Python script to exit
            process.WaitForExit(); // This will block until the Python script is finished

            // Now that the process has finished, you can read the output
            string result = process.StandardOutput.ReadToEnd();
            string trimmedResult = result.Trim().ToLower();
            var output = JObject.Parse(trimmedResult);
           // GD.Print("Output: ", output);

            // Parse the JSON output
           // var output = JObject.Parse(result);

            // Check if the operation was successful
            if (output["success"].Value<bool>())
            {
                // Process was successful, handle beat times
                var beatTimes = output["beat_times"].ToObject<float[]>();
                GD.Print("Beat Times: ", String.Join(", ", beatTimes));
            }
            else
            {
                // An error occurred, handle accordingly
                var errorMessage = output["error_message"].ToString();
                GD.Print("Error: " + errorMessage);
            }
        } // The using statement ensures the process is properly disposed of after use


    }
}
