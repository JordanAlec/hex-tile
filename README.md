# HexTile

![HexTile Logo](./assets/logo.png 'HexTile Logo')

> ⚠️ **THIS SOFTWARE IS FREE!** If you paid money for HexTile, you have been scammed. It is and always will be free and open source.

HexTile lets you control your HX Stomp foot switches when you can't get near them!

## About
HexTile is a desktop application that allows you to control your Line 6 HX Stomp or HX Stomp XL device via USB MIDI.

Plug your USB cable into your computer, launch HexTile, and you can control your device's footswitches, snapshots, tuner, and preset navigation from your computer screen.
The application doesn't NOT have access to change any of your device's settings, presets or any configuration. It's purely a utility to send MIDI commands via USB to your device. 
The functionality is simiilar to using a MIDI foot controller, but instead of physical footswitches, you use buttons on your computer screen.

If all the talk about MIDI sounds confusing, don't worry - HexTile is designed to be easy to use and requires no prior knowledge of MIDI. The goal of HexTile is to provide a simple and effective way to control your HX Stomp device remotely in a plug and play manner. All future improvements and features will not be at the detriment of this goal.

Logs are saved to `logs\hex_tile_log_{DATE}.log` for troubleshooting purposes. These are just text files and can be opened with any text editor. The key thing the logs provide is insight into is if your device is being detected by HexTile and if MIDI commands have failed to be sent.

## Limitations

- Windows only application
- Requires a Line 6 HX Stomp or HX Stomp XL device (naturally...)
- Requires the device to be connected via USB to your computer
- Requires your global settings (MIDI/Tempo) to have:
	- MIDI Base Channel set to match the channel configured in HexTile's Settings (default: 1)
	- USB MIDI set to On
- Some features may not be available depending on your device model. The below are only available on HX Stomp XL:
	- Footswitches 6 - 8
	- Snapshot 4
- Unfortunately it appears that HX Stomp devices do not support identity requests via USB MIDI so automatic detection of whether you have the XL model or not is not possible.
- Because these MIDI commands are fire and forget, there is no way for HexTile to confirm that your device has received the commands it sends.
- There is an 'artifical' delay during each command to ensure that the HX Stomp has enough time to process each command.

If any of the above limitations have a workaround or can be improved then I'll do my best to research, reflect and implement them after considering the effort vs benefit.
The limitations are written based on my own knowledge and testing at the time of writing. If you have any suggestions or improvements then please let me know.

## Potential Future Features

The features listed below are not guaranteed to be implemented, but are ideas for potential future development. They may be added however it depends on the effort vs benefit, how they align with the overall goals of the project and the demand for such features.
The goal of this project is to keep it simple and easy to use, so any additional features will be carefully considered.

- Looper functionality.
	- Potentially with a configurable delay option for when you want to start recording, so you can time appropriately.
- Test / reduce the delay between commands to make it more responsive.
	- For my use case its acceptable, but I know some may want it to be more responsive.
- Custom screen (commands) for advanced users.
	- This will allow users to create their own button layouts and functionality.
	- This may require some significant effort to implement, so will need to be considered carefully.
	- The goal of the project is to keep it simple and easy to use, so this may not align with that goal - This feature is not a given, its likely to be removed honestly.
- Potential cross platform application
	- Will likely mean moving away from WPF to MAUI if in .NET
	- If you believe it, MAUI isn't dead yet: https://ismauidead.net/
	- This will likely come about if there is a need and I settle on the direction of the app

## Settings

HexTile includes a Settings window accessible via `Actions > Settings` in the menu bar.

| Setting | Description | Default |
|---|---|---|
| MIDI Base Channel | The MIDI channel HexTile sends commands on. Must match the MIDI Base Channel set in your HX Stomp's global settings. | 1 |

Settings are saved automatically and persist between sessions.

## Installation

- Download the setup executable.
- Double click and choose your installation path (appdata by default)
- Finish, check to allow desktop shortcut (if you want), and launch
- Log files are located in the installation location under "logs"

## Status

Development has been paused temporarily whilst I use the application, gather specific feedback from trusted individuals, etc. 

This will ultimately drive implementation and help evaluate the future above goals. 

It maybe that this first release is the only release as it meets the many requirements initially set out to do and only maintenance updates are made. My gut feel is that this is unlikely as I'm personally keen to implement the looper functionality and reduce the delays between commands where possible.

Please see the releases for the version 1.0.0. Please note that this is currently only on windows. This is purely because I made this for myself and a few friends that showed interest as well and we all own windows devices. I'd love to make it cross platform but the goal was to get something usable as a v1.

Please feel to download, install and use at your leisure.

### Notices

- This project is not affiliated with or endorsed by Line 6, Inc.
- No AI has been used in the creation of this project**. The only exception is the generated image used in the logo, which was created using an AI image generation tool. The plan is to replace this in the future.


** : The code has been written by the repository owner without the use of explicit AI assistance. Unfortunately IDEs such as Visual Studio have AI-assisted code completion features (e.g., IntelliCode) that may have influenced the code.
