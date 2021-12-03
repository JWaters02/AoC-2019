
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
namespace AdventOfCode192
{
	class Program
	{
		static int[] Inst;
			
		public static void Main(string[] args)
		{
			StreamReader InStream = new StreamReader("Input.txt");
			
			string Raw = InStream.ReadLine();
			
			InStream.Close();
			
			string[] Data = Raw.Split(',');
			Inst = new int[Data.Length];
			for (int i = 0; i < Data.Length; i++) {
				Inst[i]=Convert.ToInt32(Data[i]);
			}
			
			int[] Incs = new int[10];
			
			Incs[1]=4;
			Incs[2]=4;
			Incs[3]=2;
			Incs[4]=2;
			Incs[5]=3;
			Incs[6]=3;
			Incs[7]=4;
			Incs[8]=4;
			
			int Loc = 0;
			List<int> Modes = new List<int> ();
			int opcode = 0;
			string OpString;
			while (Inst[Loc]!=99) {
				//clearing modes
				Modes.Clear();
				//getting the opcode and mode
				OpString=Inst[Loc].ToString();
				if (OpString.Length>2) {
					opcode = Convert.ToInt16(OpString.Substring(OpString.Length-2,2));
					for (int i = OpString.Length-3; i > -1; i--) {
						Modes.Add(Convert.ToInt16(OpString.Substring(i,1)));
					}
				}else{
					opcode = Convert.ToInt16(OpString);
					for (int i = 0; i < Incs[opcode]-1; i++) {
						Modes.Add(0);
					}
				}
				
				while (Modes.Count<Incs[opcode]) {
					Modes.Add(0);
				}
				
				switch (opcode) {
						
					case 1 :
						Inst[Inst[Loc+3]] = Eval(Modes[0],Loc+1) + Eval(Modes[1],Loc+2);
						break;
						
					case 2:
						Inst[Inst[Loc+3]] = Eval(Modes[0],Loc+1) * Eval(Modes[1],Loc+2);
						break;
						
					case 3:
						Inst[Inst[Loc+1]] = Convert.ToInt16(Console.ReadLine());
						break;
						
					case 4:
						Console.WriteLine(Eval(Modes[0],Loc+1).ToString());
						break;
						
					case 5:
						
						if (Eval(Modes[0],Loc+1) != 0) {
							Loc=Eval(Modes[1],Loc+2)-Incs[opcode];
						}
						break;
					
					case 6:
						if (Eval(Modes[0],Loc+1) == 0) {
							Loc=Eval(Modes[1],Loc+2)-Incs[opcode];
						}
						break;
						
					case 7:
						if (Eval(Modes[0],Loc+1) < Eval(Modes[1],Loc+2)) {
							Inst[Inst[Loc+3]] = 1;                          	
						}else{
							Inst[Inst[Loc+3]]=0;
						}
						break;
						
					case 8:
						if (Eval(Modes[0],Loc+1) == Eval(Modes[1],Loc+2)) {
							Inst[Inst[Loc+3]] = 1;                          	
						}else{
							Inst[Inst[Loc+3]]=0;
						}
						break;
						
					default:
						
						break;
				}
				
				Loc+=Incs[opcode];
			}
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		static int Eval(int mode, int loc){
			switch (mode) {
				case 0:
					return Inst[Inst[loc]];
					break;
					
				case 1:
					return Inst[loc];
					break;
					
				default:
					return 0;
					break;
			}
		}
		
	}
}