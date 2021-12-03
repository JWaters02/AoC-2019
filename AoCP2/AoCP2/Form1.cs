using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AoCP2 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        #region array
        int[] items = new int[109] {
            1,
0,
0,
3,
1,
1,
2,
3,
1,
3,
4,
3,
1,
5,
0,
3,
2,
10,
1,
19,
1,
19,
9,
23,
1,
23,
13,
27,
1,
10,
27,
31,
2,
31,
13,
35,
1,
10,
35,
39,
2,
9,
39,
43,
2,
43,
9,
47,
1,
6,
47,
51,
1,
10,
51,
55,
2,
55,
13,
59,
1,
59,
10,
63,
2,
63,
13,
67,
2,
67,
9,
71,
1,
6,
71,
75,
2,
75,
9,
79,
1,
79,
5,
83,
2,
83,
13,
87,
1,
9,
87,
91,
1,
13,
91,
95,
1,
2,
95,
99,
1,
99,
6,
0,
99,
2,
14,
0,
0,
        };
        #endregion

        private void button1_Click(object sender, EventArgs e) {
            newthingy();
            //int x;
            /* First bit
            x = thingy(items, 12, 2);
            label1.Text = x.ToString();
            
            // Loop through all the possible values of noun
            for (int n = 0; n < 1000; n++) {
                // Loop through all possible values of verb
                for (int v = 0; v < 1000; v++) {
                    items[v] = 0;
                    x = thingy(items, n, v);
                    if (x == 19690720) {
                        label1.Text = (100 * n + v).ToString();
                        break;
                    }
                    richTextBox1.Text += n.ToString() + v.ToString();
                }
                items[n] = 0;
            }
            */
        }

        int thingy(int[] prog, int n, int v) {
            int pc = 0;
            int a;
            int b;
            int c;
            prog[1] = n;
            prog[2] = v;

            while (prog[pc] != 99) {
                if (prog[pc] == 1) {
                    a = prog[pc + 1];
                    b = prog[pc + 2];
                    c = prog[pc + 3];
                    prog[c] = prog[a] + prog[b];
                    pc += 4;
                } else if (prog[pc] == 2) {
                    a = prog[pc + 1];
                    b = prog[pc + 2];
                    c = prog[pc + 3];
                    prog[c] = prog[a] * prog[b];
                    pc += 4;
                }
            }
            return prog[0];
        }

        void newthingy() {
            for (int n = 0; n < 100; n++) {
                for (int v = 0; v < 100; v++) {
                    int[] prog = new int[items.Length];
                    items.CopyTo(prog, 0);
                    int pc = 0;
                    int a;
                    int b;
                    int c;
                    prog[1] = n;
                    prog[2] = v;

                    while (prog[pc] != 99) {
                        if (prog[pc] == 1) {
                            a = prog[pc + 1];
                            b = prog[pc + 2];
                            c = prog[pc + 3];
                            prog[c] = prog[a] + prog[b];
                            pc += 4;
                        } else if (prog[pc] == 2) {
                            a = prog[pc + 1];
                            b = prog[pc + 2];
                            c = prog[pc + 3];
                            prog[c] = prog[a] * prog[b];
                            pc += 4;
                        }
                    }
                    if (prog[0] == 19690720) {
                        label1.Text = (100 * n + v).ToString();
                        break;
                    } else {
                        prog[1] = items[1];
                        prog[2] = items[2];
                    }
                }
            }
        }

        int[] inputs = new int[100];

        void d5() {
            // Input each value
            int[] prog = new int[inputs.Length];
            string opcode;
            inputs.CopyTo(prog, 0);
            int pc = 0;
            int a;
            int b;
            int c;

            // While the current instruction set isn't halt
            while (prog[pc] != 99) {
                // If the current part of the prog isn't 1 int long
                if (prog[pc].ToString().Length > 2) {
                    // Check the length of the opcode
                    int length = prog[pc].ToString().Length;
                    // If the length is 3
                    // Read the number right to left to get the opcode

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
                            pc += 4;
                            break;
                        // Multiply
                        case 2:
                            a = prog[pc + 1];
                            b = prog[pc + 2];
                            c = prog[pc + 3];
                            prog[c] = prog[a] * prog[b];
                            pc += 4;
                            break;
                        // Take input and put it in next address
                        case 3:
                            break;
                        case 4:
                            break;
                        default:
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
