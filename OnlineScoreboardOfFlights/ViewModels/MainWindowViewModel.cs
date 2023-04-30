using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;
using OnlineScoreboardOfFlights.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive;
using Avalonia.Controls;
using OnlineScoreboardOfFlights.Views;
using Avalonia.Media;
using System.Drawing.Drawing2D;
using System.Linq;

namespace OnlineScoreboardOfFlights.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private DataBaseLoader dataBase;
        private ViewModelBase content;
        public ObservableCollection<ITableItem> currentCollection = new ObservableCollection<ITableItem>();
        private string currentHeaderText;
        public MainWindow mainWindow;
        public DateTime datt = DateTime.Now;
        public Button dep_button, arr_button, today_button, yesterday_button, tomorrow_button;
        public MainWindowViewModel() { }
        public MainWindowViewModel(Window main_win) {
            mainWindow = (MainWindow)main_win;
            dataBase = new DataBaseLoader();
            dataBase.Load();
            Content = new DepartureTableViewModel();

            dep_button = mainWindow.Find<Button>("DepButton");
            arr_button = mainWindow.Find<Button>("ArrButton");
            today_button = mainWindow.Find<Button>("Today");
            yesterday_button = mainWindow.Find<Button>("Yesterday");
            tomorrow_button = mainWindow.Find<Button>("Tomorrow");

            ChangeArrDepButtonColor(dep_button, arr_button);
            CurrentHeaderText = "Назначение";
            SetCurrentCollection(DepartureTab, "today");
            ChangeDaysButtonColor(today_button, yesterday_button, tomorrow_button);

            OpenArrivalTableCommand = ReactiveCommand.Create(() =>
            {
                ArrivalTableViewModel viewModel = new ArrivalTableViewModel();
                Content = viewModel;
                ChangeArrDepButtonColor(arr_button, dep_button);
                SetCurrentCollection(ArrivalTab, "today");
                CurrentHeaderText = "Пункт вылета";
                ChangeDaysButtonColor(today_button, yesterday_button, tomorrow_button);
            });

            OpenDepartureTableCommand = ReactiveCommand.Create(() =>
            {
                DepartureTableViewModel viewModel = new DepartureTableViewModel();
                Content = viewModel;
                ChangeArrDepButtonColor(dep_button, arr_button);
                SetCurrentCollection(DepartureTab, "today");
                CurrentHeaderText = "Назначение";
                ChangeDaysButtonColor(today_button, yesterday_button, tomorrow_button);
            });

            TodayButtonCommand = ReactiveCommand.Create(() =>
            {
                ChangeDaysButtonColor(today_button, yesterday_button, tomorrow_button);
                if(Content is ArrivalTableViewModel)
                {
                    SetCurrentCollection(ArrivalTab, "today");
                }
                if (Content is DepartureTableViewModel)
                {
                    SetCurrentCollection(DepartureTab, "today");
                }
            });

            TomorrowButtonCommand = ReactiveCommand.Create(() =>
            {
                ChangeDaysButtonColor(tomorrow_button, today_button, yesterday_button);
                if (Content is ArrivalTableViewModel)
                {
                    SetCurrentCollection(ArrivalTab, "tomorrow");
                }
                if (Content is DepartureTableViewModel)
                {
                    SetCurrentCollection(DepartureTab, "tomorrow");
                }
            });

            YesterdayButtonCommand = ReactiveCommand.Create(() =>
            {
                ChangeDaysButtonColor(yesterday_button, today_button, tomorrow_button);
                if (Content is ArrivalTableViewModel)
                {
                    SetCurrentCollection(ArrivalTab, "yesterday");
                }
                if (Content is DepartureTableViewModel)
                {
                    SetCurrentCollection(DepartureTab, "yesterday");
                }
            });
        }
        void SetCurrentCollection(ObservableCollection<ArrivalTableItem> collection, string day)
        {
            CurrentCollection.Clear();
            DateTime now_time = DateTime.Now;
            switch (day)
            {
                case "today":
                    foreach (ArrivalTableItem item in collection)
                    {
                        if (item.EstimatedTime.Date.CompareTo(DateTime.Now.Date) == 0)
                        {
                            CurrentCollection.Add(item);
                        }
                    }
                    CurrentCollection = new ObservableCollection<ITableItem>(currentCollection.OrderBy(p => p.ScheduledTime));
                    break;

                case "tomorrow":
                    foreach (ArrivalTableItem item in collection)
                    {
                        if (item.EstimatedTime.Date.CompareTo(DateTime.Now.Date) > 0)
                        {
                            CurrentCollection.Add(item);
                        }
                    }
                    CurrentCollection = new ObservableCollection<ITableItem> (currentCollection.OrderBy(p => p.ScheduledTime));
                    break;

                case "yesterday":
                    foreach (ArrivalTableItem item in collection)
                    {
                        if (item.EstimatedTime.Date.CompareTo(DateTime.Now.Date) < 0)
                        {
                            CurrentCollection.Add(item);
                        }
                    }
                    CurrentCollection = new ObservableCollection<ITableItem>(currentCollection.OrderBy(p => p.ScheduledTime));
                    break;
            }
        }
        void SetCurrentCollection(ObservableCollection<DepartureTableItem> collection, string day)
        {
            CurrentCollection.Clear();
            DateTime now_time = DateTime.Now;
            switch (day)
            {
                case "today":
                    foreach (DepartureTableItem item in collection)
                    {
                        if (item.EstimatedTime.Date.CompareTo(DateTime.Now.Date) == 0)
                        {
                            CurrentCollection.Add(item);
                        }
                    }
                    CurrentCollection = new ObservableCollection<ITableItem>(currentCollection.OrderBy(p => p.ScheduledTime));
                    break;

                case "tomorrow":
                    foreach (DepartureTableItem item in collection)
                    {
                        if (item.EstimatedTime.Date.CompareTo(DateTime.Now.Date) > 0)
                        {
                            CurrentCollection.Add(item);
                        }
                    }
                    CurrentCollection = new ObservableCollection<ITableItem>(currentCollection.OrderBy(p => p.ScheduledTime));
                    break;

                case "yesterday":
                    foreach (DepartureTableItem item in collection)
                    {
                        if (item.EstimatedTime.Date.CompareTo(DateTime.Now.Date) < 0)
                        {
                            CurrentCollection.Add(item);
                        }
                    }
                    CurrentCollection = new ObservableCollection<ITableItem>(currentCollection.OrderBy(p => p.ScheduledTime));
                    break;
            }
            //if(day == "today")
            //{
            //    foreach (DepartureTableItem item in collection)
            //    {
            //        if (item.EstimatedTime.Day == now_time.Day && item.EstimatedTime.Month == now_time.Month)
            //        {
            //            CurrentCollection.Add(item);
            //        }
            //    }
            //    CurrentCollection = new ObservableCollection<ITableItem>(currentCollection.OrderBy(p => p.ScheduledTime));
            //}
            //else
            //{
            //    if (day == "tomorrow")
            //    {
            //        foreach (DepartureTableItem item in collection)
            //        {
            //            if (item.EstimatedTime.Day > now_time.Day && item.EstimatedTime.Month == now_time.Month)
            //            {
            //                CurrentCollection.Add(item);
            //            }
            //        }
            //        CurrentCollection = new ObservableCollection<ITableItem>(currentCollection.OrderBy(p => p.ScheduledTime));
            //    }
            //    else
            //    {
            //        if (day == "yesterday")
            //        {
            //            foreach (DepartureTableItem item in collection)
            //            {
            //                if (item.EstimatedTime.Day < now_time.Day && item.EstimatedTime.Month == now_time.Month)
            //                {
            //                    CurrentCollection.Add(item);
            //                }
            //            }
            //            CurrentCollection = new ObservableCollection<ITableItem>(currentCollection.OrderBy(p => p.ScheduledTime));
            //        }
            //    }
            //}
        }
        void ChangeArrDepButtonColor(Button cur_but, Button but)
        {
            but.Foreground = new SolidColorBrush(Color.Parse("LightGray"));
            but.Background = new SolidColorBrush(Color.Parse("DarkGray"));
            
            cur_but.Foreground = new SolidColorBrush(Color.Parse("White"));
            cur_but.Background = new SolidColorBrush(Color.Parse("Orange"));
        }
        void ChangeDaysButtonColor(Button cur_b, Button b1, Button b2)
        {
            cur_b.Foreground = new SolidColorBrush(Color.Parse("White"));
            cur_b.Background = new SolidColorBrush(Color.Parse("Gray"));
            b1.Foreground = new SolidColorBrush(Color.Parse("Gray"));
            b1.Background = new SolidColorBrush(Color.Parse("LightGray"));
            b2.Foreground = new SolidColorBrush(Color.Parse("Gray"));
            b2.Background = new SolidColorBrush(Color.Parse("LightGray"));
        }

        public ReactiveCommand<Unit, Unit> OpenArrivalTableCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenDepartureTableCommand { get; }
        public ReactiveCommand<Unit, Unit> TodayButtonCommand { get; }
        public ReactiveCommand<Unit, Unit> TomorrowButtonCommand { get; }
        public ReactiveCommand<Unit, Unit> YesterdayButtonCommand { get; }

        public string CurrentHeaderText
        {
            get => currentHeaderText;
            set
            {
                this.RaiseAndSetIfChanged(ref currentHeaderText, value);
            }
        }

        public ViewModelBase Content
        {
            get => content;
            set
            {
                this.RaiseAndSetIfChanged(ref content, value);
            }
        }

        public ObservableCollection<ArrivalTableItem> ArrivalTab
        {
            get => dataBase.arrivalsCollection;
        }
        public ObservableCollection<DepartureTableItem> DepartureTab
        {
            get => dataBase.departuresCollection;
        }
        public ObservableCollection<ITableItem> CurrentCollection
        {
            get => currentCollection;
            set
            {
                this.RaiseAndSetIfChanged(ref currentCollection, value);
            }
        }
    }
}