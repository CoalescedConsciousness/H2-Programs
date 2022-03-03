const input1 = document.getElementById("input1");
const input2 = document.getElementById("input2");
const result = document.getElementById("demo"); // I know, I know, this is actually the "result" section..

const percent = document.getElementById("percent");
const grade = document.getElementById("grade")

var num = init()

function init()
{
    return parseInt(Math.random() * 10)
}

function e3()
{
    if (input1 && input2)
    {
        let x =  parseInt(input1.value) + parseInt(input2.value)
        // console.log(input1.value)
        result.innerHTML = x

        getAggregate()
    }

    if (percent)
    {
        getGrade()
    }

    if (guess)
    {
        compGuess()
    }
}

//#region getAggregate
function getAggregate()
{
    let sum = 0;
    let x;
    let y;
    let val1 = parseInt(input1.value); 
    let val2 = parseInt(input2.value);
    val1 > val2 ? (x=val1,y=val2) : (y=val1,x=val2);

    let count = 0
    let arrNumbers = [];

    while (y < x)
    {
        arrNumbers.push(y)
        y++;
        count++;
    }
    arrNumbers.push(x)
    console.log(arrNumbers)
    
    for (const x of arrNumbers)
    {
        sum = sum + x
        // console.log(`Aggregate: ${sum}`)
    }
    document.getElementById("aggregate").innerHTML = sum;
}
//#endregion

//#region Numberguessing
function compGuess()
{
    console.log(guess.value)
    console.log(`Number: ${num}`)
    if (guess.value == num)
    {
        if (confirm("You got it! Play again?") == true)
        {
            let x = num
            let check = false
            
            while (!check)
            {
                num = init()
                if (num !== x)
                {
                    check = true;
                }
            }
        }
        
    }
}
//#endregion

//#region Grade calc:
function getGrade() {
    // console.log(percent.value)
    let val = parseInt(percent.value);
    console.log(val)
    switch (val != null)
    {
        case val > 100:
            grade.innerHTML = "Impossibru!";
            break;
        case (val <= 100 && val >= 92):  //100 >= val >= 92:
            grade.innerHTML = "12";
            break;
        case (val < 92 && val >= 81): // 92 > val >= 81:
            grade.innerHTML = "10"
            break;
        case (val < 88 && val >= 66): // 81 > val >= 66:
            grade.innerHTML = "7"
            break;
        case (val < 66 && val >= 55): // 66 > val >= 55:
            grade.innerHTML = "4"
            break;
        case (val < 55 && val >= 50): // 55 > val >= 50:
            grade.innerHTML = "02"
            break;
        case (val < 50 && val >= 20): // 50 > val >= 20:
            grade.innerHTML = "00"
            break;
        case (val < 20 && val >= 0): // 20 > val > 0:
            grade.innerHTML = "-3"
            break;
        case (val < 0):
            grade.innerHTML = "....How?"
            break;
    }
}
//#endregion
// e3();