using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AndroidUI.HolyLight
{
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ToolboxBitmap(typeof(AndroidButton), "Button.AndroidButton.bmp")]
	[ComVisible(true)]
	public class AndroidButton : Control
	{
		public enum AndroidType { Text, IconText, Icon};
		public enum AndroidState { Normal, Pressed, Disabled };

		#region Private properties
		public AndroidState _buttonState = AndroidState.Normal;
		private AndroidType _buttonType = AndroidType.Icon;
		private StringAlignment _verticalAlignment = StringAlignment.Center;
		private StringAlignment _horizontalAlignment = StringAlignment.Center;
		private bool _autoSize = true;
		private int _iconSize = 32;
		private int _iconLeftMargin = 5;
		private int _iconRightMargin = 5;
		private float _cornerRadius = 4.0f;
		private Size _minimumSize = new Size( 75, 23 );
		private Size _size = new Size( 105, 32 );
		private Padding _padding = new Padding( 5 );
		private Bitmap _icon = new Bitmap( 32, 32 );
		private Color _foreColor = ColorTranslator.FromHtml( "#333333" );
		#endregion

		public AndroidButton()
		{
			SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer,
				true);
			if( isFontInstalled() )
			{
				Font = new System.Drawing.Font( "Roboto", 10.0f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 238 ) ) );
			}
			else
			{
				Font = new System.Drawing.Font( "Tahoma", 10.0f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 238 ) ) );
			}
		}

		#region OnPaint
		protected override void OnPaint(PaintEventArgs pevent)
		{
			pevent.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			base.OnPaint(pevent);

			if( Enabled )
			{
				switch( _buttonState )
				{
					case AndroidState.Normal: ButtonStateNormal( pevent.Graphics ); break;
					case AndroidState.Pressed: ButtonStatePressed( pevent.Graphics ); break;
				}
			}
			else
			{
				ButtonStateDisabled( pevent.Graphics );
			}
		}
		#endregion

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if( Enabled ) { _buttonState = AndroidState.Pressed; }
			Refresh();
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if( Enabled ) { _buttonState = AndroidState.Normal; }
			Refresh();
		}

		#region ButtonStateNormal
		protected void ButtonStateNormal(Graphics g)
		{
			var rect = new Rectangle(0, 0, Width, Height);

			g.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#d6d6d6")), rect);

			//TOP LINE
			DrawPixel(g, ColorTranslator.FromHtml("#fdfdfd"), 0, 0);
			DrawPixel(g, ColorTranslator.FromHtml("#e8e8e8"), 1, 0);
			DrawPixel(g, ColorTranslator.FromHtml("#e4e4e4"), 2, 0);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#e3e3e3"))), 3, 0, Width - 4, 0);
			DrawPixel(g, ColorTranslator.FromHtml("#e4e4e4"), Width - 3, 0);
			DrawPixel(g, ColorTranslator.FromHtml("#e8e8e8"), Width - 2, 0);
			DrawPixel(g, ColorTranslator.FromHtml("#fdfdfd"), Width - 1, 0);
			// END TOP LINE

			// LEFT LINE
			DrawPixel(g, ColorTranslator.FromHtml("#f6f6f6"), 0, 1);
			DrawPixel(g, ColorTranslator.FromHtml("#d6d6d6"), 1, 1);
			DrawPixel(g, ColorTranslator.FromHtml("#d8d8d8"), 2, 1);
			DrawPixel(g, ColorTranslator.FromHtml("#f1f1f1"), 0, 2);
			DrawPixel(g, ColorTranslator.FromHtml("#cbcbcb"), 1, 2);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#efefef"))), 0, 3, 0, Height - 4);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#cacaca"))), 1, 3, 1, Height - 5);
			DrawPixel(g, ColorTranslator.FromHtml("#bdbdbd"), 1, Height - 4);
			DrawPixel(g, ColorTranslator.FromHtml("#c8c8c8"), 2, Height - 4);
			// END LEFT LINE

			// BOTTOM LINE
			DrawPixel(g, ColorTranslator.FromHtml("#f1f1f1"), 0, Height - 3);
			DrawPixel(g, ColorTranslator.FromHtml("#b0b0b0"), 1, Height - 3);
			DrawPixel(g, ColorTranslator.FromHtml("#f7f7f7"), 0, Height - 2);
			DrawPixel(g, ColorTranslator.FromHtml("#e2e2e2"), 1, Height - 2);
			DrawPixel(g, ColorTranslator.FromHtml("#d3d3d3"), 2, Height - 2);
			DrawPixel(g, ColorTranslator.FromHtml("#fdfdfd"), 0, Height - 1);
			DrawPixel(g, ColorTranslator.FromHtml("#f6f6f6"), 1, Height - 1);
			DrawPixel(g, ColorTranslator.FromHtml("#f1f1f1"), 2, Height - 1);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#787878"))), 2, Height - 3, Width - 3, Height - 3);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#cfcfcf"))), 3, Height - 2, Width - 4, Height - 2);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#efefef"))), 3, Height - 1, Width - 4, Height - 1);
			DrawPixel(g, ColorTranslator.FromHtml("#f1f1f1"), Width - 1, Height - 3);
			DrawPixel(g, ColorTranslator.FromHtml("#b0b0b0"), Width - 2, Height - 3);
			DrawPixel(g, ColorTranslator.FromHtml("#f7f7f7"), Width - 1, Height - 2);
			DrawPixel(g, ColorTranslator.FromHtml("#e2e2e2"), Width - 2, Height - 2);
			DrawPixel(g, ColorTranslator.FromHtml("#d3d3d3"), Width - 3, Height - 2);
			DrawPixel(g, ColorTranslator.FromHtml("#fdfdfd"), Width - 1, Height - 1);
			DrawPixel(g, ColorTranslator.FromHtml("#f6f6f6"), Width - 2, Height - 1);
			DrawPixel(g, ColorTranslator.FromHtml("#f1f1f1"), Width - 3, Height - 1);
			// END BOTTOM LINE

			// RIGHT LINE
			DrawPixel(g, ColorTranslator.FromHtml("#f6f6f6"), Width - 1, 1);
			DrawPixel(g, ColorTranslator.FromHtml("#d6d6d6"), Width - 2, 1);
			DrawPixel(g, ColorTranslator.FromHtml("#d8d8d8"), Width - 3, 1);
			DrawPixel(g, ColorTranslator.FromHtml("#f1f1f1"), Width - 1, 2);
			DrawPixel(g, ColorTranslator.FromHtml("#cbcbcb"), Width - 2, 2);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#efefef"))), Width - 1, 3, Width - 1, Height - 4);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#cacaca"))), Width - 2, 3, Width - 2, Height - 5);
			DrawPixel(g, ColorTranslator.FromHtml("#bdbdbd"), Width - 2, Height - 4);
			DrawPixel(g, ColorTranslator.FromHtml("#c8c8c8"), Width - 3, Height - 4);
			// END RIGHT LINE

			var format = new StringFormat { Alignment = HorizontalAlignment, LineAlignment = VerticalAlignment };
			int space;

			switch(ButtonType){
				case AndroidType.Text:
					g.DrawString(base.Text, Font, new SolidBrush(ForeColor), new Rectangle(Padding.Left, Padding.Top, ClientRectangle.Width - (Padding.Left + Padding.Right), ClientRectangle.Height - (Padding.Top + Padding.Bottom)), format);
					break;
				case AndroidType.IconText:
					space = Height - (Padding.Top + Padding.Bottom + IconSize);
					g.DrawImage(Icon, Padding.Left + IconLeftMargin, space / 2 + Padding.Top, IconSize, IconSize);
					g.DrawString(base.Text, Font, new SolidBrush(ForeColor), new Rectangle(Padding.Left + IconLeftMargin + IconSize + IconRightMargin, Padding.Top, ClientRectangle.Width - ( Padding.Left + IconLeftMargin + IconSize + IconRightMargin + Padding.Right ), ClientRectangle.Height - ( Padding.Top + Padding.Bottom )), format);
					break;
				case AndroidType.Icon:
					space = Height - (Padding.Top + Padding.Bottom + IconSize);
					g.DrawImage(Icon, Padding.Left + IconLeftMargin, space / 2 + Padding.Top, IconSize, IconSize);
					break;
			}
		}
		#endregion

		#region ButtonStatePressed
		private void ButtonStatePressed(Graphics g)
		{
			var X = 0;
			var Y = 0;
			var width = Width;
			var height = Height;

			// BEGIN BLUE ROUNDED BACKGROUND
			var gp = new GraphicsPath();
			gp.AddLine(X + CornerRadius, Y, X + width - (CornerRadius * 2), Y);
			gp.AddArc(X + width - (CornerRadius * 2), Y, CornerRadius * 2, CornerRadius * 2, 270, 90);
			gp.AddLine(X + width, Y + CornerRadius, X + width, Y + height - (CornerRadius * 2));
			gp.AddArc(X + width - (CornerRadius * 2), Y + height - (CornerRadius * 2), CornerRadius * 2, CornerRadius * 2, 0, 90);
			gp.AddLine(X + width - (CornerRadius * 2), Y + height, X + CornerRadius, Y + height);
			gp.AddArc(X, Y + height - (CornerRadius * 2), CornerRadius * 2, CornerRadius * 2, 90, 90);
			gp.AddLine(X, Y + height - (CornerRadius * 2), X, Y + CornerRadius);
			gp.AddArc(X, Y, CornerRadius * 2, CornerRadius * 2, 180, 90);
			gp.CloseFigure();
			g.FillPath(new SolidBrush(ColorTranslator.FromHtml("#ade1f4")), gp);
			gp.Dispose();
			g.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#4bbde8")), 5, 5, (Width - 6) - 4, (Height - 6) - 4);
			// ENG BLUE ROUNDED BACKGROUND

			// TOP LINE
			DrawPixel(g, ColorTranslator.FromHtml("#abdef1"), 3, 3);
			DrawPixel(g, ColorTranslator.FromHtml("#a7d9ec"), 4, 3);
			DrawPixel(g, ColorTranslator.FromHtml("#a3d4e6"), 5, 3);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#a2d3e5"))), 6, 3, Width - 7, 3);
			DrawPixel(g, ColorTranslator.FromHtml("#a4d5e7"), Width - 6, 3);
			DrawPixel(g, ColorTranslator.FromHtml("#a7d9ec"), Width - 5, 3);
			DrawPixel(g, ColorTranslator.FromHtml("#acdff2"), Width - 4, 3);
			DrawPixel(g, ColorTranslator.FromHtml("#a6d8ea"), 3, 4);
			DrawPixel(g, ColorTranslator.FromHtml("#7cc8e5"), 4, 4);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#7bcfee"))), 5, 4, Width - 6, 4);
			DrawPixel(g, ColorTranslator.FromHtml("#7dc8e4"), Width - 5, 4);
			DrawPixel(g, ColorTranslator.FromHtml("#a7d9ec"), Width - 4, 4);
			// END TOP LINE

			// LEFT LINE
			DrawPixel(g, ColorTranslator.FromHtml("#9dd2e5"), 3, 5);
			DrawPixel(g, ColorTranslator.FromHtml("#4cbde8"), 4, 5);
			DrawPixel(g, ColorTranslator.FromHtml("#52c0e8"), 5, 5);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#9cd0e3"))), 3, 6, 3, Height - 7);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#4ab9e2"))), 4, 6, 4, Height - 7);
			DrawPixel(g, ColorTranslator.FromHtml("#9dd1e5"), 3, Height - 6);
			DrawPixel(g, ColorTranslator.FromHtml("#47b3db"), 4, Height - 6);
			DrawPixel(g, ColorTranslator.FromHtml("#49b8e1"), 5, Height - 6);
			// END LEFT LINE

			// RIGHT LINE
			DrawPixel(g, ColorTranslator.FromHtml("#a4d5e7"), Width - 4, 5);
			DrawPixel(g, ColorTranslator.FromHtml("#4cbee7"), Width - 5, 5);
			DrawPixel(g, ColorTranslator.FromHtml("#54c1e8"), Width - 6, 5);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#a2d3e5"))), Width - 4, 6, Width - 4, Height - 7);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#49b8e1"))), Width - 5, 6, Width - 5, Height - 7);
			DrawPixel(g, ColorTranslator.FromHtml("#a4d5e7"), Width - 4, Height - 6);
			DrawPixel(g, ColorTranslator.FromHtml("#48b4da"), Width - 5, Height - 6);
			DrawPixel(g, ColorTranslator.FromHtml("#49b8e1"), Width - 6, Height - 6);
			// END RIGHT LINE

			// BOTTOM LINE
			DrawPixel(g, ColorTranslator.FromHtml("#abdef1"), 3, Height - 4);
			DrawPixel(g, ColorTranslator.FromHtml("#a7d9ec"), 4, Height - 4);
			DrawPixel(g, ColorTranslator.FromHtml("#a3d4e6"), 5, Height - 4);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#a2d3e5"))), 6, Height - 4, Width - 7, Height - 4);
			DrawPixel(g, ColorTranslator.FromHtml("#a4d5e7"), Width - 6, Height - 4);
			DrawPixel(g, ColorTranslator.FromHtml("#a7d9ec"), Width - 5, Height - 4);
			DrawPixel(g, ColorTranslator.FromHtml("#acdff2"), Width - 4, Height - 4);
			DrawPixel(g, ColorTranslator.FromHtml("#a7d9ec"), 3, Height - 5);
			DrawPixel(g, ColorTranslator.FromHtml("#62afcb"), 4, Height - 5);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#3b94b4"))), 5, Height - 5, Width - 6, Height - 5);
			DrawPixel(g, ColorTranslator.FromHtml("#66b1cd"), Width - 5, Height - 5);
			DrawPixel(g, ColorTranslator.FromHtml("#a7d9ec"), Width - 4, Height - 5);
			// END BOTTOM LINE

			var format = new StringFormat() { Alignment = HorizontalAlignment, LineAlignment = VerticalAlignment };
			var space = 0;

			switch (ButtonType)
			{
				case AndroidType.Text:
					g.DrawString(Text, Font, new SolidBrush(ColorTranslator.FromHtml("#333333")), new Rectangle(Padding.Left, Padding.Top, ClientRectangle.Width - (Padding.Left + Padding.Right), ClientRectangle.Height - (Padding.Top + Padding.Bottom)), format);
					break;
				case AndroidType.IconText:
					space = Height - (Padding.Top + Padding.Bottom + IconSize);
					g.DrawImage(Icon, Padding.Left + IconLeftMargin, space / 2 + Padding.Top, IconSize, IconSize);
					g.DrawString(Text, Font, new SolidBrush(ColorTranslator.FromHtml("#333333")), new Rectangle(Padding.Left + IconLeftMargin + IconSize + IconRightMargin, Padding.Top, ClientRectangle.Width - (Padding.Left + IconLeftMargin + IconSize + IconRightMargin + Padding.Right), ClientRectangle.Height - (Padding.Top + Padding.Bottom)), format);
					break;
				case AndroidType.Icon:
					space = Height - (Padding.Top + Padding.Bottom + IconSize);
					g.DrawImage(Icon, Padding.Left + IconLeftMargin, space / 2 + Padding.Top, IconSize, IconSize);
					break;
			}
		}
		#endregion

		#region ButtonStateDisabled
		private void ButtonStateDisabled(Graphics g)
		{
			g.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#f0f0f0")), 0, 0, Width, Height);

			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#dadada"))), 1, 0, Width - 1, 0);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#c7c7c7"))), 0, 0, 0, Height - 1);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#c2c2c2"))), 1, Height - 1, Width - 1, Height - 1);
			g.DrawLine(new Pen(new SolidBrush(ColorTranslator.FromHtml("#c6c6c6"))), Width - 1, 1, Width - 1, Height - 1);

			var format = new StringFormat() { Alignment = HorizontalAlignment, LineAlignment = VerticalAlignment };
			int space = 0;

			switch (ButtonType)
			{
				case AndroidType.Text:
					g.DrawString(Text, Font, new SolidBrush(ColorTranslator.FromHtml("#b7b7b7")), new Rectangle(Padding.Left, Padding.Top, ClientRectangle.Width - (Padding.Left + Padding.Right), ClientRectangle.Height - (Padding.Top + Padding.Bottom)), format);
					break;
				case AndroidType.IconText:
					space = Height - (Padding.Top + Padding.Bottom + IconSize);
					g.DrawImage(Icon, Padding.Left + IconLeftMargin, space / 2 + Padding.Top, IconSize, IconSize);
					g.DrawString(Text, Font, new SolidBrush(ColorTranslator.FromHtml("#b7b7b7")), new Rectangle(Padding.Left + IconLeftMargin + IconSize + IconRightMargin, Padding.Top, ClientRectangle.Width - (Padding.Left + IconLeftMargin + IconSize + IconRightMargin + Padding.Right), ClientRectangle.Height - (Padding.Top + Padding.Bottom)), format);
					break;
				case AndroidType.Icon:
					space = Height - (Padding.Top + Padding.Bottom + IconSize);
					g.DrawImage(Icon, Padding.Left + IconLeftMargin, space / 2 + Padding.Top, IconSize, IconSize);
					break;
			}
		}
		#endregion

		#region ButtonStateFocused
		private void ButtonStateFocused(Graphics g)
		{
			g.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#54c1e9")), 0, 0, Width, Height);
			g.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#85d3ef")), 2, 2, (Width - 2) - 2, (Height - 2) - 2);

			var format = new StringFormat() { Alignment = VerticalAlignment, LineAlignment = HorizontalAlignment };
			g.DrawString(Text, Font, new SolidBrush(ColorTranslator.FromHtml("#333333")), ClientRectangle, format);
		}
		#endregion

		[DefaultValue(4.0f), RefreshProperties(RefreshProperties.All), Category("Android UI"), Description("Radius of rounded background")]
		public float CornerRadius
		{
			get { return _cornerRadius; }
			set { _cornerRadius = value; Invalidate(); }
		}

		[DefaultValue(StringAlignment.Center), RefreshProperties(RefreshProperties.All), Category("Android UI"), Description("Vertical alignment of caption")]
		public StringAlignment VerticalAlignment
		{
			get { return _verticalAlignment; }
			set { _verticalAlignment = value; Invalidate(); }
		}

		[DefaultValue(StringAlignment.Center), RefreshProperties(RefreshProperties.All), Category("Android UI"), Description("Horizontal alignment of caption")]
		public StringAlignment HorizontalAlignment
		{
			get { return _horizontalAlignment; }
			set { _horizontalAlignment = value; Invalidate(); }
		}

		[DefaultValue(AndroidType.Text), RefreshProperties(RefreshProperties.All), Category("Android UI"), Description("Type of a button")]
		public AndroidType ButtonType
		{
			get { return _buttonType; }
			set { _buttonType = value; Invalidate(); }
		}

		[DefaultValue(32), RefreshProperties(RefreshProperties.All), Category("Android UI"), Description("Size of icon")]
		public int IconSize
		{
			get { return _iconSize; }
			set { _iconSize = value; Invalidate(); }
		}

		[DefaultValue(5), RefreshProperties(RefreshProperties.Repaint), Category("Android UI"), Description("Left margin of icon")]
		public int IconLeftMargin
		{
			get { return _iconLeftMargin; }
			set { _iconLeftMargin = value; Invalidate(); }
		}

		[DefaultValue(5), RefreshProperties(RefreshProperties.Repaint), Category("Android UI"), Description("Right margin of icon")]
		public int IconRightMargin
		{
			get { return _iconRightMargin; }
			set { _iconRightMargin = value; Invalidate(); }
		}

		[DefaultValue(null), RefreshProperties(RefreshProperties.Repaint), Category("Android UI"), Description("Icon of button")]
		public Bitmap Icon
		{
			get { return _icon; }
			set { _icon = value; Invalidate(); }
		}

		[DefaultValue(false), RefreshProperties(RefreshProperties.Repaint), Category("Android UI"), Description("Automatically adjust the size")]
		public bool AutoSizeButton
		{
			get { return _autoSize; }
			set 
			{
				_autoSize = value;
				if (_autoSize)
				{
					SizeF sizef;
					var w = 0;
					var h = 0;
					switch (ButtonType)
					{
						case AndroidType.Text:
							using (var g = CreateGraphics())
							{
								sizef = g.MeasureString(Text, Font);
							}
							w = (int)Math.Round(sizef.Width);
							h = (int)Math.Round(sizef.Height);
							Width = Padding.Left + Padding.Right + w + 5;
							Height = Padding.Top + Padding.Bottom + h + 5;
							MinimumSize = Size;
							MaximumSize = Size;
							break;
						case AndroidType.IconText:
							using (var g = CreateGraphics())
							{
								sizef = g.MeasureString(Text, Font);
							}
							w = (int)Math.Round(sizef.Width);
							h = (int)Math.Round(sizef.Height);
							Width = Padding.Left + Padding.Right + IconLeftMargin + IconRightMargin + IconSize + w + 5;
							Height = Padding.Top + Padding.Bottom + IconSize + 5;
							MinimumSize = Size;
							MaximumSize = Size;
							break;
						case AndroidType.Icon:
							Width = Padding.Left + Padding.Right + IconLeftMargin + IconRightMargin + IconSize;
							Height = Padding.Top + Padding.Bottom + IconSize;
							MinimumSize = Size;
							MaximumSize = Size;
							break;
					}
					Refresh();
				}
				else 
				{
					MinimumSize = new Size(8, 8);
					MaximumSize = new Size(0,0);
				}
			}
		}

		protected override void OnTextChanged( EventArgs e )
		{
			base.OnTextChanged( e );

			if( _autoSize )
			{
				SizeF sizef;
				var w = 0;
				var h = 0;
				switch( ButtonType )
				{
					case AndroidType.Text:
						using( var g = CreateGraphics() )
						{
							sizef = g.MeasureString( Text, Font );
						}
						w = ( int ) Math.Round( sizef.Width );
						h = ( int ) Math.Round( sizef.Height );
						Width = Padding.Left + Padding.Right + w + 5;
						Height = Padding.Top + Padding.Bottom + h + 5;
						MinimumSize = Size;
						MaximumSize = Size;
						break;
					case AndroidType.IconText:
						using( var g = CreateGraphics() )
						{
							sizef = g.MeasureString( Text, Font );
						}
						w = ( int ) Math.Round( sizef.Width );
						h = ( int ) Math.Round( sizef.Height );
						Width = Padding.Left + Padding.Right + IconLeftMargin + IconRightMargin + IconSize + w + 5;
						Height = Padding.Top + Padding.Bottom + IconSize + 5;
						MinimumSize = Size;
						MaximumSize = Size;
						break;
					case AndroidType.Icon:
						Width = Padding.Left + Padding.Right + IconLeftMargin + IconRightMargin + IconSize;
						Height = Padding.Top + Padding.Bottom + IconSize;
						MinimumSize = Size;
						MaximumSize = Size;
						break;
				}
				Refresh();
			}
		}

		private static void DrawPixel(Graphics g, Color color, int x, int y)
		{
			g.FillRectangle(new SolidBrush(color), x, y, 1, 1);
		}

		private bool isFontInstalled()
		{
			using ( Font fontTester = new Font("Roboto", 10F, FontStyle.Regular, GraphicsUnit.Pixel) )
			{
				if ( fontTester.Name == "Roboto" )
				{
					// Font exists
					return true;
				}
				else
				{
					// Font doesn't exist
					return false;
				}
			}
		}

		#region InstallFont
		private static void InstallFont()
		{
			// Try install the font.
			var result = AddFontResource(@"RobotoRegular.ttf");
			var error = Marshal.GetLastWin32Error();
			if (error != 0)
			{
				Console.WriteLine(new Win32Exception(error).Message);
			}
			else
			{
				Console.WriteLine((result == 0) ? "Font is already installed." :
													"Font installed successfully.");
			}
		}

		[DllImport("gdi32.dll", EntryPoint = "AddFontResourceW", SetLastError = true)]
		public static extern int AddFontResource([In][MarshalAs(UnmanagedType.LPWStr)]
										string lpFileName);
		#endregion

	}

}
