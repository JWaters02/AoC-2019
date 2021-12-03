using System;
using System.IO;

namespace AoC_IntcodeVM {
    class Program {
        static int[] inst;
        static int[,] arrayInst;
        static int[,] inputs = new int[5, 2];

        static void Main(string[] args) {
            StreamReader sr = new StreamReader("Input.txt");
            string inputData = sr.ReadLine();
            sr.Close();

            string[] data = inputData.Split(',');
            inst = new int[data.Length];
            for (int i = 0; i < data.Length; i++) {
                inst[i] = Convert.ToInt32(data[i]);
            }
        }

        static void intVM(int v) {
            int inputCount = 0;
            int[] incs = new int[10];
        }
    }
}
