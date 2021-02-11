using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AndroidUI.HolyLight
{
	[ClassInterface( ClassInterfaceType.AutoDispatch )]
	public partial class AndroidTextField : Control
	{
		public event EventHandler TextAlignChanged;
		private static Font _defaultFont = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 238 ) ));
		private static Color _defaultForeColor = Color.FromArgb(255, 51, 51, 51);

		private AndroidTextField( int i )
		{
			//Font = _defaultFont;
			//ForeColor = _defaultForeColor;
		}

		public AndroidTextField() : this(0)
		{
			InitializeComponent();

			textBox1.Resize += (o, e) =>
			{
				if ( textBox1.Multiline == false )
				{
					Height = textBox1.Height + Padding.Top + Padding.Bottom;
				}
			};

			textBox1.TextAlignChanged += (o, e) =>
			{
				OnTextAlignChanged(e);
			};

			textBox1.KeyDown += (o, e) =>
			{
				OnKeyDown(e);
			};

			textBox1.KeyUp += (o, e) =>
			{
				OnKeyUp(e);
			};

			textBox1.KeyPress += (o, e) =>
			{
				OnKeyPress(e);
			};

			textBox1.PreviewKeyDown += (o, e) =>
			{
				OnPreviewKeyDown(e);
			};

			textBox1.Click += (o, e) =>
			{
				OnClick(e);
			};
		}

		protected virtual void OnTextAlignChanged(EventArgs e)
		{
			var handler = TextAlignChanged;
			if ( handler != null )
			{
				handler(this, e);
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var color = ColorTranslator.FromHtml("#13a1d0");
			var w = 2F;
			var pen = new Pen(new SolidBrush(color), w);

			e.Graphics.DrawLine(pen, 1, Height - 6, 1, Height - 1); // left
			e.Graphics.DrawLine(pen, Width - 1, Height - 6, Width - 1, Height - 1); // right
			e.Graphics.DrawLine(pen, 0, Height - 2, Width, Height - 2); // bottom
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			textBox1.Top = Padding.Top;
			textBox1.Left = Padding.Left;
			textBox1.Width = Width - ( Padding.Left + Padding.Right );

			if ( textBox1.Multiline == false )
			{
				Height = textBox1.Height + ( Padding.Top + Padding.Bottom );
			}
			else
			{
				textBox1.Height = Height - ( Padding.Top + Padding.Bottom );
			}
		}

		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
			Padding = new Padding(8, 2, 8, 5);
		}

		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			textBox1.ForeColor = ForeColor;
		}

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			textBox1.Font = Font;
		}

		protected override void OnTextChanged( EventArgs e )
		{
			base.OnTextChanged( e );
			//textBox1.Text = Text;
		}

		[DefaultValue(false), Category("Behavior")]
		public bool Multiline
		{
			get { return textBox1.Multiline; }
			set { textBox1.Multiline = value; }
		}

		[DefaultValue(typeof(HorizontalAlignment),"Left"),Category("Appearance"), Description("Gets or sets how text is aligned in a System.Windows.Forms.TextBox control.")]
		public HorizontalAlignment TextAlign
		{
			get { return textBox1.TextAlign; }
			set { textBox1.TextAlign = value; }
		}

		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				if ( value == null )
					base.Font = _defaultFont;
				else
				{
					if ( value == System.Windows.Forms.Control.DefaultFont )
						base.Font = _defaultFont;
					else
						base.Font = value;
				}
			}
		}

		public override void ResetFont()
		{
			Font = _defaultFont;
		}

		private bool ShouldSerializeFont()
		{
			return ( !Font.Equals(_defaultFont) );
		}

		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				if ( value == null )
					base.ForeColor = _defaultForeColor;
				else
				{
					if ( value == System.Windows.Forms.Control.DefaultForeColor )
						base.ForeColor = _defaultForeColor;
					else
						base.ForeColor = value;
				}
			}
		}

		public override void ResetForeColor()
		{
			ForeColor = _defaultForeColor;
		}

		private bool ShouldSerializeForeColor()
		{
			return ( !ForeColor.Equals(_defaultForeColor) );
		}

		public override string Text
		{
			get
			{
				return textBox1.Text;
			}
			set
			{
				textBox1.Text = value;
			}
		}

		//public override bool AutoScroll
		//{
		//	get
		//	{
		//		return false;
		//	}
		//}

	}
}
