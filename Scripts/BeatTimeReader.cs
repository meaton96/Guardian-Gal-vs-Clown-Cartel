using System;
using Godot;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class BeatTimeReader : Control
{
    const string PYTHON_PATH = @"audio_importer\pythonEnvLib\Scripts\python.exe";
    const string SCRIPT_PATH = @"audio_importer\createTimestamps.py";

    Button openFileExplorerButton;
    Button confirmButton;

    public override void _Ready()
    {
      //  string audioFilePath = @"audio\8-bit-circus.wav";
     //   ExecutePythonScript(audioFilePath);
        // float[] beatTimes = ReadBeatTimes();
        //  GD.Print(beatTimes);
        openFileExplorerButton = GetNode<Button>("box/OpenFileExplorerButton");
        openFileExplorerButton.Pressed += OpenFileExplorer;
    }

    public void OpenFileExplorer() {
        var dialog = new FileDialog
        {
            Mode = FileDialog.ModeEnum.Windowed,
            CurrentDir = "res://audio",
            Filters = new string[] { "*.wav" }
        };
        dialog.PopupCentered();
    }
    // public partial class OpenFileDialog : FileDialog
    // {
    //     public override void _Ready()
    //     {
    //         Mode = ModeEnum.Windowed;
    //         Access = AccessEnum.Filesystem;
    //         Filters = new string[] { "*.wav", "*.mp3" };
    //         PopupCentered();
    //     }
    // }
    public void OnFileDialogFileSelected(string path)
    {
        GD.Print("Selected file: " + path);
        ExecutePythonScript(path);
    }

    public static void ExecutePythonScript(string audioFilePath)
    {
        GD.Print("Current directory: " + Directory.GetCurrentDirectory());
        GD.Print("Executing python script");
        ProcessStartInfo start = new()
        {
            FileName = PYTHON_PATH,
            Arguments = string.Format("\"{0}\" \"{1}\"", SCRIPT_PATH, audioFilePath),
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };
        using Process process = Process.Start(start);
        string result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        // Parse the JSON output
        var output = JObject.Parse(result);

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


    }

    public static float[] ReadBeatTimes()
    {
        var json = File.ReadAllText("beat_times.json");
        return JsonConvert.DeserializeObject<float[]>(json);
    }
}
