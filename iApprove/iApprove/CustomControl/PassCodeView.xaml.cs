using iApprove.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iApprove.CustomControl
{
	public enum PassCodeType
	{
		CheckPassCode,
		SetPassCode
	}

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PassCodeView : ContentView
	{
		bool IsPinWrong = false;
		string[] PassCodeMessage = new string[4] { "Please Enter the Pin", "Confirm The Pin", "Wrong Pin Entered", "Pins Mismatch" };

		public double Size
		{
			get;
			set;
		}

		double fontSize = 0;

		public double FontSize
		{
			set
			{
				fontSize = value;
			}
			get
			{
				return fontSize;
			}
		}

		public PassCodeType InputType { get; set; }

		bool setView;
		public bool SetView
		{
			get
			{
				return setView;
			}
			set
			{
				setView = value;
				SetViews();
			}
		}

		public static readonly BindableProperty OutPutProperty = BindableProperty.Create(
		  propertyName: "OutPut",
		  returnType: typeof(string),
		  declaringType: typeof(PassCodeView),
		  defaultValue: null,
		  defaultBindingMode: BindingMode.TwoWay);
		public string OutPut
		{
			get { return (string)GetValue(OutPutProperty); }
			set { SetValue(OutPutProperty, value); }
		}

		int i = 0;
		string[] OutPuts = new string[2] { null, null };

		public static readonly BindableProperty PassCodeProperty = BindableProperty.Create(
		 propertyName: "PassCode",
		 returnType: typeof(string),
		 declaringType: typeof(PassCodeView),
		 defaultValue: null);

		public string PassCode
		{
			get { return (string)GetValue(PassCodeProperty); }
			set { SetValue(PassCodeProperty, value); }
		}

		public static readonly BindableProperty OnPinEnteredCommandProperty = BindableProperty.Create(
		  propertyName: "PinEnteredCommand",
		  returnType: typeof(ICommand),
		  declaringType: typeof(PassCodeView),
		  defaultValue: null);
		public ICommand PinEnteredCommand
		{
			get { return (ICommand)GetValue(OnPinEnteredCommandProperty); }
			set { SetValue(OnPinEnteredCommandProperty, value); }
		}

		public string Title { set; get; }

		public string TitleColorHex { set; get; }


		public event EventHandler PinEntered;


		public PassCodeView()
		{
			InitializeComponent();
		}

		private void SetViews()
		{
			foreach (var ele in numberGrid.Children)
			{
				if (ele.GetType() == typeof(Button))
				{
					Button btn = (Button)ele;
					if (FontSize != 0)
						btn.FontSize = FontSize;
					if (Size != 0)
					{
						btn.HeightRequest = Size;
						btn.WidthRequest = Size;
					}

					if (Title != null)
						pinMessage.Text = Title;
					if (TitleColorHex != null)
						pinMessage.TextColor = Color.FromHex(TitleColorHex);
					pinMessage.Text = PassCodeMessage[0];
				}
			}
		}

		private void DeletePinDigit(object sender, EventArgs e)
		{
			if (OutPuts[i] != null)
			{
				int num = OutPuts[i].Length - 1;
				if (IsPinWrong)
				{
					pinMessage.Text = PassCodeMessage[(int)InputType];
					IsPinWrong = false;
					if (TitleColorHex != null)
						pinMessage.TextColor = Color.FromHex(TitleColorHex);
					else
						pinMessage.TextColor = Color.Gray;
				}
				OutPuts[i] = (num > 0) ? OutPuts[i].Substring(0, num) : null;
				Label digitLabel = pinView.FindByName<Label>("pinNum" + (num));
				digitLabel.IsVisible = false;
			}
		}

		private void digit_Clicked(object sender, EventArgs e)
		{
			if (OutPuts[i] == null || OutPuts[i].Length < 4)
			{
				Button clickedBtn = (Button)sender;
				OutPuts[i] = (OutPuts[i] == null) ? clickedBtn.Text : OutPuts[i] + clickedBtn.Text;
				Label digitLabel = pinView.FindByName<Label>("pinNum" + (OutPuts[i].Length - 1));
				digitLabel.IsVisible = true;
			}
			if (OutPuts[i].Length == 4)
			{
				if (InputType == PassCodeType.CheckPassCode)
				{
					CheckPassCode();
					return;
				}
				else
				{
					if (i == 0)
					{
						i = 1;
						pinMessage.Text = PassCodeMessage[1];
						int j = 0;
						while (j < 4)
						{
							Label digitLabel = pinView.FindByName<Label>("pinNum" + j++);
							digitLabel.IsVisible = false;
						}
						return;
					}
					else
					{
						if (OutPuts[0].Equals(OutPuts[1]))
						{
							InvokeOutputCallBack();
							return;
						}
						else
						{
							WrongPassCodeHandler();
						}
					}
				}
			}

		}

		private void WrongPassCodeHandler()
		{
			pinMessage.Text = PassCodeMessage[((int)InputType + 2)];
			pinMessage.TextColor = Color.Red;
			IsPinWrong = true;
		}

		void CheckPassCode()
		{
			if (PassCode != null && PassCode == OutPuts[0])
			{
				InvokeOutputCallBack();
				return;
			}
			WrongPassCodeHandler();
		}

		void ConfirmPAssCode()
		{

		}

		void InvokeOutputCallBack()
		{
			OutPut = OutPuts[0];
			if (PinEntered != null)
			{
				PinEntered.Invoke(OutPut, null);
				return;
			}
			if (PinEnteredCommand != null)
			{
				PinEnteredCommand.Execute(OutPut);
			}
		}
	}
}