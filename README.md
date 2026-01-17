# HexTile

![HexTile Logo](./assets/logo.png 'HexTile Logo')

HexTile lets you control your HX Stomp foot switches when you can't get near them!

## About
HexTile is a desktop application that allows you to control your Line 6 HX Stomp or HX Stomp XL device via USB MIDI.
Plug your USB cable into your computer, launch HexTile, and you can control your device's footswitches, snapshots, tuner, and preset navigation from your computer screen.
The application doesn't NOT have access to change any of your device's settings, presets or any configuration. It's purely a utility to send MIDI commands via USB to your device. 
The functionality is simiilar to using a MIDI foot controller, but instead of physical footswitches, you use buttons on your computer screen.

Logs are saved to `logs\hex_tile_log_{DATE}.log` for troubleshooting purposes. These are just text files and can be opened with any text editor. The key thing the logs provide is insight into is if your device is being detected by HexTile and if MIDI commands have failed to be sent.

## Limitations

- Requires a Line 6 HX Stomp or HX Stomp XL device (naturally...)
- Requires the device to be connected via USB to your computer
- Requires your global settings (MIDI/Tempo) to have:
	- MIDI Base Channel set to 1
	- USB MIDI set to On
- Some features may not be available depending on your device model. The below are only available on HX Stomp XL:
	- Footswitches 6 - 8
	- Snapshot 4

Identity requests via USB MIDI are not supported by Line 6 devices, so HexTile cannot automatically detect your connected device.
Because MIDI CC commands are essentially fire and forget, HexTile cannot confirm that your device has received the commands it sends.

## Current Features

- Toggle on board Tuner
- Navigation to the next and previous presets
- Toggle footswitches (1 - 8)**
- Switch and navigation between Snapshots**


** : Footswitches 6 - 8 are only available on HX Stomp XL
** : Snapshot 4 is only available on HX Stomp XL

## Future Features

- Improved theming, multiple screens that group functions instead of a long list, look overhaul of the UI

_The above will signal v1 release and a switch to public_
- Looper functionality
- Keyboard shortcuts (where applicable)

## Status

_Development in progress / repo owner currently using_

### Notices

- This project is not affiliated with or endorsed by Line 6, Inc.
- No AI has been used in the creation of this project**. The only exception is the generated image used in the logo, which was created using an AI image generation tool. The plan is to replace this in the future.


** : The code has been written by the repository owner without the use of explicit AI assistance. Unfortunately IDEs such as Visual Studio have AI-assisted code completion features (e.g., IntelliCode) that may have influenced the code.
