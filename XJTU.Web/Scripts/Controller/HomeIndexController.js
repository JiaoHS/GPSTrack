myApp1.controller('HomeIndexController', function ($scope, $http, $location) {
    var temp;
    var map = new BMap.Map("allmap");
    $scope.Init = function () {
        var map = new BMap.Map("allmap");
        var makerPoint = [];

        var point = new BMap.Point(108.99201870491, 34.251736393392);
        map.centerAndZoom(point, 15);
        point = { "lng": point.lng, "lat": point.lat, "carid": 0, "time": 0 };
        makerPoint.push(point);
        map.enableScrollWheelZoom(true);
        $scope.AddMarker(makerPoint, map, 1);
        //var geolocation = new BMap.Geolocation();
        //geolocation.getCurrentPosition(function (r) {
        //    if (this.getStatus() == BMAP_STATUS_SUCCESS) {
        //        var mk = new BMap.Marker(r.point);
        //        map.addOverlay(mk);
        //        map.panTo(r.point);

        //        //alert('您的位置：' + r.point.lng + ',' + r.point.lat);

        //    }
        //    else {
        //        alert('failed' + this.getStatus());
        //    }
        //}, { enableHighAccuracy: true })

        var myDate = new Date()
        $scope.currentTimestart = $scope.getNowFormatDate();
        $scope.currentTimeend = $scope.getNowFormatDate();
        $('#datetimepickerstart').datetimepicker({
            format: 'yyyy-mm-dd hh:mm:ss',      /*此属性是显示顺序，还有显示顺序是mm-dd-yyyy*/
            autoclose: true,//自动关闭
            minView: 2,//最精准的时间选择为日期0-分 1-时 2-日 3-月 
            weekStart: 0
        });
        $('#datetimepickerend').datetimepicker({
            format: 'yyyy-mm-dd hh:mm:ss',      /*此属性是显示顺序，还有显示顺序是mm-dd-yyyy*/
            autoclose: true,//自动关闭
            minView: 2,//最精准的时间选择为日期0-分 1-时 2-日 3-月 
            weekStart: 0
        });

    }
    $scope.getNowFormatDate = function () {
        var date = new Date();
        var seperator1 = "-";
        var seperator2 = ":";
        var month = date.getMonth() + 1;
        var hour = date.getHours();
        var minu = date.getMinutes();
        var sec = date.getSeconds();
        var strDate = date.getDate();
        if (month >= 1 && month <= 9) {
            month = "0" + month;
        }
        if (hour >= 1 && hour <= 9) {
            hour = "0" + hour;
        }
        if (minu >= 1 && minu <= 9) {
            minu = "0" + minu;
        }
        if (sec >= 1 && sec <= 9) {
            sec = "0" + sec;
        }
        if (strDate >= 0 && strDate <= 9) {
            strDate = "0" + strDate;
        }
        var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
                + " " + hour + seperator2 + minu
                + seperator2 + sec;
        return currentdate;
    }
  
    $scope.ApiUrl = getApiUrl();
    $scope.pager = {};
    $scope.FilterPanel = [];
    var pageSize = 15;
    var points = [];//原始点信息数组  
    var bPoints = [];//百度化坐标数组。用于更新显示范围。 
    $scope.LoadData = function (pageIndex, pageSize, filterpanel) {
        var pSize = $scope.pager.pageSize = pageSize;
        $http({
            method: "get",
            url: $scope.ApiUrl + "/Home/HomeList?pageIndex=" + pageIndex + "&pageSize=" + pageSize,
            dataType: "json",
            data: JSON.stringify(filterpanel),
            headers: { "Content-Type": "application/json;charset=utf-8" }
        }).then(function (d) {
            $scope.NoticeList = d.data.Data;
            $scope.totalCount = d.data.totalCount;


            //分页
            var totalNum = $scope.pager.totalNumbers = d.data.totalCount;
            $scope.pager.pageCount = Math.ceil(totalNum / pSize);
            $scope.pager.currentPageNumber = pageIndex;
        });
    }

    //分页跳转
    $scope.pageChanged = function (pageIndex) {
        $scope.LoadData(pageIndex, pageSize, $scope.FilterPanel);
    }
    $scope.theLocation = function () {
        if (document.getElementById("longitude").value != "" && document.getElementById("latitude").value != "") {
            map.clearOverlays();
            var new_point = new BMap.Point(document.getElementById("longitude").value, document.getElementById("latitude").value);
            var marker = new BMap.Marker(new_point);  // 创建标注
            map.addOverlay(marker);              // 将标注添加到地图中
            map.panTo(new_point);
        } else {
            map.centerAndZoom(point, 12);
        }
    }
    $scope.LoadTrack = function () {
        if ($scope.sId == null) {
            alert("车号不能为空！");
            return;
        }
        if ($scope.currentTimestart >= $scope.currentTimeend) {
            alert("开始时间不能大于结束时间！");
            return;
        }

        var arrs = [];
        var FilterPanel = [];
        var map = new BMap.Map("allmap");
        map.enableScrollWheelZoom(true);
        $http({
            method: "get",
            url: $scope.ApiUrl + "/Home/HomeList?sn_id=" + $scope.sId + "&pageIndex=" + 1 + "&pageSize=" + 150 + "&startTime=" + $scope.currentTimestart + "&endTime=" + $scope.currentTimeend,
            dataType: "json",
            data: JSON.stringify(FilterPanel),
            headers: { "Content-Type": "application/json;charset=utf-8" }
        }).then(function (d) {
            if (d == null || d.data.Data.length <= 0) {
                alert("无数据！");
                $scope.Init();
                return;
            }
            $scope.carTrackList = d.data.Data;
            angular.forEach($scope.carTrackList, function (value, index) {
                var wgs84togcj02 = coordtransform.wgs84togcj02(value.lng.toFixed(6), value.lat.toFixed(6));
                var gcj02tobd09 = coordtransform.gcj02tobd09(wgs84togcj02[0].toFixed(6), wgs84togcj02[1].toFixed(6));
                //Point = new BMap.Point(gcj02tobd09[0], gcj02tobd09[1]);
                var point = { "lng": gcj02tobd09[0], "lat": gcj02tobd09[1], "carid": value.sn, "time": value.time };
                //$scope.FormateTime(value.time);
                arrs.push(point);


            });
            dynamicLine(arrs, map);


            //$scope.LoadTrackData(arrs, map);

        });
    }


    //$scope.LoadData(2, pageSize, $scope.FilterPanel);

    function dynamicLine(arrs, map) {
        map.clearOverlays();
        var arrsTemp = [];
        var startIndex = 0;
        var endIndex = 1;
        var myVar = setInterval(function () {
            arrsTemp = arrs.slice(startIndex, endIndex);//取一个
            var len = points.length;
            var lng = arrsTemp[0].lng;
            var lat = arrsTemp[0].lat;
            var point = { "lng": lng, "lat": lat, "carid": arrsTemp[0].carid, "time": arrsTemp[0].time };
            var makerPoints = [];
            var newLinePoints = [];


            makerPoints.push(point);
            if (startIndex == 0 || startIndex == arrs.length - 1) {
                $scope.AddMarker(makerPoints, map, 1);//增加起点 终点的标注  
            }

            points.push(point);
            bPoints.push(new BMap.Point(lng, lat));
            len = points.length;
            newLinePoints = points.slice(len - 2, len);

            $scope.AddLine(newLinePoints, map);//增加轨迹线  
            $scope.setZoom(bPoints, map);
            startIndex++;
            endIndex = startIndex + 1;
            if (startIndex == arrs.length) {
                clearInterval(myVar);
                alert("OVER");
            }
        }, 1000);
    }
    $scope.LoadTrackData = function (arrs, map) {
        //var donePoints = [];//已经显示的点。
        //var bPoints = [];//保存百度化的坐标组。用于重设地图的中心点和显示级别。
        //var timerArr = [];//定时器
        //var interval;
        ////var map = new BMap.Map("allmap");
        //map.enableScrollWheelZoom(true);
        ////清除当前所有的定时器和地图上的覆盖物。
        //map.clearOverlays();
        //for (var t = 0; t < timerArr.length; t++) {
        //    clearTimeout(timerArr[t]);
        //};
        //timerArr = [];
        //clearInterval(interval);
        //bPoints.length = 0;
        //donePoints.length = 0;
        ////从原始数组中查询符合条件的坐标点。
        //var pointsLen = arrs.length;
        //var searchRes = [];//用来装符合条件的坐标信息。

        ////满足条件的放上去。
        //for (var i = 0; i < pointsLen; i++) {
        //    searchRes.push(arrs[i]);
        //};

        //for (var j = 0; j < searchRes.length; j++) {
        //    (function () {

        //        var pointAg = [searchRes[j]], timer;//闭包
        //        timer = setTimeout(function () {

        //            var doneLen = donePoints.length;
        //            var linePoints = [];

        //            if (doneLen != 0) {
        //                linePoints.push(donePoints[doneLen - 1]);
        //            }
        //            linePoints.push(pointAg[0]);
        //            donePoints.push(pointAg[0]);
        //            $scope.AddLine(linePoints, map); //把原始数据的轨迹线添加到地图上。

        //            $scope.AddMarker(pointAg, map);

        //            bPoints.push(new BMap.Point(pointAg[0].lng, pointAg[0].lat));
        //            $scope.setZoom(bPoints, map);

        //        }, 1000);
        //        timerArr.push(timer);
        //    })();

        //};
    }

    $scope.AddMarker = function (points, map, type) {
        //循环建立标注点
        for (var i = 0, pointsLen = points.length; i < pointsLen; i++) {
            var point = new BMap.Point(points[i].lng, points[i].lat); //将标注点转化成地图上的点
            var marker = new BMap.Marker(point); //将点转化成标注点
            if (type == 1) {
                map.addOverlay(marker);  //将标注点添加到地图上
            }

            //添加监听事件
            (function () {
                var thePoint = points[i];
                marker.addEventListener("click",
                    function () {
                        $scope.showInfo(this, thePoint);
                    });
            })();
        }
    }

    $scope.AddLine = function (points, map) {
        var linePoints = [], pointsLen = points.length, i, polyline;
        if (pointsLen == 0) {
            return;
        }
        // 创建标注对象并添加到地图     
        for (i = 0; i < pointsLen; i++) {
            linePoints.push(new BMap.Point(points[i].lng, points[i].lat));
        }

        polyline = new BMap.Polyline(linePoints, { strokeColor: "blue", strokeWeight: 5, strokeOpacity: 1 });   //创建折线  
        map.addOverlay(polyline);   //增加折线  
    }
    $scope.setZoom = function (bPoints, map) {
        var view = map.getViewport(eval(bPoints));
        var mapZoom = view.zoom;
        var centerPoint = view.center;
        map.centerAndZoom(centerPoint, mapZoom);
    }
    $scope.showInfo = function (thisMarker, point) {
        var sContent =
        '<ul style="margin:0 0 5px 0;padding:0.2em 0">'
        + '<li style="line-height: 26px;font-size: 15px;">'
        + '<span style="width: 50px;display: inline-block;">CarId：</span>' + point.carid + '</li>'
        + '<li style="line-height: 26px;font-size: 15px;">'
        + '<span style="width: 50px;display: inline-block;">lng：</span>' + point.lng + '</li>'
           + '<li style="line-height: 26px;font-size: 15px;">'
          + '<span style="width: 50px;display: inline-block;">lat：</span>' + point.lat + '</li>'
             //+ '<li style="line-height: 26px;font-size: 15px;">'
             //+ '<span style="width: 50px;display: inline-block;">Time：</span>' + point.time + '</li>'
        + '</ul>';
        var infoWindow = new BMap.InfoWindow(sContent); //创建信息窗口对象
        thisMarker.openInfoWindow(infoWindow); //图片加载完后重绘infoWindow
    }
    $scope.FormateTime = function (str) {
        str = str.replace("/Date(", "").replace(")/", "");
        var now = new Date(parseInt(str) * 1000).toLocaleString().replace(/:\d{1,2}$/, ' ')
        //var year = now.getYear();
        //var month = now.getMonth() + 1;
        //var date = now.getDate();
        //var hour = now.getHours();
        //var minute = now.getMinutes();
        //var second = now.getSeconds();

        alert(now);
    }
    $scope.Init();
});