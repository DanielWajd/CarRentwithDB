document.getElementById('location-btn').addEventListener('click', getLocation);

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(success, error);
    } else {
        alert("Twoja przeglądarka nie wspiera geolokalizacji. ");
    }
}

function success(position) {
    const latitude = position.coords.latitude;
    const longitude = position.coords.longitude;

    console.log(`Twoje współrzędne: ${latitude}, ${longitude}`);

    fetch(`https://nominatim.openstreetmap.org/reverse?format=json&lat=${latitude}&lon=${longitude}`)
        .then(response => response.json())
        .then(data => {
            const city = data.address.city || data.address.town || data.address.village || "Nieznane miejsce.";
            console.log(`Jesteś w mieście: ${city}`);

            document.querySelector('input[name="city"]').value = city;
        })
        .catch(error => console.error('Błąd geokodowania:', error));
}

function error(error) {
    switch (error.code) {
        case error.PERMISSION_DENIED:
            alert("Użytkownik odmówił dostępu do lokalizacji")
            break;
        case error.POSITION_UNAVAILABLE:
            alert("Lokalizacja niedostępna")
            break;
        case error.TIMEOUT:
            alert("Przekroczono czas oczekiwania")
            break;
        case error.UNKNOWN_ERROR:
            alert("Wystąpił nieznany błąd")
            break;
    }
}