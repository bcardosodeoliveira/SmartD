﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Smartd.Bibliotecas.Controls
{
    public class STabbedPage : TabbedPage
    {
        public enum TabBarPositionType
        {
            Top,
            Bottom
        };

        public static readonly BindableProperty TabBarPositionProperty = BindableProperty.Create(
            nameof(TabBarPosition),
            typeof(TabBarPositionType),
            typeof(STabbedPage),
            TabBarPositionType.Top
            );
        public TabBarPositionType TabBarPosition { get => (TabBarPositionType)GetValue(TabBarPositionProperty); set => SetValue(TabBarPositionProperty, value); }


        public static readonly BindableProperty TabBarCellTemplateProperty = BindableProperty.Create(
            nameof(TabBarCellTemplate),
            typeof(DataTemplate),
            typeof(STabbedPage)
            );
        public DataTemplate TabBarCellTemplate { get => (DataTemplate)GetValue(TabBarCellTemplateProperty); set => SetValue(TabBarCellTemplateProperty, value); }

        public static readonly BindableProperty TabBarSelectedCellTemplateProperty = BindableProperty.Create(
            nameof(TabBarSelectedCellTemplate),
            typeof(DataTemplate),
            typeof(STabbedPage)
            );
        public DataTemplate TabBarSelectedCellTemplate { get => (DataTemplate)GetValue(TabBarSelectedCellTemplateProperty); set => SetValue(TabBarSelectedCellTemplateProperty, value); }

        public static readonly BindableProperty SplitterColorProperty = BindableProperty.Create(
            nameof(SplitterColor),
            typeof(Color),
            typeof(STabbedPage),
            Color.LightGray
            );
        public Color SplitterColor { get => (Color)GetValue(SplitterColorProperty); set => SetValue(SplitterColorProperty, value); }

        public static readonly BindableProperty SplitterWidthProperty = BindableProperty.Create(
            nameof(SplitterWidth),
            typeof(double),
            typeof(STabbedPage),
            1.0
            );
        public double SplitterWidth { get => (double)GetValue(SplitterWidthProperty); set => SetValue(SplitterWidthProperty, value); }

        public static readonly BindableProperty TopBarColorProperty = BindableProperty.Create(
            nameof(TopBarColor),
            typeof(Color),
            typeof(STabbedPage),
            Color.LightGray
            );
        public Color TopBarColor { get => (Color)GetValue(TopBarColorProperty); set => SetValue(TopBarColorProperty, value); }

        public static readonly BindableProperty TopBarHeightProperty = BindableProperty.Create(
            nameof(TopBarHeight),
            typeof(double),
            typeof(STabbedPage),
            1.0
            );
        public double TopBarHeight { get => (double)GetValue(TopBarHeightProperty); set => SetValue(TopBarHeightProperty, value); }

        public static readonly BindableProperty BottomBarColorProperty = BindableProperty.Create(
            nameof(BottomBarColor),
            typeof(Color),
            typeof(STabbedPage),
            Color.LightGray
            );
        public Color BottomBarColor { get => (Color)GetValue(BottomBarColorProperty); set => SetValue(BottomBarColorProperty, value); }

        public static readonly BindableProperty BottomBarHeightProperty = BindableProperty.Create(
            nameof(BottomBarHeight),
            typeof(double),
            typeof(STabbedPage),
            1.0
            );
        public double BottomBarHeight { get => (double)GetValue(BottomBarHeightProperty); set => SetValue(BottomBarHeightProperty, value); }

        public static readonly BindableProperty TabBarHeightProperty = BindableProperty.Create(
            nameof(TabBarHeight),
            typeof(double),
            typeof(STabbedPage),
            70.0
            );
        public double TabBarHeight { get => (double)GetValue(TabBarHeightProperty); set => SetValue(TabBarHeightProperty, value); }

        protected Grid _tabBarView = null;

        protected List<View> cells;
        protected List<View> selectedCells;

        protected void createTabBar()
        {
            _tabBarView = new Grid
            {
                HeightRequest = TabBarHeight,
                RowSpacing = 0,
                BackgroundColor = Color.FromHex("#F26839")
            };

            _tabBarView.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(TopBarHeight, GridUnitType.Absolute)
            });
            _tabBarView.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Star)
            });
            _tabBarView.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(BottomBarHeight, GridUnitType.Absolute)
            });

            _tabBarView.Children.Add(new BoxView
            {
                BackgroundColor = TopBarColor
            }, 0, 0);

            _tabBarView.Children.Add(new BoxView
            {
                BackgroundColor = BottomBarColor
            }, 0, 2);

            cells = new List<View>();
            if (TabBarSelectedCellTemplate != null)
            {
                selectedCells = new List<View>();
            }

            Grid gridTabs = new Grid()
            {
                ColumnSpacing = 0
            };
            int i = 0;
            foreach (Page page in Children)
            {
                if (i > 0)
                {
                    gridTabs.ColumnDefinitions.Add(new ColumnDefinition
                    {
                        Width = new GridLength(SplitterWidth, GridUnitType.Absolute)
                    });
                    gridTabs.Children.Add(new BoxView
                    {
                        BackgroundColor = SplitterColor
                    }, 2 * i - 1, 0);
                }

                gridTabs.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });

                View cell = (View)TabBarCellTemplate.CreateContent();
                cell.BindingContext = page.BindingContext == null ? page : page.BindingContext;
                cell.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    CommandParameter = cell,
                    Command = SelectCellCommand
                });
                gridTabs.Children.Add(cell, 2 * i, 0);
                cells.Add(cell);

                if (TabBarSelectedCellTemplate != null)
                {
                    View selectedCell = (View)TabBarSelectedCellTemplate.CreateContent();
                    selectedCell.BindingContext = cell.BindingContext;
                    selectedCell.IsVisible = false;
                    gridTabs.Children.Add(selectedCell, 2 * i, 0);
                    selectedCells.Add(selectedCell);
                }

                i++;
            }

            _tabBarView.Children.Add(gridTabs, 0, 1);

            OnCurrentPageChanged();
        }

        public Grid TabBarView
        {
            get
            {
                if (_tabBarView == null)
                    createTabBar();
                return _tabBarView;
            }
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            if (selectedCells == null)
                return;

            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].IsVisible = Children[i] != CurrentPage;
                selectedCells[i].IsVisible = !cells[i].IsVisible;
            }
        }

        protected ICommand SelectCellCommand => new Command<View>((v) =>
        {
            int index = cells.IndexOf(v);
            if (index < 0)
                return;
            CurrentPage = Children[index];
        });
    }
}
