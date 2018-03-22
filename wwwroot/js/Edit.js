tinymce.init({
    selector: '#content',
    height: 500,
    theme: 'modern',
    plugins: 'print image preview paste imagetools fullpage searchreplace autolink directionality visualblocks visualchars fullscreen image link media template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists textcolor wordcount imagetools contextmenu colorpicker textpattern help',
    toolbar1: 'formatselect | bold italic strikethrough forecolor backcolor | link | alignleft aligncenter alignright alignjustify  | numlist bullist outdent indent  | removeformat',
    paste_data_images: true
});


$('#input-tags').tagsinput({
    maxChars: 25,
    maxTags: 5,
    typeaheadjs: [{
        minLength: 1,
        highlight: true
    }, {
        minlength: 1,
        source: new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.whitespace,
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            prefetch: "/Tags.json"
        })
    }],
    freeInput: true
});


function uploadFile() {
    var blobFile = $('#filechooser').prop("files")[0];
    var formData = new FormData();
    formData.append("fileToUpload", blobFile);
    console.log(blobFile);
    console.log($('#filechooser').val())
    $.ajax({
        url: "upload.php",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            // .. do something
        },
        error: function (jqXHR, textStatus, errorMessage) {
            console.log(errorMessage); // Optional
        }
    });
}

$('#filechooser').change(function () {
    uploadFile();
});
        });

