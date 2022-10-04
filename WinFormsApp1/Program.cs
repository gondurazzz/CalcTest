/// <summary>
/// 
/// Задача: написать калькулятор 
/// В коде должны быть: наследование, исключения, ООП, паттерны и алгоритмы
/// 
/// архитектура расширяема, как с т.з. добавления новых интерфейсов пользователя, так и с т.з. дабавления арифметики
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

			// интерфейс ввода/вывода данных - при необходимости заменяется на другой
			IUserInterface MyIO = new ArithmeticIO();

			// расширяемая коллекция математических алгоритмов  - при необходимости дополняется 
			Operations MyMath = new Operations();

			// Брокер 
			new Calc(MyIO, MyMath);

			// WinForm идет последним потому, что в блокирующем потоке
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
			// все это логично было бы упихать внутрь класса Operations .. как нибудь в другй раз
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

			// вычисления
			operation.Operator1 = Convert.ToDecimal(inputBuffer[0]);
			operation.Operator2 = Convert.ToDecimal(inputBuffer[2]);
			operation.DoOperation();

			// результат
			userInterface.InputRes(operation.res.ToString());

		}
	}
}
