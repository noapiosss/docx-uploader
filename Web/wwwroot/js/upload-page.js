const emailInput = document.getElementById("email-input");
const invalidEmailMessage = document.getElementById("invalid-email-message");

const fileInput = document.getElementById("file-input");
const fileNameInput = document.getElementById("file-name-input");
const invalidFileMessage = document.getElementById("invalid-file-message");

const uploadButton = document.getElementById("upload-button");

emailInput.oninput = (e) => { 
    emailInput.className = emailInput.className.replace(" is-invalid", " is-valid");            
    invalidEmailMessage.className = invalidEmailMessage.className.replace(" visible", " hidden");
    UpdateUploadButton(); 
};

fileInput.onchange = (e) => {
    fileNameInput.className = fileNameInput.className.replace(" is-invalid", " is-valid");            
    invalidFileMessage.className = invalidFileMessage.className.replace(" visible", " hidden");
    
    if (fileInput.files.length == 0)
    {
        fileNameInput.value = "";
    }
    else
    {
        fileNameInput.value = fileInput.files[0].name;
    }
    
    UpdateUploadButton();    
};

function UpdateUploadButton() {
    if ((emailInput.value == null || emailInput.value == "") || fileInput.files.length == 0)
    {
        SetButtonStatus(uploadButton, "inactive");
    }
    else
    {
        SetButtonStatus(uploadButton, "active");
    }
}

uploadButton.onclick = async (e) => {
    if (!uploadButton.className.includes("-active"))
    {
        return;
    }

    let data = new FormData();
    data.append("email", emailInput.value);
    data.append("file", fileInput.files[0]);

    SetButtonStatus(uploadButton, "loading")
    await fetch(`${window.location.origin}/api/uploadfile`, 
    {
        method: "POST",
        body: data
    })
    .then((response) =>
    {
        SetButtonStatus(uploadButton, "inactive")
        if (response.ok)
        {
            uploadButton.innerHTML = "✓";
            return;
        }
        
        return response.json();
    })
    .then((result) =>
    {
        if (result == null) { return };
        
        if (result.message === "Invalid email")
        {
            emailInput.className = emailInput.className.replace(" is-valid", " is-invalid");
            invalidEmailMessage.className = invalidEmailMessage.className.replace(" hidden", " visible");
        }

        if (result.message === "Invalid file type")
        {
            fileNameInput.className = fileNameInput.className.replace(" is-valid", " is-invalid");
            invalidFileMessage.innerHTML = result.message;
            invalidFileMessage.className = invalidFileMessage.className.replace(" hidden", " visible");
        }

        if (result.message === "File already exists")
        {
            fileNameInput.className = fileNameInput.className.replace(" is-valid", " is-invalid");
            invalidFileMessage.innerHTML = result.message;
            invalidFileMessage.className = invalidFileMessage.className.replace(" hidden", " visible");
        }
    });
}

function SetButtonStatus(btn, status)
{
    if (status === "inactive")
    {
        btn.className = "col-12 upload-button";
        btn.innerHTML = "Upload";
    }

    if (status === "active")
    {
        btn.className = "col-12 upload-button-active";
        btn.innerHTML = "Upload";
    }

    if (status === "loading")
    {
        btn.className = "col-12 upload-button";
        btn.innerHTML = "<label class='dot-1'>·</label><label class='dot-2'>·</label><label class='dot-3'>·</label>";
    }
}