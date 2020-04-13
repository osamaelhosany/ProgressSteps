using System;
using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace XamProgressSteps
{
    public partial class XamProgressSteps
    {
        public static readonly BindableProperty StepCountProperty =
            BindableProperty.Create(nameof(StepCount), typeof(int), typeof(XamProgressSteps), 0, BindingMode.TwoWay, propertyChanged: OnStepCountChanged);
        public static readonly BindableProperty StepMaxProperty =
                  BindableProperty.Create(nameof(StepMax), typeof(int), typeof(XamProgressSteps), 0, BindingMode.TwoWay);
        public static readonly BindableProperty TextSizeProperty =
                  BindableProperty.Create(nameof(TextSize), typeof(float), typeof(XamProgressSteps), default, BindingMode.TwoWay);
        public static readonly BindableProperty StrokeWidthProperty =
          BindableProperty.Create(nameof(StrokeWidth), typeof(float), typeof(XamProgressSteps), default, BindingMode.TwoWay);
        public static readonly BindableProperty ColorActiveStepsProperty =
            BindableProperty.Create(nameof(ColorActiveSteps), typeof(Color), typeof(XamProgressSteps), Color.DarkBlue, BindingMode.TwoWay);
        public static readonly BindableProperty ColorInactiveStepsProperty =
                 BindableProperty.Create("ColorInactiveSteps", typeof(Color), typeof(XamProgressSteps), defaultValue: Color.LightGray, BindingMode.TwoWay);
        public static readonly BindableProperty TextColorProperty =
                 BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(XamProgressSteps), Color.DarkBlue, BindingMode.TwoWay);

        public int StepCount
        {
            get
            {
                return (int)GetValue(StepCountProperty);
            }
            set
            {
                SetValue(StepCountProperty, value);
            }
        }
        public int StepMax
        {
            get
            {
                return (int)GetValue(StepMaxProperty);
            }
            set
            {
                SetValue(StepMaxProperty, value);
            }
        }
        public float TextSize
        {
            get
            {
                return (float)GetValue(TextSizeProperty);
            }
            set
            {
                SetValue(TextSizeProperty, value);
            }
        }
        public float StrokeWidth
        {
            get
            {
                return (float)GetValue(StrokeWidthProperty);
            }
            set
            {
                SetValue(StrokeWidthProperty, value);
            }
        }
        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }
        public Color ColorActiveSteps
        {
            get
            {
                return (Color)GetValue(ColorActiveStepsProperty);
            }
            set
            {
                SetValue(ColorActiveStepsProperty, value);
            }
        }
        public Color ColorInactiveSteps
        {
            get
            {
                return (Color)GetValue(ColorInactiveStepsProperty);
            }
            set
            {
                SetValue(ColorInactiveStepsProperty, value);
            }
        }
        private SKCanvas canvas { get; set; }
        private float startAngle = -90;
        private static int newStep;
        private SKRect rect;

        public XamProgressSteps()
        {
            InitializeComponent();

        }

        private static void OnStepCountChanged(BindableObject bindable, object oldValue, object newValue)
        {
            newStep = (int)newValue;
            var control = (XamProgressSteps)bindable;
            if (newStep > control.StepMax || newStep < 0|| control.StepMax==0)
                return;
            control.canvasView.InvalidateSurface();
        }
        void OnCanvasViewPaintSurface(System.Object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;

            SKSurface surface = e.Surface;
            canvas = surface.Canvas;
            if (Device.RuntimePlatform == Device.Android)
            {
               // rect = new SKRect(180, 100, 380, 300);
              //  rect = new SKRect(Convert.ToInt32(canvasView.Width/2), Convert.ToInt32(canvasView.Height/2), Convert.ToInt32(canvasView.Width*2), Convert.ToInt32(canvasView.Height*2));
                rect.Size = new SKSize(150,150);
                rect.Location = new SKPoint(Convert.ToInt32((canvasView.Width/2)+25), Convert.ToInt32(canvasView.Height / 2));
            }
            else
            {
                rect = new SKRect(50, 50, 250, 250);
            }

            if (StepCount == 1)
            {
                startAngle = -90;
            }

            DrawProgressSteps(canvas);

            DrawSteps(canvas);

            canvas.DrawText(StepCount.ToString()+" / "+StepMax.ToString(), new SKPoint() { X = (info.Width /2)-TextSize, Y = (info.Height / 2)+10 }, new SKPaint
            {
                StrokeWidth = 10,
                Color = TextColor.ToSKColor(),
                TextSize = TextSize,
            });
            
            //var scale = Convert.ToInt32(info.Width / canvasView.Width);

            //canvas.Scale(scale);
        }

        void DrawProgressSteps(SKCanvas canvas)
        {
            canvas.Clear();

            startAngle = -90;
            float sweepAngle = (360/StepMax) - 5;
            for (int i = 0; i < StepMax; i++)
            {
                using (SKPath path = new SKPath())
                {
                    path.AddArc(rect, startAngle, sweepAngle);
                    canvas.DrawPath(path, new SKPaint
                    {
                        Style = SKPaintStyle.Stroke,
                        StrokeWidth = StrokeWidth,
                        Color = ColorInactiveSteps.ToSKColor() 
                    });
                    startAngle += sweepAngle + 5;
                }
            }
        }

        void DrawSteps(SKCanvas canvas)
        {
            startAngle = -90;
            float sweepAngle = (360 / StepMax) - 5;
            for (int i = 0; i < StepCount; i++)
            {
                using (SKPath path = new SKPath())
                {
                    path.AddArc(rect, startAngle, sweepAngle);
                    canvas.DrawPath(path, new SKPaint
                    {
                        Style = SKPaintStyle.Stroke,
                        StrokeWidth = StrokeWidth,
                        Color = ColorActiveSteps.ToSKColor()
                    });
                    startAngle += sweepAngle + 5;
                }
            }
        }
    }
}