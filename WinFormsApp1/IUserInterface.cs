/// <summary>
/// интерфейс ввода/вывода данных - при необходимости заменяется на другой
/// </summary>
 
using System;
using System.Collections.Generic;

namespace WinFormsApp1
{
	// Интерфейс ввода-вывода 
	public interface IUserInterface
	{
		// результат вычислений
		public void InputRes(String res) { }

		// ввод посимвольно
		public void Input(Char Input) { }

		// ввод построчно
		public void Input(String Input) { }

		// буфер ввода данных
		public List <String> InputBuffer { get; }

		// вывод данных 
		public String Output { get; set; }

		// вычислние выражения
		public event Action<List<String>> ResultEvent;
	}

	public class ArithmeticIO : IUserInterface
	{
		public ArithmeticIO()
		{
			inputBuffer = new List<String>() { "0" };
		}

		// буфер ввода данных
		private List<String> inputBuffer;

		// ввод данных
		public List<String> InputBuffer 
		{ 
			get => inputBuffer; 
		}

		// вывод данных 
		public String Output
		{
			get 
			{				
				return String.Concat(inputBuffer);
			}
			set { }
		}

		// символы для парсинга данных
		private string CharsDigits = "123456789";
		private string CharsZero = "0";
		private string CharsOperators = "+-*/";
		private string CharsComma = ",.";
		private string CharsResult = "=";
		private string CharsBcsps = "<";
		private string CharsEscp = "C";
		private string CharsRevers = ">";

		// подмена последнего выражения его результатом в буфере
		public void InputRes(String res)
		{
			inputBuffer.Clear();
			inputBuffer.Add(res);
		}

		// ввод данных построчно
		public void Input(String input)
		{
			//for (int i = 0; i < input.Length - 1; i++)
			for (int i = 0; i < input.Length; i++)
			{
				Input(input[i]);
			}
		}

		// ввод данных посимвольно, препарсинг операций
		public void Input(Char input)		
		{

			if (CharsZero.IndexOf(input) >= 0)
			{
				Input(0, '0');
			}
			else if (CharsComma.IndexOf(input) >= 0)
			{
				Input(1, ',');
			}
			else if (CharsDigits.IndexOf(input) >= 0)
			{
				Input(2, input);
			}
			else if (CharsOperators.IndexOf(input) >= 0)
			{
				Input(3, input);
			}
			else if (CharsResult.IndexOf(input) >= 0)
			{
				Input(4, '=');
			}
			else if (CharsBcsps.IndexOf(input) >= 0)
			{
				Input(5, '<');
			}
			else if (CharsRevers.IndexOf(input) >= 0)
			{
				Input(6, '>');
			}
			else if (CharsEscp.IndexOf(input) >= 0)
			{
				Input(7, 'C');
			}
		}

		// формирование операторов и операций
		private void Input(int InputType, Char Input)
		{
			if (inputBuffer == null)
			{
				inputBuffer = new List<String>() { "0" };
			}
			else if (inputBuffer.Count == 0)
			{
				inputBuffer.Add("0");
			}

			// '0'
			if (InputType == 0)
			{
				if (inputBuffer.Count != 2)
				{
					if (inputBuffer[inputBuffer.Count - 1].Length > 1 || Convert.ToDecimal(inputBuffer[inputBuffer.Count - 1]) != 0)
					{
						inputBuffer[inputBuffer.Count - 1] += "0";
					}
				}
				else
				{
					inputBuffer.Add("0");
				}
				return;
			}

			// ','
			if (InputType == 1)
			{
				if (inputBuffer.Count != 2)
				{
					if (!inputBuffer[inputBuffer.Count - 1].Contains(','))
					{
						inputBuffer[inputBuffer.Count - 1] += ",";
					}
				}
				else
				{
					inputBuffer.Add("0,");
				}
				return;
			}

			// '[1-9]'
			if (InputType == 2)
			{
				if (inputBuffer.Count != 2)
				{
					if (inputBuffer[inputBuffer.Count - 1].Length == 1 && Convert.ToDecimal(inputBuffer[inputBuffer.Count - 1]) == 0)
					{
						inputBuffer[inputBuffer.Count - 1] = Input.ToString();
					}
					else 
					{
						inputBuffer[inputBuffer.Count - 1] += Input.ToString();
					}
				}
				else
				{
					inputBuffer.Add(Input.ToString());
				}
				return;
			}

			// ['+','-','*','/']
			if (InputType == 3)
			{
				// уже есть число в буфере - добавляем
				if (inputBuffer.Count == 1)
				{
					inputBuffer.Add(Input.ToString());
				}

				// уже есть оператор в буфере - замена
				if (inputBuffer.Count == 2)
				{
					inputBuffer[inputBuffer.Count - 1] = Input.ToString();
				}

				// уже есть Digit <> operator <> Digit - вычисляем
				if (inputBuffer.Count == 3)
				{
					ResultEvent?.Invoke(inputBuffer);
					inputBuffer.Add(Input.ToString());
				}
				return;
			}

			// '='
			if (InputType == 4 && inputBuffer.Count > 2)
			{
				ResultEvent?.Invoke(inputBuffer);				
			}

			// '<' Backspace
			if (InputType == 5 && inputBuffer.Count != 2)
			{
				if (inputBuffer[inputBuffer.Count - 1].Length == 1)
				{
					inputBuffer[inputBuffer.Count - 1] = "0";
				}
				else if (inputBuffer[inputBuffer.Count - 1].Length == 2 && Convert.ToDecimal(inputBuffer[inputBuffer.Count - 1]) < 0)
				{
					inputBuffer[inputBuffer.Count - 1] = "0";
				}
				else if (inputBuffer[inputBuffer.Count - 1].Length < 5 && Convert.ToDecimal(inputBuffer[inputBuffer.Count - 1]) < 0 && Convert.ToDecimal(inputBuffer[inputBuffer.Count - 1]) > -1)
				{
					inputBuffer[inputBuffer.Count - 1] = "0";
				}
				else if (inputBuffer[inputBuffer.Count - 1].Length > 1 )
				{
					inputBuffer[inputBuffer.Count - 1] = inputBuffer[inputBuffer.Count - 1].Remove(inputBuffer[inputBuffer.Count - 1].Length - 1);
				}
				return;
			}


			// Revers '>'
			if (InputType == 6)
			{
				if (inputBuffer.Count != 2)
				{
					if (Convert.ToDecimal(inputBuffer[inputBuffer.Count - 1]) != 0) { 
						inputBuffer[inputBuffer.Count - 1] = (Convert.ToDecimal(inputBuffer[inputBuffer.Count - 1]) * -1).ToString();
					}
					else
					{
						inputBuffer[inputBuffer.Count - 1] = "0";
					}
				}
				return;
			}

			// Clear
			if (InputType == 7)
			{
				InputRes("0");
				return;
			}
		}

		// вычислние выражения
		public event Action<List<String>> ResultEvent;
	}
}
