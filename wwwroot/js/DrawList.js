

//(function () {

//    var fonts = ["Pacifico", "VT323", "Quicksand", "Inconsolata"];

//    var select = document.getElementById("font-family");
//    fonts.forEach(function (font) {
//        var option = document.createElement('option');
//        option.innerHTML = font;
//        option.value = font;
//        select.appendChild(option);
//    });

//}) ();


//(function () {
//    // Without JQuery
//    var slider = new Slider('#drawing-line-width', {
//        formatter: function (value) {
//            return 'Current value: ' + value;
//        }
//    });

//    var slider2 = new Slider('#drawing-shadow-width', {
//        formatter: function (value) {
//            return 'Current value: ' + value;
//        }
//    });

//    var slider3 = new Slider('#drawing-shadow-offset', {
//        formatter: function (value) {
//            return 'Current value: ' + value;
//        }
//    });
//})();



//(function () {
//    var $ = function (id) { return document.getElementById(id) };

//    var canvas = this.__canvas = new fabric.Canvas('canvas', {
//        isDrawingMode: true
//    });

//    fabric.Object.prototype.transparentCorners = false;

//    var drawingModeEl = $('drawing-mode'),
//        drawingOptionsEl = $('drawing-mode-options'),
//        drawingColorEl = $('drawing-color'),
//        drawingShadowColorEl = $('drawing-shadow-color'),
//        drawingLineWidthEl = $('drawing-line-width'),
//        drawingShadowWidth = $('drawing-shadow-width'),
//        drawingShadowOffset = $('drawing-shadow-offset'),
//        clearEl = $('clear-canvas'),
//        newTextBox = $('new-textbox');
//        deleteSelectedObject = $('delete-item');

//    clearEl.onclick = function () { canvas.clear() };

//    drawingModeEl.onclick = function () {
//        canvas.isDrawingMode = !canvas.isDrawingMode;
//        if (canvas.isDrawingMode) {
//            drawingModeEl.classList = 'fa fa-plus-square';
            
//        }
//        else {
//        drawingModeEl.classList = 'fa fa-close';
//        }
//    };

//    if (fabric.PatternBrush) {
//        var vLinePatternBrush = new fabric.PatternBrush(canvas);
//        vLinePatternBrush.getPatternSrc = function () {

//            var patternCanvas = fabric.document.createElement('canvas');
//            patternCanvas.width = patternCanvas.height = 10;
//            var ctx = patternCanvas.getContext('2d');

//            ctx.strokeStyle = this.color;
//            ctx.lineWidth = 5;
//            ctx.beginPath();
//            ctx.moveTo(0, 5);
//            ctx.lineTo(10, 5);
//            ctx.closePath();
//            ctx.stroke();

//            return patternCanvas;
//        };

//        var hLinePatternBrush = new fabric.PatternBrush(canvas);
//        hLinePatternBrush.getPatternSrc = function () {

//            var patternCanvas = fabric.document.createElement('canvas');
//            patternCanvas.width = patternCanvas.height = 10;
//            var ctx = patternCanvas.getContext('2d');

//            ctx.strokeStyle = this.color;
//            ctx.lineWidth = 5;
//            ctx.beginPath();
//            ctx.moveTo(5, 0);
//            ctx.lineTo(5, 10);
//            ctx.closePath();
//            ctx.stroke();

//            return patternCanvas;
//        };

//        var squarePatternBrush = new fabric.PatternBrush(canvas);
//        squarePatternBrush.getPatternSrc = function () {

//            var squareWidth = 10, squareDistance = 2;

//            var patternCanvas = fabric.document.createElement('canvas');
//            patternCanvas.width = patternCanvas.height = squareWidth + squareDistance;
//            var ctx = patternCanvas.getContext('2d');

//            ctx.fillStyle = this.color;
//            ctx.fillRect(0, 0, squareWidth, squareWidth);

//            return patternCanvas;
//        };

//        var diamondPatternBrush = new fabric.PatternBrush(canvas);
//        diamondPatternBrush.getPatternSrc = function () {

//            var squareWidth = 10, squareDistance = 5;
//            var patternCanvas = fabric.document.createElement('canvas');
//            var rect = new fabric.Rect({
//                width: squareWidth,
//                height: squareWidth,
//                angle: 45,
//                fill: this.color
//            });

//            var canvasWidth = rect.getBoundingRect().width;

//            patternCanvas.width = patternCanvas.height = canvasWidth + squareDistance;
//            rect.set({ left: canvasWidth / 2, top: canvasWidth / 2 });

//            var ctx = patternCanvas.getContext('2d');
//            rect.render(ctx);

//            return patternCanvas;
//        };

//        var img = new Image();
//        img.src = '../assets/honey_im_subtle.png';

//        var texturePatternBrush = new fabric.PatternBrush(canvas);
//        texturePatternBrush.source = img;
//    }

//    $('drawing-mode-selector').onchange = function () {

//        if (this.value === 'hline') {
//            canvas.freeDrawingBrush = vLinePatternBrush;
//        }
//        else if (this.value === 'vline') {
//            canvas.freeDrawingBrush = hLinePatternBrush;
//        }
//        else if (this.value === 'square') {
//            canvas.freeDrawingBrush = squarePatternBrush;
//        }
//        else if (this.value === 'diamond') {
//            canvas.freeDrawingBrush = diamondPatternBrush;
//        }
//        else if (this.value === 'texture') {
//            canvas.freeDrawingBrush = texturePatternBrush;
//        }
//        else {
//            canvas.freeDrawingBrush = new fabric[this.value + 'Brush'](canvas);
//        }

//        if (canvas.freeDrawingBrush) {
//            canvas.freeDrawingBrush.color = drawingColorEl.value;
//            canvas.freeDrawingBrush.width = parseInt(drawingLineWidthEl.value, 10) || 1;
//            canvas.freeDrawingBrush.shadow = new fabric.Shadow({
//                blur: parseInt(drawingShadowWidth.value, 10) || 0,
//                offsetX: 0,
//                offsetY: 0,
//                affectStroke: true,
//                color: drawingShadowColorEl.value,
//            });
//        }
//    };

//    drawingColorEl.onchange = function () {
//        canvas.freeDrawingBrush.color = this.value;
//    };
//    drawingShadowColorEl.onchange = function () {
//        canvas.freeDrawingBrush.shadow.color = this.value;
//    };
//    drawingLineWidthEl.onchange = function () {
//        canvas.freeDrawingBrush.width = parseInt(this.value, 10) || 1;
//        this.previousSibling.innerHTML = this.value;
//    };
//    drawingShadowWidth.onchange = function () {
//        canvas.freeDrawingBrush.shadow.blur = parseInt(this.value, 10) || 0;
//        this.previousSibling.innerHTML = this.value;
//    };
//    drawingShadowOffset.onchange = function () {
//        canvas.freeDrawingBrush.shadow.offsetX = parseInt(this.value, 10) || 0;
//        canvas.freeDrawingBrush.shadow.offsetY = parseInt(this.value, 10) || 0;
//        this.previousSibling.innerHTML = this.value;
//    };

//    if (canvas.freeDrawingBrush) {
//        canvas.freeDrawingBrush.color = drawingColorEl.value;
//        canvas.freeDrawingBrush.width = parseInt(drawingLineWidthEl.value, 10) || 1;
//        canvas.freeDrawingBrush.shadow = new fabric.Shadow({
//            blur: parseInt(drawingShadowWidth.value, 10) || 0,
//            offsetX: 0,
//            offsetY: 0,
//            affectStroke: true,
//            color: drawingShadowColorEl.value,
//        });
//    }



//    deleteSelectedObject.onclick = function () {
//        var activeObject = canvas.getActiveObject();
//        if (activeObject) {
//            if (confirm('Are you sure?')) {
//                if (activeObject._objects == null) {
//                    canvas.remove(activeObject);
//                }
//                else {

//                    var objectsInGroup = activeObject.getObjects();
//                    canvas.discardActiveObject();
//                    objectsInGroup.forEach(function (object) {
//                        canvas.remove(object);
//                    });

//                }
//            }
//        }
//    };

//    newTextBox.onclick = function () {

//        canvas.isDrawingMode = false;
//        if (canvas.isDrawingMode) {

//            drawingModeEl.classList = 'fa fa-plus-square';
//        }
//        else {
//            drawingModeEl.classList = 'fa fa-close';
//        }


//        var textbox = new fabric.Textbox('Text', {
//            left: 50,
//            top: 50,
//            width: 150,
//            fontSize: 20
//        });
//        canvas.add(textbox).setActiveObject(textbox);
//        fonts.unshift('Times New Roman');
//        // Populate the fontFamily select

        
//    };


//    function loadAndUse(font) {
//        var myfont = new FontFaceObserver(font)
//        myfont.load()
//            .then(function () {
//                // when font is loaded, use it.
//                canvas.getActiveObject().set("fontFamily", font);
//                canvas.requestRenderAll();
//            }).catch(function (e) {
//                console.log(e)
//                alert('font loading failed ' + font);
//            });
//    }


//    // Apply selected font on change
//    $('font-family').onchange = function () {
//        if (this.value !== 'Times New Roman') {
//            loadAndUse(this.value);
//        } else {
//            canvas.getActiveObject().set("fontFamily", this.value);
//            canvas.requestRenderAll();
//        }
//    };


//    function KeyCheck(event) {
//        var KeyID = event.keyCode;
//        switch (KeyID) {
//            case 8:
//                var activeObject = canvas.getActiveObject();
//                if (activeObject) {
//                    if (confirm('Are you sure?')) {
//                        if (activeObject._objects == null) {
//                            canvas.remove(activeObject);
//                        }
//                        else {

//                            var objectsInGroup = activeObject.getObjects();
//                            canvas.discardActiveObject();
//                            objectsInGroup.forEach(function (object) {
//                                canvas.remove(object);
//                            });

//                        }
//                    }
//                }
//                break;
//            case 46:
//                var activeObject = canvas.getActiveObject();
//                if (activeObject) {
//                    if (confirm('Are you sure?')) {
//                        if (activeObject._objects == null) {
//                            canvas.remove(activeObject);
//                        }
//                        else {

//                            var objectsInGroup = activeObject.getObjects();
//                            canvas.discardActiveObject();
//                            objectsInGroup.forEach(function (object) {
//                                canvas.remove(object);
//                            });

//                        }
//                    }
//                }

//                break;
//            default:
//                break;
//        }
//    }
//    document.addEventListener("keydown", KeyCheck);
//})();
