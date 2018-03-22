//Chart
var ctx = document.getElementById("myChart").getContext('2d');

var myChart = new Chart(ctx, {
    type: 'bar',
    data: {
        labels: ["red", "green", "yellow", "blue", "black"],
        datasets: [{
            label: "this is label",
            data: [12, 19, 3, 5, 2, 3],
            backgroundColor: [
                'rgba(255,99,132,0.4)',
                'rgba(20,99,132,0.4)',
                'rgba(23,99,22,0.4)',
                'rgba(1,99,1,0.4)',
                'rgba(225,92,32,0.4)']
        }, {
                
            label: "this is label2",
            data: [40, 33, 13, 2, 1, 10],
            backgroundColor: [
                'rgba(125,99,52,0.4)',
                'rgba(21,99,32,0.4)',
                'rgba(55,33,22,0.4)',
                'rgba(111,22,1,0.4)',
                'rgba(225,2,32,0.4)']

            }]
    },
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    }
});



//Chart
var ctxTwo = document.getElementById("myChartTwo").getContext('2d');

var myPieChart = new Chart(ctxTwo, {
    "type": "doughnut",
    "data": {
        "labels": ["Red", "Blue", "Yellow"],
        "datasets": [{
            "label": "My First Dataset",
            "data": [300, 50, 100],
            "backgroundColor": ["rgba(255, 99, 132, 0.6)", "rgba(54, 162, 235,0.6)", "rgba(255, 205, 86, 0.6)"]
        }]
    }
});


//Chart
var ctxThree = document.getElementById("myChartRadar").getContext('2d');

var myRadarChart = new Chart(ctxThree, {
    type: 'radar',
    data: {
        labels: ['Coding', 'Likes', 'Coments', 'Anather', 'Andere', 'TODO'],
        datasets: [{
            label: "#data1",
            backgroundColor: 'rgba(255,205,99,0.2)',
            borderColor: 'rgb(255,12,34,0.7)',
            data: [40, 10, 20, 78, 100, 20]
        }, {

            label: "#data2",
            backgroundColor: 'rgba(2,99,132,0.2)',
            borderColor: 'rgb(255,12,34,0.7)',
            data: [80, 30, 60, 59, 30, 20]
        }]
    },
    options: null
});