using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Input;
using Telerik.Windows.Controls.Charting;

namespace TradeChart.WorkingChart
{
    public partial class FxChart
    {
        public FxChart()
        {
            InitializeComponent();

            WebClient webClient = new WebClient();
            webClient.OpenReadCompleted += webClient_OpenReadCompleted;
            webClient.OpenReadAsync(new Uri(HtmlPage.Document.DocumentUri, "FutureService.ashx"));
        }

        void webClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<Price>));
            List<Price> prices = (List<Price>) jsonSerializer.ReadObject(e.Result);
            DataContext = prices.OrderBy(p => p.Age);
        }

        //private KeyValuePair<DateTime, double> GetPlotAreaCoordinates(Point position)
        //{
            //IComparable yAxisHit = ((IRangeAxis)YAxis).GetValueAtPosition(
            //    new UnitValue(PlotArea.ActualHeight - position.Y, Unit.Pixels));

            //IComparable xAxisHit = ((IRangeAxis)XAxis).GetValueAtPosition(
            //    new UnitValue(position.X, Unit.Pixels));

            //return new KeyValuePair<DateTime, double>((DateTime)xAxisHit, (double)yAxisHit);
        //}

        //private void CrosshairContainer_MouseMove(object sender, MouseEventArgs e)
        //{
           // if (RadChart1.ItemsSource == null)
             //   return;

            //Point mousePos = e.GetPosition(PlotArea);
            //KeyValuePair<DateTime, double> crosshairLocation = GetPlotAreaCoordinates(mousePos);

            //LocationIndicator.DataContext = crosshairLocation;
            //Crosshair.DataContext = mousePos;   
        //}

        //private void CrosshairContainer_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    SetCrossHairVisibility(true);
        //}

        //private void CrosshairContainer_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    SetCrossHairVisibility(false);
        //}

        //private void SetCrossHairVisibility(bool visible)
        //{
        //    LocationIndicator.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        //    Crosshair.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        //    RadChart1.Cursor = visible ? Cursors.None : Cursors.Arrow;
        //}
    }
}
