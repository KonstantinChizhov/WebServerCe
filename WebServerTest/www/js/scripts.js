

function drawSignalChart(id, data, options) {
    var labelsData = new Array(data.length);
    for (var i = 0; i < labelsData.length; i++) {
        labelsData[i] = i;
    }

    var data = {
        labels: labelsData,
        series: [data]
    };

    new Chartist.Line(id, data, options);
}

function loadData() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            data = JSON.parse(this.responseText);

            var options = {
                width: "100%",
                height: "100%",
                showPoint: false,
                showGridBackground: false,
                divisor: 500,
                axisY: {
                    onlyInteger: true,
                    showGrid: true,
                    showLabel: true,
                },
                axisX: {
                    onlyInteger: true,
                    showGrid: true,
                    ticks: [0, 50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000],
                    labelInterpolationFnc: function skipLabels(value, index) { return index % 100 === 0 ? value : null; }
                },
            };
            drawSignalChart('#signal', data, options);
        }
    };
    xhttp.open("GET", "/myapp/api/v1/data.json", true);
    xhttp.send();
}


function initCharts() {
     loadData();
}