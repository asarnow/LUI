using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace LUI.controls
{
    public partial class GraphControl : UserControl
    {
        Action<float,float> RescaleHandler;

        PointF Origin;
        PointF XAxisRight;
        PointF YAxisTop;

        Bitmap Bitmap;
        Graphics BitmapGraphics;
        Bitmap AxesBitmap;
        Region _Region;
        Rectangle Bound;
        
        RectangleF Axes;
        RectangleF AxesPadding;
        RectangleF Canvas;

        public float XMax { get; set; }
        public float YMax { get; set; }
        public float InitialXMax { get; set; }
        public float InitialYMax { get; set; }
        const float XMAXDEFAULT = 1023f;
        const float YMAXDEFAULT = 1.0f;

        public float XMin { get; set; }
        public float YMin { get; set; }
        public float InitialXMin { get; set; }
        public float InitialYMin { get; set; }
        const float XMINDEFAULT = 0f;
        const float YMINDEFAULT = 0.0f;

        public float ScaleHeight { get; set; }
        public float InitialScaleHeight { get; set; }
        const float SCALEHEIGHTDEFAULT = 0.0f;

        public float YAxisWidth { get; set; }
        public float XAxisHeight { get; set; }
        const float YAXISWIDTHDEFAULT = 0.05f;
        const float XAXISHEIGHTDEFAULT = 0.1f;

        public int NYTicks { get; set; }
        const int NYTICKSDEFAULT = 10;
        public int NXTicks { get; set; }
        const int NXTICKSDEFAULT = 10;

        public string XLabelFormat { get; set; }
        public string YLabelFormat { get; set; }

        public float PadLeft { get; set; }
        const float PADLEFTDEFAULT = 0.01f;
        public float PadTop { get; set; }
        const float PADTOPDEFAULT = 0.01f;
        public float PadRight { get; set; }
        const float PADRIGHTDEFAULT = 0.01f;
        public float PadBottom { get; set; }
        const float PADBOTTOMDEFAULT = 0.01f;

        public Color AxesColor { get; set; }
        public Font AxesFont { get; set; }
        public Color AxesFontColor { get; set; }

        private SizeF MarkerOffset;
        private Font _MarkerFont;
        public Font MarkerFont
        { 
            get
            {
                return _MarkerFont;
            }
            set
            {
                _MarkerFont = value;
                float Offset = _MarkerFont.SizeInPoints /
                   _MarkerFont.FontFamily.GetEmHeight(_MarkerFont.Style) *
                   _MarkerFont.FontFamily.GetCellAscent(_MarkerFont.Style) /
                   2;
                MarkerOffset = new SizeF(Offset, Offset);
            }
        }

        public Color MarkerColor { get; set; }
        private string _Marker;
        public string Marker
        {
            get
            {
                return _Marker;
            }
            set
            {
                _Marker = value;
                //MarkerOffset = TextRenderer.MeasureText(_Marker, MarkerFont);
                //MarkerOffset = BitmapGraphics.MeasureString(Marker, MarkerFont);
            }
        }

        const string MARKERDEFAULT = "*";

        public GraphControl()
        {
            InitializeComponent();
            ResizeRedraw = true;

            RescaleHandler = this.Rescale;

            AxesColor = Color.Blue;
            AxesFont = Font;
            AxesFontColor = Color.Black;
            Marker = MARKERDEFAULT;
            MarkerColor = Color.Blue;
            MarkerFont = Font;

            XAxisHeight = XAXISHEIGHTDEFAULT;
            YAxisWidth = YAXISWIDTHDEFAULT;
            XMax = InitialXMax = XMAXDEFAULT;
            YMax = InitialYMax = float.NegativeInfinity;

            XMin = InitialXMin = XMINDEFAULT;
            YMin = InitialYMin = float.PositiveInfinity;

            ScaleHeight = InitialScaleHeight = SCALEHEIGHTDEFAULT;

            PadLeft = PADLEFTDEFAULT;
            PadTop = PADTOPDEFAULT;
            PadRight = PADRIGHTDEFAULT;
            PadBottom = PADBOTTOMDEFAULT;

            NYTicks = NYTICKSDEFAULT;
            NXTicks = NXTICKSDEFAULT;
            XLabelFormat = "f0";
            YLabelFormat = "n3";
        }

        void InitBitmap()
        {
            // Bound and region of the control.
            Bound = new Rectangle(new Point(0, 0), Size);
            _Region = new Region(Bound);
            Bitmap = new Bitmap(Bound.Width, Bound.Height);
            Clear(Bitmap);
            Bitmap.MakeTransparent(BackColor);
            BitmapGraphics = Graphics.FromImage(Bitmap);
            // Total area available for drawing.
            Canvas = new RectangleF(Bound.Location, Bound.Size);            
            // Axes area w/ padding.
            AxesPadding = new RectangleF(
                Canvas.Left + Canvas.Width * YAxisWidth,
                Canvas.Top,
                Canvas.Width * (1 - YAxisWidth),
                Canvas.Height * (1 - XAxisHeight)
            );
            // Axes area inside padding.
            Axes = new RectangleF(
                AxesPadding.Left + AxesPadding.Width*PadLeft,
                AxesPadding.Top + AxesPadding.Height*PadTop,
                AxesPadding.Width - AxesPadding.Width*PadRight,
                AxesPadding.Height - AxesPadding.Height*PadBottom);
        }

        void InitAxesBitmap()
        {
            AxesBitmap = new Bitmap(Bound.Width, Bound.Height);
            Clear(AxesBitmap);
            DrawAxes(AxesBitmap);
            if (YMax != float.NegativeInfinity) DrawYLabels(AxesBitmap);
            if (XMax != float.NegativeInfinity) DrawXLabels(AxesBitmap);
            AxesBitmap.MakeTransparent(BackColor);
        }

        void Rescale(float _Min, float _Max)
        {
            float NewScaleHeight = Math.Abs(_Max - _Min);
            if (NewScaleHeight > ScaleHeight)
            {
                if (ScaleHeight != InitialScaleHeight)
                {
                    float ScaleFactor = ScaleHeight / NewScaleHeight;
                    float NewZero = Math.Abs(_Max / NewScaleHeight);
                    float Zero = Math.Abs(YMax / ScaleHeight);

                    float NewAxesHeight = Axes.Height * ScaleFactor;
                    RectangleF NewAxes = new RectangleF(
                        Axes.Left,
                        Axes.Top + Axes.Height * NewZero - NewAxesHeight * Zero,
                        Axes.Width,
                        NewAxesHeight
                        );

                    Bitmap ScaledBitmap = new Bitmap(Bound.Width, Bound.Height);
                    Clear(ScaledBitmap);
                    using (Graphics G = Graphics.FromImage(ScaledBitmap))
                    {
                        G.InterpolationMode = InterpolationMode.High;
                        G.CompositingQuality = CompositingQuality.HighQuality;
                        G.SmoothingMode = SmoothingMode.AntiAlias;
                        G.DrawImage(Bitmap, NewAxes, Axes, GraphicsUnit.Pixel);
                    }
                    Bitmap.Dispose();
                    Bitmap = ScaledBitmap;
                    Bitmap.MakeTransparent(BackColor);


                    BitmapGraphics.Dispose();
                    BitmapGraphics = Graphics.FromImage(Bitmap);
                    //BitmapGraphics.CompositingMode = CompositingMode.SourceOver;
                }
                AxesBitmap.Dispose();
                AxesBitmap = null;
                YMin = _Min;
                YMax = _Max;
                ScaleHeight = NewScaleHeight;
            }
        }

        public void DrawPoints(double[] Y)
        {
            using (Brush B = new SolidBrush(MarkerColor))
            {
                float _Min = (float)Y.Min() < YMin ? (float)Y.Min() : YMin;
                float _Max = (float)Y.Max() > YMax ? (float)Y.Max() : YMax;
                RescaleHandler(_Min, _Max);
                for (int i = 0; i < Y.Length; i++)
                {
                    float x = (float)(i + 1) / Y.Length;
                    float y = Math.Abs(YMax - (float)Y[i]) / ScaleHeight;
                    DrawPoint(BitmapGraphics, B, MarkerFont, Axes, x, y);
                }
            }
        }

        public void DrawPoints(double[] X, double[] Y)
        {
            using (Brush B = new SolidBrush(MarkerColor))
            {
                float _Min = (float)Y.Min() < YMin ? (float)Y.Min() : YMin;
                float _Max = (float)Y.Max() > YMax ? (float)Y.Max() : YMax;
                Rescale(_Min, _Max);
                for (int i = 0; i < Y.Length; i++)
                {
                    float x = (float)X[i] / XMax;
                    float y = (float)Y[i] / YMax;
                    DrawPoint(BitmapGraphics, B, MarkerFont, Axes, x, y);
                }
            }
        }

        void DrawPoints(Image Image, double[] X, double[] Y)
        {
            using (Graphics G = Graphics.FromImage(Image))
            {
                G.CompositingMode = CompositingMode.SourceOver;
                using (Brush B = new SolidBrush(MarkerColor))
                {
                    for (int i = 0; i < X.Length; i++)
                    {
                        float x = (float)X[i] / XMax;
                        float y = (float)Y[i] / YMax;
                        DrawPoint(G, B, MarkerFont, Axes, x, y);
                    }
                }
            }
        }

        void DrawPoint(Graphics G, Brush B, Font F, RectangleF Axes, float x, float y)
        {           
            float X = Axes.Left + Axes.Width * x - MarkerOffset.Width;
            float Y = Axes.Top + Axes.Height * y - MarkerOffset.Height;
            PointF Point = new PointF(X, Y);
            G.DrawString(Marker, F, B, Point);
            //TextRenderer.DrawText(G, Marker, MarkerFont, new Point((int)X, (int)Y), MarkerColor);
        }

        void DrawAxes(Image Image)
        {
            using (Graphics G = Graphics.FromImage(Image))
            {
                G.CompositingMode = CompositingMode.SourceOver;
                Origin = new PointF(Axes.Left, Axes.Bottom);
                XAxisRight = new PointF(Axes.Right, Origin.Y);
                YAxisTop = new PointF(Origin.X, Axes.Top);
                using (Pen AxisPen = new Pen(AxesColor))
                {
                    G.DrawLine(AxisPen, Origin.X, Origin.Y, YAxisTop.X, YAxisTop.Y);
                    G.DrawLine(AxisPen, XAxisRight.X, XAxisRight.Y, XAxisRight.X, YAxisTop.Y);
                    for (float i = 0; i <= NYTicks; i++)
                    {
                        G.DrawLine(AxisPen, Origin.X, Origin.Y - i / NYTicks * Axes.Height, XAxisRight.X, XAxisRight.Y - i / NYTicks * Axes.Height);
                    }
                }
            }
        }

        void DrawYLabels(Image Image)
        {
            using (Graphics G = Graphics.FromImage(Image))
            {
                using (Brush B = new SolidBrush(AxesFontColor))
                {
                    for (float i = 0; i <= NYTicks; i++)
                    {
                        float YVal = YMax - i/NYTicks*ScaleHeight;
                        string TickLabel = YVal.ToString(YLabelFormat);
                        SizeF TickLabelOffset = G.MeasureString(TickLabel, AxesFont);
                        float X = AxesPadding.Left - TickLabelOffset.Width;
                        float Y = Axes.Top + Axes.Height*i/NYTicks - TickLabelOffset.Height/2;
                        G.DrawString(TickLabel, AxesFont, B, new PointF(X, Y));
                    }
                }
            }
        }

        void DrawXLabels(Image Image)
        {
            using (Graphics G = Graphics.FromImage(Image))
            {
                using (Brush B = new SolidBrush(AxesFontColor))
                {
                    for (float i = 0; i <= NXTicks; i++)
                    {
                        int XMaxOrder = (int)(Math.Log10(XMax));
                        double XMaxRound = Math.Round(XMax / Math.Pow(10, XMaxOrder)) * Math.Pow(10, XMaxOrder);
                        float XVal = XMin + i / NXTicks * (float)XMaxRound;
                        string TickLabel = XVal.ToString(XLabelFormat);
                        SizeF TickLabelOffset = G.MeasureString(TickLabel, AxesFont);
                        float X = Axes.Left + Axes.Width * XVal/XMax - TickLabelOffset.Width/2;
                        float Y = AxesPadding.Bottom + TickLabelOffset.Height;
                        G.DrawString(TickLabel, AxesFont, B, new PointF(X, Y));
                    }
                }
            }
        }

        public void Clear(Image Image)
        {
            using (Graphics G = Graphics.FromImage(Image))
            {
                G.Clear(BackColor);
            }
        }

        public void ClearData()
        {
            BitmapGraphics.Dispose();
            Bitmap.Dispose();
            Bitmap = null;
        }

        public void Clear()
        {
            BitmapGraphics.Dispose();
            Bitmap.Dispose();
            AxesBitmap.Dispose();
            Bitmap = null;
            AxesBitmap = null;
            YMax = InitialYMax;
            YMin = InitialYMin;
            ScaleHeight = InitialScaleHeight;
        }

        public void Draw()
        {
            DrawAxes(Bitmap);
            DrawPoints(Bitmap, new double[] { 100, 100, 300, 500, 700 }, new double[] { 0.1, 0.2, 0.4, 0.6, 0.8 });
        }

        public PointF ScreenToData(Point p)
        {
            return new PointF(
                XMin + (p.X - Axes.X)/Axes.Width * XMax,
                YMax - (p.Y - Axes.Y)/Axes.Height * ScaleHeight
                );
        }

        PointF CanvasToNormalized(Point p)
        {
            return new PointF(
                p.X / Axes.Width, p.Y / Axes.Height
                );
        }

        Point NormalizedToCanvas(PointF p)
        {
            return new Point(
                (int)(p.X * Axes.Width), (int)(p.Y * Axes.Height)
                );
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (Bitmap == null) InitBitmap();
            if (AxesBitmap == null) InitAxesBitmap();
            pe.Graphics.CompositingMode = CompositingMode.SourceOver;
            pe.Graphics.CompositingQuality = CompositingQuality.GammaCorrected;
            using (Graphics G = Graphics.FromImage(AxesBitmap))
            {
                G.DrawImage(Bitmap, new Point(0,0));
            }
            pe.Graphics.DrawImage(AxesBitmap, Bound);
        }

        
    }
}
