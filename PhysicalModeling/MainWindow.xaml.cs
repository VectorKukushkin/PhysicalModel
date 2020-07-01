using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PhysicalModeling
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool type = true;
        private double m1 = 10;
        private double m2 = 10;
        private double t = 0;
        private double t1 = 0;
        private double t2 = 0;
        private double speed1 = 20;
        private double speed2 = 0;
        private double x1 = 0;
        private double x2 = 40000;
        private const double friction = 0.005;
        DispatcherTimer timer = new DispatcherTimer
        {
            Interval = new TimeSpan(10)
        };

        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += new System.EventHandler(Timer1_Tick);
            box1.Margin = new Thickness(30 + x1 / 1000 * 7.8, 140, 0, 0);
            box2.Margin = new Thickness(100 + x2 / 1000 * 7.8, 140, 0, 0);
            watch1.Content = t1;
            watch2.Content = t2;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            x1 = 0;
            x2 = 40000;
            t = 0;
            t1 = 0;
            t2 = 0;
            speed1 = 30;
            speed2 = 0;
            timer.Start();
        }

        public void Timer1_Tick(object sender, EventArgs e)
        {
            /*if (x1 < x2 && speed1 > 0)
            {
                speed1 -= friction;
                x1 += speed1;
                if (x1 > x2)
                {
                    x1 = x2;
                }
            }
            else*/
            t++;
            if (x1 == x2)
            {
                if (type)
                {
                    x1--;
                    double speed0 = speed1;
                    speed1 = (m1 - m2) * speed0 / (m1 + m2);
                    speed2 = 2 * m1 * speed0 / (m1 + m2);
                }
                else
                {
                    x1++;
                    speed1 = m1 * speed1 / (m1 + m2);
                }
            }
            if (x2 < x1)
            {
                if (speed1 > 0)
                {
                    speed1 -= friction;
                    x1 += speed1;
                    x2 += speed1;
                }
                else
                {
                    speed1 = 0;
                }
            }
            else
            {
                if (Math.Abs(speed1) > 0)
                {
                    if (Math.Abs(speed1) > friction)
                    {
                        speed1 -= Math.Sign(speed1) * friction;
                    }
                    else
                    {
                        speed1 = 0;
                    }
                    x1 += speed1;
                    if (x1 > x2)
                    {
                        x1 = x2;
                    }
                }
                if (speed2 > 0)
                {
                    speed2 -= friction;
                    x2 += speed2;
                }
            }
            if (x1 > 25641 && t1 == 0)
            {
                t1 = t;
            }
            if (x2 > 54230 && t2 == 0)
            {
                t2 = t;
            }
            box1.Margin = new Thickness(30 + x1 / 1000 * 7.8, 140, 0, 0);
            box2.Margin = new Thickness(100 + x2 / 1000 * 7.8, 140, 0, 0);
            watch1.Content = t1;
            watch2.Content = t2;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            type = true;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            type = false;
        }

        private void C1_TextChanged(object sender, TextChangedEventArgs e)
        {
            m1 = Convert.ToInt32("0" + c1.Text);
        }

        private void C2_TextChanged(object sender, TextChangedEventArgs e)
        {
            m2 = Convert.ToInt32("0" + c2.Text);
        }
    }
}
