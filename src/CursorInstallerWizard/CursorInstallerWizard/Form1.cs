using Ionic.Zip;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CursorInstallerWizard
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void InitializeCursors()
		{
			var cursors = GetCursors();
			if( cursors.Count != 0 )
			{
				for( var index = 0; index < cursors.Count; index++ )
				{
					switch( cursors[index].CursorName )
					{
						case "AppStarting":
							AppStarting.Image = cursors[index].Icon.ToBitmap();
							AppStarting.CursorPath = cursors[index].FPath + "\\" + cursors[index].FName;
							break;
						case "Arrow":
							Arrow.Image = cursors[index].Icon.ToBitmap();
							Arrow.CursorPath = cursors[index].FPath + "\\" + cursors[index].FName;
							break;
						case "Crosshair":
							Crosshair.Image = cursors[index].Icon.ToBitmap();
							Crosshair.CursorPath = cursors[index].FPath + "\\" + cursors[index].FName;
							break;
						case "Hand":
							Hand.Image = cursors[index].Icon.ToBitmap();
							Hand.CursorPath = cursors[index].FPath + "\\" + cursors[index].FName;
							break;
						case "Help":
							Help.Image = cursors[index].Icon.ToBitmap();
							Help.CursorPath = cursors[index].FPath + "\\" + cursors[index].FName;
							break;
						case "IBeam":
							IBeam.Image = cursors[index].Icon.ToBitmap();
							IBeam.CursorPath = cursors[index].FPath + "\\" + cursors[index].FName;
							break;
						case "No":
							No.Image = cursors[index].Icon.ToBitmap();
							No.CursorPath = cursors[index].FPath + "\\" + cursors[index].FName;
							break;
						case "NWPen":
							NWPen.Image = cursors[index].Icon.ToBitmap();
							NWPen.CursorPath = cursors[index].FPath + "\\" + cursors[index].FName;
							break;
						case "SizeAll":
							SizeAll.Image = cursors[index].Icon.ToBitmap();
							SizeAll.CursorPath = cursors[index].FPath + "\\" + cursors[index].FName;
							break;
						case "SizeNESW":
							SizeNESW.Image = cursors[index].Icon.ToBitmap();
							SizeNESW.CursorPath = cursors[index].FPath + "\\" + cursors[index].FName;
							break;
						case "SizeNS":
							SizeNS.Image = cursors[index].Icon.ToBitmap();
							SizeNS.CursorPath = cursors[index].FPath + "\\" + cursors[index].FName;
							break;
						case "SizeNWSE":
							SizeNWSE.Image = cursors[index].Icon.ToBitmap();
							SizeNWSE.CursorPath = cursors[index].FPath + "\\" + cursors[index].FName;
							break;
						case "SizeWE":
							SizeWE.Image = cursors[index].Icon.ToBitmap();
							SizeWE.CursorPath = cursors[index].FPath + "\\" + cursors[index].FName;
							break;
						case "UpArrow":
							UpArrow.Image = cursors[index].Icon.ToBitmap();
							UpArrow.CursorPath = cursors[index].FPath + "\\" + cursors[index].FName;
							break;
						case "Wait":
							Wait.Image = cursors[index].Icon.ToBitmap();
							Wait.CursorPath = cursors[index].FPath + "\\" + cursors[index].FName;
							break;
					}
				}
			}
		}

		private static List<CursorFile> GetCursors()
		{
			var icons = new List<CursorFile>();

			foreach( var regcur in getCursorRegList() )
			{
				var file = new System.IO.FileInfo( regcur.Value );
				if( file.Extension == ".cur" || file.Extension == ".ani" )
				{
					// Set a default icon for the file.
					Icon iconForFile = SystemIcons.WinLogo;
					iconForFile = Icon.ExtractAssociatedIcon( file.FullName );
					iconForFile = System.Drawing.Icon.ExtractAssociatedIcon( file.FullName );
					var cursorFile = new CursorFile();
					cursorFile.CursorName = regcur.Key;
					cursorFile.FName = file.Name;
					cursorFile.FPath = Path.GetDirectoryName( file.FullName );
					cursorFile.Icon = iconForFile;
					icons.Add( cursorFile );
				}
			}
			return icons;
		}

		private static List<RegistryCursor> getCursorRegList()
		{
			var RegCurList = new List<RegistryCursor>();
			var m = Registry.CurrentUser.OpenSubKey( @"Control Panel\Cursors" );
			var key = m.GetValueNames();
			for( var index = 0; index < key.Length; index++ )
			{
				if( key[index] != "Scheme Source" )
				{
					RegCurList.Add( new RegistryCursor() { Key = key[index], Value = ( string ) m.GetValue( key[index] ) } );
				}
			}
			return RegCurList;
		}

		private void Form1_Load( object sender, EventArgs e )
		{
			InitializeCursors();
		}

		private void Cancel_Click( object sender, EventArgs e )
		{
			PackageName.Text = "";
			Submit.Enabled = false;
			InitializeCursors();
		}

		private void Submit_Click( object sender, EventArgs e )
		{
			if( PackageName.Text.Length != 0 )
			{
				if( folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
				{
					CreateInstallFile( folderBrowserDialog1.SelectedPath );
					folderBrowserDialog1.SelectedPath = "";
					InitializeCursors();
				}
			}
			else
			{
				MessageBox.Show( "Fill in the title, it's required.", "Empty title field", MessageBoxButtons.OK );
			}
		}

		private void CreateInstallFile( string path )
		{
			using( ZipFile zip = new ZipFile() )
			{
				foreach( var cursorPicture in Controls.OfType<CustomPictureBox>() )
				{
					zip.AddFile( cursorPicture.CursorPath, "" );
				}

				using( System.IO.StreamWriter file = new System.IO.StreamWriter( path + "\\Install.inf" ) )
				{
					file.WriteLine( "; " + PackageName.Text + "\n" );

					file.WriteLine( "[Version]" );
					file.WriteLine( "signature=\"$CHICAGO$\"\n" );

					file.WriteLine( "[DefaultInstall]" );
					file.WriteLine( "CopyFiles = Scheme.Cur, Scheme.Txt" );
					file.WriteLine( "AddReg    = Scheme.Reg\n" );

					file.WriteLine( "[DestinationDirs]" );
					file.WriteLine( "Scheme.Cur = 10,\"%CUR_DIR%\"" );
					file.WriteLine( "Scheme.Txt = 10,\"%CUR_DIR%\"\n" );

					file.WriteLine( "[Scheme.Reg]" );
					file.WriteLine( "HKCU,\"Control Panel\\Cursors\\Schemes\",\"%SCHEME_NAME%\",,\"%10%\\%CUR_DIR%\\%pointer%,%10%\\%CUR_DIR%\\%help%,%10%\\%CUR_DIR%\\%work%,%10%\\%CUR_DIR%\\%busy%,%10%\\%CUR_DIR%\\%cross%,%10%\\%CUR_DIR%\\%Text%,%10%\\%CUR_DIR%\\%Hand%,%10%\\%CUR_DIR%\\%unavailiable%,%10%\\%CUR_DIR%\\%Vert%,%10%\\%CUR_DIR%\\%Horz%,%10%\\%CUR_DIR%\\%Dgn1%,%10%\\%CUR_DIR%\\%Dgn2%,%10%\\%CUR_DIR%\\%move%,%10%\\%CUR_DIR%\\%alternate%,%10%\\%CUR_DIR%\\%link%\"\n" );

					file.WriteLine( "; -- Common Information\n" );

					file.WriteLine( "[Scheme.Cur]" );
					file.WriteLine( Path.GetFileName( Arrow.CursorPath ) );
					file.WriteLine( Path.GetFileName( Help.CursorPath ) );
					file.WriteLine( Path.GetFileName( AppStarting.CursorPath ) );
					file.WriteLine( Path.GetFileName( Wait.CursorPath ) );
					file.WriteLine( Path.GetFileName( IBeam.CursorPath ) );
					file.WriteLine( Path.GetFileName( No.CursorPath ) );
					file.WriteLine( Path.GetFileName( SizeNS.CursorPath ) );
					file.WriteLine( Path.GetFileName( SizeWE.CursorPath ) );
					file.WriteLine( Path.GetFileName( SizeNWSE.CursorPath ) );
					file.WriteLine( Path.GetFileName( SizeNESW.CursorPath ) );
					file.WriteLine( Path.GetFileName( SizeAll.CursorPath ) );
					file.WriteLine( Path.GetFileName( Hand.CursorPath ) );
					file.WriteLine( Path.GetFileName( Crosshair.CursorPath ) );
					file.WriteLine( Path.GetFileName( NWPen.CursorPath ) );
					file.WriteLine( Path.GetFileName( UpArrow.CursorPath ) );

					file.WriteLine( "[Strings]" );
					file.WriteLine( "CUR_DIR       = \"Cursors\\" + PackageName.Text + "\"" );
					file.WriteLine( "SCHEME_NAME   = \"" + PackageName.Text + "\"" );

					file.WriteLine( "pointer       = \"" + Path.GetFileName( Arrow.CursorPath ) + "\"" );
					file.WriteLine( "help          = \"" + Path.GetFileName( Help.CursorPath ) + "\"" );
					file.WriteLine( "work          = \"" + Path.GetFileName( AppStarting.CursorPath ) + "\"" );
					file.WriteLine( "busy          = \"" + Path.GetFileName( Wait.CursorPath ) + "\"" );
					file.WriteLine( "text          = \"" + Path.GetFileName( IBeam.CursorPath ) + "\"" );
					file.WriteLine( "unavailiable  = \"" + Path.GetFileName( No.CursorPath ) + "\"" );
					file.WriteLine( "vert          = \"" + Path.GetFileName( SizeWE.CursorPath ) + "\"" );
					file.WriteLine( "horz          = \"" + Path.GetFileName( SizeNS.CursorPath ) + "\"" );
					file.WriteLine( "dgn1          = \"" + Path.GetFileName( SizeNWSE.CursorPath ) + "\"" );
					file.WriteLine( "dgn2          = \"" + Path.GetFileName( SizeNESW.CursorPath ) + "\"" );
					file.WriteLine( "move          = \"" + Path.GetFileName( SizeAll.CursorPath ) + "\"" );
					file.WriteLine( "link          = \"" + Path.GetFileName( Hand.CursorPath ) + "\"" );
					file.WriteLine( "cross         = \"" + Path.GetFileName( Crosshair.CursorPath ) + "\"" );
					file.WriteLine( "hand          = \"" + Path.GetFileName( NWPen.CursorPath ) + "\"" );
					file.WriteLine( "alternate     = \"" + Path.GetFileName( UpArrow.CursorPath ) + "\"" );

				}

				zip.AddFile( path + "\\Install.inf", "" );
				zip.Save( path + "\\" + PackageName.Text + ".zip" );
			}
			try
			{
				System.IO.File.Delete( path + "\\Install.inf" );
			}
			catch( System.IO.IOException e )
			{
			}
			MessageBox.Show( "Succesfuly created package", "File created", MessageBoxButtons.OK );
			PackageName.Text = "";
			Submit.Enabled = false;
			InitializeCursors();
		}

		private void CursorButton_Click( object sender, EventArgs e )
		{
			var button = ( AndroidUI.HolyLight.AndroidButton ) sender;

			switch( button.Name )
			{
				case "AppStartingButton":
					if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
					{
						Console.WriteLine( openFileDialog1.FileName );
						var file = new System.IO.FileInfo( openFileDialog1.FileName );

						Icon iconForFile = SystemIcons.WinLogo;
						iconForFile = Icon.ExtractAssociatedIcon( file.FullName );
						iconForFile = System.Drawing.Icon.ExtractAssociatedIcon( file.FullName );
						AppStarting.Image = iconForFile.ToBitmap();
						AppStarting.CursorPath = openFileDialog1.FileName;
					}
					openFileDialog1.FileName = "";
					break;
				case "ArrowButton":
					if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
					{
						Console.WriteLine( openFileDialog1.FileName );
						var file = new System.IO.FileInfo( openFileDialog1.FileName );

						Icon iconForFile = SystemIcons.WinLogo;
						iconForFile = Icon.ExtractAssociatedIcon( file.FullName );
						iconForFile = System.Drawing.Icon.ExtractAssociatedIcon( file.FullName );
						Arrow.Image = iconForFile.ToBitmap();
						Arrow.CursorPath = openFileDialog1.FileName;
					}
					openFileDialog1.FileName = "";
					break;
				case "CrosshairButton":
					if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
					{
						Console.WriteLine( openFileDialog1.FileName );
						var file = new System.IO.FileInfo( openFileDialog1.FileName );

						Icon iconForFile = SystemIcons.WinLogo;
						iconForFile = Icon.ExtractAssociatedIcon( file.FullName );
						iconForFile = System.Drawing.Icon.ExtractAssociatedIcon( file.FullName );
						Crosshair.Image = iconForFile.ToBitmap();
						Crosshair.CursorPath = openFileDialog1.FileName;
					}
					break;
				case "HandButton":
					if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
					{
						Console.WriteLine( openFileDialog1.FileName );
						var file = new System.IO.FileInfo( openFileDialog1.FileName );

						Icon iconForFile = SystemIcons.WinLogo;
						iconForFile = Icon.ExtractAssociatedIcon( file.FullName );
						iconForFile = System.Drawing.Icon.ExtractAssociatedIcon( file.FullName );
						Hand.Image = iconForFile.ToBitmap();
						Hand.CursorPath = openFileDialog1.FileName;
					}
					openFileDialog1.FileName = "";
					break;
				case "HelpButton":
					if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
					{
						Console.WriteLine( openFileDialog1.FileName );
						var file = new System.IO.FileInfo( openFileDialog1.FileName );

						Icon iconForFile = SystemIcons.WinLogo;
						iconForFile = Icon.ExtractAssociatedIcon( file.FullName );
						iconForFile = System.Drawing.Icon.ExtractAssociatedIcon( file.FullName );
						Help.Image = iconForFile.ToBitmap();
						Help.CursorPath = openFileDialog1.FileName;
					}
					openFileDialog1.FileName = "";
					break;
				case "IBeamButton":
					if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
					{
						Console.WriteLine( openFileDialog1.FileName );
						var file = new System.IO.FileInfo( openFileDialog1.FileName );

						Icon iconForFile = SystemIcons.WinLogo;
						iconForFile = Icon.ExtractAssociatedIcon( file.FullName );
						iconForFile = System.Drawing.Icon.ExtractAssociatedIcon( file.FullName );
						IBeam.Image = iconForFile.ToBitmap();
						IBeam.CursorPath = openFileDialog1.FileName;
					}
					openFileDialog1.FileName = "";
					break;
				case "NoButton":
					if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
					{
						Console.WriteLine( openFileDialog1.FileName );
						var file = new System.IO.FileInfo( openFileDialog1.FileName );

						Icon iconForFile = SystemIcons.WinLogo;
						iconForFile = Icon.ExtractAssociatedIcon( file.FullName );
						iconForFile = System.Drawing.Icon.ExtractAssociatedIcon( file.FullName );
						No.Image = iconForFile.ToBitmap();
						No.CursorPath = openFileDialog1.FileName;
					}
					openFileDialog1.FileName = "";
					break;
				case "NWPenButton":
					if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
					{
						Console.WriteLine( openFileDialog1.FileName );
						var file = new System.IO.FileInfo( openFileDialog1.FileName );

						Icon iconForFile = SystemIcons.WinLogo;
						iconForFile = Icon.ExtractAssociatedIcon( file.FullName );
						iconForFile = System.Drawing.Icon.ExtractAssociatedIcon( file.FullName );
						NWPen.Image = iconForFile.ToBitmap();
						NWPen.CursorPath = openFileDialog1.FileName;
					}
					openFileDialog1.FileName = "";
					break;
				case "SizeAllButton":
					if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
					{
						Console.WriteLine( openFileDialog1.FileName );
						var file = new System.IO.FileInfo( openFileDialog1.FileName );

						Icon iconForFile = SystemIcons.WinLogo;
						iconForFile = Icon.ExtractAssociatedIcon( file.FullName );
						iconForFile = System.Drawing.Icon.ExtractAssociatedIcon( file.FullName );
						SizeAll.Image = iconForFile.ToBitmap();
						SizeAll.CursorPath = openFileDialog1.FileName;
					}
					openFileDialog1.FileName = "";
					break;
				case "SizeNESWButton":
					if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
					{
						Console.WriteLine( openFileDialog1.FileName );
						var file = new System.IO.FileInfo( openFileDialog1.FileName );

						Icon iconForFile = SystemIcons.WinLogo;
						iconForFile = Icon.ExtractAssociatedIcon( file.FullName );
						iconForFile = System.Drawing.Icon.ExtractAssociatedIcon( file.FullName );
						SizeNESW.Image = iconForFile.ToBitmap();
						SizeNESW.CursorPath = openFileDialog1.FileName;
					}
					openFileDialog1.FileName = "";
					break;
				case "SizeNSButton":
					if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
					{
						Console.WriteLine( openFileDialog1.FileName );
						var file = new System.IO.FileInfo( openFileDialog1.FileName );

						Icon iconForFile = SystemIcons.WinLogo;
						iconForFile = Icon.ExtractAssociatedIcon( file.FullName );
						iconForFile = System.Drawing.Icon.ExtractAssociatedIcon( file.FullName );
						SizeNS.Image = iconForFile.ToBitmap();
						SizeNS.CursorPath = openFileDialog1.FileName;
					}
					openFileDialog1.FileName = "";
					break;
				case "SizeNWSEButton":
					if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
					{
						Console.WriteLine( openFileDialog1.FileName );
						var file = new System.IO.FileInfo( openFileDialog1.FileName );

						Icon iconForFile = SystemIcons.WinLogo;
						iconForFile = Icon.ExtractAssociatedIcon( file.FullName );
						iconForFile = System.Drawing.Icon.ExtractAssociatedIcon( file.FullName );
						SizeNWSE.Image = iconForFile.ToBitmap();
						SizeNWSE.CursorPath = openFileDialog1.FileName;
					}
					openFileDialog1.FileName = "";
					break;
				case "SizeWEButton":
					if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
					{
						Console.WriteLine( openFileDialog1.FileName );
						var file = new System.IO.FileInfo( openFileDialog1.FileName );

						Icon iconForFile = SystemIcons.WinLogo;
						iconForFile = Icon.ExtractAssociatedIcon( file.FullName );
						iconForFile = System.Drawing.Icon.ExtractAssociatedIcon( file.FullName );
						SizeWE.Image = iconForFile.ToBitmap();
						SizeWE.CursorPath = openFileDialog1.FileName;
					}
					openFileDialog1.FileName = "";
					break;
				case "UpArrowButton":
					if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
					{
						Console.WriteLine( openFileDialog1.FileName );
						var file = new System.IO.FileInfo( openFileDialog1.FileName );

						Icon iconForFile = SystemIcons.WinLogo;
						iconForFile = Icon.ExtractAssociatedIcon( file.FullName );
						iconForFile = System.Drawing.Icon.ExtractAssociatedIcon( file.FullName );
						UpArrow.Image = iconForFile.ToBitmap();
						UpArrow.CursorPath = openFileDialog1.FileName;
					}
					openFileDialog1.FileName = "";
					break;
				case "WaitButton":
					if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
					{
						Console.WriteLine( openFileDialog1.FileName );
						var file = new System.IO.FileInfo( openFileDialog1.FileName );

						Icon iconForFile = SystemIcons.WinLogo;
						iconForFile = Icon.ExtractAssociatedIcon( file.FullName );
						iconForFile = System.Drawing.Icon.ExtractAssociatedIcon( file.FullName );
						Wait.Image = iconForFile.ToBitmap();
						Wait.CursorPath = openFileDialog1.FileName;
					}
					openFileDialog1.FileName = "";
					break;
			}

		}

		private void PackageName_KeyUp( object sender, KeyEventArgs e )
		{
			Submit.Enabled = ( PackageName.Text.Length > 3 ) ? true : false;
		}

	}
}
