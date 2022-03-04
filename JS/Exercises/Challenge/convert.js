const b64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"
const b16 = "0123456789abcdef"

function convertToB64()
{
    let finalResult = "";
    let hexResult = "";
    let input = document.getElementById("input").value
    // document.getElementById("binary").innerHTML = btoa(input) // Easy-peasy way.


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
        hexResult += hexString
    }

    document.getElementById("binary").innerHTML = finalResult
    document.getElementById("hex").innerHTML = hexResult
}

function getBinary(number)
{
    // console.log(number)
    let arrRes = [];
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
            arrRes.push(part.toString())
            // console.log(number)
            // result += part.toString()
        }
    }
    // console.log(result)
    // console.log(result.length)
    while (arrRes.length < 6)
    {
        arrRes.push(0)
    }
    // console.log(result.length)
    // console.log(result)
    
    return arrRes.reverse().toString().replaceAll(",", "")
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
    if (int == 0)
    {
        return b16[int]
    }
    else 
    {
    while (int != 0)
        {
            let part = int % 16
            int = Math.floor(int / 16)
            // result += part.toString()
            console.log("------")
            console.log(part)
            console.log(b16[part])
            result += b16[part]
        }
    }
    
    return result
}
