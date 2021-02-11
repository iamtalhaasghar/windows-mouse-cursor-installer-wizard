using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CursorInstallerWizard
{
	class CustomPictureBox : System.Windows.Forms.PictureBox
	{
		public string CursorPath { get; set; }

		protected override void OnPaint( System.Windows.Forms.PaintEventArgs pe )
		{
			base.OnPaint( pe );
			pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			var color = ColorTranslator.FromHtml( "#13a1d0" );
			pe.Graphics.DrawRectangle( new Pen( new SolidBrush( color ) ), new Rectangle( 0, 0, 31, 31 ) );
		}
	}
}
