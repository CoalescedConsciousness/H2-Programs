const fiveNinths = 5/9;
const fahrenFreeze = 32;
const fahrenheit = document.getElementById("fahren")

const celsius = document.getElementById("celsius")
const nineTwentieths = 1.8;


function toFahren()
{
    if (celsius)
    {
        fahrenheit.value = (celsius.value * nineTwentieths) + fahrenFreeze
    }
}

function toCelsius()
{
    if (fahrenheit)
    {
        console.log(fiveNinths)
        celsius.value = (fahrenheit.value - fahrenFreeze) * fiveNinths
    }
}