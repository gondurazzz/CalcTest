/// <summary>
/// 
/// ������: �������� ����������� 
/// � ���� ������ ����: ������������, ����������, ���, �������� � ���������
/// 
/// ����������� ����������, ��� � �.�. ���������� ����� ����������� ������������, ��� � � �.�. ���������� ����������
/// 
/// </summary>

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsApp1
{
	static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// ��������� �����/������ ������ - ��� ������������� ���������� �� ������
			IUserInterface MyIO = new ArithmeticIO();

			// ����������� ��������� �������������� ����������  - ��� ������������� ����������� 
			Operations MyMath = new Operations();

			// ������ 
			new Calc(MyIO, MyMath);

			// WinForm ���� ��������� ������, ��� � ����������� ������
			Application.Run(new Form1(MyIO));
		}
	}

	public class Calc
	{
		private IUserInterface userInterface;
		private IOperation operation;
		private Operations operations;
		public Calc(IUserInterface UserInterface, Operations Pull)
		{
			userInterface = UserInterface;
			operations = Pull;

			// 
			userInterface.ResultEvent += UserInterface_ResultEvent;
		}

		private void UserInterface_ResultEvent(List<String> inputBuffer)
		{
			//
			// ��� ��� ������� ���� �� ������� ������ ������ Operations .. ��� ������ � ����� ���
			// 
			if (inputBuffer[1] == "+")
			{
				operation = operations.Pull.Find(o => o.code == "Summation");
			}

			if (inputBuffer[1] == "-")
			{
				operation = operations.Pull.Find(o => o.code == "Substraction");
			}

			if (inputBuffer[1] == "*")
			{
				operation = operations.Pull.Find(o => o.code == "Multiplication");
			}

			if (inputBuffer[1] == "/")
			{
				operation = operations.Pull.Find(o => o.code == "Division");
			}

			// ����������
			operation.op1 = Convert.ToDecimal(inputBuffer[0]);
			operation.op2 = Convert.ToDecimal(inputBuffer[2]);
			operation.DoOperation();

			// ���������
			userInterface.InputRes(operation.res.ToString());

		}
	}
}
