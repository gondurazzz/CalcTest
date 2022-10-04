/// <summary>
/// расширяемая коллекция математических операций/алгоритмов  - при необходимости дополняется 
/// </summary>

using System;
using System.Collections.Generic;

namespace WinFormsApp1
{
	public interface IOperation
	{
		public decimal Operator1 { get; set; }
		public decimal Operator2 { get; set; }
		public decimal res { get; set; }
		public OperationNames code { get; }
		public void DoOperation() { }
	}

	public class Summation : IOperation
	{
		public decimal Operator1 { get; set; }
		public decimal Operator2 { get; set; }
		public decimal res { get; set; }
		public OperationNames code { get { return OperationNames.Summation; } }
		public void DoOperation() { res = Operator1 + Operator2; }

	}

	public class Substraction : IOperation
	{
		public decimal Operator1 { get; set; }
		public decimal Operator2 { get; set; }
		public decimal res { get; set; }
		public OperationNames code { get { return OperationNames.Substraction; } }
		public void DoOperation() { res = Operator1 - Operator2; }

	}

	public class Multiplication : IOperation
	{
		public decimal Operator1 { get; set; }
		public decimal Operator2 { get; set; }
		public decimal res { get; set; }
		public OperationNames code { get { return OperationNames.Multiplication; } }
		public void DoOperation()
		{ 
			if (Operator2 == 0)
			{
				res = 0;
			}
			else
			{
				res = Operator1 * Operator2;
			}
		}

	}

	public class Division : IOperation
	{
		public decimal Operator1 { get; set; }
		public decimal Operator2 { get; set; }
		public decimal res { get; set; }
		public OperationNames code { get { return OperationNames.Division; } }
		public void DoOperation()
		{
			try
			{
				res = Operator1 / Operator2;
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

	public enum OperationNames
	{
		Summation,
		Substraction,
		Multiplication,
		Division
	}
}
