
using Xamarin.Forms;

using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.ComponentModel;

namespace ProgressSteps
{
    [DesignTimeVisible(false)]

    public partial class AngleArcPage : ContentPage , INotifyPropertyChanged
    {
       
        public int stepcount { get; set; }

        public AngleArcPage()
        {
            InitializeComponent();
        }

        void NextButton_Clicked(System.Object sender, System.EventArgs e)
        {
            stepcount++;
        }

        void BackButton_Clicked(System.Object sender, System.EventArgs e)
        {
            stepcount--;    
        }
    }
}