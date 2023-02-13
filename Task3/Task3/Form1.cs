using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace Task3
{
    public partial class Form1 : Form
    {
        Random _random = new Random();

        Pen _reproductionColor;
        Pen _newColor;
        List<Point> _points = new List<Point>();
        class Lines 
        {
            public List<Point> LinePoints = new List<Point>(); 
            public Pen Color;            
            public Lines(List<Point> _points, Pen color)
            {
                for (int i = 0; i < _points.Count; i++)
                {
                    LinePoints.Add(_points[i]);
                }
                Color = color;
            }
        }
        List<Lines> _lines = new List<Lines>(); // Массив всех линий, нарисованных на панели
        int count = 0;
        public Form1()
        {
            InitializeComponent();
            ColorButton.Enabled = false;
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            if (ColorDialog.ShowDialog() == DialogResult.OK)
            {
                _newColor = new Pen(ColorDialog.Color, 10);
                ColorButton.BackColor = _newColor.Color;
            }
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Panel.Capture = true;
                _reproductionColor = new Pen(Color.FromArgb(_random.Next(255), _random.Next(255), _random.Next(255), _random.Next(255)), 10);
            }
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Panel.Capture = false;
                _lines.Add(new Lines(_points, _reproductionColor)); 
                count++; 
                LinesListBox.Items.Add("Фигура " + count); 
                _points.Clear();
            }
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (Panel.Capture)
            {
                _points.Add(new Point(e.X, e.Y));
                if (_points.Count >= 2)
                {
                    Graphics g = Panel.CreateGraphics();
                    g.DrawLine(_reproductionColor, _points[_points.Count - 2], _points[_points.Count - 1]);
                }
            }
        }
    }
}