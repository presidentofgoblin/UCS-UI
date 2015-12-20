﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ultrapowa_Clash_Server_GUI
{
    /// <summary>
    /// Logica di interazione per PopupUpdater.xaml
    /// </summary>
    public partial class PopupUpdater : Window
    {
        private bool IsGoingPage = false;

        public PopupUpdater()
        {
            Opacity = 0;
            InitializeComponent();
            RTB_Console.Document.Blocks.Clear();
            RTB_Console.AppendText(Sys.ConfUCS.Changelog);
            Version thisAppVer = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            lbl_CurVer.Content = "Current UCS version: " + thisAppVer.Major + "." + thisAppVer.Minor + "." + thisAppVer.Build + "." + thisAppVer.MinorRevision;
            lbl_NewVer.Content = "New UCS version: " + Sys.ConfUCS.NewVer.Major + "." + Sys.ConfUCS.NewVer.Minor + "." + Sys.ConfUCS.NewVer.Build + "." + Sys.ConfUCS.NewVer.MinorRevision;

        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_GoPage_Click(object sender, RoutedEventArgs e)
        {
            IsGoingPage = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OpInW();
           
            int DeltaVariation = 100;
            AnimationLib.MoveToTarget(btn_Cancel, DeltaVariation, 0.25);
            AnimationLib.MoveToTarget(btn_GoPage, DeltaVariation, 0.25, 50);
            AnimationLib.MoveToTarget(RTB_Console, DeltaVariation, 0.25, 100);
            AnimationLib.MoveToTarget(lbl_Changelog, DeltaVariation, 0.25, 150);
            AnimationLib.MoveToTarget(lbl_CurVer, DeltaVariation, 0.25, 200);
            AnimationLib.MoveToTarget(lbl_NewVer, DeltaVariation, 0.25, 250);
            AnimationLib.MoveToTarget(lbl_Title, DeltaVariation, 0.25, 300);

            AnimationLib.MoveWindowToTarget(this, DeltaVariation, Top, 0.25);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OpOutW(sender, e);
        }

        private void OpInW()
        {
            var OpIn = new DoubleAnimation(1, TimeSpan.FromSeconds(0.5));
            BeginAnimation(OpacityProperty, OpIn);

        }

        private void OpOutW(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Closing -= Window_Closing;
            e.Cancel = true;
            var OpOut = new DoubleAnimation(0, TimeSpan.FromSeconds(0.125));
            OpOut.Completed += (s, _) => { this.Close(); MainWindow.IsFocusOk = true; if (IsGoingPage) System.Diagnostics.Process.Start(Sys.ConfUCS.UrlPage);  IsGoingPage = false; };
            BeginAnimation(OpacityProperty, OpOut);
        }


        

    }
}
