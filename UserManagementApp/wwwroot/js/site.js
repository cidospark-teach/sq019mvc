
let addRoleBtn = document.querySelector("#add-role-btn");
let addUserRoleBtn = document.querySelector("#add-user-role-btn"); 
let addRolePanel = document.querySelector("#new-role-panel");
let newUserRolePanel = document.querySelector("#new-user-role-panel");


/*console.log(delBtns)*/

if (addRoleBtn != null) {
    addRoleBtn.addEventListener('click', () => {
        addRolePanel.classList.remove("hide");
        addRolePanel.classList.add("show");
    })
}

if (addUserRoleBtn != null) {

    addUserRoleBtn.addEventListener('click', () => {
        newUserRolePanel.classList.remove("hide");
        newUserRolePanel.classList.add("show")
    })
}