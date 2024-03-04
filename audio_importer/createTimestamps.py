import librosa
import sys
import json
import os  # Import the os module

# Initialize a dictionary for output
output = {}
beat_frames = []
try:
    # Check if an argument is passed
    if len(sys.argv) < 2:
        raise ValueError("No audio file path provided")

    filename = sys.argv[1]

    # Attempt to load the audio file and process it
    y, sr = librosa.load(filename, sr=None)
    tempo, beat_frames = librosa.beat.beat_track(y=y, sr=sr)
    beat_times = librosa.frames_to_time(beat_frames, sr=sr)

    # If successful, store the results in the output dictionary
    output['beat_times'] = beat_times.tolist()
    output['tempo'] = tempo
    output['success'] = True

except Exception as e:
    # If an error occurs, store an error message and success flag in the output dictionary
    output['error_message'] = str(e)
    output['success'] = False

# Determine the output JSON file name based on the input file
json_filename = os.path.splitext(filename)[0] + '.json'

# Write the output dictionary to a JSON file
with open(json_filename, 'w') as f:
    json.dump(output, f)

# Read the JSON file and print its contents
with open(json_filename, 'r') as f:
    json_data = json.load(f)
    print(json_data)
