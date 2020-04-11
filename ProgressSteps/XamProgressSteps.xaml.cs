using System;
using System.Collections.Generic;
using SkiaSharp;
using Xamarin.Forms;

namespace ProgressSteps
{
    public partial class XamProgressSteps : ContentView
    {
        public static readonly BindableProperty StepCountProperty =
            BindableProperty.Create(nameof(StepCount), typeof(int), typeof(XamProgressSteps), 0, BindingMode.TwoWay,propertyChanged: OnStepCountChanged);

        public static readonly BindableProperty StepMaxProperty =
                  BindableProperty.Create(nameof(StepMax), typeof(int), typeof(XamProgressSteps), 0, BindingMode.TwoWay);
        public static readonly BindableProperty TextSizeProperty =
                  BindableProperty.Create(nameof(TextSize), typeof(float), typeof(XamProgressSteps),default,BindingMode.TwoWay);
        public static readonly BindableProperty ColorActiveStepsProperty =
            BindableProperty.Create(nameof(ColorActiveSteps), typeof(SKColor), typeof(XamProgressSteps),SKColors.DarkBlue, BindingMode.TwoWay);
        public static readonly BindableProperty ColorInactiveStepsProperty =
                 BindableProperty.Create(nameof(ColorInactiveSteps), typeof(SKColor), typeof(XamProgressSteps), SKColors.LightGray, BindingMode.TwoWay);
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

        public SKColor ColorActiveSteps
        {
            get
            {
                return (SKColor)GetValue(ColorActiveStepsProperty);
            }
            set
            {
                SetValue(ColorActiveStepsProperty, value);
            }
        }
        public SKColor ColorInactiveSteps
        {
            get
            {
                return (SKColor)GetValue(ColorInactiveStepsProperty);
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
            if (newStep > control.StepMax || newStep < 0)
                return;
            control.canvasView.InvalidateSurface();
        }
        void OnCanvasViewPaintSurface(System.Object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;

            SKSurface surface = e.Surface;
            canvas = surface.Canvas;
            var x = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Height;
            rect = new SKRect(100, 100, 300, 300);

            if (StepCount == 1)
            {
                startAngle = -90;
            }

            DrawProgressSteps(canvas);

            DrawSteps(canvas);
            var midy = rect.MidY;
            var y = (info.Height / 2)- midy;
            var newY = y > 0 ? (info.Height / 2) + y : (info.Height / 2) - y;
            canvas.DrawText(StepCount.ToString()+" / "+StepMax.ToString(), new SKPoint() { X = (info.Width /2)-TextSize, Y = (info.Height / 2)+10 }, new SKPaint
            {
                StrokeWidth = 10,
                Color = SKColors.Red,
                TextSize = TextSize,
            });
         //   var scale = e.Info.Width / canvasView.Width;
           // canvas.Scale(Convert.ToInt32(scale));
           // var scale = Convert.ToInt32(info.Width / canvasView.Width);

          //  canvas.Scale(scale);
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
                        StrokeWidth = 20,
                        Color =ColorInactiveSteps 
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
                        StrokeWidth = 20,
                        Color = ColorActiveSteps
                    });
                    startAngle += sweepAngle + 5;
                }
            }
        }
    }
}