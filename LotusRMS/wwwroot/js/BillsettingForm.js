
// Getting Data from Final Preview to Working Form
let finalCompanyName = document.getElementById("finalName").textContent;
document.getElementById("companyNameInput").value = finalCompanyName;
let finalCompanyAddress = document.getElementById("finalAddress").textContent;
document.getElementById("companyAddressInput").value = finalCompanyAddress;
let finalNote = document.getElementById("notes").textContent;
document.getElementById("inputNote").value = finalNote;
let finalPrefix = document.getElementById("final-prefix").textContent;
document.getElementById("prefix").value = finalPrefix;
let finalBill = document.getElementById("final-bill").textContent;
console.log(finalBill);
document.getElementById("billNo").textContent = finalBill;
// Putting Data back from form to Preview While Changing.
// Prefix 
function changePrefix(me) {
    if (!me.changePrefix) {
        document.getElementById("final-prefix").textContent = document.getElementById("prefix").value;
    }
}
// Company name
function changeName(me) {
    if (!me.changeName) {
        document.getElementById("finalName").textContent = document.getElementById("companyNameInput").value;
    }
}
// Company Address
function changeAdd(me) {
    if (!me.changeAdd) {
        document.getElementById("finalAddress").textContent = document.getElementById("companyAddressInput").value;
    }
}
// Notes 
function changeNote(me) {
    if (!me.changeAdd) {
        document.getElementById("notes").textContent = document.getElementById("inputNote").value;
    }
}
// Hiding Phone No if unchecked
const phone = document.getElementById("phoneNo");
function hidePhone(me) {
    if (me.checked) {
        phone.style.display = "block";
    } else {
        phone.style.display = "none";
        let tableHead = document.getElementById("top-tablehead");
        tableHead.style.marginTop = "2rem";
    }
}
// Hiding Pan No if unchecked
const pan = document.getElementById("panNo");
function hidePan(me) {
    if (me.checked) {
        pan.style.display = "block";
    } else {
        pan.style.display = "none";
    }
}
// Hiding Date in Bill Number if unchecked
const date = document.getElementById("final-date");
function hideDate(me) {
    if (me.checked) {
        date.style.display = "inline";
    } else {
        date.style.display = "none";
    }
}

