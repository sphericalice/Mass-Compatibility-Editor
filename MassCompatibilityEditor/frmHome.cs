using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using GBA;

// The species text file is a list of decimal Pokémon species numbers separated by newlines.
// 1
// 152
// 277
// The above would represent TM compatibility for Bulbasaur, Chikorita and Treecko (in an unedited ROM).
// Note that it is NOT the Pokédex number (Treecko's Pokédex number is 252, but its species number is 277).

namespace MassCompatibilityEditor // This tool allows the user to quickly edit the TM and HM compatibility of large amounts of Pokémon in a GBA Pokémon ROM.
{

    public partial class frmHome : Form
    {
        private ROM LoadedRom; // Stores data about the loaded ROM, particularly its base ROM.
        private string RomFileName; // Path of the loaded ROM.
        private string SpeciesTextFileName; // Path of the species text file. See comments at the top of this document for further information.

        private string CurrentMode; // Determines what mode to use: TM, HM, or Tutor.

        private int PointerToTMCompatibilityTable; // Location in the ROM of the pointer to the TM compatibility table.
        private int PointerToMoveTutorCompatibilityTable; // Location in the ROM of the pointer to the Move Tutor compatibility table.
        private int NumberOfMoveTutorMoves; // Number of moves in the move tutor list.
        private int PointerToTMMoveList; // Location in the ROM of the pointer to the list of TM moves.
        private int PointerToMoveTutorMoveList; // Location in the ROM of the pointer to the list of Move Tutor moves.
        private int PointerToMoveNames; // Location in the ROM of the pointer to the list of move names.
        private int SizeOfCompatibilityTableEntry; // Size of an individual entry in the compatibility table.
        private int GameCodeLocation = 0xAC; // Location in the ROM of the game code (BPRE, BPEE, etc.).
        private int PointerToNumberOfSpecies; // Location in the ROM of the number of Pokémon species.
        private int NumberOfSpecies = 411; // Number of species of Pokémon.

        public List<string> ListOfMoveNames = new List<string>(); // List of the names of all moves in the ROM.
        public List<string> ListOfMoves = new List<string>(); // The specific list of moves for a given set, whether that be TMs, HMs or Move Tutor moves.

        private Dictionary<byte, char> CharacterValues; // Dictionary of the GBA games' character encoding and their equivalents.

        public frmHome()
        {
            InitializeComponent();
            RomFileName = "";
            CharacterValues = ReadTableFile(System.Windows.Forms.Application.StartupPath + @"\table.ini"); // Try to load the table file into a Dictionary file.
            if (CharacterValues == null)
            { // If this fails, create a new table.ini file.
                string[] lines = { "[Table]", "0=\" \"", "1=À", "2=Á", "3=Â", "4=Ç", "5=È", "6=É", "7=Ê", "8=Ë", "9=Ì", "B=Î", "C=Ï", "D=Ò", "E=Ó", "F=Ô", "10=Œ", "11=Ù", "12=Ú", "13=Û", "14=Ñ", "15=ß", "16=à", "17=á", "19=ç", "1A=è", "1B=é", "1C=ê", "1D=ë", "1E=ì", "20=î", "21=ï", "22=ò", "23=ó", "24=ô", "25=œ", "26=ù", "27=ú", "28=û", "29=ñ", "2A=º", "2B=ª", "2D=&", "2E=+", "34=[Lv]", "35==", "36=;", "51=¿", "52=¡", "53=[pk]", "54=[mn]", "55=[po]", "56=[ké]", "57=[bl]", "58=[oc]", "59=[$k]", "5A=Í", "5B=%", "5C=(", "5D=)", "68=â", "6F=í", "79=[up]", "7A=[down]", "7B=[left]", "7C=[right]", "85=<", "86=>", "A1=0", "A2=1", "A3=2", "A4=3", "A5=4", "A6=5", "A7=6", "A8=7", "A9=8", "AA=9", "AB=!", "AC=?", "AD=.", "AE=-", "AF=·", "B0=[...]", "B1=«", "B2=»", "B3=[`]", "B4='", "B5=[m]", "B6=[f]", "B7=$", "B8=,", "B9=*", "BA=/", "BB=A", "BC=B", "BD=C", "BE=D", "BF=E", "C0=F", "C1=G", "C2=H", "C3=I", "C4=J", "C5=K", "C6=L", "C7=M", "C8=N", "C9=O", "CA=P", "CB=Q", "CC=R", "CD=S", "CE=T", "CF=U", "D0=V", "D1=W", "D2=X", "D3=Y", "D4=Z", "D5=a", "D6=b", "D7=c", "D8=d", "D9=e", "DA=f", "DB=g", "DC=h", "DD=i", "DE=j", "DF=k", "E0=l", "E1=m", "E2=n", "E3=o", "E4=p", "E5=q", "E6=r", "E7=s", "E8=t", "E9=u", "EA=v", "EB=w", "EC=x", "ED=y", "EE=z", "EF=[&hEF]", "F0=:", "F1=Ä", "F2=Ö", "F3=Ü", "F4=ä", "F5=ö", "F6=ü", "F7=[&hF7]", "F8=[&hF8]", "F9=[&hF9]", "FA=[&hFA]", "FB=[&hFB]", "FC=[&hFC]", "FD=[&hFD]", "FE=[nl]", "FF=[end]" };
                System.IO.File.WriteAllLines(System.Windows.Forms.Application.StartupPath + @"\table.ini", lines);
                CharacterValues = ReadTableFile(System.Windows.Forms.Application.StartupPath + @"\table.ini");
            }
        }

        private enum OpenROMResult
        {
            Success, FileNotFound, NoM4A
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath); // Load the icon.
            LockControls();
            CurrentMode = "TM";
        }

        private string ROMCharactersToString(int MaxStringLength, uint StringLocation, byte TerminatorByte = 0xFF)
        {
            // From https://github.com/Joexv/IPES/blob/master/IPES/Form1.cs
            // Accessed 16/03/2016

            string result = ""; // Initialize the string to be returned.
            using (GBABinaryReader RomRead = new GBABinaryReader(LoadedRom)) // Access the ROM as a readable file.
            {
                for (int CurrentCharacter = 0; CurrentCharacter < MaxStringLength; CurrentCharacter++) // Loop through the set length of the string.
                {
                    RomRead.BaseStream.Seek(StringLocation + CurrentCharacter, SeekOrigin.Begin); // Go to the current character's ROM location.
                    byte CharacterByte = RomRead.ReadByte(); // Read the byte of the next character.
                    if ((CharacterByte != TerminatorByte)) // If it is not the terminator...
                    {
                        char temp = ';';
                        bool success = CharacterValues.TryGetValue(CharacterByte, out temp); // ... then try to retrieve its ASCII equivalent from the dictionary.
                        if (success)
                        {
                            result += temp; // If the character was successfully retrieved, add it to the string to be returned.
                        }
                    }
                    else // If the terminator has been reached, the string is to be assumed complete, so exit the loop.
                    {
                        break;
                    }
                }
            }
            return result;
        }

        private Dictionary<byte, char> ReadTableFile(string TableFileFileName)
        {
            // From https://github.com/Joexv/IPES/blob/master/IPES/Form1.cs
            // Accessed 16/03/2016

            if (File.Exists(TableFileFileName))
            {
                Dictionary<byte, char> result = new Dictionary<byte, char>(); // Create a new Dictionary.
                string[] TableFile = System.IO.File.ReadAllLines(TableFileFileName); // Load the table file.

                int index = 0;
                foreach (string CurrentLineFromFile in TableFile)
                {
                    if (!CurrentLineFromFile.Equals("") && !CurrentLineFromFile[0].Equals('[') && index != 0x9E && index != 0x9F)
                    {
                        string[] HexCharacterCode = CurrentLineFromFile.Split('='); // Get the hex character code from the current line of the file.
                        switch (Byte.Parse(HexCharacterCode[0], System.Globalization.NumberStyles.HexNumber)) // Convert it to hex and parse it as a byte.
                        {
                            default: // If it is not a special character...
                                result.Add(Byte.Parse(HexCharacterCode[0], System.Globalization.NumberStyles.HexNumber), HexCharacterCode[1].ToCharArray()[0]); // Then retrieve the provided ASCII equivalent and add it to the Dictionary.
                                break;
                            // Special characters:
                            case 0:
                                result.Add(0x00, ' ');
                                break;
                            case 0x35:
                                result.Add(0x35, '=');
                                break;
                            case 0x34:
                            case 0x53:
                            case 0x54:
                            case 0x55:
                            case 0x56:
                            case 0x57:
                            case 0x58:
                            case 0x59:
                            case 0x79:
                            case 0x7A:
                            case 0x7B:
                            case 0x7C:
                            case 0xB0:
                            case 0xEF:
                            case 0xF7:
                            case 0xF8:
                            case 0xF9:
                            case 0xFA:
                            case 0xFB:
                            case 0xFC:
                            case 0xFD:
                            case 0xFE:
                            case 0xFF:
                                break;
                        }
                        index++; // Move to the next line of the table file.
                    }
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        public static string ReverseString(string StringToBeReversed)
        {
            // From http://stackoverflow.com/a/228060

            char[] charArray = StringToBeReversed.ToCharArray();

            Array.Reverse(charArray);

            return new string(charArray);
        }

        public static string ConvertToBinary(ulong value)
        {
            // From http://stackoverflow.com/a/6986104

            if (value == 0)
            {
                return "0";
            }

            System.Text.StringBuilder result = new System.Text.StringBuilder();

            while (value != 0)
            {
                result.Insert(0, ((value & 1) == 1) ? '1' : '0');
                value >>= 1;
            }

            return result.ToString();
        }

        public static string BinaryStringToHexString(string binary)
        {
            // From http://stackoverflow.com/a/5612479

            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

            if ((binary.Length % 8) != 0)
            {
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }

        public static byte[] StringToByteArray(String StringToConvert)
        {
            // From http://stackoverflow.com/a/311179

            int NumberChars = StringToConvert.Length;
            byte[] result = new byte[NumberChars / 2];

            for (int i = 0; i < NumberChars; i += 2)
            {
                result[i / 2] = Convert.ToByte(StringToConvert.Substring(i, 2), 16);
            }

            return result;
        }

        public static string ByteArrayToString(byte[] ByteArrayToConvert)
        {
            // From http://stackoverflow.com/a/311179

            string result = BitConverter.ToString(ByteArrayToConvert);

            return result.Replace("-", "");
        }

        private void ReadRomData(string FileName)
        {
            using (GBABinaryReader RomRead = new GBABinaryReader(File.OpenRead(FileName))) // Access the ROM as a readable file.
            {
                RomRead.BaseStream.Seek(GameCodeLocation, SeekOrigin.Begin); // Go to the location of the ROM code.
                string code = Encoding.UTF8.GetString(RomRead.ReadBytes(4)); // Read the ROM code.

                LoadedRom.File = FileName;
                LoadedRom.Code = code;
            }
        }

        private void menuLoadROM_Click(object sender, EventArgs e)
        {
            LockControls();

            openFileDialog.FileName = "";
            openFileDialog.Filter = "GameBoy Advance ROMs|*.gba";
            openFileDialog.Title = "Open ROM";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string OldRomFileName = RomFileName; // In case the ROM that has been loaded is unsupported and the tool needs to undo any changes made.
                RomFileName = openFileDialog.FileName;

                if (File.Exists(RomFileName) && (openFileDialog.FileName.EndsWith(".gba") || openFileDialog.FileName.EndsWith(".GBA"))) // Verify the file exists and is a GBA ROM.
                {

                    ReadRomData(RomFileName);

                    switch (LoadedRom.Code)
                    {
                        case "BPRE":
                            // Moves
                            PointerToMoveNames = 0x148;

                            // Pokémon
                            PointerToNumberOfSpecies = 0x459EC;

                            // TMs
                            PointerToTMCompatibilityTable = 0x43C68;
                            PointerToTMMoveList = 0x125A8C;

                            // Move Tutor
                            PointerToMoveTutorCompatibilityTable = 0x120C30;
                            PointerToMoveTutorMoveList = 0x120BE4;
                            NumberOfMoveTutorMoves = 15;

                            UpdateROMData();
                            UnlockControls();
                            break;
                        case "BPEE":
                            // Moves
                            PointerToMoveNames = 0x148;

                            // Pokémon
                            PointerToNumberOfSpecies = 0x70080;

                            // TMs
                            PointerToTMCompatibilityTable = 0x6E048;
                            PointerToTMMoveList = 0x1B6D10;

                            // Move Tutor
                            PointerToMoveTutorCompatibilityTable = 0x1B2390;
                            PointerToMoveTutorMoveList = 0x13AF04;
                            NumberOfMoveTutorMoves = 30;

                            UpdateROMData();
                            UnlockControls();
                            break;
                        case "AXVE":
                        case "AXPE":
                        case "BPGE":
                        default:
                            MessageBox.Show("ROM type " + LoadedRom.Code + " is not supported.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            if (OldRomFileName != "") // If there was a ROM loaded before the unsupported ROM was loaded...
                            {
                                RomFileName = OldRomFileName; // ...then reload it...
                                ReadRomData(RomFileName); // ...and reset the global variables to undo the changes.
                            }
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("No ROM file found, or the ROM file was corrupted." + Environment.NewLine + Environment.NewLine + "The ROM must be a .GBA ROM file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLoadSpeciesFile_Click(object sender, EventArgs e)
        {
            LockControls();

            openFileDialog.FileName = "";
            openFileDialog.Filter = "Text files|*.txt";
            openFileDialog.Title = "Load species list";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SpeciesTextFileName = openFileDialog.FileName;

                if (File.Exists(SpeciesTextFileName) && (openFileDialog.FileName.EndsWith(".txt") || openFileDialog.FileName.EndsWith(".TXT"))) // Verify the file exists and is a text file.
                {
                    btnAddCompatibility.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No species file found, or the species file was corrupted." + Environment.NewLine + Environment.NewLine + "The species file should be a text file with a list of decimal Pokémon species IDs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnAddCompatibility.Enabled = false;
                }
            }

            UnlockControls();
        }

        private void btnRemoveCompatibility_Click(object sender, EventArgs e)
        {
            LockControls();
            DialogResult result = DialogResult.Yes;
            if (numPokemonMax.Value > NumberOfSpecies)
            {
                result = MessageBox.Show("The range of Pokémon species exceeds the maximum number of species in the ROM." + Environment.NewLine + "This may lead to ROM corruption." + Environment.NewLine + Environment.NewLine + "Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }

            if (numPokemonMin.Value > numPokemonMax.Value)
            {
                result = MessageBox.Show("The 'From' value is larger than the 'to' value." + Environment.NewLine + "Continuing may lead to ROM corruption." + Environment.NewLine + Environment.NewLine + "Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }

            if (result == DialogResult.Yes)
            {
                SetCompatibility('0');
            }

            UnlockControls();
        }

        private void btnAddCompatibility_Click(object sender, EventArgs e)
        {
            LockControls();

            // Load the data from the SpeciesTextFile into a List of strings.
            // This code assumes that the SpeciesTextFile is structured as explained at the top of this source code.
            List<string> ListOfCompatibleSpecies = new List<string>();
            int CurrentLineNumber = 0;
            string CurrentLine;

            System.IO.StreamReader SpeciesTextFile = new System.IO.StreamReader(SpeciesTextFileName); // Read the SpeciesTextFile.
            while ((CurrentLine = SpeciesTextFile.ReadLine()) != null) // Loop through the code, reading each line from the file.
            {
                try
                {
                    int test = Convert.ToInt32(CurrentLine); // Confirm the CurrentLine is a number.
                    ListOfCompatibleSpecies.Add(CurrentLine); // Add it to the list of species.
                }
                catch
                {
                    // If the code failed to convert the current line to a number, show this message to inform the user, skip this line, and continue loading.
                    MessageBox.Show("Failed to read species number at line #" + Convert.ToString(CurrentLineNumber) + "." + Environment.NewLine + "This line will be skipped and loading shall continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                CurrentLineNumber++;
            }
            SpeciesTextFile.Close();

            SetCompatibility('1', ListOfCompatibleSpecies);

            UnlockControls();
        }

        private void SwitchMode(string Mode = "TM")
        {
            // Exit out of the current mode.
            menuTM.Checked = false;
            menuHM.Checked = false;
            menuTutor.Checked = false;

            CurrentMode = Mode; // Update to the new mode.

            switch (CurrentMode) // Update the form and mode-specific variables.
            {
                case "TM":
                default:
                    SizeOfCompatibilityTableEntry = 8;
                    menuTM.Checked = true;
                    break;
                case "HM":
                    SizeOfCompatibilityTableEntry = 8;
                    menuHM.Checked = true;
                    break;
                case "Tutor":
                    SizeOfCompatibilityTableEntry = 2;
                    menuTutor.Checked = true;
                    break;
            }

            numTMNumberRemove.Value = 1; // Reset the num boxes to 1 to prevent errors with differing list sizes.
            numTMNumberAdd.Value = 1; // E.g. Value = 50, then switch to Tutor mode would break as the list is not long enough.
            UpdateROMData(); // Update the move lists.

            // Update the rest of the form.
            lblTMNumberAdd.Text = CurrentMode + " #:";
            lblTMNumberRemove.Text = CurrentMode + " #:";

            numTMNumberAdd.Maximum = ListOfMoves.Count;
            numTMNumberRemove.Maximum = ListOfMoves.Count;
        }


        private void menuTM_Click(object sender, EventArgs e)
        {
            SwitchMode("TM");
        }

        private void menuHM_Click(object sender, EventArgs e)
        {
            SwitchMode("HM");
        }

        private void menuTutor_Click(object sender, EventArgs e)
        {
            SwitchMode("Tutor");
        }

        private void numTMNumberRemove_ValueChanged(object sender, EventArgs e)
        {
            UpdateROMData();
        }

        private void numTMNumberAdd_ValueChanged(object sender, EventArgs e)
        {
            UpdateROMData();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LockControls()
        {
            // Prevents access to the form.
            // Update this every time a new component is added to the form.
            // Call this routine at the beginning of every sensitive operation to prevent user interference.

            // Menu components
            menuChangeMode.Enabled = false;

            // 'Mass-remove compatibility' components.
            groupMassRemove.Enabled = false;
            lblTMNumberRemove.Enabled = false;
            numTMNumberRemove.Enabled = false;
            lblPokemonRemove.Enabled = false;
            lblFrom.Enabled = false;
            numPokemonMin.Enabled = false;
            lblTo.Enabled = false;
            numPokemonMax.Enabled = false;
            btnRemoveCompatibility.Enabled = false;

            // 'Mass-add compatibility' components.
            groupMassAdd.Enabled = false;
            lblTMNumberAdd.Enabled = false;
            numTMNumberAdd.Enabled = false;
            btnLoadSpeciesFile.Enabled = false;
            // btnAddCompatibility is disabled until SpeciesTextFile is loaded.
        }

        private void UnlockControls()
        {
            // Inverse of LockControls.
            // Enables access to the form.
            // Update this every time a new component is added to the form.
            // Call this routine at the end of every sensitive operation, and when a ROM is successfully loaded, to enable user control.

            // Menu components
            menuChangeMode.Enabled = true;

            // 'Mass-remove compatibility' components.
            groupMassRemove.Enabled = true;
            lblTMNumberRemove.Enabled = true;
            numTMNumberRemove.Enabled = true;
            lblPokemonRemove.Enabled = true;
            lblFrom.Enabled = true;
            numPokemonMin.Enabled = true;
            lblTo.Enabled = true;
            numPokemonMax.Enabled = true;
            btnRemoveCompatibility.Enabled = true;

            // 'Mass-add compatibility' components.
            groupMassAdd.Enabled = true;
            lblTMNumberAdd.Enabled = true;
            numTMNumberAdd.Enabled = true;
            btnLoadSpeciesFile.Enabled = true;
            // btnAddCompatibility is enabled when SpeciesTextFile is successfully loaded.

            UpdateROMData(); // This is mainly to update the move name label when the ROM is first loaded.
        }

        private void UpdateROMData()
        {
            // Retrieve the number of Pokémon species in the ROM.
            using (GBABinaryReader RomRead = new GBABinaryReader(LoadedRom)) // Access the ROM as a readable file.
            {
                RomRead.BaseStream.Seek(PointerToNumberOfSpecies, SeekOrigin.Begin); // Go to the pointer to the number of species.
                NumberOfSpecies = RomRead.ReadUInt16(); // Read the number of species. It is two bytes long, so is a 16bit integer.
            }

            numPokemonMin.Value = 0;
            numPokemonMax.Value = NumberOfSpecies;

            // Clear the move lists.
            ListOfMoveNames.Clear();
            ListOfMoves.Clear();

            int PointerToMoveList = PointerToTMMoveList; // Take the TM as default.
            int NumberOfMovesInList = 50;

            switch (CurrentMode)
            {
                case "TM": // TMs are taken as default, so just exit the switch.
                default:
                    break;
                case "HM": // HMs are a part of the TM move list, so the PointerToMoveList is the same.
                    NumberOfMovesInList = 8; // However, there are only eight HMs, so change the NumberOfMovesInList to reflect this.
                    break;
                case "Tutor":
                    PointerToMoveList = PointerToMoveTutorMoveList;
                    NumberOfMovesInList = NumberOfMoveTutorMoves;
                    break;
            }

            using (GBABinaryReader RomRead = new GBABinaryReader(LoadedRom)) // Access the ROM as a readable file.
            {
                RomRead.BaseStream.Seek(PointerToMoveList, SeekOrigin.Begin); // Go to the pointer to the move list.
                uint MoveList = RomRead.ReadPointer(); // Read the pointer.

                if (CurrentMode == "HM")
                {
                    MoveList += 50 * 2; // Skip past the TMs to the HM list.
                }

                RomRead.BaseStream.Seek(PointerToMoveNames, SeekOrigin.Begin);
                uint MoveNames = RomRead.ReadPointer();
                for (uint i = 0; i < 0x1FF; i++) // Loop through every move and retrieve its name.
                {
                    string MoveName = ROMCharactersToString(0xD, MoveNames + i * 0xD); // Retrieve the name from its ROM location and convert it from the Pokémon character encoding to ASCII.
                    ListOfMoveNames.Add(MoveName); // Add the name to the list.
                }

                RomRead.BaseStream.Seek(MoveList, SeekOrigin.Begin); // Go to the move list.
                for (int i = 0; i < NumberOfMovesInList; i++)
                {
                    int MoveId = RomRead.ReadUInt16(); // Read the 2-byte little endian move ID.
                    if (MoveId <= 0x1FF)
                    {
                        ListOfMoves.Add(ListOfMoveNames[MoveId]); // Add the name of the move MoveId.
                    }
                    else
                    {
                        break; // If move ID is too large, or the list delimiter has been reached, exit the loop.
                    }
                }
            }

            lblSelectedMoveRemove.Text = ListOfMoves[Convert.ToInt32(numTMNumberRemove.Value) - 1]; // Update the form.
            lblSelectedMoveAdd.Text = ListOfMoves[Convert.ToInt32(numTMNumberAdd.Value) - 1];
        }

        private void UnknownError()
        {
            MessageBox.Show(this, "An error has occured." + Environment.NewLine + "Please try to perform the action again." + Environment.NewLine + "If this error persists, report it in the download thread with an explanation of the actions taken to prompt this error message.", "Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SetCompatibility(char BitToChangeTo, List<string> ListOfCompatibleSpecies = null)
        {
            int MoveNumber = -1; // The bitfields are zero-indexed.
            if (CurrentMode == "HM")
            {
                MoveNumber += 50; // The TM and HM compatibility lists are contiguous, so HMs start after the 50 TMs.
            }
            switch (BitToChangeTo)
            {
                case '0':
                    for (int CurrentPokemon = Convert.ToInt32(numPokemonMin.Value); CurrentPokemon <= Convert.ToInt32(numPokemonMax.Value); CurrentPokemon++)
                    {
                        MoveNumber += Convert.ToInt32(numTMNumberRemove.Value); // Get the user-defined move number.
                        EditCompatibility(BitToChangeTo, CurrentPokemon, MoveNumber); // Update the compatibility of every Pokémon within the user-defined range.
                    }
                    break;
                case '1':
                    for (int CurrentPokemon = 0; CurrentPokemon < ListOfCompatibleSpecies.Count; CurrentPokemon++)
                    {
                        MoveNumber += Convert.ToInt32(numTMNumberAdd.Value); // Get the user-defined move number.
                        EditCompatibility(BitToChangeTo, Convert.ToInt32(ListOfCompatibleSpecies[CurrentPokemon]), MoveNumber); // Update the compatibility of every Pokémon listed in the SpeciesTextFile.
                    }
                    break;
                default:
                    UnknownError(); // In the event of typos.
                    break;
            }
        }

        private void EditCompatibility(char BitToChangeTo, int PokemonToChange, int MoveNumber)
        {
            // Assume TM/HM editing to be the default.
            int PointerToCompatibilityTable = PointerToTMCompatibilityTable;
            int SizeOfEntryInBits = 58;
            switch (CurrentMode)
            {
                case "TM":
                case "HM":
                default:
                    break;
                case "Tutor":
                    PointerToCompatibilityTable = PointerToMoveTutorCompatibilityTable;
                    SizeOfEntryInBits = NumberOfMoveTutorMoves;
                    break;
            }

            using (GBABinaryReader RomRead = new GBABinaryReader(LoadedRom)) // Access the ROM as a readable file.
            {
                RomRead.BaseStream.Seek(PointerToCompatibilityTable, SeekOrigin.Begin); // Go to the pointer to the compatibility table.
                uint CompatibilityTable = RomRead.ReadPointer(); // Read the pointer.

                RomRead.BaseStream.Seek(CompatibilityTable + (PokemonToChange * SizeOfCompatibilityTableEntry), SeekOrigin.Begin); // Go to the specified Pokémon's entry in the compatibility table.
                ulong hexCompatibilityEntry; // Needs to be a ulong to support the 64bit TM/HM compatibility entries.
                switch (CurrentMode)
                {
                    case "TM":
                    case "HM":
                    default:
                        hexCompatibilityEntry = RomRead.ReadUInt64(); // Read the specified Pokémon's TM/HM compatibility entry. It is eight bytes long, so is a 64bit integer.
                        break;
                    case "Tutor":
                        switch (LoadedRom.Code)
                        {
                            case "BPRE":
                            default:
                                hexCompatibilityEntry = RomRead.ReadUInt16(); // Read the specified Pokémon's move tutor compatibility entry. It is two bytes long, so is a 16bit integer.
                                break;
                            case "BPEE":
                                hexCompatibilityEntry = RomRead.ReadUInt32(); // In Emerald, the move tutor compatibility entries are four bytes long, and so are 32bit integers.
                                break;
                        }
                        break;
                }

                string stringCompatibilityEntry = ReverseString(ConvertToBinary(hexCompatibilityEntry).PadLeft(SizeOfEntryInBits, '0')); // Convert the compatibility entry to binary, as it is structured as a bitfield.
                // As the leading zeroes will not be there, pad the left of the string with 0s to make it the size of the entry in bits.
                // The bitfield is backwards, so reverse it.

                char[] CompatibilityWithMove = stringCompatibilityEntry.ToCharArray(); // Convert this string into an chararray.
                // This allows access of individual characters, and thereby modify individual entries in the bitfield.
                // The chararray is 0-indexed, so:
                // CompatibilityWithMove[0] = TM1 / Move Tutor move 1
                // CompatibilityWithMove[14] = TM15 / Move Tutor move 15
                // CompatibilityWithMove[49] = TM50
                // CompatibilityWithMove[50] = HM01 --> CompatibilityWithMove[57] = HM08

                CompatibilityWithMove[MoveNumber] = BitToChangeTo; // Update the specified compatibility with the move to be the specified value.
                // 0 for no compatibility, 1 for compatibility.

                stringCompatibilityEntry = new String(CompatibilityWithMove); // Revert the chararray back into a string.

                stringCompatibilityEntry = BinaryStringToHexString(ReverseString(stringCompatibilityEntry)); // The bitfield needs to be backwards again, so reverse it.
                // Convert it back into hex from binary.

                byte[] bytearrayCompatibilityEntry = StringToByteArray(stringCompatibilityEntry); // Convert the string into a bytearray, to be reinserted into the ROM.
                Array.Reverse(bytearrayCompatibilityEntry); // Reverse it once more to accommodate the ROM's endianness.

                using (GBABinaryWriter RomWrite = new GBABinaryWriter(LoadedRom)) // Access the ROM as a writeable ROM.
                {
                    RomWrite.BaseStream.Seek(CompatibilityTable + (PokemonToChange * SizeOfCompatibilityTableEntry), SeekOrigin.Begin); // Go to the specified Pokémon's entry in the compatibility table.
                    RomWrite.Write(bytearrayCompatibilityEntry); // Write the new compatibility entry to the ROM.
                }
            }
        }

        private void numPokemonMin_ValueChanged(object sender, EventArgs e)
        {
            if (numPokemonMin.Value > NumberOfSpecies)
            {
                numPokemonMin.ForeColor = Color.Red;
            }
            else
            {
                numPokemonMin.ForeColor = Color.Black;
            }
        }

        private void numPokemonMax_ValueChanged(object sender, EventArgs e)
        {
            if (numPokemonMax.Value > NumberOfSpecies)
            {
                numPokemonMax.ForeColor = Color.Red;
            }
            else
            {
                numPokemonMax.ForeColor = Color.Black;
            }
        }

    }
}
