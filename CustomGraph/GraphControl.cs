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
        Action<float, float> RescaleHandler;

        PointF Origin;
        PointF XAxisRight;
        PointF YAxisTop;

        Bitmap CanvasBitmap;
        Bitmap DataBitmap;
        Graphics BitmapGraphics;
        Bitmap AxesBitmap;
        Bitmap AnnotationBitmap;
        Region _Region;
        Rectangle Bound;

        RectangleF Axes;
        RectangleF AxesPadding;
        RectangleF Canvas;

        public float XLeft { get; set; }
        public float InitialXLeft { get; set; }
        const float XLEFTDEFAULT = 0f;

        public float XRight { get; set; }
        public float InitialXRight { get; set; }
        const float XRIGHTDEFAULT = 1023f;

        public float XMin
        {
            get
            {
                return Math.Min(XLeft, XRight);
            }
        }

        public float XMax
        {
            get
            {
                return Math.Max(XLeft, XRight);
            }
        }

        public float XRange
        {
            get
            {
                return Math.Abs(XRight - XLeft);
            }
        }

        public float YMin { get; set; }
        public float InitialYMin { get; set; }
        const float YMINDEFAULT = float.NegativeInfinity;

        public float YMax { get; set; }
        public float InitialYMax { get; set; }
        const float YMAXDEFAULT = float.PositiveInfinity;

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
        const string MARKERDEFAULT = "*";
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

        //IEnumerator<Color> COLORORDERDEFAULT = (new LinkedList<Color>(new List<Color>() { Color.Blue, Color.Green, Color.Red, Color.Magenta })).GetEnumerator();
        List<Color> COLORORDERDEFAULT = new List<Color>() { Color.Blue, Color.Green, Color.Red, Color.Magenta };
        private List<Color> _ColorOrder;
        public List<Color> ColorOrder
        {
            get
            {
                return _ColorOrder;
            }
            set
            {
                _ColorOrder = value;
            }
        }

        private int ColorIndex = 0;
        public Color NextColor
        {
            get
            {
                ColorIndex %= ColorOrder.Count;
                return ColorOrder[ColorIndex++];
            }
        }

        public enum Annotation
        {
            VERTLINE, HORZLINE
        }

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

            ColorOrder = COLORORDERDEFAULT;

            XAxisHeight = XAXISHEIGHTDEFAULT;
            YAxisWidth = YAXISWIDTHDEFAULT;
            XRight = InitialXRight = XRIGHTDEFAULT;
            YMax = InitialYMax = YMAXDEFAULT;

            XLeft = InitialXLeft = XLEFTDEFAULT;
            YMin = InitialYMin = YMINDEFAULT;

            ScaleHeight = InitialScaleHeight = SCALEHEIGHTDEFAULT;

            PadLeft = PADLEFTDEFAULT;
            PadTop = PADTOPDEFAULT;
            PadRight = PADRIGHTDEFAULT;
            PadBottom = PADBOTTOMDEFAULT;

            NYTicks = NYTICKSDEFAULT;
            NXTicks = NXTICKSDEFAULT;
            XLabelFormat = "f0";
            YLabelFormat = "n3";

            Load += new EventHandler(HandleLoad);
        }

        void HandleLoad(object sender, EventArgs e)
        {
            if (CanvasBitmap == null) InitCanvasBitmap();
            if (DataBitmap == null) InitDataBitmap();
            if (AxesBitmap == null) InitAxesBitmap();
            if (AnnotationBitmap == null) InitAnnotationBitmap();
        }

        void InitCanvasBitmap()
        {
            // Bound and region of the control.
            Bound = new Rectangle(new Point(0, 0), Size);
            _Region = new Region(Bound);
            CanvasBitmap = new Bitmap(Bound.Width, Bound.Height);
            Clear(CanvasBitmap);
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
                AxesPadding.Left + AxesPadding.Width * PadLeft,
                AxesPadding.Top + AxesPadding.Height * PadTop,
                AxesPadding.Width - AxesPadding.Width * PadRight,
                AxesPadding.Height - AxesPadding.Height * PadBottom);
        }

        void InitDataBitmap()
        {
            DataBitmap = new Bitmap(Bound.Width, Bound.Height);
            Clear(DataBitmap);
            DataBitmap.MakeTransparent(BackColor);
            BitmapGraphics = Graphics.FromImage(DataBitmap);
        }

        void InitAxesBitmap()
        {
            AxesBitmap = new Bitmap(Bound.Width, Bound.Height);
            Clear(AxesBitmap);
            DrawAxes(AxesBitmap);
            if (YMax != float.NegativeInfinity) DrawYLabels(AxesBitmap);
            if (XRight != float.NegativeInfinity) DrawXLabels(AxesBitmap);
            AxesBitmap.MakeTransparent(BackColor);
        }

        void InitAnnotationBitmap()
        {
            AnnotationBitmap = new Bitmap(Bound.Width, Bound.Height);
            Clear(AnnotationBitmap);
            AnnotationBitmap.MakeTransparent(BackColor);
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
                        G.DrawImage(DataBitmap, NewAxes, Axes, GraphicsUnit.Pixel);
                    }
                    DataBitmap.Dispose();
                    DataBitmap = ScaledBitmap;
                    DataBitmap.MakeTransparent(BackColor);


                    BitmapGraphics.Dispose();
                    BitmapGraphics = Graphics.FromImage(DataBitmap);
                    //BitmapGraphics.CompositingMode = CompositingMode.SourceOver;
                }
                InvalidateAxes();
                InvalidateCanvas();
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

        public void DrawPoints(int[] Y)
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
                    float x = Math.Abs((float)X[i] - XLeft) / XRange;
                    float y = (YMax - (float)Y[i]) / ScaleHeight;
                    //float y = Math.Abs(YMax - (float)Y[i]) / ScaleHeight;
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
                        float x = Math.Abs((float)X[i] - XLeft) / XRange;
                        float y = (float)Y[i] / YMax;
                        DrawPoint(G, B, MarkerFont, Axes, x, y);
                    }
                }
            }
        }

        /// <summary>
        /// Draw marker at point in normalized coordinates.
        /// </summary>
        /// <param name="G"></param>
        /// <param name="B"></param>
        /// <param name="F"></param>
        /// <param name="Axes"></param>
        /// <param name="x">Normalized X-coordinate</param>
        /// <param name="y">Normalized Y-coordinate</param>
        void DrawPoint(Graphics G, Brush B, Font F, RectangleF Axes, float x, float y)
        {
            float X = Axes.Left + Axes.Width * x - MarkerOffset.Width;
            float Y = Axes.Top + Axes.Height * y - MarkerOffset.Height;
            PointF Point = new PointF(X, Y);
            G.DrawString(Marker, F, B, Point);
            //TextRenderer.DrawText(G, Marker, MarkerFont, new Point((int)X, (int)Y), MarkerColor);
        }

        public void Annotate(Annotation A, params object[] args)
        {
            switch (A)
            {
                case Annotation.VERTLINE:
                    DrawVerticalLine(AnnotationBitmap, (Color)args[0], Convert.ToSingle(args[1]));
                    break;
                case Annotation.HORZLINE:
                    DrawHorizontalLine(AnnotationBitmap, (Color)args[0], Convert.ToSingle(args[1]));
                    break;
            }
        }

        void DrawVerticalLine(Image Image, Color C, double x)
        {
            DrawVerticalLine(Image, C, (float)x);
        }

        /// <summary>
        /// Draw vertical line at specified data coordinate.
        /// </summary>
        /// <param name="Image"></param>
        /// <param name="C"></param>
        /// <param name="x"></param>
        void DrawVerticalLine(Image Image, Color C, float x)
        {
            using (Graphics G = Graphics.FromImage(Image))
            {
                float X = Axes.Left + Axes.Width * Math.Abs(x - XLeft) / XRange;
                //G.CompositingMode = CompositingMode.SourceOver;
                using (Pen Pen = new Pen(C))
                {
                    G.DrawLine(Pen, X, Axes.Bottom, X, Axes.Top);
                }
            }
        }

        void DrawHorizontalLine(Image Image, Color C, double y)
        {
            DrawHorizontalLine(Image, C, (float)y);
        }

        /// <summary>
        /// Draw horizontal line at specified normalized data coordinate.
        /// </summary>
        /// <param name="Image"></param>
        /// <param name="C"></param>
        /// <param name="y"></param>
        void DrawHorizontalLine(Image Image, Color C, float y)
        {
            using (Graphics G = Graphics.FromImage(Image))
            {
                float Y = Axes.Top + Axes.Height * y;
                //G.CompositingMode = CompositingMode.SourceOver;
                using (Pen Pen = new Pen(C))
                {
                    G.DrawLine(Pen, Axes.Left, Y, Axes.Right, Y);
                }
            }
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
                    G.DrawLine(AxisPen, Origin.X, Origin.Y, YAxisTop.X, YAxisTop.Y); // Draw left Y-Axis
                    G.DrawLine(AxisPen, XAxisRight.X, XAxisRight.Y, XAxisRight.X, YAxisTop.Y); // Draw right Y-Axis
                    // Draw X-Axis, then horizontal grid lines
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
                        float YVal = YMax - i / NYTicks * ScaleHeight;
                        string TickLabel = YVal.ToString(YLabelFormat);
                        SizeF TickLabelOffset = G.MeasureString(TickLabel, AxesFont);
                        float X = AxesPadding.Left - TickLabelOffset.Width;
                        float Y = Axes.Top + Axes.Height * i / NYTicks - TickLabelOffset.Height / 2;
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
                        int XRangeOrder = (int)(Math.Log10(XRange)); // Rounds down
                        double XRangeRound = Math.Round(XRange / Math.Pow(10, XRangeOrder)) * Math.Pow(10, XRangeOrder);
                        float XVal = i / NXTicks * (float)XRangeRound;
                        string TickLabel = (XLeft < XRight ? XLeft + XVal : XLeft - XVal).ToString(XLabelFormat);
                        SizeF TickLabelOffset = G.MeasureString(TickLabel, AxesFont);
                        float X = Axes.Left + Axes.Width * XVal/XRange - TickLabelOffset.Width / 2;
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

        void InvalidateCanvas()
        {
            // The null check permits redundant calls, e.g. when rapidly changing selection.
            if (CanvasBitmap != null) CanvasBitmap.Dispose();
            CanvasBitmap = null;
        }

        void InvalidateData()
        {
            if (BitmapGraphics != null) BitmapGraphics.Dispose();
            if (DataBitmap != null) DataBitmap.Dispose();
            DataBitmap = null;
        }

        void InvalidateAxes()
        {
            if (AxesBitmap != null) AxesBitmap.Dispose();
            AxesBitmap = null;
        }

        void InvalidateAnnotation()
        {
            if (AnnotationBitmap != null) AnnotationBitmap.Dispose();
            AnnotationBitmap = null;
        }

        public void Clear()
        {
            InvalidateCanvas();
            InvalidateAxes();
            InvalidateData();
            InvalidateAnnotation();
            YMax = InitialYMax;
            YMin = InitialYMin;
            ScaleHeight = InitialScaleHeight;
        }

        public void ClearData()
        {
            InvalidateData();
            InitDataBitmap();
            InvalidateCanvas();
        }

        public void ClearAnnotation()
        {
            InvalidateAnnotation();
            InitAnnotationBitmap();
            InvalidateCanvas();
        }

        public void ClearAxes()
        {
            InvalidateAxes();
            InitAxesBitmap();
            InvalidateCanvas();
        }

        public void Draw()
        {
            DrawAxes(DataBitmap);
            DrawPoints(DataBitmap, new double[] { 100, 100, 300, 500, 700 }, new double[] { 0.1, 0.2, 0.4, 0.6, 0.8 });
        }

        public PointF ScreenToData(Point p)
        {
            return new PointF(
                XLeft + (p.X - Axes.X) / (Axes.Width - 1) * XRange,
                YMax - (p.Y - Axes.Y) / (Axes.Height - 1) * ScaleHeight
                );
        }

        public Point ScreenToAxes(Point p)
        {
            return new Point(
                p.X - (int)Axes.X,
                p.Y - (int)Axes.Y
                );
        }

        public PointF AxesToNormalized(Point p)
        {
            return new PointF(
                p.X / (Axes.Width - 1), p.Y / (Axes.Height - 1)
                );
        }

        public Point AxesToCanvas(PointF p)
        {
            return new Point(
                (int)(p.X * (Axes.Width - 1)), (int)(p.Y * (Axes.Height - 1))
                );
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (CanvasBitmap == null) InitCanvasBitmap();
            if (DataBitmap == null) InitDataBitmap();
            if (AxesBitmap == null) InitAxesBitmap();
            if (AnnotationBitmap == null) InitAnnotationBitmap();
            pe.Graphics.CompositingMode = CompositingMode.SourceOver;
            pe.Graphics.CompositingQuality = CompositingQuality.GammaCorrected;
            using (Graphics G = Graphics.FromImage(CanvasBitmap))
            {
                G.DrawImage(AxesBitmap, new Point(0, 0));
                G.DrawImage(DataBitmap, new Point(0, 0));
                G.DrawImage(AnnotationBitmap, new Point(0, 0));
            }
            pe.Graphics.DrawImage(CanvasBitmap, Bound);
        }


    }
}
