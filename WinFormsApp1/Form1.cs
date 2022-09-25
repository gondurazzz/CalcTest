using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
	public partial class Form1 : Form
	{
		private IUserInterface userInterface;
		public Form1(IUserInterface UserInterface)
		{
			InitializeComponent();
			userInterface = UserInterface;
			FldTextResult.Text = userInterface.Output;
		}


		#region Цифры

		// делитель дробной части
		private void BtnNumberComma_Click(object sender, EventArgs e)
		{
			userInterface.Input(',');
			FldTextResult.Text = userInterface.Output;
		}
		
		// смена знака числа
		private void BtnRevers_Click(object sender, EventArgs e)
		{
			userInterface.Input('>');
			FldTextResult.Text = userInterface.Output;
		}

		// цифры
		private void BtnNumber_Click(object sender, EventArgs e)
		{
			if (sender is Button && (sender as Button).Text.Length == 1)
			{
				userInterface.Input(Convert.ToChar((sender as Button).Text));
				FldTextResult.Text = userInterface.Output;
			}
		}
		#endregion


		#region операторы
		private void BtnOperatorDiv_Click(object sender, EventArgs e)
		{
			userInterface.Input('/');
			FldTextResult.Text = userInterface.Output;
		}

		private void BtnOperatorMult_Click(object sender, EventArgs e)
		{
			userInterface.Input('*');
			FldTextResult.Text = userInterface.Output;
		}

		private void BtnOperatorSubst_Click(object sender, EventArgs e)
		{
			userInterface.Input('-');
			FldTextResult.Text = userInterface.Output;
		}

		private void BtnOperatorAdd_Click(object sender, EventArgs e)
		{
			userInterface.Input('+');
			FldTextResult.Text = userInterface.Output;
		}

		private void BtnEnter_Click(object sender, EventArgs e)
		{
			userInterface.Input('=');
			FldTextResult.Text = userInterface.Output;
		}
		#endregion


		#region стирание

		// Backspase
		private void BtnEraze_Click(object sender, EventArgs e)
		{
			userInterface.Input('<');
			FldTextResult.Text = userInterface.Output;
		}


		// Clear
		private void BtnClear_Click(object sender, EventArgs e)
		{
			userInterface.Input('C');
			FldTextResult.Text = userInterface.Output;
		}
		#endregion


		#region клавиатурный ввод
		private void Form1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyData == Keys.Escape) { userInterface.Input('C'); }
			if (e.KeyData == Keys.Enter) { userInterface.Input('='); }
			if (e.KeyData == Keys.Add) { userInterface.Input('+'); }
			if (e.KeyData == Keys.Subtract) { userInterface.Input('-'); }
			if (e.KeyData == Keys.Multiply) { userInterface.Input('*'); }
			if (e.KeyData == Keys.Divide) { userInterface.Input('/'); }
			if (e.KeyData == Keys.Back) { userInterface.Input('<'); }
			FldTextResult.Text = userInterface.Output;
		}

		private void Form1_KeyPress(object sender, KeyPressEventArgs e)
		{
			userInterface.Input(e.KeyChar);
			FldTextResult.Text = userInterface.Output;
		}
		#endregion

	}
}
