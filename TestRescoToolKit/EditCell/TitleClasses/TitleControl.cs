using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EditCell
{
	/// <summary>
	/// The purpose of this control is to provide an unified form title bar.
	/// </summary>
	/// <remarks>
	/// The control displays a caption, battery level and current time.
	/// </remarks>
	public partial class TitleControl : Control
	{
		/// <summary>
		/// A common static timer, used by all <see cref="TitleControl"/> instances.
		/// </summary>
		private static Timer m_timer;
		/// <summary>
		/// Creates and initializes a new <see cref="TitleControl"/> instance.
		/// </summary>
		public TitleControl()
		{
            this.Text = "Resco Toolkit";
			if (m_timer == null)
			{
				m_timer = new Timer();
				m_timer.Interval = 10000;
				m_timer.Enabled = true;
			}
			m_timer.Tick += new EventHandler(ControlTimer_Tick);
		}
		/// <summary>
		/// Invalidates the control in response to the update timer tick.
		/// </summary>
		/// <param name="sender">The event sender.</param>
		/// <param name="e">The event arguments.</param>
		private void ControlTimer_Tick(object sender, EventArgs e)
		{
			this.Invalidate();
		}
		/// <summary>
		/// Unregisters itself from the update timer on dispose.
		/// </summary>
		/// <param name="disposing">Whether to release managed resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (m_timer != null)
				{
					m_timer.Tick -= ControlTimer_Tick;
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Battery indicator icon width.
		/// </summary>
		private const int BATTERY_WIDTH = 25;
		/// <summary>
		/// Control left/top/right/bottom margin.
		/// </summary>
		private const int MARGIN = 4;

		/// <summary>
		/// Invalidates the control when the control text is modified.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		protected override void OnTextChanged(EventArgs e)
		{
			this.Invalidate();
			base.OnTextChanged(e);
		}
		/// <summary>
		/// Draws the battery life indicator icon.
		/// </summary>
		/// <param name="g">The drawing context to draw into.</param>
		/// <param name="brush">The icon foreground brush.</param>
		/// <param name="rect">The battery icon position and size.</param>
		/// <param name="level">The actual battery life level [0..100].</param>
		private void DrawBattery(Graphics g, Brush brush, RectangleF rect, int level)
		{
			if (level < 0 || level > 100)
				level = 100;

            int b = SmartGridForm.ScaleCoord(1);
			int h = b * 8;
			int b2 = b * 2;
			int b4 = b * 4;
			int w = (int)rect.Width - b * 8;
			int x = (int)rect.X;
			int y = (int)(rect.Y + (rect.Height-h)/2);

			g.FillRectangle(brush, x, y + b2, b2, b4);

			w -= b2;
			x += b2;
			g.FillRectangle(brush, x, y, w, b);
			g.FillRectangle(brush, x, y, b, h);
			g.FillRectangle(brush, x, y + h - b, w, b);
			g.FillRectangle(brush, x + w - b, y, b, h);

			w -= b4;
			x += b2;
			y += b2;
			h -= b4;
			int r = (w * level) / 100;
			g.FillRectangle(brush, x + w - r, y, r, h);
		}
		/// <summary>
		/// Draws the control.
		/// </summary>
		/// <param name="pe">The paint event arguments.</param>
		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;

			using (Brush brush = new SolidBrush(this.BackColor))
			{
				Font font = this.Font;
				Rectangle rect = this.ClientRectangle;
				g.FillRectangle(brush, rect);

				string time = DateTime.Now.ToShortTimeString();

				int timeWidth = (int)Math.Ceiling(g.MeasureString(time, font).Width);
                int batteryWidth = SmartGridForm.ScaleCoord(BATTERY_WIDTH);
                int margin = SmartGridForm.ScaleCoord(MARGIN);
				int textWidth = rect.Width - timeWidth - batteryWidth - margin*2;

				// [Caption][Battery][Time]
				using (var fore = new SolidBrush(this.ForeColor))
				{
					RectangleF rText = new RectangleF(rect.X+margin, rect.Y, textWidth, rect.Height);
					var sf = new StringFormat()
								{
									Alignment = StringAlignment.Near,
									LineAlignment = StringAlignment.Center
								};

					string title = this.Text.ToUpper();
					/*
					using (var black = new SolidBrush(Color.Black))
					{
						var r = rText;
						r.X += 2;
						r.Y += 2;
						g.DrawString(title, font, black, r, sf);
					}
					*/

					g.DrawString(title, font, fore, rText, sf);

					rText.X = rect.Right - margin - timeWidth;
					rText.Width = timeWidth;
					g.DrawString(time, font, fore, rText, sf);

					rText.X = rect.X + margin + textWidth;
					rText.Width = batteryWidth;
					DrawBattery(g, fore, rText, PowerStatus.BatteryLife);
				}
			}

			base.OnPaint(pe);
		}
	}
}
