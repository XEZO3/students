// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
$(window).on("load", function () {
    $('.loader').addClass("complete")
    $('.load').attr("src", "")
    $('#rowcard').hide();
    $('#rowcard3').hide();
    $('#rowcard4').hide();
});
// Write your JavaScript code.
function AddToCart(courseId) {
    $.ajax({
        url: "/cart/AddToCart/" + courseId,
        type: "GET",
        success: function (data) {
            if (data == 0) {
                alert("you already add this course");

            } else if (data == 33) {
                alert("you already have this course");
            } else {
                document.getElementById("counter").innerHTML = data;
            }
        }
    })
}
function RemoveFromCart(courseId) {
    $.ajax({
        url: "/cart/RemoveFromCart/" + courseId,
        type: "GET",
        success: function (data) {
            if (data[0] == "empty") {
                alert("you dont have this course in your cart");

            } else {
                document.getElementById("counter").innerHTML = data[1];
                document.getElementById("body-counter").innerHTML = data[1];
                loadCart();
                
            }
        }
    })
}
function loadCart() {
    var body1 = document.getElementById("return");
    $.ajax({
        url: "/cart/Index?isajax="+true,
        type: "GET",
        success: function (data) {
            body1.innerHTML = data
        }
    })
}

function filterCourses(name) {
    var body1 = document.getElementById("returne");
    $.ajax({
        url: "/courses/index/?name="+name+"&isajax="+true,
        type: "get",
        success: function (data) {
            body1.innerHTML = data;
        }
    })
}

    $("#show").click(function(){
        $("#rowcard").toggle()
    })
    $("#show2").click(function(){
        $("#rowcard2").toggle()
    })
    $("#show3").click(function(){
        $("#rowcard3").toggle()
    })
    $("#show4").click(function(){
        $("#rowcard4").toggle()
    })

