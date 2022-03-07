const playerSection = document.getElementById("playerSection");
function buildHtml()
{
    
    playerSection.innerHTML = ""
    for (item in players)
    {
        let counter = item + 1
        let playerBoard = document.createElement('div')
        playerBoard.setAttribute("id", "playerContainer");
        playerBoard.setAttribute("class", "col-sm-12");
        playerBoard.style.margin = "2%";
        playerBoard.style.height = "auto";
        playerBoard.style.width = "20%";
        playerSection.appendChild(playerBoard)
        
        // Table
        let table = document.createElement('table')
        table.style.border = "2px solid lightgray"
        table.setAttribute("class", "col-sm-11")
        table.style.margin = "20px";
        playerBoard.appendChild(table)

        
        // Header rows
        let headerRow = table.appendChild(document.createElement('thead'))
       
        headerCellOne = headerRow.appendChild(document.createElement('td'))
        headerCellOne.appendChild(document.createTextNode('Player:'))
        headerCellTwo = headerRow.appendChild(document.createElement('td'))
        headerCellTwo.appendChild(document.createTextNode('Hand:'))
        headerCellThree = headerRow.appendChild(document.createElement('td'))
        headerCellThree.appendChild(document.createTextNode('Score:'))
        
        // Data rows
        let dataRow = table.appendChild(document.createElement('tbody'))

        dataCellOne = dataRow.appendChild(document.createElement('td'))
        input = document.createElement('input')
        input.style.width = "100px"
        input.setAttribute("id", `playerName${counter}`)
        dataCellOne.appendChild(input)

        dataCellTwo = dataRow.appendChild(document.createElement('td'))
        hand = document.createElement('p')
        hand.style.width = "50px"
        hand.setAttribute("id", `playerHand${counter}`)
        hand.setAttribute("placeholder", "0")
        dataCellTwo.appendChild(hand)

        dataCellThree = dataRow.appendChild(document.createElement('td'))
        score = document.createElement('p')
        score.style.width = "100px"
        score.setAttribute("id", `playerScore${counter}`)
        score.setAttribute("placeholder", "0")
        dataCellThree.appendChild(score)

        // Buttons
        let hitButton = document.createElement('button')
        hitButton.setAttribute("onclick", "playerHit()")
        hitButton.textContent = "Hit"
        hitButton.style.margin = "10px"
        hitButton.style.padding = "10px"
        hitButton.style.width = "300px"
        playerBoard.appendChild(hitButton)

        let standButton = document.createElement('button')
        standButton.setAttribute("onclick", "playerStand()")
        standButton.textContent = "Stand"
        standButton.style.margin = "10px"
        standButton.style.width = "auto"
        standButton.style.padding = "10px"
        playerBoard.appendChild(standButton)
    }
}

