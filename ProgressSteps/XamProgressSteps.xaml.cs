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

        private bool IsDrawed { get; set; }
        private SKCanvas canvas { get; set; }
        private float startAngle1 = -90;
        private static int oldStep, newStep;
        private SKRect rect;
        public XamProgressSteps()
        {
            InitializeComponent();
            
        }

        private static void OnStepCountChanged(BindableObject bindable, object oldValue, object newValue)
        {
            oldStep = (int)oldValue;
            newStep = (int)newValue;
            var control = (XamProgressSteps)bindable;
            if (newStep > control.StepMax || newStep < 0)
                return;
            control.canvasView.InvalidateSurface();
        }
        void OnCanvasViewPaintSurface(System.Object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            canvas = surface.Canvas;
            rect = new SKRect(75, 75, Convert.ToInt32(this.WidthRequest*2), Convert.ToInt32(this.HeightRequest*2));

            //if (!IsDrawed)
            //{
            //    DrawProgressSteps(canvas);
            //}
            //else
            if (oldStep > newStep)
            {
                DrawProgressSteps(canvas);
                DrawSteps(canvas);
            }
            else if (StepCount <= StepMax)
            { 
                if (StepCount == 1)
                {
                    startAngle1 = -90;
                }
                DrawProgressSteps(canvas);
                DrawSteps(canvas);
            }
            canvas.DrawText(StepCount.ToString()+" / "+StepMax.ToString(), new SKPoint() { X = 200, Y = 200 }, new SKPaint
            {
                StrokeWidth = 10,
                Color = SKColors.Red,
                TextSize = 60
            });
        }

        void DrawProgressSteps(SKCanvas canvas)
        {
            canvas.Clear();

            float startAngle = -90;
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
            IsDrawed = true;
        }

        void DrawNextButton(SKCanvas canvas)
        {
            float sweepAngle = (360 / StepMax) - 5;

            using (SKPath path = new SKPath())
            {
                path.AddArc(rect, startAngle1, sweepAngle);
                canvas.DrawPath(path, new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 20,
                    Color = ColorActiveSteps
                });
                startAngle1 += sweepAngle + 5;
            }
        }

        void DrawSteps(SKCanvas canvas)
        {
            float startAngle = -90;
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
            startAngle1 = startAngle;
        }
    }
}