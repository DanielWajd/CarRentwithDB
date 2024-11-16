function toggleFields() {
    const userType = document.getElementById("userTypeSelect").value;
    const formContainer = document.getElementById("formContainer");

    const existingDrivingLicenceField = document.getElementById("drivingLicenceField");
    const existingEmployeeTypeField = document.getElementById("employeeTypeField");

    if (existingDrivingLicenceField) {
        existingDrivingLicenceField.remove();
    }
    if (existingEmployeeTypeField) {
        existingEmployeeTypeField.remove();
    }

    if (userType === "Customer") {
        const drivingLicenceDiv = document.createElement("div");
        drivingLicenceDiv.classList.add("form-group");
        drivingLicenceDiv.id = "drivingLicenceField";

        drivingLicenceDiv.innerHTML = `
            <label for="drivingLicence" class="control-label">Prawo Jazdy</label>
            <input type="text" name="registerCustomer.DrivingLicence" id="drivingLicence" class="form-control" />
            <span class="text-danger" id="drivingLicenceError"></span>
        `;

        formContainer.appendChild(drivingLicenceDiv);
    }
    else if (userType === "Employee") {
        const employeeTypeDiv = document.createElement("div");
        employeeTypeDiv.classList.add("form-group");
        employeeTypeDiv.id = "employeeTypeField";

        employeeTypeDiv.innerHTML = `
            <label for="employeeType" class="control-label">Typ Pracownika</label>
            <select name="registerEmployee.EmployeeType" id="employeeType" class="form-control">
                <option value="Administrator">Administrator</option>
                <option value="Manager">Menadżer</option>
                <option value="Salesperson">Sprzedawca</option>
                <option value="SupportStaff">Personel wsparcia</option>
                <option value="Accountant">Księgowy</option>
            </select>
            <span class="text-danger" id="employeeTypeError"></span>
        `;
        formContainer.appendChild(employeeTypeDiv);
    }
}

document.addEventListener("DOMContentLoaded", function () {
    toggleFields(); 

    const userTypeSelect = document.getElementById("userTypeSelect");
    if (userTypeSelect) {
        userTypeSelect.addEventListener("change", toggleFields);
    }
});
