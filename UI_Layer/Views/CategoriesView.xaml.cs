using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace UI_Layer.Views
{
    /// <summary>
    /// Interaction logic for CategoriesView.xaml
    /// </summary>
    public partial class CategoriesView : UserControl
    {
        public CategoriesView()
        {
            InitializeComponent();
        }       
        private void SideBarInfo_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((sender as Grid).DataContext is null)
            {
                Debug.WriteLine("SidebarInfo DataContext is null");
                (sender as Grid).BeginAnimation(OpacityProperty, null);
                //(sender as Grid).Opacity = 0;
            }

            else
            {
                Storyboard storyboardVanish = new Storyboard();

                DoubleAnimation doubleAnimation = new DoubleAnimation(1, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
                DoubleAnimation doubleAnimation2 = new DoubleAnimation(0, new Duration(new TimeSpan(0, 0, 0, 0, 0)));
                doubleAnimation.AccelerationRatio = 0.7;
                storyboardVanish.Children.Add(doubleAnimation2);
                storyboardVanish.Children.Add(doubleAnimation);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
                Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("Opacity"));
                (sender as Grid).BeginStoryboard(storyboardVanish);
                (((sender as Grid).Children[2]) as Grid).DataContextChanged += Grid_DataContextChanged;
                //Grid_DataContextChanged(((sender as Grid).Children[2]), e);
            }
        }

        private void TextBlock_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
                Storyboard storyboardVanish = new Storyboard();

                DoubleAnimation doubleAnimation = new DoubleAnimation(1, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
                DoubleAnimation doubleAnimation2 = new DoubleAnimation(0, new Duration(new TimeSpan(0, 0, 0, 0, 0)));
                doubleAnimation.AccelerationRatio = 0.7;
                doubleAnimation.BeginTime = TimeSpan.FromMilliseconds(500);
                storyboardVanish.Children.Add(doubleAnimation2);
                storyboardVanish.Children.Add(doubleAnimation);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
                Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("Opacity"));
                (sender as TextBlock).BeginStoryboard(storyboardVanish);            
        }

        private void Grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Storyboard storyboardVanish = new Storyboard();

            DoubleAnimation doubleAnimation = new DoubleAnimation(1, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
            DoubleAnimation doubleAnimation2 = new DoubleAnimation(0, new Duration(new TimeSpan(0, 0, 0, 0, 0)));
            doubleAnimation.AccelerationRatio = 0.7;
            doubleAnimation.BeginTime = TimeSpan.FromMilliseconds(1000);
            storyboardVanish.Children.Add(doubleAnimation2);
            storyboardVanish.Children.Add(doubleAnimation);
            storyboardVanish.FillBehavior = FillBehavior.HoldEnd;
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
            Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("Opacity"));
            (sender as Grid).BeginStoryboard(storyboardVanish);

        }
        private ToggleButton previousButton = new();
        private void CategorisButton_Click(object sender, RoutedEventArgs e)
        {
            if ((ToggleButton)sender != previousButton)
            {
                previousButton.IsChecked = false;
                previousButton = sender as ToggleButton; 
            }
            else
            {
                ((ToggleButton)sender).IsChecked = true;
            }
        }
    }
}
