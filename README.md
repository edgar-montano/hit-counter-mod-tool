# hit-counter-mod-tool
**Hit Counter Mod Tool** is designed to monitor memory values and automate hit detection. The Hit Counter Mod Tool was designed for use in conjunction with StreamLabs or OBS streaming software.

**DISCLAIMER: WHILE HCMT ONLY READS MEMORY ADDRESS VALUES, IT IS NOT RECOMMENDED TO USE THIS FOR ANY ONLINE MULTIPLAYER GAME. USE AT YOUR OWN DISCRETION.**

## How it works 

HCMT uses VAMemory to read the specified process memory address. From here HCMT will monitor any changes in this memory address value, upon each change, a buffer would store the difference in the changes and dump the increments to a file. This file can be streamed via StreamLabs or OBS by loading up a text file option. 

## How to compile

Compilation is fairly straight forward. It requires Visual Studio 2019 and the latest .NET framework (3.5 or greater) to compile. In addition you need to manually download VAMemory and link it to the references in the project file.

Download link for [VAMemory](https://vivid-abstractions.net/logical/programming/vamemory-c-memory-class-net-3-5/)

Open the solution in Visual Studio, in the Solution Explorer under references, right click -> add reference -> add VAMemory.dll to project

From here you can simply build the project. 

## How to run 

As of now, HCMT is a command-line interface application. The following demonstrates and example of how to run HCMT in either cmd or powerline:

`./ds-counter.exe <game_name> <file_path_for_output> <static_ptr_adddress>`

So the following is an accepted launch for monitoring Dark Souls Remastered (note the omission of the .exe extension on the game name)

`./ds-counter.exe DarkSoulsRemastered ./hitcounter.txt 0D914C88 `

**Note: In order to run HCMT you need to have downloaded [VAMemory](https://vivid-abstractions.net/logical/programming/vamemory-c-memory-class-net-3-5/) and place it in the same folder as the executable. VAMemory is an external library that allows a program to read and write process values, therefore some anti-virus systems may flag it as a false-positive. HCMT does not write to memory address, it simply reads from it.**



### How to obtain the file name 

The file name can be obtained by opening up task manager and located the main game file you wish to hook to. 

### How to obtain a static pointer address

Static pointers can easily be obtained using CheatEngine and Cheat Engine Table. Simply locate the memory value of cheat engine and copy and paste this value. 

## List of Tested Games

In theory, any game in which you can obtain a static pointer to the health value can be supported, however these are the following list of games I specifically test to ensure HCMT works. 

- Dark Souls: Remastered
- Dark Souls 3
- Sonic Mania
- Hollow Knight
- For The King (*intptr must be converted to long for it to work for online*)

