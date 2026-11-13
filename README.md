# Discord Spoof

A Windows Forms activity test utility with launchers for ten application names.
The included executables are dummy processes, not the actual games or browser.
Discord detection and quest completion are unverified.

## Run

1. Download the project and keep its folder structure intact.
2. Open the `Start … .cmd` launcher for the application you want to test.
3. Run one activity test at a time. Close its window to stop it.

The window displays local elapsed time, not Discord quest progress.
See [อ่านก่อนใช้.txt](อ่านก่อนใช้.txt) for the original Thai instructions.

## Source

`ActivityTest.cs` contains the Windows Forms implementation. The process name
selects the displayed application. The utility does not connect to the network
or modify Discord.

The project includes launchers and small prebuilt executables for Roblox,
MapleStory Classic World, Where Winds Meet, Honkai: Star Rail, Deadzone Rogue 2,
Arknights: Endfield, Once Human, Firefox, EA Sports FC 27, and ASPHALT LEGENDS.
