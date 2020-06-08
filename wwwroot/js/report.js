var Report = {
    model: null,
    Init: function (_model) {
        this.model = _model;
        this._renderHourlyData();
        this._renderDataOverDate();
        this._renderUserData();
        this._renderDayOfWeekMap();
    },
    getObject: function (item) {
        return {
            name: item.name,
            data: [{ name: item.name, value: item.value }]
        }
    },
    _renderHourlyData: function () {
        var that = this;
        Highcharts.chart('divHourRate', {
            chart: {
                type: 'column'
            },
            title: {
                text: ''
            },
            xAxis: {
                categories: ['00', '01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23']
            },
            title: {
                text: 'Messages count per hour per user'
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Messages'
                },
                stackLabels: {
                    enabled: true,
                    style: {
                        fontWeight: 'bold',
                        color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                    }
                }
            },
            legend: {
                align: 'center',
                x: -30,
                verticalAlign: 'top',
                y: 30,
                floating: false,
                backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                borderColor: '#CCC',
                borderWidth: 1,
                shadow: false
            },
            tooltip: {
                headerFormat: '<b>{point.x}</b><br/>',
                pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b> (<span style="color:{series.color}">{point.percentage:.0f}</span>%)<br/>',
                shared: true
            },
            plotOptions: {
                column: {
                    stacking: 'normal'
                }
            },
            series: that.model.hourlyData.map(function (y) { return { name: y.Name, data: y.Value } })
        });
    },
    _renderDataOverDate: function () {
        var that = this;
        Highcharts.chart('divDataOverDate', {
            chart: {
                type: 'spline'
            },
            title: {
                text: 'Daily Messages Rate'
            },
            xAxis: {
                categories: that.model.dataOverData.map(x => x.Date)
            },
            yAxis: {
                title: {
                    text: 'Messages'
                }
            },
            tooltip: {
                crosshairs: true,
                shared: true
            },
            plotOptions: {
                line: {
                    dataLabels: {
                        enabled: true
                    },
                    enableMouseTracking: false
                }
            },
            series: [{
                name: 'Messages',
                data: that.model.dataOverData.map(x => x.Value)
            }]
        });
    },
    _renderUserData: function () {
        var that = this;
        
        Highcharts.chart('divUserCounts', {
            chart: {
                type: 'packedbubble'
            },
            title: {
                text: 'Total messages per user'
            },
            tooltip: {
                useHTML: true,
                pointFormat: '<b>{point.name}:</b> {point.value}'
            },
            plotOptions: {
                packedbubble: {
                    minSize: '0%',
                    maxSize: '120%',
                    zMin: 0,
                    zMax: 1000,
                    layoutAlgorithm: {
                        splitSeries: false,
                        gravitationalConstant: 0.02
                    },
                    dataLabels: {
                        enabled: true,
                        format: '{point.name}',
                        filter: {
                            property: 'y',
                            operator: '>',
                            value: 10
                        },
                        style: {
                            color: 'black',
                            textOutline: 'none',
                            fontWeight: 'normal'
                        }
                    }
                }
            },
            series: that.model.usersCounts.map(x => that.getObject(x))
        });
    },
    _renderDayOfWeekMap: function () {
        var that = this;
        function getDayName(point) {
            return point.series['yAxis'].categories[point['y']];
        }

        Highcharts.chart('divWeekDaysHours', {

            chart: {
                type: 'heatmap',
                marginTop: 40,
                marginBottom: 80,
                plotBorderWidth: 1
            },


            title: {
                text: 'Messages count per hour per weekday'
            },

            xAxis: {
                categories: ['00', '01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23']
            },

            yAxis: {
                categories: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
                title: null,
                reversed: true
            },

            //accessibility: {
            //    point: {
            //        descriptionFormatter: function (point) {
            //            var ix = point.index + 1,
            //                xName = getPointCategoryName(point, 'x'),
            //                yName = getPointCategoryName(point, 'y'),
            //                val = point.value;
            //            return ix + '. ' + xName + ' sales ' + yName + ', ' + val + '.';
            //        }
            //    }
            //},

            colorAxis: {
                min: 0,
                minColor: '#FFFFFF',
                maxColor: Highcharts.getOptions().colors[0]
            },

            legend: {
                align: 'right',
                layout: 'vertical',
                margin: 0,
                verticalAlign: 'top',
                y: 25,
                symbolHeight: 280
            },

            tooltip: {
                formatter: function () {
                    return `${this.point.value} messages were sent on ${getDayName(this.point)}`;
                        //;'At <b>' + this.point + ':00 </b> OClock sold <br><b>' +
                        //this.point.value + '</b> items on <br><b>' + getPointCategoryName(this.point, 'y') + '</b>';
                }
            },

            series: [{
                name: 'Sales per employee',
                borderWidth: 1,
                data: that.model.dayOfWeekHours,//[[0, 0, 10], [0, 1, 19], [0, 2, 8], [0, 3, 24], [0, 4, 67], [1, 0, 92], [1, 1, 58], [1, 2, 78], [1, 3, 117], [1, 4, 48], [2, 0, 35], [2, 1, 15], [2, 2, 123], [2, 3, 64], [2, 4, 52], [3, 0, 72], [3, 1, 132], [3, 2, 114], [3, 3, 19], [3, 4, 16], [4, 0, 38], [4, 1, 5], [4, 2, 8], [4, 3, 117], [4, 4, 115], [5, 0, 88], [5, 1, 32], [5, 2, 12], [5, 3, 6], [5, 4, 120], [6, 0, 13], [6, 1, 44], [6, 2, 88], [6, 3, 98], [6, 4, 96], [7, 0, 31], [7, 1, 1], [7, 2, 82], [7, 3, 32], [7, 4, 30], [8, 0, 85], [8, 1, 97], [8, 2, 123], [8, 3, 64], [8, 4, 84], [9, 0, 47], [9, 1, 114], [9, 2, 31], [9, 3, 48], [9, 4, 91]],
                dataLabels: {
                    enabled: true,
                    color: '#000000'
                }
            }],

            responsive: {
                rules: [{
                    condition: {
                        maxWidth: 500
                    },
                    chartOptions: {
                        yAxis: {
                            labels: {
                                formatter: function () {
                                    return this.value.charAt(0);
                                }
                            }
                        }
                    }
                }]
            }

        });
    }
}