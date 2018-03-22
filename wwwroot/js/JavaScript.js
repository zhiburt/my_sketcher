"use strict";
//TODO VJOPU!
function loadCanvas() {
    var e = localStorage.getItem("canvas"),
        a = new Image();
    (a.src = e), (a.onload = function () {
        ctx.drawImage(a, 0, 0);
    }), (document.getElementById("canvasImg").src = e || error);
}
function loadCanvasBg() {
    var e = localStorage.getItem("canvasBg"),
        a = new Image();
    (a.src = e), (a.onload = function () {
        ctxBg.drawImage(a, 0, 0);
    }), (document.getElementById("canvasBgImg").src = e || error);
}
function activeTool(e, a) {
    a ? e.classList.add("active") : e.classList.remove("active");
}


function showBrush() {
    (showBrushPanel = !showBrushPanel), activeTool(
        brushTool,
        showBrushPanel
    ), showBrushPanel
            ? (
                brushPanel.classList.remove("hide")
            )
            : brushPanel.classList.add("hide");
}

function showText() {
    (showTextPanel = !showTextPanel), activeTool(
        textTool,
        showTextPanel
    ), showTextPanel
            ? (
                textPanel.classList.remove("hide")
            )
        : textPanel.classList.add("hide");
}

function showNav() {
    (showNavPanel = !showNavPanel), activeTool(
        navTool,
        showNavPanel
    ), showNavPanel
            ? navPanel.classList.remove("hide")
            : navPanel.classList.add("hide");
}
function dragStart(e) {
    var a = window.getComputedStyle(e.target, null),
        t =
            parseInt(a.getPropertyValue("left")) -
            e.clientX +
            "," +
            (parseInt(a.getPropertyValue("top")) - e.clientY) +
            "," +
            e.target.id;
    e.dataTransfer.setData("Text", t);
}
function drop(e) {
    var a = e.dataTransfer.getData("Text").split(","),
        t = document.getElementById(a[2]);
    return (t.style.left = e.clientX + parseInt(a[0], 10) + "px"), (t.style.top =
        e.clientY + parseInt(a[1], 10) + "px"), e.preventDefault(), !1;
}
function dragOver(e) {
    return e.preventDefault(), !1;
}
var canvas = document.querySelector("#draw"),
    canvasBg = document.querySelector("#canvasBg"),
    colorPicker = document.querySelector(".colorSelector"),
    brushSize = document.querySelector(".brushSize"),
    brushSizePreview = document.querySelector(".brushSizePreview"),
    brushOpacity = document.querySelector(".brushOpacity"),
    brushTool = document.querySelector(".brush"),
    brushPanel = document.querySelector(".brushPanel"),
    panelCross = document.querySelector("#panelCross"),
    panelCross1 = document.querySelector("#panelCross1"),
    textTool = document.querySelector(".text"),
    textPanel = document.querySelector(".textPanel"),
    navPanel = document.querySelector(".imgNav");
var canErase = !1;
var showBrushPanel = !1;
var showTextPanel = !1;
brushTool.addEventListener("click", showBrush), panelCross.addEventListener(
    "click",
    showBrush
);
textTool.addEventListener("click", showText), panelCross1.addEventListener(
    "click",
    showText
);
var showNavPanel = !0;
