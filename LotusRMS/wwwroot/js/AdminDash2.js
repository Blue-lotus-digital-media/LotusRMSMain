﻿

function GetTableBooked(me) {
    var type = $(me).attr("type");
    var text = $(me).text();

    $.ajax({
        type: 'GET',
        url: "/Admin/Home/GetTableBooked",
        data: "type=" + type,
        success: function (data) {
            var span = document.querySelector("#TableBookedDataType");
            span.innerHTML = text;
            var h6 = document.querySelector("#TableBookedData");
            h6.innerHTML=data.data;

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function GetTop5Item() {
    
    $.ajax({
        type: 'GET',
        url: "/Admin/Home/GetTop5Item",
        
        success: function (data) {
            loadChart(data);

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function GetTransection(me) {
    
    var type = $(me).attr("type");
    var text = $(me).text();

    $.ajax({
        type: 'GET',
        url: "/Admin/Home/GetTransection",
        data: "type=" + type,
        success: function (data) {
            var span = document.querySelector("#TransectionDataType");
            span.innerHTML = text;
            var h6 = document.querySelector("#TransectionData");
            h6.innerHTML = data.data;

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function GetStandingOrder() {
    
   

    $.ajax({
        type: 'GET',
        url: "/Admin/Home/GetStandingOrder",
        success: function (data) {
            console.log(data);

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function loadChart(data){
    const labels = [];
    const today = [];
    const yesterday = [];

    data.forEach(function (item) {
        labels.push(item[0]);
        today.push(item[1]);
        yesterday.push(item[2]);
    });
    echarts.init(document.querySelector("#verticalBarChart")).setOption({
        title: {
            text: 'Top sale Items'
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            }
        },
        legend: {},
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        xAxis: {
            type: 'value',
            boundaryGap: [0, 0.01]
        },
        yAxis: {
            type: 'category',
            data: labels
        },
        series: [{
            name: 'Today',

            type: 'bar',
            data: today
        },
        {
            name: 'Yesterday',
            type: 'bar',
            data: yesterday
        }
        ]
    });

}
document.addEventListener("DOMContentLoaded", () => {
    $("#tableBookedFirst").click();
    $("#transectionFirst").click();
    GetTop5Item();
    GetStandingOrder();



});