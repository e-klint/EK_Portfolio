//HAMBURGER-MENU (toggle)
function menuToggle(){
    const mobileNavOptions = document.getElementById("mobile-nav-options");

    if(mobileNavOptions.style.display === "flex"){
        mobileNavOptions.style.display = "none"
    }else{
        mobileNavOptions.style.display = "flex"
    }
}

// DOWNLOAD RESUME BTN

function startDownload(){
    var link = document.createElement("a");
    link.setAttribute("href", "files/EK_Resume.pdf");
    link.setAttribute("download", "Student_Resume");
    
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

//SLIDESHOW

let slideIndex = 1;
showSlide(slideIndex);

//Skiftar slide - lägger till antal steg (prev/next).
function changeSlide(step){
showSlide(slideIndex += step);
};

//Visar nuvarande slide
function showSlide(slideNumber){

    let slides = document.querySelectorAll(".testimonialSlide");

    if (slideNumber > slides.length){
        slideIndex = 1; //Så man kommer till första bilden efter sista bilden.
    }

    if (slideNumber < 1){
        slideIndex = slides.length; // Så man kommer till sista bilden innan första bilden. 
    }
    
    for (let i = 0; i < slides.length; i++){
        slides[i].style.display = "none"
    } //För att "tömma" slidern, innan nästa bild visas. 

    slides[slideIndex-1].style.display = "flex";
}     

// WEATHERAPP
const weatherForm = document.querySelector(".weatherForm");
const cityInput = document.querySelector(".cityInput");
const weatherCard = document.querySelector(".weatherCard");
const apiKey = "d75afaae8edd05053645ffba6f2dd93a";

weatherForm.addEventListener("submit",async event =>{

    event.preventDefault();

    const city = cityInput.value;

    if(city){
        try{
            const weatherData = await getWeatherData(city);
            displayWeatherInfo(weatherData);
        }
        catch(error){
        console.error(error);
        displayError(error);
        }
    }
    else{
        displayError("Ange stad");
    }

});

async function getWeatherData(city) {

    const apiUrl = `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${apiKey}`;

    const response = await fetch(apiUrl);

    if(!response.ok){
     throw new Error("Kunde inte hämta väderdata.");
    }
    
    return response.json();
};

function displayWeatherInfo(data){

    const {name: city, 
        main:{temp}, 
        weather: [{description, icon: iconCode}]} = data;

    weatherCard.textContent = "";
    weatherCard.style.display = "flex";
    
    const cityDisplay = document.createElement("h4");
    const weatherIcon = document.createElement("img");
    const tempDisplay = document.createElement("p");
    const personalText = document.createElement("p");
    
    cityDisplay.textContent = city;
    weatherIcon.src = getWeatherIcon(iconCode);
    weatherIcon.alt = "Väderikon"
    tempDisplay.textContent = `${(temp - 273.15).toFixed(1)}°C` //Converting from Kalvin and displaying one decimal.
    personalText.textContent = getPersonalText(description, (temp - 273.15));
    
    weatherCard.setAttribute("class", "weatherCard tcolor-taupe flx-container center p-1r mt-1r")
    cityDisplay.setAttribute("class", "mb-1r");
    weatherIcon.classList.add("weatherIcon");
    personalText.classList.add("italic");
    
    weatherCard.appendChild(cityDisplay);
    weatherCard.appendChild(weatherIcon);
    weatherCard.appendChild(tempDisplay);
    weatherCard.appendChild(personalText);  
};

function getWeatherIcon(iconCode){
    
    const iconUrl = `https://openweathermap.org/payload/api/media/file/${iconCode}.png`
    
    return iconUrl; 
};

function getPersonalText(description, temp){
     if (description.includes("rain")) return "Ta med paraply! ☔";
    if (description.includes("snow")) return "Plocka fram vinterjackan! ❄️";
    if (description.includes("thunderstorm")) return "Stanna inne med en kopp te! ⛈";
    if (temp < 0) return "Kallt ute, klä dig varmt! 🧤";
    if (temp > 20 && description.includes("clear")) return "Perfekt promenadväder! ☀️";
    return "Oavsett väder- alltid skönt att komma ut! 😄";
}

function displayError(message){

    const errorDisplay = document.createElement("p");
    errorDisplay.textContent = message;
    errorDisplay.classList.add("errorDisplay");
    
    weatherCard.textContent = "";
    weatherCard.style.display = "flex";
    weatherCard.appendChild(errorDisplay);
};