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
				operation = operations.Pull.Find(o => o.code == OperationNames.Summation);
			}

			if (inputBuffer[1] == "-")
			{
				operation = operations.Pull.Find(o => o.code == OperationNames.Substraction);
			}

			if (inputBuffer[1] == "*")
			{
				operation = operations.Pull.Find(o => o.code == OperationNames.Multiplication);
			}

			if (inputBuffer[1] == "/")
			{
				operation = operations.Pull.Find(o => o.code == OperationNames.Division);
			}

			// ����������
			operation.Operator1 = Convert.ToDecimal(inputBuffer[0]);
			operation.Operator2 = Convert.ToDecimal(inputBuffer[2]);
			operation.DoOperation();

			// ���������
			userInterface.InputRes(operation.res.ToString());

		}
	}
}
