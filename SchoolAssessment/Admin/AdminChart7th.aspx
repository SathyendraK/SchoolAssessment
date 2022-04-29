<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminChart7th.aspx.cs" Inherits="SchoolAssessment.Admin.AdminChart7th" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Admin</title>
  <link rel="stylesheet" type="text/css" href="Styles.css" />
  <!--<script type="text/javascript" src="https://www.google.com/jsapi"></script>-->
  <!--<script type="text/javascript" src="../Scripts/jsapi.js"></script> Google function undefined 11/16/2020-->
  <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script> <!-- Load this to use Google graph 11/16/2020--> 
  <script type="text/javascript">

    // Load the Visualization API and the piechart package.
    google.load('visualization', '1.0', { 'packages': ['corechart'] });

    // Set a callback to run when the Google Visualization API is loaded.
    google.setOnLoadCallback(drawCharts);

    function drawCharts() {
      if (document.getElementById('chart_div') != null)
        drawDateChart();
      if (document.getElementById('chart_county') != null)
        drawCountyChart();
    }

    function drawDateChart() {

      var chartDataRaw;
      var array1, array2, array3, array4;
      var chartDataArray;
      var date, d, m, y;
      var reported, subtotal, percentage, tooltip;
      var total;

      chartDataRaw = document.getElementById('chartData').value;
      total = document.getElementById('totalSchools').value;

      chartDataArray = new Array();
      subtotal = 0;

      array1 = chartDataRaw.split(',');
      for (i = 0; i < array1.length; i++) {
        array2 = array1[i].split('|');
        date = array2[0];
        array3 = array2[0].split('-');
        y = array3[0];
        m = array3[1];
        d = array3[2];

        reported = parseInt(array2[1]);
        subtotal += reported;
        percentage = parseFloat((subtotal * 100 / total).toFixed(2));
        tooltip = date + "\n" + reported + ' reported' + "\n" + percentage + '% overall (' + subtotal + ' of ' + total + ')';
        array4 = new Array(new Date(y, m - 1, d), percentage, tooltip, reported);

        chartDataArray.push(array4);
      }

      // Create the data table.
      var data = new google.visualization.DataTable();
      data.addColumn('date', 'Submit Date');
      data.addColumn('number', '% Reported');
      data.addColumn({ type: 'string', role: 'tooltip' });
      data.addColumn({ type: 'number', role: 'annotation' });
      data.addRows(chartDataArray);

      // Set chart options
      var options = {
        'pointSize': 5,
        'height': 170,
        'fontSize': 11,
        'chartArea': { 'top': 10, 'left': 0, 'height': 125 },
        'vAxis': { 'textPosition': 'in', 'maxValue': 100 },
        'hAxis': { 'format': 'MMM d' },
        'tooltip': { 'textStyle': { 'fontName': 'Courier', 'fontSize': 12} }
      };

      // Instantiate and draw our chart, passing in some options.
      var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));
      chart.draw(data, options);

    }

    function drawCountyChart() {

      var chartDataRaw;
      var array1, array2, array3, array4;
      var chartDataArray;
      var countyName, reported, reportedLastWeek, total;
      var percentage, percentageLastWeek, tooltip;

      chartDataRaw = document.getElementById('chartCountyData').value;

      chartDataArray = new Array();

      array1 = chartDataRaw.split(',');
      for (i = 0; i < array1.length; i++) {
        array2 = array1[i].split('|');
        countyName = array2[0];
        reported = parseInt(array2[1]);
        reportedLastWeek = parseInt(array2[2]);
        total = parseInt(array2[3]);

        percentage = parseFloat((reported * 100 / total).toFixed(2));
        percentageLastWeek = parseFloat((reportedLastWeek * 100 / total).toFixed(2));


        array4 = new Array(countyName, percentage, percentageLastWeek);

        chartDataArray.push(array4);
      }

      // Create the data table.
      var data = new google.visualization.DataTable();
      data.addColumn('string', 'County');
      data.addColumn('number', '% now');
      data.addColumn('number', '% last week');
      data.addRows(chartDataArray);

      // Set chart options
      var options = {
        'height': 230,
        'chartArea': { 'top': 10, 'left': 0, 'height': 125 },
        'vAxis': { 'textPosition': 'in', 'gridlines': { 'count': 5} },
        'hAxis': { 'showTextEvery': 1, 'textStyle': { 'fontSize': 10 }, 'slantedText': true, 'slantedTextAngle': 90 },
        'tooltip': { 'textStyle': { 'fontName': 'Courier', 'fontSize': 12} }
      };

      // Instantiate and draw our chart, passing in some options.
      var chart = new google.visualization.ColumnChart(document.getElementById('chart_county'));
      chart.draw(data, options);

    }  
  </script>
</head>
<body>
  <form id="form1" runat="server">
  <h1>7th Grade Summary and Downloads</h1>

  <asp:Panel ID="panelCountyChart" runat="server">
      <h2>Percentage Reported by County</h2>
      <p>
          <asp:Label ID="lblCountyChartRegion" runat="server" Text="Label">Show Region:</asp:Label>
          <asp:DropDownList ID="cmbCountyChart" runat="server" autopostback="true">
            <asp:listitem value="00" text="All" />
            <asp:listitem value="01" text="NORTHERN" />
            <asp:listitem value="02" text="BAY AREA" />
            <asp:listitem value="03" text="CENTRAL" />
            <asp:listitem value="04" text="LOS ANGELES" />
            <asp:listitem value="05" text="SOUTHERN" />
         </asp:DropDownList>
          Sort by: <asp:DropDownList ID="cmbCountyChartSort" runat="server">
                        <asp:listitem value="CountyName" text="County Name Ascending" />
                        <asp:listitem value="Reported" text="Percentage Reported Ascending" />  
                   </asp:DropDownList>
      </p>
      <div id="chart_county" style="width: 100%"></div>
  </asp:Panel>


  


  <h2>Cumulative Percentage Reported by Date</h2>
  <asp:Panel ID="DateChartOptions" runat="server">
      <p>Show County: <asp:DropDownList ID="cmbDateChartCounties" runat="server" autopostback="true"  ></asp:DropDownList></p>
  </asp:Panel>
 
      
  <div id="chart_div" style="width: 100%">
  </div>
  
  <h2>Downloads</h2>
  <h4>7th Grade</h4>
      <p> 
          Select Year: <asp:DropDownList ID="DownloadListYear" runat="server" autopostback="true" >
                <asp:listitem value="20212022">2021-2022</asp:listitem>  
                <asp:listitem value="20202021">2020-2021</asp:listitem>
                <asp:listitem value="20192020">2019-2020</asp:listitem>
                <asp:listitem value="20182019">2018-2019</asp:listitem>
                <asp:listitem value="20172018">2017-2018</asp:listitem>
                <asp:listitem value="20162017">2016-2017</asp:listitem>
                <asp:listitem value="20152016">2015-2016</asp:listitem>
                <asp:listitem value="20142015">2014-2015</asp:listitem>
          </asp:DropDownList></p>
  <p>
    <asp:button id="btnAllList" runat="server" text="All List" causesvalidation="False" OnClick="btnAllList_Click" />
    <asp:button id="btnNotReported" runat="server" text="Delinquent List" causesvalidation="False" OnClick="btnNotReported_Click" />
    <asp:button id="btnReported" runat="server" text="Completed List" causesvalidation="False" OnClick="btnReported_Click" />
    <asp:button id="btnSummaryReport" runat="server" text="Summary Report" causesvalidation="False" OnClick="btnSummaryReport_Click" />
    <asp:Button ID="btnConditionalOverdue" runat="server" Text=">=10% Tdap Conditional + Overdue" OnClick="btnConditionalOverdue_Click" />
      <asp:Button ID="btnVaricellaConditionalOverdue" runat="server" Text=">=10% Var Conditional + Overdue" OnClick="btnVaricellaConditionalOverdue_Click"/>
  </p>
      
  <h4>8th Grade&nbsp;</h4>
  <p>Select Year:
   <asp:DropDownList ID="DownloadListYear_8th" runat="server"  autopostback="true">
       <asp:listitem value="20202021">2020-2021</asp:listitem>
   </asp:DropDownList>
  </p>
  <p>
      <asp:Button ID="btnAllList_8th" runat="server" text="All List" causesvalidation="False" OnClick="btnAllList_8th_Click" />
      <asp:Button ID="btnNotReported_8th" runat="server" text="Delinquent List" causesvalidation="False" OnClick="btnNotReported_8th_Click" />
      <asp:Button ID="btnReported_8th" runat="server" text="Completed List" causesvalidation="False" OnClick="btnReported_8th_Click" />
      <asp:Button ID="btnSummaryReport_8th" runat="server" text="Summary Report" causesvalidation="False" OnClick="btnSummaryReport_8th_Click" />
      <asp:Button ID="btnConditionalOverdue_8th" runat="server" Text=">=10% Tdap Conditional + Overdue" OnClick="btnConditionalOverdue_8th_Click" />
      <asp:Button ID="btnVaricellaConditionalOverdue_8th" runat="server" Text=">=10% Var Conditional + Overdue" OnClick="btnVaricellaConditionalOverdue_8th_Click" />
  </p>
  <input type="hidden" id="chartData" runat="server" />
  <input type="hidden" id="chartCountyData" runat="server" />
  <input type="hidden" id="totalSchools" runat="server" />
  <input type="hidden" id="selectedCounty" runat="server" />
  
  
  </form>
</body>
</html>

