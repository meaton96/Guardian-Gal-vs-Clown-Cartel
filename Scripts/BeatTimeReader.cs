using System;
using Godot;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public partial class BeatTimeReader : Control
{
    const string PYTHON_PATH = @"c:\audio_importer\env\Scripts\python.exe";
   
    const string SCRIPT_PATH = @"c:\audio_importer\createTimestamps.py";

    const string AUDIO_FOLDER_PATH_EDITOR = @"audio";

    const string AUDIO_FOLDER_PATH_RELEASE = @"";

    private List<string> songs = new();

    Button openFileExplorerButton;
    Button loadButton;




    public override void _Ready()
    {


        openFileExplorerButton = GetNode<Button>("AddNewSongsButton");
        openFileExplorerButton.Pressed += CheckForNewSongs;

        loadButton = GetNode<Button>("LoadSongButton");
        loadButton.Pressed += OnLoadSongPressed;


        CheckForNewSongs();
    }

    private void CheckForNewSongs()
    {
        // Check if the audio folder exists

        //check for new songs in the audio folder

        //if new songs are found, add them to the list of songs

        //call python script for each new song

        ExecutePythonScript("audio\\8-bit-circus.wav");

        //songNames.ForEach(songName => ExecutePythonScript($"audio\\{songName}.mp3"));
    }

    public void OnLoadSongPressed()
    {
        //open new menu to select song
        //new menu is new scene that will create a little button for each .json file in the audio folder
    }

    private void OnFileSelected(string path)
    {
        GD.Print("Selected file: " + path);
        // Here, add the logic to handle the selected file, like loading the song
    }


    public static void ExecutePythonScript(string audioFilePath)
    {
        
        GD.Print("Executing python script");



        ProcessStartInfo start = new ProcessStartInfo
        {
            FileName = PYTHON_PATH,
            Arguments = $"\"{SCRIPT_PATH}\" \"{audioFilePath}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };
        // GD.Print("running command: \n" +
        //     $"{PYTHON_PATH} {SCRIPT_PATH} {audioFilePath}");

        using Process process = Process.Start(start);
        // Wait for the Python script to exit
        process.WaitForExit(); // This will block until the Python script is finished

        // Now that the process has finished, you can read the output
        string result = process.StandardOutput.ReadToEnd();
        string errorOutput = process.StandardError.ReadToEnd();
        // GD.Print("Error Output: " + errorOutput);
        //  GD.Print("string result: " + result);
        string trimmedResult = result.Trim().ToLower();

        var output = JObject.Parse(trimmedResult);
        GD.Print("Output: ", output);

        // Parse the JSON output
        // var output = JObject.Parse(result);

        // Check if the operation was successful
        if (output["success"].Value<bool>())
        {
            // Process was successful, handle beat times
            var beatTimes = output["beat_times"].ToObject<float[]>();
            var noteTypes = output["note_types"].ToObject<int[]>();
            // GD.Print("Beat Times: ", String.Join(", ", beatTimes));
            //  GD.Print("Beat Times: ", String.Join(", ", noteTypes));
            GD.Print("success");
        }
        else
        {
            // An error occurred, handle accordingly
            var errorMessage = output["error_message"].ToString();
            GD.Print("Error: " + errorMessage);
        }
        // The using statement ensures the process is properly disposed of after use



    }
}
