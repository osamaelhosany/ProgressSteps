
using Xamarin.Forms;

using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.ComponentModel;

namespace ProgressSteps
{
    [DesignTimeVisible(false)]

    public partial class AngleArcPage : ContentPage , INotifyPropertyChanged
    {
        SKPaint outlinePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 3,
            Color = SKColors.Black
        };

        SKPaint arcPaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 20,
            Color = SKColors.Blue
        };
        bool IsDrawed { get; set; }
        bool frombackbtn { get; set; }
        public int state { get; set; }
        SKCanvas canvas { get; set; }
        float startAngle1 = -90;

        public AngleArcPage()
        {
            InitializeComponent();
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            canvas = surface.Canvas;
            if (!IsDrawed)
            {
                DrawProgressSteps(canvas);
            }
            else if (frombackbtn)
            {
                frombackbtn = false;
                if (state != 0)
                {
                    DrawProgressSteps(canvas);
                    DrawToPrevious(canvas);
                    state--;
                }
            }
            else
            {
                if (state < 5)
                {
                    if (state ==0)
                    {
                        startAngle1 = -90;
                    }
                    DrawNextButton(canvas);
                    state++;
                }
            }
        }

        void DrawProgressSteps(SKCanvas canvas)
        {
            canvas.Clear();

            SKRect rect = new SKRect(300, 300, 500, 500);
            float startAngle = -90;
            float sweepAngle = 67;
            for (int i = 0; i < 5; i++)
            {
                using (SKPath path = new SKPath())
                {
                    path.AddArc(rect, startAngle, sweepAngle);
                    canvas.DrawPath(path, new SKPaint
                    {
                        Style = SKPaintStyle.Stroke,
                        StrokeWidth = 20,
                        Color = SKColors.LightGray
                    });
                    startAngle += sweepAngle + 5;
                }
            }
            IsDrawed = true;
        }

        void DrawNextButton(SKCanvas canvas)
        {
            SKRect rect = new SKRect(300, 300, 500, 500);
            float sweepAngle = 67;

            using (SKPath path = new SKPath())
            {
                path.AddArc(rect, startAngle1, sweepAngle);
                canvas.DrawPath(path, new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 20,
                    Color = SKColors.DarkBlue
                });
                startAngle1 += sweepAngle + 5;
            }
        }

        void DrawToPrevious(SKCanvas canvas)
        {
            SKRect rect = new SKRect(300, 300, 500, 500);
            float startAngle = -90;
            float sweepAngle = 67;
            for (int i = 0; i < state-1; i++)
            {
                using (SKPath path = new SKPath())
                {
                    path.AddArc(rect, startAngle, sweepAngle);
                    canvas.DrawPath(path, new SKPaint
                    {
                        Style = SKPaintStyle.Stroke,
                        StrokeWidth = 20,
                        Color = SKColors.DarkBlue
                    });
                    startAngle += sweepAngle + 5;
                }
            }
        }

        void NextButton_Clicked(System.Object sender, System.EventArgs e)
        {
            canvasView.InvalidateSurface();
        }

        void BackButton_Clicked(System.Object sender, System.EventArgs e)
        {
            frombackbtn = true;
            canvasView.InvalidateSurface();
        }
    }
}