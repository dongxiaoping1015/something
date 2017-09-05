// ==UserScript==
// @name         喵播脚本(打卡and白嫖)
// @namespace    http://tampermonkey.net/
// @version      0.1
// @description  try to take over the world!
// @author       You
// @match        https://www.douyu.com/71771
// @grant        none
// ==/UserScript==
(function() {
    // 随机发送的弹幕，可自行新增修改
    var danmuList = new Array("*召唤顺毛", "*命运之饭白嫖", "*命运之饭白嫖啊", "*命运之饭白嫖", "*命运之饭白嫖啊", "*命运之饭白嫖"); 
    
    var  bp_siv = 0; // 自动白嫖
    var  dm_siv = 0; // 弹幕定时接收值，用来控制定时开或关
    // 添加控制开关html
    $(function(){
        var switchHtml =
            '<li class="myFlLi fl assort" style="height:50px;margin-left:20px" id="dm_switch">' +
            '<a href="javascript:;" data="0">打卡</a><i></i>' +
            '</li>';
        switchHtml +=
            '<li class="myFlLi fl assort" style="height:50px;margin-left:20px" id="bp_switch">' +
            '<a href="javascript:;" data="0">白嫖</a><i></i>' +
            '</li>';
        $(".head-nav").append(switchHtml);
        
        $("#header div.head").css("width", "100%");
        $("a.head-logo").css("margin-left", "30px");
        $("div.head-oth").css("margin-right", "50px");
    });

    //自动发送弹幕
    var maxNum = danmuList.length;
    var bpNum = 0;
    var sendNum = 0;
    var dmSended = 0;
    function send()
    {
        var dm_switch = $("#dm_switch>a").attr("data")+"";
        if(dm_switch=="1"){
            var num = 0;
            for (var i=0;i<100;i++) {
                num = parseInt(Math.random() * 100);
                if (num >= 60 && num <= 90 && dmSended !== num)
                {
                    dmSended = num;
                    break;
                }
            }
            $(".cs-textarea").val("*" + num);
            $("div.b-btn[data-type='send']").click();
            if (sendNum === 0) {
                sendNum++;
                clearInterval(dm_siv);
                dm_siv = setInterval(send, 30000);
            } else {
                sendNum = 0;
                clearInterval(dm_siv);
                dm_siv = setInterval(send, 60*60000 - 30000);
            }
        }
    }
    function bp_send() {
        var bp_switch = $("#bp_switch>a").attr("data")+"";
        if(bp_switch=="1"){
            $(".cs-textarea").val(danmuList[bpNum]);
            $("div.b-btn[data-type='send']").click();
            if (bpNum < 5) {
                bpNum++;
                clearInterval(bp_siv);
                bp_siv = setInterval(bp_send, 3000);
            } else {
                bpNum = 0;
                clearInterval(bp_siv);
            }
        }
    }
    // 弹幕开关控制
    $(document).on('click', '#dm_switch>a', function(){
		var old_switch = $("#dm_switch>a").attr("data")+"";
        if(old_switch=="1"){ // 由开到关
            $("#dm_switch").removeClass("current");
            $("#dm_switch>a").attr("data", "0");
            clearInterval(dm_siv);
        }
        else{
            $("#dm_switch").addClass("current");
            $("#dm_switch>a").attr("data", "1");
            //var dm_times = $(".dm_times.current").attr("data")+"";
                var mydate = new Date();
                dm_siv = setInterval(send, (60-mydate.getMinutes())*60000);
        }
	});
    $(document).on('click', '#bp_switch>a', function(){
		var old_switch = $("#bp_switch>a").attr("data")+"";
        if(old_switch=="1"){ // 由开到关
            $("#bp_switch").removeClass("current");
            $("#bp_switch>a").attr("data", "0");
            clearInterval(dm_siv);
        }
        else{
            $("#bp_switch").addClass("current");
            $("#bp_switch>a").attr("data", "1");
            bp_siv = setInterval(bp_send, 1000);
        }
	});

})();