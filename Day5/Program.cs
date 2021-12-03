using System;
using System.IO;

namespace AoCP5 {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            d5();
            Console.ReadKey(true);
        }

        public static void d5() {
            int[] inputs = new int[200];
            // Input each value
            string selectedFile = ("./file.txt");
            // Now that validation is done, make new file in directory, open it and write to it
            using (StreamReader sr = File.OpenText(selectedFile)) {
                string s;
                string[] splitText;
                int i = 0;
                while ((s = sr.ReadLine()) != null) {
                    splitText = s.Split(',');
                    inputs[i] = Convert.ToInt32(splitText[i]);
                    // Temp
                    Console.WriteLine(inputs[i]);
                    i++;
                }
            }
            
            int[] prog = new int[inputs.Length];
            string opcode;
            int pcIncLength;
            int[] parameterMode = new int[3]; // 0 = position, 1 = value || 0 = A, 1 = B, 2 = C
            inputs.CopyTo(prog, 0);
            int pc = 0;
            int a;
            int b;
            int c;

            // While the current instruction set isn't halt
            while (prog[pc] != 99) {
                // If the length of current program is more than 2
                if (prog[pc].ToString().Length > 2) {
                    // Check the length of the opcode
                    int length = prog[pc].ToString().Length;
                    switch (length) {
                        case 3:
                            // Then get the 3rd character from right (C)
                            parameterMode[2] = Convert.ToInt32(prog[pc].ToString().Substring(2, 1));
                            // Set the others to 0
                            parameterMode[0] = 0;
                            parameterMode[1] = 0;
                            pcIncLength = 6;
                            break;
                        case 4:
                            // Then get the 3rd (C) and 4th (B) character from right
                            parameterMode[2] = Convert.ToInt32(prog[pc].ToString().Substring(2, 1));
                            parameterMode[1] = Convert.ToInt32(prog[pc].ToString().Substring(1, 1));
                            // Set the others to 0
                            parameterMode[0] = 0;
                            pcIncLength = 7;
                            break;
                        case 5:
                            // Then get 3rd (C), 4th (B) and 5th (A) character from right
                            parameterMode[2] = Convert.ToInt32(prog[pc].ToString().Substring(2, 1));
                            parameterMode[1] = Convert.ToInt32(prog[pc].ToString().Substring(1, 1));
                            parameterMode[0] = Convert.ToInt32(prog[pc].ToString().Substring(0, 1));
                            pcIncLength = 8;
                            break;
                    }
                // If the current part of the prog has an opcode at the start
                } else if (prog[pc].ToString().Length > 1 && prog[pc].ToString().Length < 3) {
                    opcode = prog[pc].ToString().Substring(1, 1);
                } else {
                    switch (prog[pc]) {
                        // Add
                        case 1:
                            a = prog[pc + 1];
                            b = prog[pc + 2];
                            c = prog[pc + 3];
                            prog[c] = prog[a] + prog[b];
                            pc += pcIncLength;
                            break;
                        // Multiply
                        case 2:
                            a = prog[pc + 1];
                            b = prog[pc + 2];
                            c = prog[pc + 3];
                            prog[c] = prog[a] * prog[b];
                            pc += pcIncLength;
                            break;
                        // Take input and put it in next address
                        case 3:
                            Console.WriteLine("Please input the ID - 1");
                            int inputtedID = Convert.ToInt32(Console.ReadLine());
                            prog[pc + 1] = inputtedID;
                            break;
                        // Outputs value of next pc
                        case 4:
                            Console.WriteLine(prog[pc + 1].ToString());
                            break;
                        default:
                            // Output diagnostic code, because it failed test
                            Console.WriteLine(prog[pc].ToString());
                            break;
                    }
                }
                
            }
            
        }

        void d5Instructions() {
            // Opcode 3 takes a single integer as input as saves it to the position given by its only parameter.
            // E.g. the instruction 3,50 would take an input value and store it at address 50
            // Opcode 4 outputs the value of its only parameter.
            // E.g. the instruction 4, 50 would output the value at address 50
            // Parameter modes - currently on mode 0, but need support for mode 1 - immediate mode
            // In this, a parameter is interpreted as a value - if the parameter is 50, its value is simply 50
            // 1002,4,3,4,33
            // ABCDE
            //  1002
            // DE - two-digit opcode,       02 == opcode
            // C - mode of 1st parameter,   0  == position mode
            // B - mode of 2nd parameter,   1  == immediate mode
            // A - mode of 3rd parameter,   0  == position mode, omitted due to being a leading zero
            // This instruction multiplies its first two parameters
            // The first parameter, 4 in position mode, works like it did before - its value is the value stored at address 4 (33)
            // The second parameter, 3 in immediate mode, simply has value 3
            // The result of this operation, 33 * 3 = 99, is written according to 4 in position mode - 99 is written to address 4
            // Parameters that an instruction writes to will never be in immediate mode
            // The instruction pointer should increase by the number of values in the instruction after the instruction finishes
            // Integers can be negative: 1101,100,-1,4,0 is a valid program (find 100 + -1, store the result in position 4)
            // Program will start by requesting from the user the ID of the system to test by running an input instruction
            // 
        }
    }
}
