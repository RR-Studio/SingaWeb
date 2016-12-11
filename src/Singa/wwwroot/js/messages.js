$(function () {
    var combination = ''
    key('5', function () {
        combination += '5';
        checkCombination();
    });
    key('6', function () {
        combination += '6';
        checkCombination();
    });
    
    function checkCombination() {
        if (combination.length > 4)
        {
            combination = combination.substr(combination.length - 4);
        }
        if (combination === '5565') {
            console.log("combinationPressed");

            showMyItem();
        }
    }
});


function showMyItem() {
    //$("#forHide").css('dispaly','block');
    $("#forHide").show(1000);

    var myTimer = setTimeout(hideMyItem, 4000);
}

function hideMyItem() {
    $("#forHide").hide(1000);
}