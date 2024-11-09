function calculatePrice() {
    const startDate = new Date(document.getElementById('StartDate').value);
    const endDate = new Date(document.getElementById('EndDate').value);
    const dailyRate = parseFloat(document.getElementById('DailyRate').value);



    const timeDiff = endDate - startDate;
    const daysDiff = Math.ceil(timeDiff / (1000 * 60 * 60 * 24));

    const rentalDays = daysDiff > 0 ? daysDiff : 1;
    const totalPrice = rentalDays * dailyRate;

    document.getElementById('Price').value = totalPrice; 
}

document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('StartDate').addEventListener('change', calculatePrice);
    document.getElementById('EndDate').addEventListener('change', calculatePrice);

    calculatePrice();  
});
