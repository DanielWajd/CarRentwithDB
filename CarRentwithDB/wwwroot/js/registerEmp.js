function toggleFields() {
    const userType = document.getElementById("userTypeSelect").value;
    const formContainer = document.getElementById("formContainer");

    const existingDrivingLicenceField = document.getElementById("drivingLicenceField");
    const existingEmployeeTypeField = document.getElementById("employeeTypeField");
    const existingAddressFileds = document.getElementById("addressFields")

    if (existingDrivingLicenceField) {
        existingDrivingLicenceField.remove();
    }
    if (existingEmployeeTypeField) {
        existingEmployeeTypeField.remove();
    }
    if (existingAddressFileds) {
        existingAddressFileds.remove();
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

        const addressDiv = document.createElement("div");
        addressDiv.id = "addressFields";

        addressDiv.innerHTML = `
            <div class="form-group">
                <label for="street" class="control-label">Ulica</label>
                <input type="text" name="registerCustomer.Street" id="street" class="form-control" />
                <span class="text-danger" id="streetError"></span>
            </div>
            <div class="form-group">
                <label for="houseNumber" class="control-label">Numer Domu</label>
                <input type="text" name="registerCustomer.HouseNumber" id="houseNumber" class="form-control" />
                <span class="text-danger" id="houseNumberError"></span>
            </div>
            <div class="form-group">
                <label for="city" class="control-label">Miasto</label>
                <input type="text" name="registerCustomer.City" id="city" class="form-control" />
                <span class="text-danger" id="cityError"></span>
            </div>
            <div class="form-group">
                <label for="voivodeship" class="control-label">Województwo</label>
                <input type="text" name="registerCustomer.Voivodeship" id="voivodeship" class="form-control" />
                <span class="text-danger" id="voivodeshipError"></span>
            </div>
            <div class="form-group">
                <label for="zip" class="control-label">Kod Pocztowy</label>
                <input type="text" name="registerCustomer.Zip" id="zip" class="form-control" />
                <span class="text-danger" id="zipError"></span>
            </div>
        `;

        formContainer.appendChild(addressDiv);
    }
    else if (userType === "Employee") {
        const employeeTypeDiv = document.createElement("div");
        employeeTypeDiv.classList.add("form-group");
        employeeTypeDiv.id = "employeeTypeField";

        employeeTypeDiv.innerHTML = `
            <label for="employeeType" class="control-label">Typ Pracownika</label>
            <select name="registerEmployee.EmployeeType" id="employeeType" class="form-control">
                <option value="Manager">Menadżer</option>
                <option value="Salesperson">Sprzedawca</option>
                <option value="TechnicalStaff">Pracownik techniczny</option>
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
