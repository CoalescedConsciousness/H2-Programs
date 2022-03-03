

function calcGeom()
{
    let a = parseFloat(document.getElementById("a").value)
    let b = parseFloat(document.getElementById("b").value)
    let c = parseFloat(document.getElementById("c").value)
    let res = document.getElementById("res")
    const s = 0.5 * (a + b + c)

    if (!(a == b && b == c))
    {
        res.innerHTML = Math.sqrt(s*(s-a)*(s-b)*(s-c))
    }

    if (a == b && b == c)
    {
        res.innerHTML = Math.sqrt(3)/4*Math.pow(a, 2)
    }
    console.log(res.value)

}