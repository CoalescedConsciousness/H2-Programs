

function convertToB64()
{
    let finalResult = "";
    let input = document.getElementById("input").value
    document.getElementById("base64").innerHTML = btoa(input) // Easy-peasy way.

    let b64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"
    let arrB64 = []
    for (const a in b64)
    {
        arrB64.unshift(b64[a])
    }
    arrB64 = arrB64.reverse()
    
    for (const x in input)
    {
        let pos = arrB64.indexOf(input[x])
        // console.log(pos)
        let binaryString = getBinary(pos)
        let hexString = getHex(pos)
        finalResult += binaryString + " "
    }

    document.getElementById("base64").innerHTML = finalResult
}

function getBinary(number)
{
    // console.log(number)
    let result = "";
    if (number != 0)
    {
        while (number != 0)
        {
            let part = number % 2
            number = number / 2
            if (part == 1)
            {    
                number = Math.floor(number)
            }
            // console.log(number)
            result += part.toString()
        }
    }
    // console.log(result)
    // console.log(result.length)
    while (result.length < 6)
    {
        result += "0"
    }
    // console.log(result.length)
    // console.log(result)
    
    return result
}


function getHex(int)
{
    // let intRes = 0;
    // hexNumber = hexNumber.reverse()
    // for (let i = 0; i < hexNumber.length; i++)
    // {
    //     intRes += Math.pow(hexNumber[i], i)
    // }
    let result = ""
    while (int != 0)
    {
        let part = int % 16
        int = int / 16  
        result += part
    }
}
