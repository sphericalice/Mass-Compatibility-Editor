# Mass Compatibility Editor v1.1
by Spherical Ice
http://www.pokecommunity.com/showthread.php?t=365264

Requites .NET 4.0.

This tool allows the user to quickly edit the TM and HM compatibility of large amounts of Pokémon in a GBA Pokémon ROM.
Compatible with BPRE and BPEE ROMs.

This tool supports ROMs with repointed things, but still makes some assumptions:
- each entry in the TM/HM compatibility table is 8 bytes long.
- each entry in the move tutor compatibility table is 2 bytes long in BPRE and 4 bytes in BPEE.
- there are, at most:
	- 50 TMs
	- 8 HMs
	- 15 move tutor moves in BPRE
	- 30 move tutor moves in BPEE
	- 511 moves in total
	- moves of name length 0xD
- the string terminating byte is 0xFF
- the following offsets are true:
	BPRE:
        Pointer to the TM compatibility table is 0x43C68
        Pointer to the Move Tutor compatibility table is 0x120C30
        Pointer to the TM move list is 0x125A8C
        Pointer to the Move Tutor move list is 0x120BE4
        Pointer to the list of move names is 0x148
    BPEE:
        Pointer to the TM compatibility table is 0x6E048
        Pointer to the Move Tutor compatibility table is 0x1B2390
        Pointer to the TM move list is 0x1B6D10
        Pointer to the Move Tutor move list is 0x13AF04
        Pointer to the list of move names is 0x148

Eventually the tool may be updated to support these things being changed, probably with an INI file.

The tool automatically makes changes: there's no save button. Make backups before using it!

1. Load ROM using:
	> File...
	> Load ROM
   or Ctrl + O.

2. To mass-remove compatibility, change the TM #: field to the TM# you want to change. Enter the range of Pokémon to remove compatibility from in the relevant fields. Click 'Remove compatibility'.
3. Click Editor Mode and select the relevant mode to swap between TM, HM and Move Tutor editing.
4. To mass-add compatibility, create a text file. The file should be a list of decimal Pokémon species numbers, separated by newlines.
1
152
277
The above would represent TM compatibility for Bulbasaur, Chikorita and Treecko (in an unedited ROM).
Note that it is NOT the Pokédex number (Treecko's Pokédex number is 252, but its species number is 277).
Load this text file and click Add compatibility.

# Special thanks to:
Lostelle
karatekid552
stackoverflow (see source code for more information)
