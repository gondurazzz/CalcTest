/// <summary>
/// расширяемая коллекция математических операций/алгоритмов  - при необходимости дополняется 
/// </summary>

using System;
using System.Collections.Generic;

namespace WinFormsApp1
{
	public interface IOperation
	{
		public decimal op1 { get; set; }
		public decimal op2 { get; set; }
		public decimal res { get; set; }
		public string code { get; }
		public void DoOperation() { }
	}

	public class Summation : IOperation
	{
		public decimal op1 { get; set; }
		public decimal op2 { get; set; }
		public decimal res { get; set; }
		public string code { get { return "Summation"; } }
		public void DoOperation() { res = op1 + op2; }

	}

	public class Substraction : IOperation
	{
		public decimal op1 { get; set; }
		public decimal op2 { get; set; }
		public decimal res { get; set; }
		public string code { get { return "Substraction"; } }
		public void DoOperation() { res = op1 - op2; }

	}

	public class Multiplication : IOperation
	{
		public decimal op1 { get; set; }
		public decimal op2 { get; set; }
		public decimal res { get; set; }
		public string code { get { return "Multiplication"; } }
		public void DoOperation()
		{ 
			if (op2 == 0)
			{
				res = 0;
			}
			else
			{
				res = op1 * op2;
			}
		}

	}

	public class Division : IOperation
	{
		public decimal op1 { get; set; }
		public decimal op2 { get; set; }
		public decimal res { get; set; }
		public string code { get { return "Division"; } }
		public void DoOperation()
		{
			try
			{
				res = op1 / op2;
			}
			catch(DivideByZeroException)
			{
				// Ну все понятно, как бы 
			}
		}
	}

	// коллекция 
	public class Operations
	{
		public Operations()
		{
			pull = new List<IOperation>() { new Summation(), new Substraction(), new Multiplication(), new Division() };
		}

		private List<IOperation> pull;

		public List<IOperation> Pull { get { return pull; } }

		public void AppendPool(IOperation NewOperation)
		{
			if (Pull.Find(o => o.code == NewOperation.code) == null) { return; }
			pull.Add(NewOperation);
		}

	}
}
