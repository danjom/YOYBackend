
/*
    THIS FILE CONTAINS ALL THE FUNCTIONS AND AJAX CALL TO BE USED IN GRAPHICS RENDERING
    AUTHOR: PEDRO GUZMÁN
    COPYRIGHT (C)
*/
$(function () {

    // ===============================================================================================================//
    // FUNCTION LOAD CANJES CHART DATA ------------------------------------------------------------------------------ //
    // ============================================================================================================== //
    /**
     * This functions retrieves data asociated with anually statistics of price redeem rate
     **/
    function loadCanjesChardData() {

        $.ajax({
            url: "/Data/GetCanjesChartData",
            dataType: 'JSON',
            type: 'GET',
            success: function (response) {

                // IF RESPONSE IS NOT NULL, THEN RENDER CHART
                Morris.Area({
                    element: 'canjes-chart',
                    data: response,
                    xkey: 'Period',
                    ykeys: ['MediumPizza', 'LargePizza'],
                    labels: ['Pizza Mediana', 'Pizza Grande'],
                    pointSize: 2,
                    hideHover: 'auto',
                    resize: true
                });
            },
            error: function () {
                alert("Error al intentar recuperar estadísticas");
            }
        });

    } // FUNCTION LOAD CANGES CHART DATA ENDS ------------------------------------------------------------------------ //

    // ===============================================================================================================//
    // FUNCTION LOAD CANJES CHART DATA ------------------------------------------------------------------------------ //
    // ============================================================================================================== //
    /**
     * This functions retrieves data asociated with anually statistics of price redeem rate
     **/
    function loadCuponStats() {

        $.ajax({
            url: "/Data/GetCuponStats",
            dataType: 'JSON',
            type: 'GET',
            success: function (response) {

                // IF RESPONSE IS NOT NULL, THEN RENDER CHART
                Morris.Area({
                    element: 'cupons-chart',
                    data: response,
                    xkey: 'Period',
                    ykeys: ['ValidCuponsPizzaL', 'ValidCuponsPizzaM', 'ValidActivatedCuponsPizzaL', 'ValidActivatedCuponsPizzaM', 'ValidNonActivatedCuponsPizzaL', 'ValidNonActivatedCuponsPizzaM'],
                    labels: ['Se emitieron cupones Pizza Grande', 'Se emitieron cupones Pizza Mediana', 'Cupones activados pizza grande', 'Cupones activados pizza mediana', 'Cupones no activados pizza grande', 'Cupones no activados pizza mediana'],
                    pointSize: 2,
                    hideHover: 'auto',
                    resize: true
                });
            },
            error: function () {
                alert("Error al intentar recuperar estadísticas");
            }
        });

    } // FUNCTION LOAD CANGES CHART DATA ENDS ------------------------------------------------------------------------ // 





    // ================================================================================================================ //
    // FUNCTION CALLS                                                                                                   //
    // ================================================================================================================ //

    // RENDER CANJES CHART
    loadCanjesChardData();
    loadCuponStats();




/*
    // Donut Chart
    Morris.Donut({
        element: 'morris-donut-chart',
        data: [{
            label: "Download Sales",
            value: 12
        }, {
            label: "In-Store Sales",
            value: 30
        }, {
            label: "Mail-Order Sales",
            value: 20
        }],
        resize: true
    });



    // Line Chart
    Morris.Line({
        // ID of the element in which to draw the chart.
        element: 'morris-line-chart',
        // Chart data records -- each entry in this array corresponds to a point on
        // the chart.
        data: [{
            d: '2012-10-01',
            visits: 802
        }, {
            d: '2012-10-02',
            visits: 783
        }, {
            d: '2012-10-03',
            visits: 820
        }, {
            d: '2012-10-04',
            visits: 839
        }],
        // The name of the data record attribute that contains x-visitss.
        xkey: 'd',
        // A list of names of data record attributes that contain y-visitss.
        ykeys: ['visits'],
        // Labels for the ykeys -- will be displayed when you hover over the
        // chart.
        labels: ['Visits'],
        // Disables line smoothing
        smooth: false,
        resize: true
    });

    // Bar Chart
    Morris.Bar({
        element: 'morris-bar-chart',
        data: [{
            device: 'iPhone',
            geekbench: 136
        }, {
            device: 'iPhone 3G',
            geekbench: 137
        }, {
            device: 'iPhone 3GS',
            geekbench: 275
        }, {
            device: 'iPhone 4',
            geekbench: 380
        }, {
            device: 'iPhone 4S',
            geekbench: 655
        }, {
            device: 'iPhone 5',
            geekbench: 1571
        }],
        xkey: 'device',
        ykeys: ['geekbench'],
        labels: ['Geekbench'],
        barRatio: 0.4,
        xLabelAngle: 35,
        hideHover: 'auto',
        resize: true
    });

    */
});
