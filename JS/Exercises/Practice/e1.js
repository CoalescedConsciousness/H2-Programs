let x = 5;
let y = 10;
const target = document.getElementById("demo")

function something() 
{ 
    let z = x + y
    // alert(z)
    document.getElementById("demo").innerHTML = x + y; 
    let fName = "John"; let lName = "Smith"; let name = `${fName} ${lName}`;
    target.innerHTML = name;
}

something()
