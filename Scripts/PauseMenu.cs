using System;
using Godot;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using NAudio;
using NAudioBPM;
using System.Globalization;
using NAudio.Wave;

public partial class PauseMenu : Control
{
    //const string PYTHON_PATH = @"c:\audio_importer\env\Scripts\python.exe";

    //const string SCRIPT_PATH = @"c:\audio_importer\createTimestamps.py";

    const string AUDIO_FOLDER_PATH_EDITOR = @"audio";

    const string AUDIO_FOLDER_PATH_RELEASE = @"";

    private List<string> songs = new();

    Button openFileExplorerButton;
    Button loadButton;

    NoteController noteController;



    public override void _Ready()
    {
        

        openFileExplorerButton = GetNode<Button>("AddNewSongsButton");
        openFileExplorerButton.Pressed += CheckForNewSongs;

        loadButton = GetNode<Button>("LoadSongButton");
        loadButton.Pressed += OnLoadSongPressed;


        //CheckForNewSongs();
    }
    

    private void CheckForNewSongs()
    {
        string file = @"audio\8-bit-circus.wav";
        int start = 0;
        int length = new AudioFileReader(file).TotalTime.Seconds;

        BPMDetector bpmDetector = new BPMDetector(file, start, length);

        if (bpmDetector.Groups.Length > 0)
        {
            List<float> beatTimings = bpmDetector.BeatPositions;
            List<int> beatsWithZeros = new List<int>(new int[beatTimings.Count]);

            // Serialize beatTimings and note_types to JSON
            JObject json = new()
            {
                ["beat_times"] = JArray.FromObject(beatTimings),
                ["note_types"] = new JArray(beatsWithZeros),
                ["tempo"] = bpmDetector.Groups[0].Tempo
            };

            // Convert JSON object to string
            string jsonString = json.ToString();

            // Print the JSON string
           // GD.Print("JSON: " + jsonString);

            // Dump JSON text to a .json file
            string jsonFilePath = Path.ChangeExtension(file, ".json");
            File.WriteAllText(jsonFilePath, jsonString);
        }
    }
    // Define a function to get beat timings from a file path
    

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

}
